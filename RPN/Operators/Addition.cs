namespace RPN.Operators
{
    internal class Addition : IBinaryOperator
    {
        public char Symbol => '+';
        public int Precedence => 0;
        public bool IsUnary => false;
        public bool IsBinary => true;
        public Associativity Associativity => Associativity.Left;

        public decimal Calculate(decimal leftOperand, decimal rightOperand)
        {
            return leftOperand + rightOperand;
        }
    }
}