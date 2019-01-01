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
        Task Execute(string[] args);
    }
}