namespace RPN
{
    public interface IOperator
    {
        char Symbol { get; }
        bool IsUnary { get; }
        bool IsBinary { get; }
        int Precedence { get; }
        Associativity Associativity { get; }
    }

    public enum Associativity
    {
        Left,
        Right
    }
}