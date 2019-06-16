using FirstVM.ValueTypes;
namespace FirstVM.AST
{
    public interface IExpression
    {
        IValue Eval();
    }
}