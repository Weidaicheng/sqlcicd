using System.Threading.Tasks;
using sqlcicd.Commands.Entity;
using sqlcicd.Help;

namespace sqlcicd.Commands
{
    /// <summary>
    /// Help command
    /// </summary>
    public class CMDHelpCommand : ICommand
    {
        private readonly ICMDHelpDisplay _icmdHelpDisplay;

        public CMDHelpCommand(ICMDHelpDisplay icmdHelpDisplay)
        {
            _icmdHelpDisplay = icmdHelpDisplay;
        }
        
        public async Task<ExecutionResult> Execute()
        {
            _icmdHelpDisplay.Display();

            return await Task.FromResult(new ExecutionResult()
            {
                Success = true,
                ErrorMessage = string.Empty
            });
        }
    }
}