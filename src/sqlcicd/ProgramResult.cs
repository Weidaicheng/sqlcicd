using sqlcicd.Commands.Entity;

namespace sqlcicd
{
    /// <summary>
    /// Program execution result
    /// </summary>
    public class ProgramResult
    {
        public const int Success = 0;

        public const int Fail = -1;

        /// <summary>
        /// Get ProgramResult from <see cref="ExecutionResult"/>
        /// </summary>
        /// <param name="result"></param>
        /// <returns>Result</returns>
        public static int GetResult(ExecutionResult result)
        {
            return result.Success ? Success : Fail;
        }
    }
}