using System;
using System.Threading.Tasks;
using sqlcicd.Commands.Entity;

namespace sqlcicd.Commands
{
    /// <summary>
    /// Command invoker in command line
    /// </summary>
    public class CMDCommandInvoker : ICommandInvoker
    {
        private ICommand _command;

        public CMDCommandInvoker(ICommand command)
        {
            _command = command;
        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns><see cref="ExecutionResult" /></returns>
        public async Task<ExecutionResult> Invoke()
        {
            try
            {
                var result = await _command.Execute();
                // TODO: print errors
                
                return result;
            }
            catch(Exception ex)
            {
                return new ExecutionResult()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}