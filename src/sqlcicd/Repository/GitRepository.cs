using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;
using sqlcicd.Repository.Entity;
using GitRepo = LibGit2Sharp.Repository;

namespace sqlcicd.Repository
{
    public class GitRepository : IRepository
    {
        public IEnumerable<string> GetChangedFiles(string repoPath, RepositoryVersion latestVersion, RepositoryVersion latestSqlVersion)
        {
            using(var repo = new GitRepo(repoPath))
            {
                var newestCommit = repo.Commits.FirstOrDefault(c => c.Sha == latestVersion.Version);
                var lastDeliveredCommit = repo.Commits.FirstOrDefault(c => c.Sha == latestSqlVersion.Version);

                var changes = repo.Diff.Compare<TreeChanges>(lastDeliveredCommit.Tree, newestCommit.Tree);
                var changedFiles = new List<string>();
                changedFiles.AddRange(changes.Added.Select(c => c.Path).ToList());
                changedFiles.AddRange(changes.Modified.Select(c => c.Path).ToList());

                return changedFiles;
            }
        }

        public RepositoryVersion GetNewestCommit(string repoPath)
        {
            using(var repo = new GitRepo(repoPath))
            {
                var newestCommit = repo.Commits.First();

                return new RepositoryVersion(newestCommit.Sha);
            }
        }
    }
}