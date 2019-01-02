using System.Threading.Tasks;

namespace sqlcicd.Commands
{
    /// <summary>
    /// Command invoker
    /// </summary>
    public class CommandInvoker
    {
        private ICommand _command;

        public CommandInvoker(ICommand command)
        {
            _command = command;
        }

        /// <summary>
        /// Command
        /// </summary>
        public ICommand Command
        {
            set
            {
                _command = value;
            }
        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="args">args</param>
        /// <returns><see cref="ExecutionResult" /></returns>
        public async Task<ExecutionResult> Invoke(string[] args)
        {
            return await _command.Execute(args);
        }
    }
}