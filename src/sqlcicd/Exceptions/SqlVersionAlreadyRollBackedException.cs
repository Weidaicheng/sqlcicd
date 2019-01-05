using System;

namespace sqlcicd.Exceptions
{
    /// <summary>
    /// SqlVersion has already roll backed exception
    /// </summary>
    public class SqlVersionAlreadyRollBackedException : Exception
    {
        /// <summary>
        /// SqlVersion has already roll backed exception
        /// </summary>
        /// <param name="message"></param>
        public SqlVersionAlreadyRollBackedException(string message)
            : base(message)
        {
        }
    }
}