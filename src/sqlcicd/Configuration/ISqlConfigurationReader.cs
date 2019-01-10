using System.Threading.Tasks;
using sqlcicd.Configuration.Entity;

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
        /// Get base configuration
        /// </summary>
        /// <returns><see cref="BaseConfiguration" /></returns>
        Task<BaseConfiguration> GetBaseConfiguration();
    }
}