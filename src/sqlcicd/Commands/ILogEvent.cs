using System;

namespace sqlcicd.Commands
{
    /// <summary>
    /// Log event
    /// </summary>
    public interface ILogEvent
    {
        /// <summary>
        /// Log event
        /// </summary>
        event Action<string> Log;
    }
}