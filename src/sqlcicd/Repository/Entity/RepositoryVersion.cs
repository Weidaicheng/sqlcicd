using sqlcicd.Database;

namespace sqlcicd.Repository.Entity
{
    /// <summary>
    /// Repository version
    /// </summary>
    public class RepositoryVersion
    {
        private RepositoryVersion(string version)
        {
            Version = version;
        }

        /// <summary>
        /// Version id
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Get a <see cref="RepositoryVersion" /> instance from <see cref="SqlVersion" />
        /// </summary>
        public static RepositoryVersion GetRepositoryVersion(SqlVersion sv)
        {
            return new RepositoryVersion(sv.Version);
        }
    }
}