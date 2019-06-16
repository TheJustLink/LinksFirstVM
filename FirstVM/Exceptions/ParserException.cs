using System;
namespace FirstVM.Exceptions
{
    class ParserException : Exception
    {
        public ParserException(string msg) : base(msg) { }
    }
}