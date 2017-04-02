using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RPN
{
    public class Tokenizer
    {
        public readonly IEnumerable<IOperator> Operators;

        public Tokenizer(IEnumerable<IOperator> operators)
        {
            Operators = operators;
        }

        private TokenType DetermineType(char ch)
        {
            if (char.IsDigit(ch) || ch == '.')
                return TokenType.Number;
            if (char.IsWhiteSpace(ch))
                return TokenType.WhiteSpace;
            if (ch == '(')
                return TokenType.ParenthesisLeft;
            if (ch == ')')
                return TokenType.ParenthesisRight;
            if (Operators.Any(x => x.Symbol == ch))
                return TokenType.Operator;

            throw new Exception($"Wrong character {ch}");
        }

        public IEnumerable<Token> Tokenize(TextReader reader)
        {
            var token = new StringBuilder();

            int curr;
            while ((curr = reader.Read()) != -1)
            {
                var ch = (char)curr;
                var currType = DetermineType(ch);
                if (currType == TokenType.WhiteSpace)
                    continue;

                token.Append(ch);

                var next = reader.Peek();
                var nextType = next != -1 ? DetermineType((char)next) : TokenType.WhiteSpace;
                if (currType != nextType || currType == TokenType.Operator && nextType == TokenType.Operator)
                {
                    yield return new Token(currType, token.ToString());
                    token.Clear();
                }
            }
        }
    }
}