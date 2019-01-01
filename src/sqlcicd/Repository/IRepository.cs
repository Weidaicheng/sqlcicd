using System.Collections.Generic;

namespace sqlcicd.Repository
{
    /// <summary>
    /// Repository operations, read changes, etc.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Get newest commit version
        /// </summary>
        /// <param name="repoPath">repository path</param>
        /// <returns><see cref="IRepositoryVersion" /></returns>
        IRepositoryVersion GetNewestCommit(string repoPath);

        /// <summary>
        /// Get changed files(Added, Modified)
        /// </summary>
        /// <param name="repoPath">repository path</param>
        /// <param name="latestVersion">Latest committed version</param>
        /// <param name="latestSqlVersion">Latest deliveried version</param>
        /// <returns>Changed files' path</returns>
        IEnumerable<string> GetChangedFiles(string repoPath, IRepositoryVersion latestVersion, IRepositoryVersion latestSqlVersion);
    }
}