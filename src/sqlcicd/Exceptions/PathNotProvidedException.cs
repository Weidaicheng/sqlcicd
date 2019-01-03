using System;

namespace sqlcicd.Exceptions
{
    /// <summary>
    /// Path argument is not provided exception
    /// </summary>
    public class PathNotProvidedException : Exception
    {
        /// <summary>
        /// Path argument is not provided exception
        /// </summary>
        public PathNotProvidedException(string message)
            : base(message)
        { }
    }
}