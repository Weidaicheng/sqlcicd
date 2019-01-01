using System;

namespace sqlcicd.Utility
{
    /// <summary>
    /// Customized DateTime
    /// </summary>
    public class TimeUtility
    {
        private static DateTime now;

        static TimeUtility()
        {
            now = DateTime.UtcNow;
        }

        /// <summary>
        /// Now
        /// </summary>
        public static DateTime Now 
        { 
            get
            {
                return now;
            }
            #if DEBUG
            set
            {
                now = value;
            }
            #endif
        }
    }
}