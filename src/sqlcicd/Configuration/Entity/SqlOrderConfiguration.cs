using System.Collections.Generic;

namespace sqlcicd.Configuration.Entity
{
    /// <summary>
    /// Sql order configuration
    /// </summary>
    public class SqlOrderConfiguration
    {
        /// <summary>
        /// File orders
        /// </summary>
        public IEnumerable<string> FileOrder { get; set; }
    }
}