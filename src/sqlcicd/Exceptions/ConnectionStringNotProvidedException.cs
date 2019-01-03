using System;

namespace sqlcicd.Exceptions
{
    /// <summary>
    /// Database connection string is not configured exception
    /// </summary>
    public class ConnectionStringNotProvidedException : Exception
    {
        /// <summary>
        /// Database connection string is not configured exception
        /// </summary>
        public ConnectionStringNotProvidedException(string message)
            : base(message)
        { }
    }
}