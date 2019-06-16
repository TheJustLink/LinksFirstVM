namespace FirstVM.AST
{
    enum ConditionExpressionType : byte
    {
        Less,       // <
        Great,      // >
        LessEq,     // <=
        GreatEq,    // >=

        Eq,         // ==
        NotEq,      // != 

        AND,        // &
        XOR,        // ^
        OR,         // |
        ANDAND,     // &&
        OROR        // ||
    }
}