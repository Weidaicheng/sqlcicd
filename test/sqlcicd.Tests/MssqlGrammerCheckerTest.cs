using NUnit.Framework;
using sqlcicd.Syntax;

namespace sqlcicd.Tests
{
    public class MssqlGrammerCheckerTest
    {
        private MssqlGrammerChecker checker;

        [SetUp]
        public void SetUp()
        {
            checker = new MssqlGrammerChecker();
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
    }
}