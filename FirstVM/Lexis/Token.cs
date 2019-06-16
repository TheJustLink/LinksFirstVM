using FirstVM.ValueTypes;

namespace FirstVM.Lexis
{
    public class Token
    {
        public readonly TokenType Type;
        public readonly IValue Value;
        public readonly ushort Line;
        public readonly ushort Column;
        public Token(TokenType tokenType, IValue value, ushort line, ushort column)
        {
            this.Type = tokenType;
            this.Value = value;

            this.Line = line;
            this.Column = column;
        }

        public override string ToString()
        {
            string valueStr = Value != null ? "=" + Value.ToString() : "";
            return string.Format("[{0}:{1}] {2}{3}", Line, Column, Type.ToString(), valueStr);
        }
    }
}