using FirstVM.ValueTypes;
namespace FirstVM.AST
{
    class BracketExpression : IExpression
    {
        private readonly IExpression exp;
        public BracketExpression(IExpression exp)
        {
            this.exp = exp;
        }

        public IValue Eval()
        {
            return exp.Eval();
        }

        public override string ToString()
        {
            return "(" + exp.ToString() + ")";
        }
    }
}