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
        private static Dictionary<string, ICommandInjector> commands;

        static CommandInjectorEnum()
        {
            commands = new Dictionary<string, ICommandInjector>();

            // integrate command
            commands.Add(CommandEnum.INTEGRATE_CMD, new IntegrateCommandInjector());
            commands.Add(CommandEnum.INTEGRATE_CMD_SHORT, new IntegrateCommandInjector());
            // delivery command
            commands.Add(CommandEnum.DELIVERY_CMD, new DeliveryCommandInjector());
            commands.Add(CommandEnum.DELIVERY_CMD_SHORT, new DeliveryCommandInjector());
            // help command
            commands.Add(CommandEnum.HELP_CMD, new HelpCommandInjector());
            commands.Add(CommandEnum.HELP_CMD_SHORT, new HelpCommandInjector());
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
                throw new UnSupportedCommandException($"Command {command} is not supported");
            }

            return commands[command];
        }
    }
}