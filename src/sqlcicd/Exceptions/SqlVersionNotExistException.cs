using System;

namespace sqlcicd.Exceptions
{
    /// <summary>
    /// SqlVersion doesn't exist exception
    /// </summary>
    public class SqlVersionNotExistException : Exception
    {
        /// <summary>
        /// SqlVersion doesn't exist exception
        /// </summary>
        /// <param name="message"></param>
        public SqlVersionNotExistException(string message)
            : base(message)
        {
        }
    }
}