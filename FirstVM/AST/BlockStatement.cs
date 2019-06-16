using System.Text;
namespace FirstVM.AST
{
    class BlockStatement : IStatement
    {
        private readonly IStatement[] statements;
        public BlockStatement(IStatement[] statements)
        {
            this.statements = statements;
        }

        public void Execute()
        {
            Contexts.PushNewContext();
            for (int i = 0; i < statements.Length; i++)
                statements[i].Execute();
            Contexts.PopContext();
        }

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.AppendLine("{");
            for (int i = 0; i < statements.Length; i++)
                buffer.AppendLine(statements[i].ToString());
            buffer.Append("}");
            return buffer.ToString();
        }
    }
}