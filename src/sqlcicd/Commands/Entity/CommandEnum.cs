using System.Collections.Generic;

namespace sqlcicd.Commands.Entity
{
    /// <summary>
    /// Command enums
    /// </summary>
    public class CommandEnum
    {
        // integrate
        public const string INTEGRATE_CMD = "--integrate";
        public const string INTEGRATE_CMD_SHORT = "-i";
        
        // delivery
        public const string DELIVERY_CMD = "--delivery";
        public const string DELIVERY_CMD_SHORT = "-d";
        
        // help
        public const string HELP_CMD = "--help";
        public const string HELP_CMD_SHORT = "-h";
        
        // version
        public const string VERSION_CMD = "--version";
        public const string VERSION_CMD_SHORT = "-v";

        /// <summary>
        /// Command descriptions
        /// </summary>
        public static Dictionary<string, string> CommandDescription { get; } = new Dictionary<string, string>()
        {
            {INTEGRATE_CMD, "Continuous Integrate"},
            {INTEGRATE_CMD_SHORT, "Continuous Integrate"},
            {DELIVERY_CMD, "Continuous Delivery"},
            {DELIVERY_CMD_SHORT, "Continuous Delivery"},
            {HELP_CMD, "Display help"},
            {HELP_CMD_SHORT, "Display help"},
            {VERSION_CMD, "Display version"},
            {VERSION_CMD_SHORT, "Display Version"}
        };
    }
}