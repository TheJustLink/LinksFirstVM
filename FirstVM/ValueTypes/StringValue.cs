namespace FirstVM.ValueTypes
{
    class StringValue : Value
    {
        public static StringValue ZERO = null;

        private readonly string value;
        public StringValue(string value)
            : base(ValueType.String)
        {
            this.value = value;
        }

        public override bool AsBool()
        {
            return bool.Parse(value);
        }
        public override int AsInt()
        {
            return int.Parse(value);
        }
        public override string AsString()
        {
            return value;
        }
        public override double AsDouble()
        {
            return double.Parse(value);
        }

        public string Reverse()
        {
            char[] array = value.ToCharArray();
            System.Array.Reverse(array);
            return new string(array);
        }
        public override string ToString()
        {
            return "\"" + AsString().Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t") + "\"";
        }
    }
}