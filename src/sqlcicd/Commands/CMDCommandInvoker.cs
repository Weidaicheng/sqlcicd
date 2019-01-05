using System;
using System.Threading.Tasks;
using sqlcicd.Commands.Entity;
using sqlcicd.Configuration;

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
        /// Print error
        /// </summary>
        /// <param name="error">error message</param>
        private void printError(string error)
        {
            Console.WriteLine("Error:");
            Console.WriteLine($"\t{error}");
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
                var resultStr = result.Success ? "success." : "fail.";
                Console.WriteLine($"{CommandEnum.CommandDescription[Singletons.GetCmd()]} {resultStr}");
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