using System.Collections.Generic;
using System.Linq;

using FirstVM.Exceptions;
using FirstVM.ValueTypes;
using FirstVM.Lexis;
using FirstVM.AST;

namespace FirstVM.Parser
{
    public class TokenParser
    {
        private static Token EOFToken = new Token(TokenType.EOF, null, 0, 0);
        private static ValueExpression ValueTrue = new ValueExpression(new BooleanValue(true));
        private static ValueExpression ValueFalse = new ValueExpression(new BooleanValue(false));

        private int pos;
        private Token[] tokens;

        public IStatement Parse(Token[] tokens)
        {
            this.tokens = tokens;

            List<IStatement> statements = new List<IStatement>();
            do statements.Add(ParseStatement());
            while (!Match(TokenType.EOF));

            return new BlockStatement(statements.ToArray());
        }

        private IStatement ParseStatement()
        {
            if (Match(TokenType.BRC_LT)) return ParseBlockStatement();
            else if (Match(TokenType.ECHO)) return new EchoStatement(ParseExpression());
            else if (Match(TokenType.IF)) return ParseIfStatement();
            else if (Match(TokenType.WHILE)) return ParseWhileStatement();
            else if (Match(TokenType.DO)) return ParseDoWhileStatement();
            else if (Match(TokenType.FOR)) return ParseForStatement();
            else return ParseAssignment();
        }
        private IStatement ParseForStatement()
        {
            IStatement initialize = ParseStatement();
            Consume(TokenType.SEMICOLON);
            IExpression condition = ParseExpression();
            Consume(TokenType.SEMICOLON);
            return new ForStatement(initialize, condition, ParseStatement(), ParseStatement());
        }
        private IStatement ParseDoWhileStatement()
        {
            IStatement statement = ParseStatement();
            Consume(TokenType.WHILE);
            return new DoWhileStatement(statement, ParseExpression());
        }
        private IStatement ParseWhileStatement()
        {
            return new WhileStatement(ParseExpression(), ParseStatement());
        }
        private IStatement ParseIfStatement()
        {
            IExpression condition = ParseExpression();
            IStatement statement = ParseStatement();
            IStatement elseStatement = null;
            if (Match(TokenType.ELSE)) elseStatement = ParseStatement();

            return new IfStatement(condition, statement, elseStatement);
        }
        private IStatement ParseBlockStatement()//
        {
            if (Match(TokenType.BRC_RT)) return new BlockStatement(new IStatement[0]);

            List<IStatement> statements = new List<IStatement>();
            do statements.Add(ParseStatement());
            while (!Compare(PeekType(), TokenType.BRC_RT, TokenType.EOF));

            Consume(TokenType.BRC_RT);
            return new BlockStatement(statements.ToArray());
        }
        private IStatement ParseAssignment()
        {
            IExpression exp = ParseExpression();
            if (exp is AssignmentExpression) return new ExpressionStatement(exp);
            else throw Error("Unexpected statement - " + exp.ToString());
        }

