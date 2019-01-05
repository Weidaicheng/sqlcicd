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
        /// Check if <see cref="SqlVersion"/> table exists
        /// </summary>
        /// <returns></returns>
        Task<bool> IsVersionTableExists();

        /// <summary>
        /// Create <see cref="SqlVersion"/> table
        /// </summary>
        /// <returns></returns>
        Task CreateVersionTable();

        /// <summary>
        /// Get latest sql version record
        /// </summary>
        /// <returns><see cref="SqlVersions" /></returns>
        Task<SqlVersion> GetLatestSqlVersion();

        /// <summary>
        /// Set all records as non latest
        /// </summary>
        /// <returns></returns>
        Task SetAllNonLatest();

        /// <summary>
        /// Insert a sql version record
        /// </summary>
        /// <param name="sv"><see cref="SqlVersion" /></param>
        Task InsertSqlVersion(SqlVersion sv);

//        /// <summary>
//        /// Set a sql version record to roll backed
//        /// </summary>
//        /// <param name="sv"><see cref="SqlVersion" /></param>
//        Task SetSqlVersionRollback(SqlVersion sv);
    }
}