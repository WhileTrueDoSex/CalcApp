namespace RPN
{
    public class Calculator
    {
        private readonly InfixToPostfix _infixToPostfix;
        private readonly PostfixCalculate _postfixCalculate;
        private string PostfixExpression { get; set; }

        public Calculator()
        {
            _infixToPostfix = new InfixToPostfix(new Tokenizer(CalcBuilder.Operators));
            _postfixCalculate = new PostfixCalculate(CalcBuilder.Operators);
        }

        public decimal Calculate(string infixExpression)
        {
            PostfixExpression = _infixToPostfix.Convert(infixExpression);
            return _postfixCalculate.Calculate(PostfixExpression);
        }
    }
}