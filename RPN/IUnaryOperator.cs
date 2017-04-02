namespace RPN
{
    internal interface IUnaryOperator : IOperator
    {
        decimal Calculate(decimal operand);
    }
}