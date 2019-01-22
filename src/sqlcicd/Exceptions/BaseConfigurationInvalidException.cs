using System;

namespace sqlcicd.Exceptions
{
    /// <summary>
    /// Database connection string is not configured exception
    /// </summary>
    public class BaseConfigurationInvalidException : Exception
    {
        /// <summary>
        /// Database connection string is not configured exception
        /// </summary>
        public BaseConfigurationInvalidException(string message)
            : base(message)
        { }
    }
}