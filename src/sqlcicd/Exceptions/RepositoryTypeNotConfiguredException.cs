using System;

namespace sqlcicd.Exceptions
{
    /// <summary>
    /// RepositoryType configuration is not configured exception
    /// </summary>
    public class RepositoryTypeNotConfiguredException : Exception
    {
        /// <summary>
        /// RepositoryType configuration is not configured exception
        /// </summary>
        public RepositoryTypeNotConfiguredException(string message)
            : base(message)
        { }
    }
}