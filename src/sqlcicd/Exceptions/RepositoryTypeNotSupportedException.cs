using System;

namespace sqlcicd.Exceptions
{
    /// <summary>
    /// Repository type is not supported exception
    /// </summary>
    public class RepositoryTypeNotSupportedException : Exception
    {
        /// <summary>
        /// Repository type is not supported exception
        /// </summary>
        public RepositoryTypeNotSupportedException(string message)
            : base(message)
        { }
    }
}