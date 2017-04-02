namespace RPN
{
    public class Calculator
    {
        private readonly InfixToPostfix _infixToPostfix;
        private readonly PostfixCalculate _postfixCalculate;
        private string PostfixExpression { get; set; }

        public Calculator()
        {
            var tokenizer = new Tokenizer(CalcBuilder.Operators);
            _infixToPostfix = new InfixToPostfix(tokenizer);
            _postfixCalculate = new PostfixCalculate(CalcBuilder.Operators);
        }

        public decimal Calculate(string infixExpression)
        {
            PostfixExpression = _infixToPostfix.Convert(infixExpression);
            return _postfixCalculate.Calculate(PostfixExpression);
        }
    }
}