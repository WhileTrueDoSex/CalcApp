namespace RPN.Operators
{
    public class SquareRoot : IUnaryOperator
    {
        public char Symbol => '#';
        public bool IsUnary => true;
        public bool IsBinary => false;
        public int Precedence => 2;
        public Associativity Associativity => Associativity.Right;

        public decimal Calculate(decimal operand)
        {
            return operand.Sqrt();
        }
    }
}