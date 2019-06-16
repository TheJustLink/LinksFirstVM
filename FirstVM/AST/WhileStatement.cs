namespace FirstVM.AST
{
    class WhileStatement : IStatement
    {
        private readonly IExpression condition;
        private readonly IStatement statement;
        public WhileStatement(IExpression condition, IStatement statement)
        {
            this.condition = condition;
            this.statement = statement;
        }

        public void Execute()
        {
            while (condition.Eval().AsBool()) statement.Execute();
        }

        public override string ToString()
        {
            string newLine = statement is BlockStatement ? System.Environment.NewLine : " ";
            return "while " + condition.ToString() + newLine + statement.ToString();
        }
    }
}