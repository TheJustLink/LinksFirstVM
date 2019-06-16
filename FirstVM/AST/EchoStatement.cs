using System;
namespace FirstVM.AST
{
    class EchoStatement : IStatement
    {
        private readonly IExpression expression;
        public EchoStatement(IExpression expression)
        {
            this.expression = expression;
        }

        public void Execute()
        {
            Console.Write(expression.Eval().AsString());
        }

        public override string ToString()
        {
            return "echo " + expression.ToString();
        }
    }
}