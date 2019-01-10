using System;
using sqlcicd.Commands.Entity;

namespace sqlcicd.Help
{
    /// <summary>
    /// Display for global help
    /// </summary>
    public class GlobalCMDHelpDisplay : ICMDHelpDisplay
    {
        public void Display()
        {
            Console.WriteLine($"Usage: {typeof(Program).Assembly.GetName().Name} [options]");
            Console.WriteLine();
            Console.WriteLine("options:");
            Console.WriteLine(
                $"\t{CommandEnum.INTEGRATE_CMD}|{CommandEnum.INTEGRATE_CMD_SHORT}\t{CommandEnum.CommandDescription[CommandEnum.INTEGRATE_CMD]}");
            Console.WriteLine(
                $"\t{CommandEnum.DELIVERY_CMD}|{CommandEnum.DELIVERY_CMD_SHORT}\t{CommandEnum.CommandDescription[CommandEnum.DELIVERY_CMD]}");
            Console.WriteLine(
                $"\t{CommandEnum.HELP_CMD}|{CommandEnum.HELP_CMD_SHORT}\t{CommandEnum.CommandDescription[CommandEnum.HELP_CMD]}");
            Console.WriteLine(
                $"\t{CommandEnum.VERSION_CMD}|{CommandEnum.VERSION_CMD_SHORT}\t{CommandEnum.CommandDescription[CommandEnum.VERSION_CMD]}");
        }
    }
}