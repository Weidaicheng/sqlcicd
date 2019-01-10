using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using sqlcicd.Database.Entity;

namespace sqlcicd.Database
{
    /// <summary>
    /// Talk to db
    /// </summary>
    public abstract class DbNegotiator
    {
        /// <summary>
        /// DbConnection
        /// </summary>
        protected readonly IDbConnection DbConnection;

        protected DbNegotiator(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }
        
        /// <summary>
        /// Execute sql script
        /// </summary>
        /// <param name="sqlScript"></param>
        /// <returns></returns>
        public virtual async Task Execute(string sqlScript)
        {
            await DbConnection.ExecuteAsync(sqlScript);
        }

        /// <summary>
        /// Check if <see cref="SqlVersion"/> table exists
        /// </summary>
        /// <returns></returns>
        public abstract Task<bool> IsVersionTableExists();

        /// <summary>
        /// Create <see cref="SqlVersion"/> table
        /// </summary>
        /// <returns></returns>
        public virtual async Task CreateVersionTable()
        {
            await DbConnection.ExecuteAsync($@"CREATE TABLE {nameof(SqlVersion)}
                (
                  {nameof(SqlVersion.Id)}              INT IDENTITY (1, 1) PRIMARY KEY,
                  {nameof(SqlVersion.RepositoryType)}  INT         NOT NULL,
                  {nameof(SqlVersion.Version)}         VARCHAR(50) NOT NULL,
                  {nameof(SqlVersion.DeliveryTime)}    DATETIME    NOT NULL,
                  {nameof(SqlVersion.TransactionCost)} FLOAT       NOT NULL,
                  {nameof(SqlVersion.LastVersion)}     INT,
                  {nameof(SqlVersion.IsLatest)}        BIT         NOT NULL,
                  {nameof(SqlVersion.IsRollBacked)}    BIT         NOT NULL,
                  {nameof(SqlVersion.IsDeleted)}       BIT         NOT NULL,
                  
                  FOREIGN KEY (Id) REFERENCES SqlVersion (Id)
                );");
        }

        /// <summary>
        /// Drop <see cref="SqlVersion"/> table
        /// </summary>
        /// <returns></returns>
        public virtual async Task DropVersionTable()
        {
            await DbConnection.ExecuteAsync($@"DROP TABLE {nameof(SqlVersion)}");
        }

        /// <summary>
        /// Get latest <see cref="SqlVersion"/>
        /// </summary>
        /// <returns></returns>
        public virtual async Task<SqlVersion> GetLatestSqlVersion()
        {
            return (await GetAllSqlVersions()).FirstOrDefault(v => !v.IsDeleted && v.IsLatest);
        }

        /// <summary>
        /// Get all records of <see cref="SqlVersion"/>
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<SqlVersion>> GetAllSqlVersions()
        {
            return await DbConnection.QueryAsync<SqlVersion>($@"SELECT 
                {nameof(SqlVersion.Id)}, 
                {nameof(SqlVersion.RepositoryType)},
                {nameof(SqlVersion.Version)},
                {nameof(SqlVersion.DeliveryTime)},
                {nameof(SqlVersion.TransactionCost)},
                {nameof(SqlVersion.LastVersion)},
                {nameof(SqlVersion.IsLatest)},
                {nameof(SqlVersion.IsRollBacked)} 
                FROM {nameof(SqlVersion)}");
        }

        /// <summary>
        /// Set all <see cref="SqlVersion"/> as not latest
        /// </summary>
        /// <returns></returns>
        public virtual async Task SetAllNonLatest()
        {
            await DbConnection.ExecuteAsync($@"UPDATE {nameof(SqlVersion)} 
                    SET {nameof(SqlVersion.IsLatest)} = 0;");
        }

        /// <summary>
        /// Insert a record of <see cref="SqlVersion"/>
        /// </summary>
        /// <param name="sv"></param>
        /// <returns></returns>
        public virtual async Task InsertSqlVersion(SqlVersion sv)
        {
            await DbConnection.ExecuteAsync($@"INSERT INTO {nameof(SqlVersion)} (
                {nameof(SqlVersion.RepositoryType)},
                {nameof(SqlVersion.Version)},
                {nameof(SqlVersion.DeliveryTime)},
                {nameof(SqlVersion.TransactionCost)},
                {nameof(SqlVersion.LastVersion)},
                {nameof(SqlVersion.IsLatest)},
                {nameof(SqlVersion.IsRollBacked)},
                {nameof(SqlVersion.IsDeleted)})
                VALUES (
                @{nameof(SqlVersion.RepositoryType)},
                @{nameof(SqlVersion.Version)},
                @{nameof(SqlVersion.DeliveryTime)},
                @{nameof(SqlVersion.TransactionCost)},
                @{nameof(SqlVersion.LastVersion)},
                @{nameof(SqlVersion.IsLatest)},
                @{nameof(SqlVersion.IsRollBacked)},
                @{nameof(SqlVersion.IsDeleted)}
                )", sv);
        }
    }
}