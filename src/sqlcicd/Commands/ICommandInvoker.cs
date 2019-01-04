using System.Threading.Tasks;
using sqlcicd.Commands.Entity;

namespace sqlcicd.Commands
{
    /// <summary>
    /// Command invoker, handle all errors
    /// </summary>
    public interface ICommandInvoker
    {
        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns><see cref="ExecutionResult" /></returns>
        Task<ExecutionResult> Invoke();
    }
}