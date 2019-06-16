namespace FirstVM.ValueTypes
{
    public abstract class Value : IValue
    {
        public readonly ValueType valueType;
        public Value(ValueType valueType)
        {
            this.valueType = valueType;
        }

        public abstract bool AsBool();
        public abstract int AsInt();
        public abstract double AsDouble();
        public abstract string AsString();

        public ValueType GetTypeValue()
        {
            return valueType;
        }
        public bool IsType(ValueType valueType)
        {
            return this.valueType == valueType;
        }
        public override string ToString()
        {
            return AsString();
        }
    }
}