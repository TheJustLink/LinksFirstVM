using System.Collections.Generic;

using FirstVM.ValueTypes;
using FirstVM.Exceptions;
namespace FirstVM.AST
{
    class UnaryExpression : IExpression
    {
        public static Dictionary<UnaryExpressionType, string> unaryTypeTable = new Dictionary<UnaryExpressionType, string>()
        {
            {UnaryExpressionType.ADD, "+"},
            {UnaryExpressionType.SUB, "-"},
            {UnaryExpressionType.NOT, "!"}
        };

        private readonly IExpression exp;
        private readonly UnaryExpressionType unaryType;
        public UnaryExpression(IExpression exp, UnaryExpressionType unaryType)
        {
            this.exp = exp;
            this.unaryType = unaryType;
        }

        public IValue Eval()
        {
            switch (unaryType)
            {
                case UnaryExpressionType.ADD: return exp.Eval();
                case UnaryExpressionType.SUB: return Sub(exp.Eval());
                case UnaryExpressionType.NOT: return Not(exp.Eval());
                default: Error(); return null;
            }
        }

        private IValue Sub(IValue val)
        {
            switch (val.GetTypeValue())
            {
                case ValueType.Int: return Sub(val as IntValue);
                case ValueType.Double: return Sub(val as DoubleValue);
                case ValueType.String: return Sub(val as StringValue);
                default: Error(); return null;
            }
        }
        private IValue Sub(IntValue val)
        {
            return new IntValue(-val.AsInt());
        }
        private IValue Sub(DoubleValue val)
        {
            return new DoubleValue(-val.AsDouble());
        }
        private IValue Sub(StringValue val)
        {
            return new StringValue(val.Reverse());
        }

        private IValue Not(IValue val)
        {
            switch (val.GetTypeValue())
            {
                case ValueType.Boolean: return Not(val as BooleanValue);
                default: Error(); return null;
            }
        }
        private IValue Not(BooleanValue val)
        {
            return new BooleanValue(!val.AsBool());
        }

        private void Error()
        {
            throw new UnaryExpressionException("Unexpected expression - " + ToString());
        }
        public override string ToString()
        {
            return "(" + unaryTypeTable[unaryType] + exp.ToString() + ")";
        }
    }
}