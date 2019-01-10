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

            if (_command is ILogEvent log)
            {
                log.Log += logPrinter;
            }
        }

        /// <summary>
        /// Print error
        /// </summary>
        /// <param name="error">error message</param>
        private void printError(string error)
        {
            Console.WriteLine("Error:");
            Console.WriteLine($"\t{error}");
        }

        /// <summary>
        /// Print log
        /// </summary>
        /// <param name="log"></param>
        private void logPrinter(string log)
        {
            Console.WriteLine(log);
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

                if (!result.Success)
                {
                    printError(result.ErrorMessage);
                }

                return result;
            }
            catch (Exception ex)
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