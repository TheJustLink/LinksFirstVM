using System.Collections.Generic;
using System.Linq;

using FirstVM.ValueTypes;
using FirstVM.Exceptions;
namespace FirstVM.AST
{
    class BinaryExpression : IExpression
    {
        public static Dictionary<BinaryExpressionType, string> expTypeTable = new Dictionary<BinaryExpressionType, string>()
        {
            {BinaryExpressionType.ADD, "+"},
            {BinaryExpressionType.SUB, "-"},
            {BinaryExpressionType.MUL, "*"},
            {BinaryExpressionType.DIV, "/"},
            {BinaryExpressionType.MOD, "%"},
        };

        private readonly IExpression exp1;
        private readonly IExpression exp2;
        private readonly BinaryExpressionType expType;
        public BinaryExpression(IExpression exp1, IExpression exp2, BinaryExpressionType expType)
        {
            this.exp1 = exp1;
            this.exp2 = exp2;
            this.expType = expType;
        }

        public IValue Eval()
        {
            IValue val1 = exp1.Eval();
            IValue val2 = exp2.Eval();
            switch (expType)
            {
                case BinaryExpressionType.ADD: return Add(val1, val2);
                case BinaryExpressionType.SUB: return Sub(val1, val2);
                case BinaryExpressionType.MUL: return Mul(val1, val2);
                case BinaryExpressionType.DIV: return Div(val1, val2);
                case BinaryExpressionType.MOD: return Mod(val1, val2);
                default: Error(); return null;
            }
        }

        private IValue Add(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Boolean && type2 == ValueType.String) return Add(val1 as BooleanValue, val2 as StringValue);

            else if (type1 == ValueType.Int && type2 == ValueType.Int) return Add(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Double) return Add(val1 as IntValue, val2 as DoubleValue);
            else if (type1 == ValueType.Int && type2 == ValueType.String) return Add(val1 as IntValue, val2 as StringValue);

            else if (type1 == ValueType.Double && type2 == ValueType.Double) return Add(val1 as DoubleValue, val2 as DoubleValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Int) return Add(val1 as DoubleValue, val2 as IntValue);
            else if (type1 == ValueType.Double && type2 == ValueType.String) return Add(val1 as DoubleValue, val2 as StringValue);

            else if (type1 == ValueType.String && type2 == ValueType.String) return Add(val1 as StringValue, val2 as StringValue);
            else if (type1 == ValueType.String && type2 == ValueType.Boolean) return Add(val1 as StringValue, val2 as BooleanValue);
            else if (type1 == ValueType.String && type2 == ValueType.Int) return Add(val1 as StringValue, val2 as IntValue);
            else if (type2 == ValueType.String && type2 == ValueType.Double) return Add(val1 as StringValue, val2 as DoubleValue);

            else Error(); return null;
        }
        private IValue Add(BooleanValue val1, StringValue val2)
        {
            return new StringValue(val1.AsString() + val2.AsString());
        }
        private IValue Add(IntValue val1, IntValue val2)
        {
            return new IntValue(val1.AsInt() + val2.AsInt());
        }
        private IValue Add(IntValue val1, DoubleValue val2)
        {
            return new DoubleValue(val1.AsInt() + val2.AsDouble());
        }
        private IValue Add(IntValue val1, StringValue val2)
        {
            return new StringValue(val1.AsString() + val2.AsString());
        }
        private IValue Add(DoubleValue val1, DoubleValue val2)
        {
            return new DoubleValue(val1.AsDouble() + val2.AsDouble());
        }
        private IValue Add(DoubleValue val1, IntValue val2)
        {
            return new DoubleValue(val1.AsDouble() + val2.AsInt());
        }
        private IValue Add(DoubleValue val1, StringValue val2)
        {
            return new StringValue(val1.AsString() + val2.AsString());
        }
        private IValue Add(StringValue val1, StringValue val2)
        {
            return new StringValue(val1.AsString() + val2.AsString());
        }
        private IValue Add(StringValue val1, BooleanValue val2)
        {
            return new StringValue(val1.AsString() + val2.AsString());
        }
        private IValue Add(StringValue val1, IntValue val2)
        {
            return new StringValue(val1.AsString() + val2.AsString());
        }
        private IValue Add(StringValue val1, DoubleValue val2)
        {
            return new StringValue(val1.AsString() + val2.AsString());
        }
        
