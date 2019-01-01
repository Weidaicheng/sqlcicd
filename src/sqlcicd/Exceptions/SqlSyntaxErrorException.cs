using System;

namespace sqlcicd.Exceptions
{
    /// <summary>
    /// Sql syntax has error exception
    /// </summary>
    public class SqlSyntaxErrorException : Exception
    {
        /// <summary>
        /// Sql syntax has error exception
        /// </summary>
        public SqlSyntaxErrorException(string message)
            : base(message)
        { }
    }
}