using System.Collections.Generic;

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
        void Execute(string sqlScript);

        #region SqlVersion operations
        /// <summary>
        /// Get all sql version records
        /// </summary>
        /// <returns><see cref="SqlVersion" /> collection</returns>
        IEnumerable<SqlVersion> GetSqlVersions();

        /// <summary>
        /// Get latest sql version record
        /// </summary>
        /// <returns><see cref="SqlVersions" /></returns>
        SqlVersion GetLatestSqlVersion();

        /// <summary>
        /// Insert a sql version record
        /// </summary>
        /// <param name="sv"><see cref="SqlVersion" /></param>
        void InsertSqlVersion(SqlVersion sv);

        /// <summary>
        /// Set a sql version record to rollbacked
        /// </summary>
        /// <param name="sv"><see cref="SqlVersion" /></param>
        void SetSqlVersionRollback(SqlVersion sv);
        #endregion
    }
}