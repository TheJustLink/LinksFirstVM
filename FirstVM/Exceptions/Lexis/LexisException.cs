using System;
namespace FirstVM.Exceptions.Lexis
{
    public abstract class LexisException : Exception
    {
        public LexisException(int line, int column, string msg)
            : base(string.Format("[{0}:{1}] {2}", line, column, msg)) { }
    }
}