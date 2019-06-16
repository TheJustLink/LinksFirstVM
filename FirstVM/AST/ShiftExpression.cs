using System.Collections.Generic;

using FirstVM.ValueTypes;
using FirstVM.Exceptions;
namespace FirstVM.AST
{
    class ShiftExpression : IExpression
    {
        public static Dictionary<ShiftExpressionType, string> shiftTypeTable = new Dictionary<ShiftExpressionType, string>()
        {
            {ShiftExpressionType.LeftShift, "<<"},
            {ShiftExpressionType.RightShift, ">>"}
        };

        private readonly IExpression exp1;
        private readonly IExpression exp2;
        private readonly ShiftExpressionType shiftType;
        public ShiftExpression(IExpression exp1, IExpression exp2, ShiftExpressionType shiftType)
        {
            this.exp1 = exp1;
            this.exp2 = exp2;
            this.shiftType = shiftType;
        }

        public IValue Eval()
        {
            switch (shiftType)
            {
                case ShiftExpressionType.LeftShift: return LeftShift(exp1.Eval(), exp2.Eval());
                case ShiftExpressionType.RightShift: return RightShift(exp1.Eval(), exp2.Eval());
                default: Error(); return null;
            }
        }

        private IValue LeftShift(IValue val1, IValue val2)
        {
            if (val1.IsType(ValueType.Int) && val2.IsType(ValueType.Int)) return LeftShift(val1 as IntValue, val2 as IntValue);
            else Error(); return null;
        }
        private IValue LeftShift(IntValue val1, IntValue val2)
        {
            return new IntValue(val1.AsInt() << val2.AsInt());
        }

        private IValue RightShift(IValue val1, IValue val2)
        {
            if (val1.IsType(ValueType.Int) && val2.IsType(ValueType.Int)) return RightShift(val1 as IntValue, val2 as IntValue);
            else Error(); return null;
        }
        private IValue RightShift(IntValue val1, IntValue val2)
        {
            return new IntValue(val1.AsInt() >> val2.AsInt());
        }

        private void Error()
        {
            throw new ShiftExpressionException("Unexpected expression - " + ToString());
        }
        public override string ToString()
        {
            return exp1.ToString() + shiftTypeTable[shiftType] + exp2.ToString();
        }
    }
}