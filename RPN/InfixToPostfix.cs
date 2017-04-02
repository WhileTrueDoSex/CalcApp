using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RPN.Operators;

namespace RPN
{
    public class InfixToPostfix
    {
        private readonly Tokenizer _tokenizer;
        private List<Token> _tokens;

        private readonly Queue<string> _queue = new Queue<string>();
        private readonly Stack<string> _stack = new Stack<string>();

        public InfixToPostfix(Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public string Convert(string infix)
        {
            using (var reader = new StringReader(infix))
            {
                _queue.Clear();
                _stack.Clear();

                _tokens = _tokenizer.Tokenize(reader).ToList();
                return DoShuntingYardAlg(_tokens).Trim();
            }
        }

        private string DoShuntingYardAlg(IEnumerable<Token> tokens)
        {
            var lastToken = new Token();
            var afterOpenParenthesis = false;
            var first = true;

            foreach (var token in tokens)
            {
                if (token.Type == TokenType.Number)
                    AddToOutput(token.Value);
                else if (token.Type == TokenType.ParenthesisLeft)
                    _stack.Push(token.Value);
                else if (token.Type == TokenType.ParenthesisRight)
                {
                    var lastOperation = _stack.Peek();
                    while (lastOperation != "(")
                    {
                        AddToOutput(_stack.Pop());
                        lastOperation = _stack.Peek();
                    }
                    if (_stack.Count == 0)
                        throw new Exception("Missing open parenthesis");
                    _stack.Pop();
                    afterOpenParenthesis = true;
                }
                else if (token.Type == TokenType.Operator)
                {
                    var currentOperation = token.Value;
                    if (first && currentOperation == "-")
                    {
                        currentOperation = "m";
                    }
                    else if (_stack.Count > 0)
                    {
                        var lastOperation = _stack.Peek();
                        if (!afterOpenParenthesis)
                        {
                            if ((IsOperator(lastOperation) || lastOperation == "(") && 
                                 currentOperation == "-" && lastOperation != "m" && 
                                 lastToken.Type != TokenType.Number)
                            {
                                currentOperation = "m";
                            }
                        }
                        while (IsOperator(lastOperation))
                        {
                            if (IsLeftAssociative(currentOperation) && LessOrEqualPrecedence(currentOperation, lastOperation) ||
                                IsRightAssociative(currentOperation) && LessPrecedence(currentOperation, lastOperation))
                            {
                                AddToOutput(_stack.Pop());

                                if (_stack.Count == 0)
                                    break;
                                lastOperation = _stack.Peek();
                            }
                            else
                                break;
                        }
                    }

                    _stack.Push(currentOperation);
                    afterOpenParenthesis = false;
                }
                else
                {
                    throw new Exception($"Invalid Syntax! Unknown symbol \"{token.Value}\"");
                }

                lastToken = token;
                first = false;
            }
            while (_stack.Count > 0)
            {
                var lastOperation = _stack.Peek();
                if (lastOperation == "(" || lastOperation == ")")
                    throw new Exception("Missing closing parenthesis");
                AddToOutput(_stack.Pop());
            }

            return CreateOutput(_queue);
        }

        private bool IsOperator(string symbol)
        {
            return !string.IsNullOrEmpty(symbol) && _tokenizer.Operators.Any(x => x.Symbol == symbol[0]);
        }

        private bool LessPrecedence(string currentOperationSymbol, string lastOperationSymbol)
        {
            var op1 = GetOperation(currentOperationSymbol);
            var op2 = GetOperation(lastOperationSymbol);

            return op1.Precedence < op2.Precedence;
        }

        private bool LessOrEqualPrecedence(string currentOperationSymbol, string lastOperationSymbol)
        {
            var op1 = GetOperation(currentOperationSymbol);
            var op2 = GetOperation(lastOperationSymbol);

            return op1?.Precedence <= op2?.Precedence;
        }

        private bool IsLeftAssociative(string operationSymbol)
        {
            var op = GetOperation(operationSymbol);

            return op?.Associativity == Associativity.Left;
        }

        private bool IsRightAssociative(string operationSymbol)
        {
            var op = GetOperation(operationSymbol);

            return op?.Associativity == Associativity.Right;
        }

        private IOperator GetOperation(string operationSymbol)
        {
            return _tokenizer.Operators.FirstOrDefault(x => x.Symbol == operationSymbol[0]);
        }

        private void AddToOutput(string text)
        {
            _queue.Enqueue(text + " ");
        }

        private string CreateOutput(IEnumerable<string> output)
        {
            var sb = new StringBuilder();

            foreach (var entry in output)
                sb.Append(entry);

            return sb.ToString();
        }
    }
}