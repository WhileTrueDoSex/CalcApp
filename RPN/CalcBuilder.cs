using System.Collections.Generic;
using RPN.Operators;

namespace RPN
{
    public static class CalcBuilder
    {
        public static readonly List<IOperator> Operators;

        static CalcBuilder()
        {
            Operators = new List<IOperator>
            {
                new Addition(),
                new Degree(),
                new Division(),
                new Multiplication(),
                new Subtraction(),
                new UnaryMinus(),
                new SquareRoot()
            };
        }

        public static void AddOperation(IOperator op)
        {
            Operators.Add(op);
        }

        public static void RemoveOperation(IOperator op)
        {
            Operators.Remove(op);
        }
    }
}