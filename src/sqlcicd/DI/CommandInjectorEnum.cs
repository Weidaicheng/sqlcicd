using System.Collections.Generic;
using sqlcicd.Exceptions;

namespace sqlcicd.DI
{
    /// <summary>
    /// Enum for <see cref="ICommandInjector" />
    /// </summary>
    public class CommandInjectorEnum
    {
        #region const
        public const string DEFAULT_CMD = "";
        public const string INTEGRATE_CMD = "--integrate";
        public const string INTEGRATE_CMD_SHORT = "-i";
        #endregion

        private static Dictionary<string, ICommandInjector> commands;

        static CommandInjectorEnum()
        {
            commands = new Dictionary<string, ICommandInjector>();

            // TODO: add commands
            // default command TODO: change to DocCommandInjector
            commands.Add(DEFAULT_CMD, new IntegrateCommandInjector());
            // integrate command
            commands.Add(INTEGRATE_CMD, new IntegrateCommandInjector());
            commands.Add(INTEGRATE_CMD_SHORT, new IntegrateCommandInjector());
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