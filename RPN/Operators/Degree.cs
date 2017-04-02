namespace RPN.Operators
{
    internal class Degree : IBinaryOperator
    {
        public char Symbol => '^';
        public bool IsUnary => false;
        public bool IsBinary => true;
        public int Precedence => 2;
        public Associativity Associativity => Associativity.Right;

        public decimal Calculate(decimal leftOperand, decimal rightOperand)
        {
            return leftOperand.Pow(rightOperand);
        }
    }
}