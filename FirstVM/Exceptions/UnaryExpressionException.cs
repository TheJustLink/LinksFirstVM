using System;
namespace FirstVM.Exceptions
{
    class UnaryExpressionException : Exception
    {
        public UnaryExpressionException(string msg) : base(msg) { }
    }
}