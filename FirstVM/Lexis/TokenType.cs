namespace FirstVM.Lexis
{
    public enum TokenType : byte
    {
        VAR,        //  Variable
        NUM,        //  Number 10(int) or 10.10(double)
        STR,        //  String "10"

        TRUE,       // Constant true
        FALSE,      // Constatnt false
        
        EQ,         //  =
        ADD,        //  +
        SUB,        //  -
        DIV,        //  /
        MUL,        //  *
        MOD,        //  %

        SHIFT_LT,   // <<
        SHIFT_RT,   // >>

        LESS,       // <
        GREAT,      // >
        LESS_EQ,    // <=
        GREAT_EQ,   // >=

        EQ_EQ,       // ==
        NOT_EQ,      // !=

        IS,         // 0.1 is double = true
        AS,         // 0.1 as int = 0

        AND,        // &
        XOR,        // ^
        OR,         // |

        AND_AND,    // &&
        OR_OR,      // ||

        ECHO,       // echo
        IF,         // if
        ELSE,       // else
        FOR,        // for
        DO,         // do
        WHILE,      // while

        SQBKT_LT,   // [
        SQBKT_RT,   // ]
        BKT_LT,     // (
        BKT_RT,     // )
        BRC_LT,     // {
        BRC_RT,     // }
        COMMA,      // ,
        SEMICOLON,  // ;
        COLON,      // :
        EXMK,       // !
        QSTMK,      // ?

        EOF         // EndOfFile
    }
}