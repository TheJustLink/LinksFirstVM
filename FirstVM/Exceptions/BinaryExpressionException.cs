using System;
namespace FirstVM.Exceptions
{
    class BinaryExpressionException : Exception
    {
        public BinaryExpressionException(string msg) : base(msg) { }
    }
}