        private IValue Sub(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Int && type2 == ValueType.Int) return Sub(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Double) return Sub(val1 as IntValue, val2 as DoubleValue);
            else if (type1 == ValueType.Int && type2 == ValueType.String) return Sub(val1 as IntValue, val2 as StringValue);

            else if (type1 == ValueType.Double && type2 == ValueType.Double) return Sub(val1 as DoubleValue, val2 as DoubleValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Int) return Sub(val1 as DoubleValue, val2 as IntValue);

            else if (type1 == ValueType.String && type2 == ValueType.String) return Sub(val1 as StringValue, val2 as StringValue);
            else if (type1 == ValueType.String && type2 == ValueType.Int) return Sub(val1 as StringValue, val2 as IntValue);

            else Error(); return null;
        }
        private IValue Sub(IntValue val1, IntValue val2)
        {
            return new IntValue(val1.AsInt() - val2.AsInt());
        }
        private IValue Sub(IntValue val1, DoubleValue val2)
        {
            return new DoubleValue(val1.AsInt() - val2.AsDouble());
        }
        private IValue Sub(IntValue val1, StringValue val2)
        {
            string s = val2.AsString();
            int i = val1.AsInt();
            if (i < 0) i = 0;
            if (i > s.Length) i = s.Length;
            return new StringValue(s.Remove(0, i));
        }
        private IValue Sub(DoubleValue val1, DoubleValue val2)
        {
            return new DoubleValue(val1.AsDouble() - val2.AsDouble());
        }
        private IValue Sub(DoubleValue val1, IntValue val2)
        {
            return new DoubleValue(val1.AsDouble() - val2.AsInt());
        }
        private IValue Sub(StringValue val1, StringValue val2)
        {
            return new StringValue(val1.AsString().Replace(val2.AsString(), ""));
        }
        private IValue Sub(StringValue val1, IntValue val2)
        {
            string s = val1.AsString();
            int i = val2.AsInt();
            if (i < 0) i = 0;
            if (i > s.Length) i = s.Length;
            return new StringValue(s.Remove(s.Length - i, i));
        }
        
        private IValue Mul(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Int && type2 == ValueType.Int) return Mul(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Double) return Mul(val1 as IntValue, val2 as DoubleValue);
            else if (type1 == ValueType.Int && type2 == ValueType.String) return Mul(val1 as IntValue, val2 as StringValue);

            else if (type1 == ValueType.Double && type2 == ValueType.Double) return Mul(val1 as DoubleValue, val2 as DoubleValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Int) return Mul(val1 as DoubleValue, val2 as IntValue);
            else if (type1 == ValueType.Double && type2 == ValueType.String) return Mul(val1 as DoubleValue, val2 as StringValue);

            else if (type1 == ValueType.String && type2 == ValueType.Int) return Mul(val1 as StringValue, val2 as IntValue);
            else if (type2 == ValueType.String && type2 == ValueType.Double) return Mul(val1 as StringValue, val2 as DoubleValue);

            else Error(); return null;
        }
        private IValue Mul(IntValue val1, IntValue val2)
        {
            return new IntValue(val1.AsInt() * val2.AsInt());
        }
        private IValue Mul(IntValue val1, DoubleValue val2)
        {
            return new DoubleValue(val1.AsInt() * val2.AsDouble());
        }
        private IValue Mul(IntValue val1, StringValue val2)
        {
            return Mul(val2, val1);
        }
        private IValue Mul(DoubleValue val1, DoubleValue val2)
        {
            return new DoubleValue(val1.AsDouble() * val2.AsDouble());
        }
        private IValue Mul(DoubleValue val1, IntValue val2)
        {
            return new DoubleValue(val1.AsDouble() * val2.AsInt());
        }
        private IValue Mul(DoubleValue val1, StringValue val2)
        {
            return Mul(val2, val1);
        }
        private IValue Mul(StringValue val1, IntValue val2)
        {
            return new StringValue(string.Concat<string>(Enumerable.Repeat<string>(val1.AsString(), val2.AsInt())));
        }
        private IValue Mul(StringValue val1, DoubleValue val2)
        {
            return new StringValue(string.Concat<string>(Enumerable.Repeat<string>(val1.AsString(), val2.AsInt())));
        }
        
        private IValue Div(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Int && type2 == ValueType.Int) return Div(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Double) return Div(val1 as IntValue, val2 as DoubleValue);

            else if (type1 == ValueType.Double && type2 == ValueType.Double) return Div(val1 as DoubleValue, val2 as DoubleValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Int) return Div(val1 as DoubleValue, val2 as IntValue);

            else if (type1 == ValueType.String && type2 == ValueType.String) return Div(val1 as StringValue, val2 as StringValue);

            else Error(); return null;
        }
        private IValue Div(IntValue val1, IntValue val2)
        {
            return new DoubleValue(val1.AsInt() / val2.AsInt());
        }
        private IValue Div(IntValue val1, DoubleValue val2)
        {
            return new DoubleValue(val1.AsInt() / val2.AsDouble());
        }
        private IValue Div(DoubleValue val1, DoubleValue val2)
        {
            return new DoubleValue(val1.AsDouble() / val2.AsDouble());
        }
        private IValue Div(DoubleValue val1, IntValue val2)
        {
            return new DoubleValue(val1.AsDouble() / val2.AsInt());
        }
        private IValue Div(StringValue val1, StringValue val2)
        {
            return new DoubleValue(val1.AsString().Length / val2.AsString().Length);
        }
        
        private IValue Mod(IValue val1, IValue val2)
        {
            ValueType type1 = val1.GetTypeValue();
            ValueType type2 = val2.GetTypeValue();

            if (type1 == ValueType.Int && type2 == ValueType.Int) return Mod(val1 as IntValue, val2 as IntValue);
            else if (type1 == ValueType.Int && type2 == ValueType.Double) return Mod(val1 as IntValue, val2 as DoubleValue);

            else if (type1 == ValueType.Double && type2 == ValueType.Double) return Mod(val1 as DoubleValue, val2 as DoubleValue);
            else if (type1 == ValueType.Double && type2 == ValueType.Int) return Mod(val1 as DoubleValue, val2 as IntValue);

            else if (type1 == ValueType.String && type2 == ValueType.String) return Mod(val1 as StringValue, val2 as StringValue);

            else Error(); return null;
        }
        private IValue Mod(IntValue val1, IntValue val2)
        {
            return new DoubleValue(val1.AsInt() % val2.AsInt());
        }
        private IValue Mod(IntValue val1, DoubleValue val2)
        {
            return new DoubleValue(val1.AsInt() % val2.AsDouble());
        }
        private IValue Mod(DoubleValue val1, DoubleValue val2)
        {
            return new DoubleValue(val1.AsDouble() % val2.AsDouble());
        }
        private IValue Mod(DoubleValue val1, IntValue val2)
        {
            return new DoubleValue(val1.AsDouble() % val2.AsInt());
        }
        private IValue Mod(StringValue val1, StringValue val2)
        {
            return new DoubleValue(val1.AsString().Length % val2.AsString().Length);
        }

        private void Error()
        {
            throw new BinaryExpressionException("Unexpected expression - " + ToString());
        }
        public override string ToString()
        {
            return exp1.ToString() + expTypeTable[expType] + exp2.ToString();
        }
    }
}