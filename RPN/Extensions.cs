using System;

namespace RPN
{
    public static class Extensions
    {
        public static decimal Pow(this decimal leftOperand, decimal rightOperand)
        {
            return (decimal)Math.Pow((double)leftOperand, (double)rightOperand);
        }
    }
}