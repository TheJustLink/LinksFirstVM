namespace FirstVM.AST
{
    class IfStatement : IStatement
    {
        private readonly IExpression condition;
        private readonly IStatement statement;
        private readonly IStatement elseStatement;
        public IfStatement(IExpression condition, IStatement statement, IStatement elseStatement = null)
        {
            this.condition = condition;
            this.statement = statement;
            this.elseStatement = elseStatement;
        }

        public void Execute()
        {
            if (condition.Eval().AsBool()) statement.Execute();
            else if (elseStatement != null) elseStatement.Execute();
        }

        public override string ToString()
        {
            string ifStatementNewLine = statement is BlockStatement ? System.Environment.NewLine : " ";
            string elseStatementNewLine = elseStatement != null && (elseStatement is BlockStatement) ? System.Environment.NewLine : "";
            string elseStr = elseStatement != null ? System.Environment.NewLine + "else " + elseStatementNewLine + elseStatement.ToString() : "";
            return "if " + condition.ToString() + ifStatementNewLine + statement.ToString() + elseStr;
        }
    }
}