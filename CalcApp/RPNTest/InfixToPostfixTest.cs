using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPN;

namespace RPNTest
{
    [TestClass]
    public class InfixToPostfixTest
    {
        private InfixToPostfix _i2p = new InfixToPostfix(new Tokenizer(CalcBuilder.Operators));

        [TestMethod]
        public void ConvertMethodTest()
        {
            var expr = "-6/-(-1-5)+-1";
            var expr2 = "3 + 4 * 2 / (1 - 5) ^ 2 ^ 3";
            var expr3 = "1+(1)+(1)+1";
            var expr4 = "1+(-1)+(-1)+1";
            var expr5 = "(1)-(-1)";

            Assert.AreEqual(_i2p.Convert(expr), "6 m 1 m 5 - m / 1 m +");
            Assert.AreEqual(_i2p.Convert(expr2), "3 4 2 * 1 5 - 2 3 ^ ^ / +");
            Assert.AreEqual(_i2p.Convert(expr3), "1 1 + 1 + 1 +");
            Assert.AreEqual(_i2p.Convert(expr4), "1 1 m + 1 m + 1 +");
            Assert.AreEqual(_i2p.Convert(expr5), "1 1 m -");
        }
    }
}
