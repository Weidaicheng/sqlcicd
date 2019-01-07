using sqlcicd.Commands.Entity;
using sqlcicd.Utility;

namespace sqlcicd.Commands
{
    /// <summary>
    /// Factory for <see cref="Command"/>
    /// </summary>
    public class CommandFactory
    {
        #region consts
        public const int INDEX_0 = 0;
        public const int INDEX_1 = 1;
        #endregion
        
        /// <summary>
        /// Generate <see cref="Command"/>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Command GenerateCommand(string[] args)
        {
            var command = new Command()
            {
                MainCommand = string.IsNullOrEmpty(args.Get(INDEX_0)) ? CommandEnum.HELP_CMD : args.Get(INDEX_0), // set empty command to help
                SubCommand = string.IsNullOrEmpty(args.Get(INDEX_1)) ? CommandEnum.HELP_CMD : args.Get(INDEX_1), // set empty command to help
                Path = string.Empty
            };

            if (command.SubCommand == CommandEnum.HELP_CMD || command.SubCommand == CommandEnum.HELP_CMD_SHORT)
                return command;
            command.Path = command.SubCommand; // set path if sub command isn't help command
            command.SubCommand = string.Empty; // set sub command as empty if sub command isn't help command

            return command;
        }
    }
}