        private IExpression ParseExpression()
        {
            return ParseAssignmentAndLambda();
        }
        private IExpression ParseAssignmentAndLambda()
        {
            IExpression exp = ParseTernary();
            while (Match(TokenType.EQ))
            {
                if (exp is VariableExpression)
                    return new AssignmentExpression(exp as VariableExpression, ParseAssignmentAndLambda());
                else throw Error("'" + exp.ToString() + "' is not a variable");
            }
            return exp;
        }
        private IExpression ParseTernary()
        {
            IExpression exp = ParseConditionalOR();
            if (Match(TokenType.QSTMK))
            {
                IExpression expTrue = ParseConditionalOR();
                Consume(TokenType.COLON);
                return new TernaryExpression(exp, expTrue, ParseConditionalOR());
            }
            return exp;
        }
        private IExpression ParseConditionalOR()
        {
            IExpression exp = ParseConditionalAND();
            while (Match(TokenType.OR_OR))
                exp = new ConditionExpression(exp, ParseConditionalAND(), ConditionExpressionType.OROR);
            return exp;
        }
        private IExpression ParseConditionalAND()
        {
            IExpression exp = ParseLogicOR();
            while (Match(TokenType.AND_AND))
                exp = new ConditionExpression(exp, ParseLogicOR(), ConditionExpressionType.ANDAND);
            return exp;
        }
        private IExpression ParseLogicOR()
        {
            IExpression exp = ParseLogicXOR();
            while (Match(TokenType.OR))
                exp = new ConditionExpression(exp, ParseLogicXOR(), ConditionExpressionType.OR);
            return exp;
        }
        private IExpression ParseLogicXOR()
        {
            IExpression exp = ParseLogicAND();
            while (Match(TokenType.XOR))
                exp = new ConditionExpression(exp, ParseLogicAND(), ConditionExpressionType.XOR);
            return exp;
        }
        private IExpression ParseLogicAND()
        {
            IExpression exp = ParseEquality();
            while (Match(TokenType.AND))
                exp = new ConditionExpression(exp, ParseEquality(), ConditionExpressionType.AND);
            return exp;
        }
        private IExpression ParseEquality()
        {
            IExpression exp = ParseRelationalAndType();
            while (true)
            {
                if (Match(TokenType.EQ_EQ)) exp = new ConditionExpression(exp, ParseRelationalAndType(), ConditionExpressionType.Eq);
                else if (Match(TokenType.NOT_EQ)) exp = new ConditionExpression(exp, ParseRelationalAndType(), ConditionExpressionType.NotEq);
                else break;
            }
            return exp;
        }
        private IExpression ParseRelationalAndType()//Is/As//
        {
            IExpression exp = ParseShift();
            while (true)
            {
                if (Match(TokenType.LESS)) exp = new ConditionExpression(exp, ParseShift(), ConditionExpressionType.Less);
                else if (Match(TokenType.GREAT)) exp = new ConditionExpression(exp, ParseShift(), ConditionExpressionType.Great);
                else if (Match(TokenType.LESS_EQ)) exp = new ConditionExpression(exp, ParseShift(), ConditionExpressionType.LessEq);
                else if (Match(TokenType.GREAT_EQ)) exp = new ConditionExpression(exp, ParseShift(), ConditionExpressionType.GreatEq);
                else break;
                //Types-op - IS, AS//
            }
            return exp;
        }
        private IExpression ParseShift()
        {
            IExpression exp = ParseAddSub();
            while (true)
            {
                if (Match(TokenType.SHIFT_LT)) exp = new ShiftExpression(exp, ParseAddSub(), ShiftExpressionType.LeftShift);
                else if (Match(TokenType.SHIFT_RT)) exp = new ShiftExpression(exp, ParseAddSub(), ShiftExpressionType.RightShift);
                else break;
            }
            return exp;
        }
        private IExpression ParseAddSub()
        {
            IExpression exp = ParseMulDiv();
            while (true)
            {
                if (Match(TokenType.ADD)) exp = new BinaryExpression(exp, ParseMulDiv(), BinaryExpressionType.ADD);
                else if (Match(TokenType.SUB)) exp = new BinaryExpression(exp, ParseMulDiv(), BinaryExpressionType.SUB);
                else break;
            }
            return exp;
        }
        private IExpression ParseMulDiv()
        {
            IExpression exp = ParseUnary();
            while (true)
            {
                if (Match(TokenType.MUL)) exp = new BinaryExpression(exp, ParseUnary(), BinaryExpressionType.MUL);
                else if (Match(TokenType.DIV)) exp = new BinaryExpression(exp, ParseUnary(), BinaryExpressionType.DIV);
                else if (Match(TokenType.MOD)) exp = new BinaryExpression(exp, ParseUnary(), BinaryExpressionType.MOD);
                else break;
            }
            return exp;
        }
        private IExpression ParseUnary()
        {
            if (Match(TokenType.ADD)) return new UnaryExpression(ParsePrimary(), UnaryExpressionType.ADD);
            else if (Match(TokenType.SUB)) return new UnaryExpression(ParsePrimary(), UnaryExpressionType.SUB);
            else if (Match(TokenType.EXMK)) return new UnaryExpression(ParsePrimary(), UnaryExpressionType.NOT);
            else return ParsePrimary();
        }
        private IExpression ParsePrimary()
        {
            TokenType tt = PeekType();

            if (tt == TokenType.BKT_LT)
            {
                AddPos();
                IExpression exp = ParseExpression();
                Consume(TokenType.BKT_RT);
                return new BracketExpression(exp);
            }

            if (Compare(tt, TokenType.NUM, TokenType.STR)) return new ValueExpression(Get().Value);
            else if (tt == TokenType.TRUE) { AddPos(); return ValueTrue; }
            else if (tt == TokenType.FALSE) { AddPos(); return ValueFalse; }
            else if (tt == TokenType.VAR) return new VariableExpression(Get().Value as StringValue);
            else throw Error(Peek());
        }

        private bool IsRange(int pos)
        {
            return pos < tokens.Length;
        }
        private Token Peek(int s = 0)
        {
            int pos = this.pos + s;
            return IsRange(pos) ? tokens[pos] : EOFToken;
        }
        private Token Get(int s = 0)
        {
            Token token = Peek(s);
            AddPos();
            return token;
        }
        private TokenType PeekType(int s = 0)
        {
            return Peek(s).Type;
        }
        private TokenType GetType(int s = 0)
        {
            return Get(s).Type;
        }
        private bool Match(TokenType match, int s = 0)
        {
            Token token = Peek(s);
            if (token.Type != match) return false;

            AddPos();
            return true;
        }
        private void Consume(TokenType match, int s = 0)
        {
            if (!Match(match, s)) throw Error("Expected token type - " + match.ToString());
        }
        private bool Compare(TokenType tokenType, params TokenType[] tokenTypes)
        {
            return tokenTypes.Contains<TokenType>(tokenType);
        }
        private void AddPos()
        {
            pos++;
        }

        private ParserException Error(Token token)
        {
            return Error("Unexpected token - " + token.ToString());
        }
        private ParserException Error(string msg)
        {
            return new ParserException(msg);
        }
    }
}