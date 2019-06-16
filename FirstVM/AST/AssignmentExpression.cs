using FirstVM.ValueTypes;
namespace FirstVM.AST
{
    class AssignmentExpression : IExpression
    {
        private readonly VariableExpression variable;
        private readonly IExpression expression;
        public AssignmentExpression(VariableExpression variable, IExpression expression)
        {
            this.variable = variable;
            this.expression = expression;
        }

        public IValue Eval()
        {
            IValue value = expression.Eval();
            variable.Set(value);
            return value;
        }

        public override string ToString()
        {
            return variable.ToString() + "=" + expression.ToString();
        }
    }
}