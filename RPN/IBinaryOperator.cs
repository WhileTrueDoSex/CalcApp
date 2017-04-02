namespace RPN
{
    internal interface IBinaryOperator : IOperator
    {
        decimal Calculate(decimal leftOperand, decimal rightOperand);
    }
}