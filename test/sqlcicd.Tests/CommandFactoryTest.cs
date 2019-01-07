using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;
using NUnit.Framework;
using sqlcicd.Commands;
using sqlcicd.Commands.Entity;

namespace sqlcicd.Tests
{
    public class CommandFactoryTest
    {
        private CommandFactory cmdFactory;

        [SetUp]
        public void SetUp()
        {
            cmdFactory = new CommandFactory();
        }

        [Test]
        public void GenerateCommand_Empty_ReturnsHelpHelpWithEmptyPath()
        {
            var args = new string[0];

            var result = cmdFactory.GenerateCommand(args);

            Assert.IsTrue(result.MainCommand == CommandEnum.HELP_CMD &&
                          result.SubCommand == CommandEnum.HELP_CMD &&
                          result.Path == string.Empty);
        }

        [Test]
        public void GenerateCommand_InegrateEmpty_ReturnsIntegrateEmptyWithEmptyPath()
        {
            var args = new string[]
            {
                CommandEnum.INTEGRATE_CMD
            };

            var result = cmdFactory.GenerateCommand(args);

            Assert.IsTrue(result.MainCommand == CommandEnum.INTEGRATE_CMD &&
                          result.SubCommand == CommandEnum.HELP_CMD &&
                          result.Path == string.Empty);
        }
        
        [Test]
        public void GenerateCommand_DeliveryEmpty_ReturnsDeliveryEmptyWithEmptyPath()
        {
            var args = new string[]
            {
                CommandEnum.DELIVERY_CMD
            };

            var result = cmdFactory.GenerateCommand(args);

            Assert.IsTrue(result.MainCommand == CommandEnum.DELIVERY_CMD &&
                          result.SubCommand == CommandEnum.HELP_CMD &&
                          result.Path == string.Empty);
        }

        [Test]
        public void GenerateCommand_IntegratePath_ReturnsIntegrateEmptyWithPath()
        {
            var args = new string[]
            {
                CommandEnum.INTEGRATE_CMD,
                "path"
            };

            var result = cmdFactory.GenerateCommand(args);

            Assert.IsTrue(result.MainCommand == CommandEnum.INTEGRATE_CMD &&
                          result.SubCommand == string.Empty &&
                          !string.IsNullOrEmpty(result.Path));
        }
        
        [Test]
        public void GenerateCommand_DeliveryPath_ReturnsDeliveryEmptyWithPath()
        {
            var args = new string[]
            {
                CommandEnum.DELIVERY_CMD,
                "path"
            };

            var result = cmdFactory.GenerateCommand(args);

            Assert.IsTrue(result.MainCommand == CommandEnum.DELIVERY_CMD &&
                          result.SubCommand == string.Empty &&
                          !string.IsNullOrEmpty(result.Path));
        }
    }
}