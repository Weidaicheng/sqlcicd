using System;
using sqlcicd.Commands.Entity;
using sqlcicd.Exceptions;

namespace sqlcicd.Configuration
{
    /// <summary>
    /// Singletons
    /// </summary>
    public static class Singletons
    {
        private static string[] args;

        /// <summary>
        /// Args
        /// </summary>
        public static string[] Args
        {
            get { return args; }
            set // TODO: find a way to replay the below stupid if-else
            {
                if (value.Length == 0)
                {
                    // 1. if args[0] is not provided, set the default command which is help
                    args = new[] {CommandEnum.HELP_CMD, CommandEnum.HELP_CMD};
                }
                else if (value[0].ToLower() == CommandEnum.HELP_CMD || value[0].ToLower() == CommandEnum.HELP_CMD_SHORT)
                {
                    // 2. if args[0] is help command
                    args = new[] {value[0], CommandEnum.HELP_CMD};
                }
                else if (value[0].ToLower() == CommandEnum.INTEGRATE_CMD ||
                         value[0].ToLower() == CommandEnum.INTEGRATE_CMD_SHORT ||
                         value[0].ToLower() == CommandEnum.DELIVERY_CMD ||
                         value[0].ToLower() == CommandEnum.DELIVERY_CMD_SHORT)
                {
                    // 3. if args[0] is integrate or delivery
                    if (value.Length == 1)
                    {
                        // 3.1 if args[1] is not provided, set the default sub command which is help
                        args = new[] {CommandEnum.HELP_CMD, value[0]};
                    }
                    else if (value[1].ToLower() == CommandEnum.HELP_CMD ||
                             value[1].ToLower() == CommandEnum.HELP_CMD_SHORT)
                    {
                        // 3.2 if args[1] is provided, but it's a help sub command
                        args = new[] {CommandEnum.HELP_CMD, value[0]};
                    }
                    else
                    {
                        // 3.3 if args[1] is provided, and it's a path
                        args = new[] {value[0], value[1]};
                    }
                }
                else
                {
                    // 4. if args[0] is not a supported command
                    throw new UnSupportedCommandException($"Command {value[0]} is not supported, please try again.");
                }
            }
        }

        /// <summary>
        /// Get path parameter
        /// </summary>
        /// <returns>path</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetPath()
        {
            return Args[1];
        }

        /// <summary>
        /// Get command parameter
        /// </summary>
        /// <returns>path</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetCmd()
        {
            return Args[0];
        }

        /// <summary>
        /// Get sub command parameter
        /// </summary>
        /// <returns>sub command</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetSubCmd()
        {
            return Args[1];
        }
    }
}