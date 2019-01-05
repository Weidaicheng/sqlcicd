using System.Collections.Generic;
using System.Threading.Tasks;
using sqlcicd.Database.Entity;

namespace sqlcicd.Database
{
    /// <summary>
    /// Talk to db
    /// </summary>
    public interface IDbNegotiator
    {
        /// <summary>
        /// Execute sql script
        /// </summary>
        /// <param name="sqlScript">sql script</param>
        Task Execute(string sqlScript);

        /// <summary>
        /// Execute batch script
        /// </summary>
        /// <param name="sqlScripts">sql scripts</param>
        /// <returns></returns>
        Task ExecuteBunch(IEnumerable<string> sqlScripts);

        #region SqlVersion operations
        /// <summary>
        /// Get all sql version records
        /// </summary>
        /// <returns><see cref="SqlVersion" /> collection</returns>
        Task<IEnumerable<SqlVersion>> GetSqlVersions();

        /// <summary>
        /// Get latest sql version record
        /// </summary>
        /// <returns><see cref="SqlVersions" /></returns>
        Task<SqlVersion> GetLatestSqlVersion();

        /// <summary>
        /// Insert a sql version record
        /// </summary>
        /// <param name="sv"><see cref="SqlVersion" /></param>
        Task InsertSqlVersion(SqlVersion sv);

        /// <summary>
        /// Set a sql version record to roll backed
        /// </summary>
        /// <param name="sv"><see cref="SqlVersion" /></param>
        Task SetSqlVersionRollback(SqlVersion sv);
        #endregion
    }
}