namespace FirstVM.ValueTypes
{
    class IntValue : Value
    {
        public static IntValue ZERO = new IntValue(0);

        private readonly int value;
        public IntValue(int value)
            : base(ValueType.Int)
        {
            this.value = value;
        }

        public override bool AsBool()
        {
            return value > 0 ? true : false;
        }
        public override int AsInt()
        {
            return value;
        }
        public override double AsDouble()
        {
            return (double)value;
        }
        public override string AsString()
        {
            return value.ToString();
        }
    }
}