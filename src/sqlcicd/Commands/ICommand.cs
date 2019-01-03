using System.Threading.Tasks;
using sqlcicd.Commands.Entity;

namespace sqlcicd.Commands
{
    /// <summary>
    /// Command
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>If execute successful</returns>
        Task<ExecutionResult> Execute();
    }
}