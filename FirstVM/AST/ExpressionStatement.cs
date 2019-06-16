namespace FirstVM.AST
{
    class ExpressionStatement : IStatement
    {
        private readonly IExpression expression;
        public ExpressionStatement(IExpression expression)
        {
            this.expression = expression;
        }

        public void Execute()
        {
            expression.Eval();
        }

        public override string ToString()
        {
            return expression.ToString();
        }
    }
}