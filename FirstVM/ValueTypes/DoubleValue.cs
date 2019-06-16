namespace FirstVM.ValueTypes
{
    class DoubleValue : Value
    {
        public static DoubleValue ZERO = new DoubleValue(0);

        private readonly double value;
        public DoubleValue(double value)
            : base(ValueType.Double)
        {
            this.value = value;
        }

        public override bool AsBool()
        {
            return value > 0 ? true : false;
        }
        public override int AsInt()
        {
            return (int)value;
        }
        public override double AsDouble()
        {
            return value;
        }
        public override string AsString()
        {
            return value.ToString().Replace(',', '.');
        }
    }
}