namespace FirstVM.Exceptions.Lexis
{
    public class UnexpectedCharacterException : LexisException
    {
        public UnexpectedCharacterException(int line, int column, char ch)
            : base(line, column, string.Format("Unexpected  character - '{0}'", ch)) { }
    }
}