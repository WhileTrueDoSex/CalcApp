namespace RPN
{
    public enum TokenType
    {
        Number,
        Operator,
        WhiteSpace,
        ParenthesisLeft,
        ParenthesisRight,
    };

    public struct Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public override string ToString() => $"{Type}: {Value}";

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}