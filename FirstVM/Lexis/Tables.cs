using System.Collections.Generic;
namespace FirstVM.Lexis
{
    partial class LexicalAnalyzer
    {
        private static HashSet<char> _charNoneTable = new HashSet<char>() { NONE, CR, LF, END, TAB };
        private static Dictionary<char, TokenType> _charTable = new Dictionary<char, TokenType>()
        {
            {'=', TokenType.EQ},
            {'+', TokenType.ADD},
            {'-', TokenType.SUB},
            {'*', TokenType.MUL},
            {'/', TokenType.DIV},
            {'%', TokenType.MOD},

            {'<', TokenType.LESS},
            {'>', TokenType.GREAT},

            {'&', TokenType.AND},
            {'^', TokenType.XOR},
            {'|', TokenType.OR},

            {'[', TokenType.SQBKT_LT},
            {']', TokenType.SQBKT_RT},
            {'(', TokenType.BKT_LT},
            {')', TokenType.BKT_RT},
            {'{', TokenType.BRC_LT},
            {'}', TokenType.BRC_RT},
            {',', TokenType.COMMA},
            {';', TokenType.SEMICOLON},
            {':', TokenType.COLON},
            {'!', TokenType.EXMK},
            {'?', TokenType.QSTMK}
        };
        private static Dictionary<string, TokenType> _doubleCharTable = new Dictionary<string, TokenType>()
        {
            {"<<", TokenType.SHIFT_LT},
            {">>", TokenType.SHIFT_RT},

            {"<=", TokenType.LESS_EQ},
            {">=", TokenType.GREAT_EQ},
            {"==", TokenType.EQ_EQ},
            {"!=", TokenType.NOT_EQ},

            {"&&", TokenType.AND_AND},
            {"||", TokenType.OR_OR},
        };
        private static Dictionary<string, TokenType> _operatorsTable = new Dictionary<string, TokenType>()
        {
            {"true", TokenType.TRUE},
            {"false", TokenType.FALSE},

            {"is", TokenType.IS},
            {"as", TokenType.AS},

            {"echo", TokenType.ECHO},
            {"if", TokenType.IF},
            {"else", TokenType.ELSE},
            {"for", TokenType.FOR},
            {"do", TokenType.DO},
            {"while", TokenType.WHILE}
        };
    }
}