using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using sqlcicd.Commands;
using sqlcicd.Commands.Entity;

namespace sqlcicd.Tests
{
    public class CMDCommandInvokerTest
    {
        [Test]
        public async Task Invoke_ExecuteFail_ReturnsFalse()
        {
            var command = Substitute.For<ICommand>();
            command.Execute().Returns(new ExecutionResult()
            {
                Success = false
            });

            var invoker = new CMDCommandInvoker(command);
            var result = await invoker.Invoke();

            Assert.IsFalse(result.Success);
        }

        [Test]
        public async Task Invoke_ExecuteSuccess_ReturnsTrue()
        {
            var command = Substitute.For<ICommand>();
            command.Execute().Returns(new ExecutionResult()
            {
                Success = true
            });

            var invoker = new CMDCommandInvoker(command);
            var result = await invoker.Invoke();

            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task Invoke_ExecuteThrowsException_ReturnsFalse()
        {
            var command = Substitute.For<ICommand>();
            command
                .When(c => c.Execute())
                .Do(info =>
                {
                    throw new Exception();
                });

            var invoker = new CMDCommandInvoker(command);
            var result = await invoker.Invoke();

            Assert.IsFalse(result.Success);
        }
    }
}