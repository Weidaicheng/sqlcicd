using System;

namespace sqlcicd.Exceptions
{
    /// <summary>
    /// File extension is not supported exception
    /// </summary>
    public class FileExtensionNotSupportedException : Exception
    {
        /// <summary>
        /// File extension is not supported exception
        /// </summary>
        public FileExtensionNotSupportedException(string message)
            : base(message)
        { }
    }
}