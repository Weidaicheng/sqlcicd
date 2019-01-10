using System.Collections.Generic;
using sqlcicd.Commands.Entity;
using sqlcicd.Exceptions;

namespace sqlcicd.DI
{
    /// <summary>
    /// Enum for <see cref="ICommandInjector" />
    /// </summary>
    public class CommandInjectorEnum
    {
        private static readonly Dictionary<string, ICommandInjector> commands;

        static CommandInjectorEnum()
        {
            commands = new Dictionary<string, ICommandInjector>();
            
            // * global help
            // sqlcicd.exe --help --help
            // sqlcicd.exe --help -h
            // sqlcicd.exe -h --help
            // sqlcicd.exe -h -h
            commands.Add($"{CommandEnum.HELP_CMD},{CommandEnum.HELP_CMD}", new HelpCommandInjector());
            commands.Add($"{CommandEnum.HELP_CMD},{CommandEnum.HELP_CMD_SHORT}", new HelpCommandInjector());
            commands.Add($"{CommandEnum.HELP_CMD_SHORT},{CommandEnum.HELP_CMD}", new HelpCommandInjector());
            commands.Add($"{CommandEnum.HELP_CMD_SHORT},{CommandEnum.HELP_CMD_SHORT}", new HelpCommandInjector());
            // * Continuous Integrate help
            // sqlcicd.exe --integrate --help
            // sqlcicd.exe -i --help
            // sqlcicd.exe --integrate -h
            // sqlcicd.exe -i -h
            commands.Add($"{CommandEnum.INTEGRATE_CMD},{CommandEnum.HELP_CMD}", new HelpCommandInjector());
            commands.Add($"{CommandEnum.INTEGRATE_CMD_SHORT},{CommandEnum.HELP_CMD}", new HelpCommandInjector());
            commands.Add($"{CommandEnum.INTEGRATE_CMD},{CommandEnum.HELP_CMD_SHORT}", new HelpCommandInjector());
            commands.Add($"{CommandEnum.INTEGRATE_CMD_SHORT},{CommandEnum.HELP_CMD_SHORT}", new HelpCommandInjector());
            // * Continuous Delivery help
            // sqlcicd.exe --delivery --help
            // sqlcicd.exe -d --help
            // sqlcicd.exe --delivery -h
            // sqlcicd.exe -d -h
            commands.Add($"{CommandEnum.DELIVERY_CMD},{CommandEnum.HELP_CMD}", new HelpCommandInjector());
            commands.Add($"{CommandEnum.DELIVERY_CMD_SHORT},{CommandEnum.HELP_CMD}", new HelpCommandInjector());
            commands.Add($"{CommandEnum.DELIVERY_CMD},{CommandEnum.HELP_CMD_SHORT}", new HelpCommandInjector());
            commands.Add($"{CommandEnum.DELIVERY_CMD_SHORT},{CommandEnum.HELP_CMD_SHORT}", new HelpCommandInjector());
            // * Continuous Integrate
            // sqlcicd.exe --integrate path
            // sqlcicd.exe -i path
            commands.Add($"{CommandEnum.INTEGRATE_CMD},", new IntegrateCommandInjector());
            commands.Add($"{CommandEnum.INTEGRATE_CMD_SHORT},", new IntegrateCommandInjector());
            // * Continuous Delivery
            // sqlcicd.exe --delivery path
            // sqlcicd.exe -d path
            commands.Add($"{CommandEnum.DELIVERY_CMD},", new DeliveryCommandInjector());
            commands.Add($"{CommandEnum.DELIVERY_CMD_SHORT},", new DeliveryCommandInjector());
            // * Version
            // sqlcicd.exe --version
            // sqlcicd.exe -v
            commands.Add($"{CommandEnum.VERSION_CMD},{CommandEnum.HELP_CMD}", new HelpCommandInjector());
            commands.Add($"{CommandEnum.VERSION_CMD_SHORT},{CommandEnum.HELP_CMD}", new HelpCommandInjector());
        }

        /// <summary>
        /// Get an instance of <see cref="ICommandInjector" />
        /// </summary>
        /// <param name="command">command string</param>
        /// <returns><see cref="ICommandInjector" /></returns>
        public static ICommandInjector GetInjector(string command)
        {
            if (!commands.ContainsKey(command))
            {
                throw new UnSupportedCommandException($"Command is not supported");
            }

            return commands[command];
        }
    }
}