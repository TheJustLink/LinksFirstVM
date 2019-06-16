namespace FirstVM.ValueTypes
{
    public interface IValue
    {
        bool AsBool();
        int AsInt();
        double AsDouble();
        string AsString();

        ValueType GetTypeValue();
        bool IsType(ValueType valueType);
    }
}