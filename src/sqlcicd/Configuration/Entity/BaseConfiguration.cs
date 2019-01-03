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

        /// <summary>DbType
        /// Database connection string
        /// </summary>
        public string ConnectionString { get; set; }
    }
}