namespace sqlcicd.Configuration
{
    /// <summary>
    /// Singletons
    /// </summary>
    public static class Singletons
    {
        /// <summary>
        /// Args
        /// </summary>
        public static string[] Args { get; set; }

        /// <summary>
        /// Check if path is provided
        /// </summary>
        /// <returns>If path is provided</returns>
        public static bool ArgsPathCheck()
        {
            return Args.Length >= 2;
        }
    }
}