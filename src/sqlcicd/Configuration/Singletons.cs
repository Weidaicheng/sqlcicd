using System;
using sqlcicd.Commands.Entity;

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
        public static string[] Args { private get; set; }

        /// <summary>
        /// Default command
        /// </summary>
        public static string DEFAULT_CMD { get; } = CommandEnum.HELP_CMD;

        /// <summary>
        /// Check if path is provided
        /// </summary>
        /// <returns>If path is provided</returns>
        private static bool argsPathCheck()
        {
            return Args?.Length >= 2;
        }
        
        /// <summary>
        /// Check if command is provided
        /// </summary>
        /// <returns>If command is provided</returns>
        private static bool argsCmdCheck()
        {
            return Args?.Length >= 1;
        }
        
        /// <summary>
        /// Check if sub command is provided
        /// </summary>
        /// <returns>If sub command is provided</returns>
        private static bool subCommandCheck()
        {
            return Args?.Length >= 2;
        }
        
        /// <summary>
        /// Get path parameter
        /// </summary>
        /// <returns>path</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetPath()
        {
            if (!argsPathCheck())
            {
                throw new ArgumentNullException("Path is not provided.");
            }
            
            return Args[1];
        }
        
        /// <summary>
        /// Get command parameter
        /// </summary>
        /// <returns>path</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetCmd()
        {
            return !argsCmdCheck() ? DEFAULT_CMD : Args[0];
        }

        /// <summary>
        /// Get sub command parameter
        /// </summary>
        /// <returns>sub command</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetSubCmd()
        {
            if (!subCommandCheck())
            {
                throw new ArgumentNullException("Sub command is not provided.");
            }
            
            return Args[1];
        }
    }
}