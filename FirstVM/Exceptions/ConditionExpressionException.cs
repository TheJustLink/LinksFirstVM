using System;
namespace FirstVM.Exceptions
{
    class ConditionExpressionException : Exception
    {
        public ConditionExpressionException(string msg) : base(msg) { }
    }
}