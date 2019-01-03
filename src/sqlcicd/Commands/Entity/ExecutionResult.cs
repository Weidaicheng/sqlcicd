using System.Collections.Generic;

namespace sqlcicd.Commands.Entity
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
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}