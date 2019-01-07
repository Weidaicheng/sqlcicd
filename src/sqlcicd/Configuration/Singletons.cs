using System;
using System.Linq;
using sqlcicd.Commands.Entity;
using sqlcicd.Exceptions;

namespace sqlcicd.Configuration
{
    /// <summary>
    /// Singletons
    /// </summary>
    public static class Singletons
    {
        /// <summary>
        /// Command
        /// </summary>
        public static Command Command { get; set; }

        #region Original Command

        private static string originalCommand;

        /// <summary>
        /// Set original command
        /// </summary>
        /// <param name="args"></param>
        public static void SetOriginalCommand(string[] args)
        {
            originalCommand = args.Length == 0 ? string.Empty : args.Aggregate((prev, next) => $"{prev} {next}");
        }

        /// <summary>
        /// Original command
        /// </summary>
        public static string OriginalCommand => originalCommand;

        #endregion
    }
}