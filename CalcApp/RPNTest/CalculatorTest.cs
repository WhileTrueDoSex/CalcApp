using System;
using System.CodeDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPN;

namespace RPNTest
{
    [TestClass]
    public class CalculatorTest
    {
        private readonly Calculator _calc = new Calculator();

        [TestMethod]
        public void TestCaltulateMethod()
        {
            var expr1 = _calc.Calculate("100/10");
            var expr2 = _calc.Calculate("5/2");
            var expr3 = _calc.Calculate("2.5*2");
            var expr4 = _calc.Calculate("2^2");
            var expr5 = _calc.Calculate("-1");
            var expr6 = _calc.Calculate("2+2*2");
            var expr7 = _calc.Calculate("(2+2)*2");
            var expr8 = _calc.Calculate("(2+2)*2*#4");

            Assert.AreEqual(expr1, 10);     
            Assert.AreEqual(expr2, 2.5m);
            Assert.AreEqual(expr3, 5);
            Assert.AreEqual(expr4, 4);
            Assert.AreEqual(expr5, -1);
            Assert.AreEqual(expr6, 6);
            Assert.AreEqual(expr7, 8);
            Assert.AreEqual(expr8, 16);
        }
    }
}
