using System.Collections.Generic;

namespace sqlcicd.Configuration
{
    /// <summary>
    /// Sql file selector
    /// </summary>
    public interface ISqlSelector
    {
        /// <summary>
        /// Sort the files by orders
        /// </summary>
        /// <param name="orderConfiguration">sql order configuration</param>
        /// <param name="files">files that need to be ordered</param>
        void Sort(SqlOrderConfiguration orderConfiguration, ref IEnumerable<string> files);

        /// <summary>
        /// Exclude the files by ignore
        /// </summary>
        /// <param name="ignoreConfiguration">sql ignore configuration</param>
        /// <param name="files">files that need to be excluded</param>
        void Exclude(SqlIgnoreConfiguration ignoreConfiguration, ref IEnumerable<string> files);
    }
}