namespace RPN.Operators
{
    internal class UnaryMinus : IUnaryOperator
    {
        public char Symbol => 'm';
        public bool IsUnary => true;
        public bool IsBinary => false;
        public int Precedence => 2;
        public Associativity Associativity => Associativity.Right;

        public decimal Calculate(decimal operand)
        {
            return -operand;
        }
    }
}