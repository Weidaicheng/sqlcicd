using System.Collections.Generic;

namespace sqlcicd.Configuration.Entity
{
    /// <summary>
    /// Sql ignore configuration
    /// </summary>
    public class SqlIgnoreConfiguration
    {
        /// <summary>
        /// Ignored files
        /// </summary>
        public IEnumerable<string> IgnoredFile { get; set; }
    }
}