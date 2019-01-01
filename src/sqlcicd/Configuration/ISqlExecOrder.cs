using System.Collections.Generic;

namespace sqlcicd.Configuration
{
    /// <summary>
    /// Sql file execution order
    /// </summary>
    public interface ISqlExecOrder
    {
        /// <summary>
        /// Order the files
        /// </summary>
        /// <param name="files">files that need to be ordered</param>
        void Order(ref IEnumerable<string> files);
    }
}