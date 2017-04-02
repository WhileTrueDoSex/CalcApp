namespace RPN.Operators
{
    internal class Multiplication : IBinaryOperator
    {
        public char Symbol => '*';
        public bool IsUnary => false;
        public bool IsBinary => true;
        public int Precedence => 1;
        public Associativity Associativity => Associativity.Left;

        public decimal Calculate(decimal leftOperand, decimal rightOperand)
        {
            return leftOperand * rightOperand;
        }
    }
}