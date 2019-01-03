using System;
using System.Threading.Tasks;
using sqlcicd.Commands.Entity;

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
        /// <returns><see cref="ExecutionResult" /></returns>
        public async Task<ExecutionResult> Invoke()
        {
            return await _command.Execute();
        }
    }
}