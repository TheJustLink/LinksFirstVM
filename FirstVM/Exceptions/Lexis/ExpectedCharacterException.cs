namespace FirstVM.Exceptions.Lexis
{
    public class ExpectedCharacterException : LexisException
    {
        public ExpectedCharacterException(int line, int column, char ch)
            : base(line, column, string.Format("Expected character - '{0}'", ch)) { }
    }
}