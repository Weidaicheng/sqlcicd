using System;

namespace sqlcicd.Exceptions
{
    /// <summary>
    /// SqlVersion can not be roll backed exception
    /// </summary>
    public class SqlVersionCannotRollBackException : Exception
    {
        /// <summary>
        /// SqlVersion can not be roll backed exception
        /// </summary>
        /// <param name="message"></param>
        public SqlVersionCannotRollBackException(string message)
            : base(message)
        {
        }
    }
}