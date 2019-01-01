using System.Collections.Generic;

namespace sqlcicd.Repository
{
    /// <summary>
    /// Repository operations, read changes, etc.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Get changed files(Added, Modified)
        /// </summary>
        /// <param name="latestVersion">Latest committed version</param>
        /// <param name="latestSqlVersion">Latest deliveried version</param>
        /// <returns>Changed files' path</returns>
        IEnumerable<string> GetChangedFiles(IRepositoryVersion latestVersion, IRepositoryVersion latestSqlVersion);
    }
}