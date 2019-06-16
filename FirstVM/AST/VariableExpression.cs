using FirstVM.ValueTypes;
namespace FirstVM.AST
{
    class VariableExpression : IExpression
    {
        private readonly StringValue name;
        public VariableExpression(StringValue name)
        {
            this.name = name;
        }

        public IValue Eval()
        {
            return Get();
        }

        public IValue Get()
        {
            return VariablesTable.GetValue(name.AsString());
            //return Contexts.GetValue(name.AsString());
        }
        public void Set(IValue value)
        {
            VariablesTable.SetValue(name.AsString(), value);
            //Contexts.SetValue(name.AsString(), value);
        }

        public override string ToString()
        {
            return name.AsString();
        }
    }
}