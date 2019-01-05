using System;

namespace sqlcicd.Utility
{
    /// <summary>
    /// Customized DateTime
    /// </summary>
    public class TimeUtility
    {
        /// <summary>
        /// Now
        /// </summary>
        public static DateTime Now => DateTime.UtcNow;
    }
}