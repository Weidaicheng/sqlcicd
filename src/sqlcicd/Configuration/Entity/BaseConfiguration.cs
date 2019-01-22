using sqlcicd.Database.Entity;
using sqlcicd.Repository.Entity;

namespace sqlcicd.Configuration.Entity
{
    /// <summary>
    /// Base configuration
    /// </summary>
    public class BaseConfiguration
    {
        /// <summary>
        /// <see cref="RepositoryType" />
        /// </summary>
        public RepositoryType RepositoryType { get; set; }

        /// <summary>DbType
        /// <see cref="RepositoryType" />
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// Database server
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Database
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Database login id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Database login password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Backup path, optional
        /// </summary>
        public string BackupPath { get; set; }
    }
}