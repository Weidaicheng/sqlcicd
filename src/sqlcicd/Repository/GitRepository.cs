using System.Collections.Generic;
using sqlcicd.Repository.Entity;

namespace sqlcicd.Repository
{
    public class GitRepository : IRepository
    {
        public IEnumerable<string> GetChangedFiles(string repoPath, RepositoryVersion latestVersion, RepositoryVersion latestSqlVersion)
        {
            throw new System.NotImplementedException();
        }

        public RepositoryVersion GetNewestCommit(string repoPath)
        {
            throw new System.NotImplementedException();
        }
    }
}