using FirstVM.ValueTypes;
namespace FirstVM.AST
{
    class TernaryExpression : IExpression
    {
        private readonly IExpression condition;
        private readonly IExpression expTrue;
        private readonly IExpression expFalse;
        public TernaryExpression(IExpression condition, IExpression expTrue, IExpression expFalse)
        {
            this.condition = condition;
            this.expTrue = expTrue;
            this.expFalse = expFalse;
        }

        public IValue Eval()
        {
            return condition.Eval().AsBool() ? expTrue.Eval() : expFalse.Eval();
        }

        public override string ToString()
        {
            return condition.ToString() + "?" + expTrue + ":" + expFalse;
        }
    }
}