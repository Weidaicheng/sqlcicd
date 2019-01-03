using System;

namespace sqlcicd.Exceptions
{
    /// <summary>
    /// Command is not supported exception
    /// </summary>
    public class UnSupportedCommandException : Exception
    {
        /// <summary>
        /// Command is not supported exception
        /// </summary>
        public UnSupportedCommandException(string message)
            : base(message)
        { }
    }
}