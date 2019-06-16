namespace FirstVM.ValueTypes
{
    class BooleanValue : Value
    {
        public static BooleanValue ZERO = new BooleanValue(false);

        public const string SFALSE = "false";
        public const string STRUE = "true";

        private readonly bool value;
        public BooleanValue(bool value)
            : base(ValueType.Boolean)
        {
            this.value = value;
        }

        public override bool AsBool()
        {
            return value;
        }
        public override int AsInt()
        {
            return value ? 1 : 0;
        }
        public override double AsDouble()
        {
            return AsInt();
        }
        public override string AsString()
        {
            return value ? STRUE : SFALSE;
        }
    }
}