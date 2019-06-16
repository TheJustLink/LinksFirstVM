using System.Collections.Generic;

using FirstVM.ValueTypes;
using FirstVM.Exceptions;
namespace FirstVM.AST
{
    class ConditionExpression : IExpression
    {
        public static Dictionary<ConditionExpressionType, string> conditionTypeTable = new Dictionary<ConditionExpressionType, string>()
        {
            {ConditionExpressionType.Less, "<"},
            {ConditionExpressionType.Great, ">"},
            {ConditionExpressionType.LessEq, "<="},
            {ConditionExpressionType.GreatEq, ">="},

            {ConditionExpressionType.Eq, "=="},
            {ConditionExpressionType.NotEq, "!="},

            {ConditionExpressionType.AND, "&"},
            {ConditionExpressionType.XOR, "^"},
            {ConditionExpressionType.OR, "|"},
            {ConditionExpressionType.ANDAND, "&&"},
            {ConditionExpressionType.OROR, "||"},
        };

        private readonly IExpression exp1;
        private readonly IExpression exp2;
        private readonly ConditionExpressionType conditionType;
        public ConditionExpression(IExpression exp1, IExpression exp2, ConditionExpressionType conditionType)
        {
            this.exp1 = exp1;
            this.exp2 = exp2;
            this.conditionType = conditionType;
        }

        public IValue Eval()
        {
            switch (conditionType)
            {
                case ConditionExpressionType.Less: return Less(exp1.Eval(), exp2.Eval());
                case ConditionExpressionType.Great: return Great(exp1.Eval(), exp2.Eval());
                case ConditionExpressionType.LessEq: return LessEQ(exp1.Eval(), exp2.Eval());
                case ConditionExpressionType.GreatEq: return GreatEQ(exp1.Eval(), exp2.Eval());
                
                case ConditionExpressionType.Eq: return Equal(exp1.Eval(), exp2.Eval());
                case ConditionExpressionType.NotEq: return NotEqual(exp1.Eval(), exp2.Eval());

                case ConditionExpressionType.AND: return And(exp1.Eval(), exp2.Eval());
                case ConditionExpressionType.XOR: return Xor(exp1.Eval(), exp2.Eval());
                case ConditionExpressionType.OR: return Or(exp1.Eval(), exp2.Eval());

                case ConditionExpressionType.ANDAND: return AndAnd(exp1.Eval(), exp2.Eval());
                case ConditionExpressionType.OROR: return OrOr(exp1.Eval(), exp2.Eval());
                default: Error(); return null;
            }
        }

        private IValue Less(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Int && type2 == ValueType.Int) return Less(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Double) return Less(val1 as IntValue, val2 as DoubleValue);
            else if (type1 == ValueType.Int && type2 == ValueType.String) return Less(val1 as IntValue, val2 as StringValue);

            else if (type1 == ValueType.Double && type2 == ValueType.Double) return Less(val1 as DoubleValue, val2 as DoubleValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Int) return Less(val1 as DoubleValue, val2 as IntValue);
            else if (type1 == ValueType.Double && type2 == ValueType.String) return Less(val1 as DoubleValue, val2 as StringValue);

            else if (type1 == ValueType.String && type2 == ValueType.String) return Less(val1 as StringValue, val2 as StringValue);
            else if (type1 == ValueType.String && type2 == ValueType.Int) return Less(val1 as StringValue, val2 as IntValue);
            else if (type1 == ValueType.String && type2 == ValueType.Double) return Less(val1 as StringValue, val2 as DoubleValue);

