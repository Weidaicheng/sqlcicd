using System.Data;
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
        private readonly IDbConnection _dbConnection;

        protected DbNegotiator(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        public virtual async Task Execute(string sqlScript)
        {
            await _dbConnection.ExecuteAsync(sqlScript);
        }

        public abstract Task<bool> IsVersionTableExists();

        public virtual async Task CreateVersionTable()
        {
            await _dbConnection.ExecuteAsync($@"CREATE TABLE {nameof(SqlVersion)}
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

        public virtual async Task<SqlVersion> GetLatestSqlVersion()
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<SqlVersion>($@"SELECT 
                {nameof(SqlVersion.Id)}, 
                {nameof(SqlVersion.RepositoryType)},
                {nameof(SqlVersion.Version)},
                {nameof(SqlVersion.DeliveryTime)},
                {nameof(SqlVersion.TransactionCost)},
                {nameof(SqlVersion.LastVersion)},
                {nameof(SqlVersion.IsLatest)},
                {nameof(SqlVersion.IsRollBacked)} 
                FROM {nameof(SqlVersion)} 
                WHERE {nameof(SqlVersion.IsLatest)} = 1 
                AND {nameof(SqlVersion.IsDeleted)} = 0;");
        }

        public virtual async Task SetAllNonLatest()
        {
            await _dbConnection.ExecuteAsync($@"UPDATE {nameof(SqlVersion)} 
                    SET {nameof(SqlVersion.IsLatest)} = 0;");
        }

        public virtual async Task InsertSqlVersion(SqlVersion sv)
        {
            await _dbConnection.ExecuteAsync($@"INSERT INTO {nameof(SqlVersion)} (
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