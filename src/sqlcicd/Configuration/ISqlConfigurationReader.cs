using System.Data;
using System.Threading.Tasks;
using sqlcicd.Configuration.Entity;
using sqlcicd.Repository;
using sqlcicd.Repository.Entity;

namespace sqlcicd.Configuration
{
    /// <summary>
    /// Including read configuration operations
    /// </summary>
    public interface ISqlConfigurationReader
    {
        /// <summary>
        /// Get sql ignore configuration
        /// </summary>
        /// <returns><see cref="SqlIgnoreConfiguration" /></returns>
        Task<SqlIgnoreConfiguration> GetSqlIgnoreConfiguration();

        /// <summary>
        /// Get sql order configuration
        /// </summary>
        /// <returns><see cref="SqlOrderConfiguration" /></returns>
        Task<SqlOrderConfiguration> GetSqlOrderConfiguration();

        /// <summary>
        /// Get database type configuration
        /// </summary>
        /// <returns><see cref="DbType" /></returns>
        Task<DbType> GetDbTypeConfiguration();

        /// <summary>
        /// Get repository type configuration
        /// </summary>
        /// <returns><see cref="RepositoryType" /></returns>
        Task<RepositoryType> GetRepositoryTypeConfiguration();
    }
}