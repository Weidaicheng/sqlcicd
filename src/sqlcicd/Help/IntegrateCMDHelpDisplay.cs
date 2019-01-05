using System;
using sqlcicd.Commands.Entity;

namespace sqlcicd.Help
{
    /// <summary>
    /// Display for Continuous Integrate help
    /// </summary>
    public class IntegrateCMDHelpDisplay : ICMDHelpDisplay
    {
        public void Display()
        {
            Console.WriteLine(
                $"Usage: {typeof(Program).Assembly.GetName().Name} {CommandEnum.INTEGRATE_CMD} <path>");
            Console.WriteLine(
                $"Usage: {typeof(Program).Assembly.GetName().Name} {CommandEnum.INTEGRATE_CMD_SHORT} <path>");
            Console.WriteLine(
                $"Usage: {typeof(Program).Assembly.GetName().Name} {CommandEnum.INTEGRATE_CMD} [options]");
            Console.WriteLine(
                $"Usage: {typeof(Program).Assembly.GetName().Name} {CommandEnum.INTEGRATE_CMD_SHORT} [options]");
            Console.WriteLine();
            Console.WriteLine("options:");
            Console.WriteLine(
                $"\t{CommandEnum.HELP_CMD}|{CommandEnum.HELP_CMD_SHORT}\t{CommandEnum.CommandDescription[CommandEnum.HELP_CMD]}");
        }
    }
}