using System;
using System.Collections.Generic;

namespace sqlcicd.Commands
{
    /// <summary>
    /// Command enum
    /// </summary>
    public class CommandEnum
    {
        /// <summary>
        /// Command string for <see cref="ICommand" />
        /// </summary>
        public readonly static Dictionary<string, Type> Commands;

        static CommandEnum()
        {
            // add commands
        }
    }
}