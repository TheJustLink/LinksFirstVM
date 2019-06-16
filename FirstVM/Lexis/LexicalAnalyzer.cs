using System.Collections.Generic;
using System.Text;

using FirstVM.ValueTypes;
using FirstVM.Exceptions.Lexis;

namespace FirstVM.Lexis
{
    public partial class LexicalAnalyzer
    {
        private const char NONE = ' ';
        private const char END = '\0';
        private const char CR = '\r';
        private const char LF = '\n';
        private const char TAB = '\t';
        private const char QTS = '"';
        private const char SLASH = '/';
        private const char BACKSLASH = '\\';
        private const char STAR = '*';
        private const char UNDERSCORE = '_';
        private const char COMMA = ',';
        private const char DOT = '.';

        private ushort startColumn;
        private ushort column;
        private ushort line;
        private int pos;
        private int length;
        private string code;
        private StringBuilder buffer;

        public Token[] Analyze(string code)
        {
            buffer = new StringBuilder();

            this.code = code;
            length = code.Length;
            column = 1;
            line = 1;

            List<Token> tokens = new List<Token>();
            for (pos = 0; pos < length; )
            {
                char cChar = Peek();
                char nChar = Peek(1);
                if (char.IsLetter(cChar) || cChar == UNDERSCORE || cChar == '$') tokens.Add(AnalyzeLetter());
                else if (char.IsNumber(cChar)) tokens.Add(AnalyzeNumber());
                else if (cChar == QTS) tokens.Add(AnalyzeString());
                else if ((cChar == SLASH && nChar == SLASH) || cChar == '#') AnalyzeCommentLine();
                else if (cChar == SLASH && nChar == STAR) AnalyzeCommentLines();
                else if (_doubleCharTable.ContainsKey(cChar.ToString() + nChar.ToString())) tokens.Add(AnalyzeDoubleChar());
                else if (_charTable.ContainsKey(cChar)) tokens.Add(AnalyzeChar());
                else if (_charNoneTable.Contains(cChar)) AnalyzeWhiteSpace();
                else throw ErrorUnexpected(cChar);
            }
            return tokens.ToArray();
        }

        private Token AnalyzeLetter()
        {
            startColumn = column;

            buffer.Clear();
            char cChar;
            do
            {
                buffer.Append(Get());
                cChar = Peek();
            }
            while (char.IsLetterOrDigit(cChar) || cChar == UNDERSCORE);

            string str = buffer.ToString();
            if (_operatorsTable.ContainsKey(str)) return new Token(_operatorsTable[str], null, line, startColumn);
            else return new Token(TokenType.VAR, new StringValue(str), line, startColumn);
        }
        private Token AnalyzeNumber()
        {
            startColumn = column;

            buffer.Clear();
            buffer.Append(Get());

            bool doubled = false;
            while (true)
            {
                char cChar = Peek();
                if (char.IsNumber(cChar)) buffer.Append(cChar);
                else if (cChar == DOT || cChar == COMMA)
                {
                    if (doubled) throw ErrorUnexpected(cChar);
                    doubled = true;
                    buffer.Append(COMMA);
                }
                else break;

                AddPos();
            }
            string str = buffer.ToString().TrimEnd(COMMA);
            

            IValue value = doubled ? (IValue)new DoubleValue(double.Parse(str)) : (IValue)new IntValue(int.Parse(str));
            return new Token(TokenType.NUM, value, line, startColumn);
        }
        private Token AnalyzeString()
        {
            startColumn = column;

            AddPos();
            buffer.Clear();
            while (true)
            {
                char cChar = Get();
                if (cChar == QTS) break;
                else if (cChar == BACKSLASH)
                {
                    char nChar = Get();
                    switch (nChar)
                    {
                        case 't': buffer.Append(TAB); break;
                        case 'r': buffer.Append(CR); break;
                        case 'n': buffer.Append(LF); break;
                        case QTS: buffer.Append(QTS); break;
                        case BACKSLASH: buffer.Append(BACKSLASH); break;
                        default: throw ErrorUnexpected(nChar);
                    }
                }
                else if (cChar == END || cChar == LF ||
                    (cChar == CR && Peek() == LF)) throw ErrorExpected(QTS);
                else buffer.Append(cChar);
            }
            return new Token(TokenType.STR, new StringValue(buffer.ToString()), line, startColumn);
        }
        private Token AnalyzeDoubleChar()
        {
            startColumn = column;
            string s = Get().ToString() + Get().ToString();
            return new Token(_doubleCharTable[s], null, line, startColumn);
        }
        private Token AnalyzeChar()
        {
            startColumn = column;
            char ch = Get();
            return new Token(_charTable[ch], null, line, startColumn);
        }
        private void AnalyzeCommentLine()
        {
            if (Get() == SLASH) AddPos();
            while (true)
            {
                char ch = Peek();
                if (ch == END) break;
                else if (ch == LF ||
                    (ch == CR && Peek(1) == LF)) break;
                AddPos();
            }
        }
        private void AnalyzeCommentLines()
        {
            AddPos(2);
            while (true)
            {
                char cChar = Get();
                if (_charNoneTable.Contains(cChar))
                {
                    if (cChar == LF) line++;
                    else if (cChar == END) throw ErrorExpected(STAR);
                }
                else if (cChar == STAR && Peek() == SLASH)
                {
                    AddPos();
                    break;
                }
            }
        }
        private void AnalyzeWhiteSpace()
        {
            char cChar = Peek();
            switch (cChar)
            {
                case LF: AddLine(); break;
                case TAB: column += 3; break;
            }
            AddPos();
        }

        private bool IsRange(int s = 0)
        {
            return pos + s < length;
        }
        private char Peek(int s = 0)
        {
            int pos = this.pos + s;
            if (pos >= length) return END;
            else return code[pos];
        }
        private char Get()
        {
            if (IsRange())
            {
                AddPos();
                return code[pos - 1];
            }
            else return END;
        }

        private void AddPos(int i = 1)
        {
            pos += i;
            column += (ushort)i;
        }
        private void SubPos(int i = 1)
        {
            pos -= i;
            column -= (ushort)i;
        }
        private void AddLine()
        {
            line++;
            column = 0;
        }

        private ExpectedCharacterException ErrorExpected(char ch)
        {
            return new ExpectedCharacterException(line, column, ch);
        }
        private UnexpectedCharacterException ErrorUnexpected(char ch)
        {
            return new UnexpectedCharacterException(line, column, ch);
        }
    }
}