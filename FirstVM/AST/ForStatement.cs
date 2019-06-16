namespace FirstVM.AST
{
    class ForStatement : IStatement
    {
        private readonly IStatement initialize;
        private readonly IExpression condition;
        private readonly IStatement action;
        private readonly IStatement statement;
        public ForStatement(IStatement initialize, IExpression condition, IStatement action, IStatement statement)
        {
            this.initialize = initialize;
            this.condition = condition;
            this.action = action;
            this.statement = statement;
        }

        public void Execute()
        {
            for (initialize.Execute(); condition.Eval().AsBool(); action.Execute())
                statement.Execute();
        }

        public override string ToString()
        {
            return "for (" + initialize.ToString() + "; " + condition.ToString() + "; " + action.ToString() + ")" +
                System.Environment.NewLine + statement;
        }
    }
}