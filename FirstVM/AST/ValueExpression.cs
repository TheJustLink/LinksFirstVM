using FirstVM.ValueTypes;
namespace FirstVM.AST
{
    class ValueExpression : IExpression
    {
        readonly IValue value;
        public ValueExpression(IValue value)
        {
            this.value = value;
        }
        public IValue Eval()
        {
            return value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}