using System.Threading.Tasks;

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
        /// <param name="args">Arguments</param>
        /// <returns>If execute successful</returns>
        Task<ExecutionResult> Execute(string[] args);
    }
}