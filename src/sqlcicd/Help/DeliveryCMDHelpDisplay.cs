using System;
using sqlcicd.Commands.Entity;

namespace sqlcicd.Help
{
    /// <summary>
    /// Display for Continuous Delivery help
    /// </summary>
    public class DeliveryCMDHelpDisplay : ICMDHelpDisplay
    {
        public void Display()
        {
            Console.WriteLine(
                $"Usage: {typeof(Program).Assembly.GetName().Name} {CommandEnum.DELIVERY_CMD} <path>");
            Console.WriteLine(
                $"Usage: {typeof(Program).Assembly.GetName().Name} {CommandEnum.DELIVERY_CMD_SHORT} <path>");
            Console.WriteLine(
                $"Usage: {typeof(Program).Assembly.GetName().Name} {CommandEnum.DELIVERY_CMD} [options]");
            Console.WriteLine(
                $"Usage: {typeof(Program).Assembly.GetName().Name} {CommandEnum.DELIVERY_CMD_SHORT} [options]");
            Console.WriteLine();
            Console.WriteLine("options:");
            Console.WriteLine(
                $"\t{CommandEnum.HELP_CMD}|{CommandEnum.HELP_CMD_SHORT}\t{CommandEnum.CommandDescription[CommandEnum.HELP_CMD]}");
        }
    }
}