using System.Collections.Generic;

namespace sqlcicd.Commands
{
    /// <summary>
    /// Execution result
    /// </summary>
    public class ExecutionResult
    {
        /// <summary>
        /// Is successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Execution return result
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// Error messages
        /// </summary>
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}