using System;

namespace sqlcicd.Utility
{
    /// <summary>
    /// Extensions for string array
    /// </summary>
    public static class StringArrayExtensions
    {
        /// <summary>
        /// Get value by index, returns empty if out of range
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string Get(this string[] array, int index)
        {
            try
            {
                return array[index];
            }
            catch (IndexOutOfRangeException)
            {
                return string.Empty;
            }
        }
    }
}