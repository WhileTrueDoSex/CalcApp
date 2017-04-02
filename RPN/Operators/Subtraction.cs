namespace RPN.Operators
{
    internal class Subtraction : IBinaryOperator
    {
        public char Symbol => '-';
        public bool IsUnary => false;
        public bool IsBinary => true;
        public int Precedence => 0;
        public Associativity Associativity => Associativity.Left;

        public decimal Calculate(decimal leftOperand, decimal rightOperand)
        {
            return leftOperand - rightOperand;
        }
    }
}