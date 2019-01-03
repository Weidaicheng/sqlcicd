using System;

namespace sqlcicd.Exceptions
{
    /// <summary>
    /// DbType configuration is not configured exception
    /// </summary>
    public class DbTypeNotConfiguredException : Exception
    {
        /// <summary>
        /// DbType configuration is not configured exception
        /// </summary>
        public DbTypeNotConfiguredException(string message)
            : base(message)
        { }
    }
}