            else Error(); return null;
        }
        private IValue Less(IntValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsInt() < val2.AsInt());
        }
        private IValue Less(IntValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsInt() < val2.AsDouble());
        }
        private IValue Less(IntValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsInt() < val2.AsString().Length);
        }
        private IValue Less(DoubleValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsDouble() < val2.AsDouble());
        }
        private IValue Less(DoubleValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsDouble() < val2.AsInt());
        }
        private IValue Less(DoubleValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsDouble() < val2.AsString().Length);
        }
        private IValue Less(StringValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsString().Length < val2.AsString().Length);
        }
        private IValue Less(StringValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsString().Length < val2.AsInt());
        }
        private IValue Less(StringValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsString().Length < val2.AsDouble());
        }

        private IValue Great(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Int && type2 == ValueType.Int) return Great(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Double) return Great(val1 as IntValue, val2 as DoubleValue);
            else if (type1 == ValueType.Int && type2 == ValueType.String) return Great(val1 as IntValue, val2 as StringValue);

            else if (type1 == ValueType.Double && type2 == ValueType.Double) return Great(val1 as DoubleValue, val2 as DoubleValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Int) return Great(val1 as DoubleValue, val2 as IntValue);
            else if (type1 == ValueType.Double && type2 == ValueType.String) return Great(val1 as DoubleValue, val2 as StringValue);

            else if (type1 == ValueType.String && type2 == ValueType.String) return Great(val1 as StringValue, val2 as StringValue);
            else if (type1 == ValueType.String && type2 == ValueType.Int) return Great(val1 as StringValue, val2 as IntValue);
            else if (type1 == ValueType.String && type2 == ValueType.Double) return Great(val1 as StringValue, val2 as DoubleValue);

            else Error(); return null;
        }
        private IValue Great(IntValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsInt() > val2.AsInt());
        }
        private IValue Great(IntValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsInt() > val2.AsDouble());
        }
        private IValue Great(IntValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsInt() > val2.AsString().Length);
        }
        private IValue Great(DoubleValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsDouble() > val2.AsDouble());
        }
        private IValue Great(DoubleValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsDouble() > val2.AsInt());
        }
        private IValue Great(DoubleValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsDouble() > val2.AsString().Length);
        }
        private IValue Great(StringValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsString().Length > val2.AsString().Length);
        }
        private IValue Great(StringValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsString().Length > val2.AsInt());
        }
        private IValue Great(StringValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsString().Length > val2.AsDouble());
        }

        private IValue LessEQ(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Int && type2 == ValueType.Int) return LessEQ(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Double) return LessEQ(val1 as IntValue, val2 as DoubleValue);
            else if (type1 == ValueType.Int && type2 == ValueType.String) return LessEQ(val1 as IntValue, val2 as StringValue);

            else if (type1 == ValueType.Double && type2 == ValueType.Double) return LessEQ(val1 as DoubleValue, val2 as DoubleValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Int) return LessEQ(val1 as DoubleValue, val2 as IntValue);
            else if (type1 == ValueType.Double && type2 == ValueType.String) return LessEQ(val1 as DoubleValue, val2 as StringValue);

            else if (type1 == ValueType.String && type2 == ValueType.String) return LessEQ(val1 as StringValue, val2 as StringValue);
            else if (type1 == ValueType.String && type2 == ValueType.Int) return LessEQ(val1 as StringValue, val2 as IntValue);
            else if (type1 == ValueType.String && type2 == ValueType.Double) return LessEQ(val1 as StringValue, val2 as DoubleValue);

            else Error(); return null;
        }
        private IValue LessEQ(IntValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsInt() <= val2.AsInt());
        }
        private IValue LessEQ(IntValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsInt() <= val2.AsDouble());
        }
        private IValue LessEQ(IntValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsInt() <= val2.AsString().Length);
        }
        private IValue LessEQ(DoubleValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsDouble() <= val2.AsDouble());
        }
        private IValue LessEQ(DoubleValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsDouble() <= val2.AsInt());
        }
        private IValue LessEQ(DoubleValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsDouble() <= val2.AsString().Length);
        }
        private IValue LessEQ(StringValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsString().Length <= val2.AsString().Length);
        }
        private IValue LessEQ(StringValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsString().Length <= val2.AsInt());
        }
        private IValue LessEQ(StringValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsString().Length <= val2.AsDouble());
        }

        private IValue GreatEQ(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Int && type2 == ValueType.Int) return GreatEQ(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Double) return GreatEQ(val1 as IntValue, val2 as DoubleValue);
            else if (type1 == ValueType.Int && type2 == ValueType.String) return GreatEQ(val1 as IntValue, val2 as StringValue);

            else if (type1 == ValueType.Double && type2 == ValueType.Double) return GreatEQ(val1 as DoubleValue, val2 as DoubleValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Int) return GreatEQ(val1 as DoubleValue, val2 as IntValue);
            else if (type1 == ValueType.Double && type2 == ValueType.String) return GreatEQ(val1 as DoubleValue, val2 as StringValue);

            else if (type1 == ValueType.String && type2 == ValueType.String) return GreatEQ(val1 as StringValue, val2 as StringValue);
            else if (type1 == ValueType.String && type2 == ValueType.Int) return GreatEQ(val1 as StringValue, val2 as IntValue);
            else if (type1 == ValueType.String && type2 == ValueType.Double) return GreatEQ(val1 as StringValue, val2 as DoubleValue);

            else Error(); return null;
        }
        private IValue GreatEQ(IntValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsInt() >= val2.AsInt());
        }
        private IValue GreatEQ(IntValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsInt() >= val2.AsDouble());
        }
        private IValue GreatEQ(IntValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsInt() >= val2.AsString().Length);
        }
        private IValue GreatEQ(DoubleValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsDouble() >= val2.AsDouble());
        }
        private IValue GreatEQ(DoubleValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsDouble() >= val2.AsInt());
        }
        private IValue GreatEQ(DoubleValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsDouble() >= val2.AsString().Length);
        }
        private IValue GreatEQ(StringValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsString().Length >= val2.AsString().Length);
        }
        private IValue GreatEQ(StringValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsString().Length >= val2.AsInt());
        }
        private IValue GreatEQ(StringValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsString().Length >= val2.AsDouble());
        }

        private IValue Equal(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Boolean && type2 == ValueType.Boolean) return Equal(val1 as BooleanValue, val2 as BooleanValue);
            else if (type1 == ValueType.Boolean && type2 == ValueType.Int) return Equal(val1 as BooleanValue, val2 as IntValue);
            else if (type1 == ValueType.Boolean && type2 == ValueType.Double) return Equal(val1 as BooleanValue, val2 as DoubleValue);
            else if (type1 == ValueType.Boolean && type2 == ValueType.String) return Equal(val1 as BooleanValue, val2 as StringValue);

            else if (type1 == ValueType.Int && type2 == ValueType.Int) return Equal(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Boolean) return Equal(val1 as IntValue, val2 as BooleanValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Double) return Equal(val1 as IntValue, val2 as DoubleValue);
            else if (type1 == ValueType.Int && type2 == ValueType.String) return Equal(val1 as IntValue, val2 as StringValue);

            else if (type1 == ValueType.Double && type2 == ValueType.Double) return Equal(val1 as DoubleValue, val2 as DoubleValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Boolean) return Equal(val1 as DoubleValue, val2 as BooleanValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Int) return Equal(val1 as DoubleValue, val2 as IntValue);
            else if (type1 == ValueType.Double && type2 == ValueType.String) return Equal(val1 as DoubleValue, val2 as StringValue);

            else if (type1 == ValueType.String && type2 == ValueType.String) return Equal(val1 as StringValue, val2 as StringValue);
            else if (type1 == ValueType.String && type2 == ValueType.Boolean) return Equal(val1 as StringValue, val2 as BooleanValue);
            else if (type1 == ValueType.String && type2 == ValueType.Int) return Equal(val1 as StringValue, val2 as IntValue);
            else if (type1 == ValueType.String && type2 == ValueType.Double) return Equal(val1 as StringValue, val2 as DoubleValue);

            else Error(); return null;
        }
        private IValue Equal(BooleanValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsBool() == val2.AsBool());
        }
        private IValue Equal(BooleanValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsInt() == val2.AsInt());
        }
        private IValue Equal(BooleanValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsDouble() == val2.AsDouble());
        }
        private IValue Equal(BooleanValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsBool() == val2.AsBool());
        }
        private IValue Equal(IntValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsInt() == val2.AsInt());
        }
        private IValue Equal(IntValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsInt() == val2.AsInt());
        }
        private IValue Equal(IntValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsInt() == val2.AsDouble());
        }
        private IValue Equal(IntValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsInt() == val2.AsString().Length);
        }
        private IValue Equal(DoubleValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsDouble() == val2.AsDouble());
        }
        private IValue Equal(DoubleValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsDouble() == val2.AsDouble());
        }
        private IValue Equal(DoubleValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsDouble() == val2.AsInt());
        }
        private IValue Equal(DoubleValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsDouble() == val2.AsString().Length);
        }
        private IValue Equal(StringValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsString() == val2.AsString());
        }
        private IValue Equal(StringValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsBool() == val2.AsBool());
        }
        private IValue Equal(StringValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsString().Length == val2.AsInt());
        }
        private IValue Equal(StringValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsString().Length == val2.AsDouble());
        }

        private IValue NotEqual(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Boolean && type2 == ValueType.Boolean) return NotEqual(val1 as BooleanValue, val2 as BooleanValue);
            else if (type1 == ValueType.Boolean && type2 == ValueType.Int) return NotEqual(val1 as BooleanValue, val2 as IntValue);
            else if (type1 == ValueType.Boolean && type2 == ValueType.Double) return NotEqual(val1 as BooleanValue, val2 as DoubleValue);
            else if (type1 == ValueType.Boolean && type2 == ValueType.String) return NotEqual(val1 as BooleanValue, val2 as StringValue);

            else if (type1 == ValueType.Int && type2 == ValueType.Int) return NotEqual(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Boolean) return NotEqual(val1 as IntValue, val2 as BooleanValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Double) return NotEqual(val1 as IntValue, val2 as DoubleValue);
            else if (type1 == ValueType.Int && type2 == ValueType.String) return NotEqual(val1 as IntValue, val2 as StringValue);

            else if (type1 == ValueType.Double && type2 == ValueType.Double) return NotEqual(val1 as DoubleValue, val2 as DoubleValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Boolean) return NotEqual(val1 as DoubleValue, val2 as BooleanValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Int) return NotEqual(val1 as DoubleValue, val2 as IntValue);
            else if (type1 == ValueType.Double && type2 == ValueType.String) return NotEqual(val1 as DoubleValue, val2 as StringValue);

            else if (type1 == ValueType.String && type2 == ValueType.String) return NotEqual(val1 as StringValue, val2 as StringValue);
            else if (type1 == ValueType.String && type2 == ValueType.Boolean) return NotEqual(val1 as StringValue, val2 as BooleanValue);
            else if (type1 == ValueType.String && type2 == ValueType.Int) return NotEqual(val1 as StringValue, val2 as IntValue);
            else if (type1 == ValueType.String && type2 == ValueType.Double) return NotEqual(val1 as StringValue, val2 as DoubleValue);

            else Error(); return null;
        }
        private IValue NotEqual(BooleanValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsBool() != val2.AsBool());
        }
        private IValue NotEqual(BooleanValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsInt() != val2.AsInt());
        }
        private IValue NotEqual(BooleanValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsDouble() != val2.AsDouble());
        }
        private IValue NotEqual(BooleanValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsBool() != val2.AsBool());
        }
        private IValue NotEqual(IntValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsInt() != val2.AsInt());
        }
        private IValue NotEqual(IntValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsInt() != val2.AsInt());
        }
        private IValue NotEqual(IntValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsInt() != val2.AsDouble());
        }
        private IValue NotEqual(IntValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsInt() != val2.AsString().Length);
        }
        private IValue NotEqual(DoubleValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsDouble() != val2.AsDouble());
        }
        private IValue NotEqual(DoubleValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsDouble() != val2.AsDouble());
        }
        private IValue NotEqual(DoubleValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsDouble() != val2.AsInt());
        }
        private IValue NotEqual(DoubleValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsDouble() != val2.AsString().Length);
        }
        private IValue NotEqual(StringValue val1, StringValue val2)
        {
            return new BooleanValue(val1.AsString() != val2.AsString());
        }
        private IValue NotEqual(StringValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsBool() != val2.AsBool());
        }
        private IValue NotEqual(StringValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsString().Length != val2.AsInt());
        }
        private IValue NotEqual(StringValue val1, DoubleValue val2)
        {
            return new BooleanValue(val1.AsString().Length != val2.AsDouble());
        }

        private IValue And(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Boolean && type2 == ValueType.Boolean) return And(val1 as BooleanValue, val2 as BooleanValue);
            else if (type1 == ValueType.Boolean && type2 == ValueType.Int) return And(val1 as BooleanValue, val2 as IntValue);

            else if (type1 == ValueType.Int && type2 == ValueType.Int) return And(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Boolean) return And(val1 as IntValue, val2 as BooleanValue);

            else Error(); return null;
        }
        private IValue And(BooleanValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsBool() & val2.AsBool());
        }
        private IValue And(BooleanValue val1, IntValue val2)
        {
            return new IntValue(val1.AsInt() & val2.AsInt());
        }
        private IValue And(IntValue val1, IntValue val2)
        {
            return new IntValue(val1.AsInt() & val2.AsInt());
        }
        private IValue And(IntValue val1, BooleanValue val2)
        {
            return new IntValue(val1.AsInt() & val2.AsInt());
        }

        private IValue Xor(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Boolean && type2 == ValueType.Boolean) return Xor(val1 as BooleanValue, val2 as BooleanValue);
            else if (type1 == ValueType.Boolean && type2 == ValueType.Int) return Xor(val1 as BooleanValue, val2 as IntValue);

            else if (type1 == ValueType.Int && type2 == ValueType.Int) return Xor(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Boolean) return Xor(val1 as IntValue, val2 as BooleanValue);

            else Error(); return null;
        }
        private IValue Xor(BooleanValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsBool() ^ val2.AsBool());
        }
        private IValue Xor(BooleanValue val1, IntValue val2)
        {
            return new IntValue(val1.AsInt() ^ val2.AsInt());
        }
        private IValue Xor(IntValue val1, IntValue val2)
        {
            return new IntValue(val1.AsInt() ^ val2.AsInt());
        }
        private IValue Xor(IntValue val1, BooleanValue val2)
        {
            return new IntValue(val1.AsInt() ^ val2.AsInt());
        }

        private IValue Or(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Boolean && type2 == ValueType.Boolean) return Or(val1 as BooleanValue, val2 as BooleanValue);
            else if (type1 == ValueType.Boolean && type2 == ValueType.Int) return Or(val1 as BooleanValue, val2 as IntValue);

            else if (type1 == ValueType.Int && type2 == ValueType.Int) return Or(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Boolean) return Or(val1 as IntValue, val2 as BooleanValue);

            else Error(); return null;
        }
        private IValue Or(BooleanValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsBool() | val2.AsBool());
        }
        private IValue Or(BooleanValue val1, IntValue val2)
        {
            return new IntValue(val1.AsInt() | val2.AsInt());
        }
        private IValue Or(IntValue val1, IntValue val2)
        {
            return new IntValue(val1.AsInt() | val2.AsInt());
        }
        private IValue Or(IntValue val1, BooleanValue val2)
        {
            return new IntValue(val1.AsInt() | val2.AsInt());
        }

        private IValue AndAnd(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Boolean && type2 == ValueType.Boolean) return AndAnd(val1 as BooleanValue, val2 as BooleanValue);
            else if (type1 == ValueType.Boolean && type2 == ValueType.Int) return AndAnd(val1 as BooleanValue, val2 as IntValue);

            else if (type1 == ValueType.Int && type2 == ValueType.Int) return AndAnd(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Boolean) return AndAnd(val1 as IntValue, val2 as BooleanValue);

            else Error(); return null;
        }
        private IValue AndAnd(BooleanValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsBool() && val2.AsBool());
        }
        private IValue AndAnd(BooleanValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsBool() && val2.AsBool());
        }
        private IValue AndAnd(IntValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsBool() && val2.AsBool());
        }
        private IValue AndAnd(IntValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsBool() && val2.AsBool());
        }

        private IValue OrOr(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Boolean && type2 == ValueType.Boolean) return OrOr(val1 as BooleanValue, val2 as BooleanValue);
            else if (type1 == ValueType.Boolean && type2 == ValueType.Int) return OrOr(val1 as BooleanValue, val2 as IntValue);

            else if (type1 == ValueType.Int && type2 == ValueType.Int) return OrOr(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Boolean) return OrOr(val1 as IntValue, val2 as BooleanValue);

            else Error(); return null;
        }
        private IValue OrOr(BooleanValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsBool() || val2.AsBool());
        }
        private IValue OrOr(BooleanValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsBool() || val2.AsBool());
        }
        private IValue OrOr(IntValue val1, IntValue val2)
        {
            return new BooleanValue(val1.AsBool() || val2.AsBool());
        }
        private IValue OrOr(IntValue val1, BooleanValue val2)
        {
            return new BooleanValue(val1.AsBool() || val2.AsBool());
        }

        private void Error()
        {
            throw new ConditionExpressionException("Unexpected expression - " + ToString());
        }
        public override string ToString()
        {
            return exp1.ToString() + conditionTypeTable[conditionType] + exp2.ToString();
        }
    }
}