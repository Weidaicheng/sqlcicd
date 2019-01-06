using System;
using NUnit.Framework;
using sqlcicd.Syntax;

namespace sqlcicd.Tests
{
    public class MssqlGrammerCheckerTest
    {
        private MssqlGrammarChecker checker;

        [SetUp]
        public void SetUp()
        {
            checker = new MssqlGrammarChecker();
        }

        [Test]
        public void Check_CorrectSyntax_ShouldPassCheck()
        {
            var result = checker.Check("SELECT * FROM TB1", out string errMsg);

            Assert.IsTrue(result);
        }

        [Test]
        public void Check_WrongSyntax_ShouldFailCheck()
        {
            var result = checker.Check("SLECT * FROM TB1", out string errMsg);

            Assert.IsFalse(result);
            Assert.AreNotEqual(string.Empty, errMsg);
        }

        [Test]
        public void Check_MultipleErrors_AggregatesErrorsIntoAString()
        {
            var result = checker.Check("SEECT * FROM TB1;\n UPDATE TB2 S * FROM TB2;", out string errMsg);

            Assert.IsFalse(result);
            StringAssert.Contains("\n", errMsg);
        }
    }
}