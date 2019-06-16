namespace FirstVM.AST
{
    class DoWhileStatement : IStatement
    {
        private readonly IStatement statement;
        private readonly IExpression condition;
        public DoWhileStatement(IStatement statement, IExpression condition)
        {
            this.statement = statement;
            this.condition = condition;
        }

        public void Execute()
        {
            do statement.Execute();
            while (condition.Eval().AsBool());
        }

        public override string ToString()
        {
            string newLine = statement is BlockStatement ? System.Environment.NewLine : " ";
            return "do" + newLine + statement.ToString() + System.Environment.NewLine +
                "while " + condition.ToString();
        }
    }
}