using System;
namespace FirstVM.Exceptions
{
    class ShiftExpressionException : Exception
    {
        public ShiftExpressionException(string msg) : base(msg) { }
    }
}