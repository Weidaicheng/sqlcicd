using System.Collections.Generic;

namespace sqlcicd.Configuration
{
    /// <summary>
    /// Sql order configuration
    /// </summary>
    public class SqlOrderConfiguration
    {
        /// <summary>
        /// Orders
        /// </summary>
        public IEnumerable<string> FileOrder { get; set; }
    }
}