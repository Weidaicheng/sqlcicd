using System.Collections.Generic;
using System.Data;
using sqlcicd.Database.Entity;
using Dapper;
using System.Threading.Tasks;
using System.Transactions;
using sqlcicd.Exceptions;

namespace sqlcicd.Database
{
    public class MssqlNegotiator : IDbNegotiator
    {
        private readonly IDbConnection _dbConnection;

        public MssqlNegotiator(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Execute(string sqlScript)
        {
            await _dbConnection.ExecuteAsync(sqlScript);
        }

        public async Task<bool> IsVersionTableExists()
        {
            var tableExists =
                await _dbConnection.QueryFirstOrDefaultAsync<string>($"SELECT object_id('{nameof(SqlVersion)}')");
            return !string.IsNullOrEmpty(tableExists);
        }

        public async Task CreateVersionTable()
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

        public async Task<SqlVersion> GetLatestSqlVersion()
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

        public async Task SetAllNonLatest()
        {
            await _dbConnection.ExecuteAsync($@"UPDATE {nameof(SqlVersion)} 
                    SET {nameof(SqlVersion.IsLatest)} = 0;");
        }

        public async Task InsertSqlVersion(SqlVersion sv)
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

//        public async Task SetSqlVersionRollback(SqlVersion sv)
//        {
//            using (var trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
//            {
//                // 1. check if this record exists
//                var svInDb = await _dbConnection.QueryFirstOrDefaultAsync<SqlVersion>($@"SELECT 
//                {nameof(SqlVersion.Id)}, 
//                {nameof(SqlVersion.RepositoryType)},
//                {nameof(SqlVersion.Version)},
//                {nameof(SqlVersion.DeliveryTime)},
//                {nameof(SqlVersion.TransactionCost)},
//                {nameof(SqlVersion.LastVersion)},
//                {nameof(SqlVersion.IsLatest)},
//                {nameof(SqlVersion.IsRollBacked)} 
//                FROM {nameof(SqlVersion)} 
//                WHERE {nameof(SqlVersion.Id)} = @{nameof(SqlVersion.Id)} 
//                AND {nameof(SqlVersion.IsDeleted)} = 0;", sv);
//
//                if (svInDb == null)
//                {
//                    throw new SqlVersionNotExistException($"SqlVersion Id {sv.Id} not exists.");
//                }
//
//                // 2. check if it has already roll backed
//                if (svInDb.IsRollBacked)
//                {
//                    throw new SqlVersionAlreadyRollBackedException($"SqlVersion Id {sv.Id} has already roll backed.");
//                }
//
//                // 3. check if it can roll back
//                if (!svInDb.CanRollback)
//                {
//                    throw new SqlVersionCannotRollBackException(
//                        $"SqlVersion is the latest version, can not roll back.");
//                }
//
//                // 4. update all other records.IsLatest = false
//                await _dbConnection.ExecuteAsync($@"UPDATE {nameof(SqlVersion)} 
//                    SET {nameof(SqlVersion.IsLatest)} = 0;");
//                // 5. update this set IsLatest = true and IsRollBacked = true
//                await _dbConnection.ExecuteAsync($@"UPDATE {nameof(SqlVersion)} 
//                    SET {nameof(SqlVersion.IsLatest)} = 1, {nameof(SqlVersion.IsRollBacked)} = 1
//                    WHERE {nameof(SqlVersion.Id)} = @{nameof(SqlVersion.Id)};", sv);
//                // 6. soft delete all the records which after this
//                await _dbConnection.ExecuteAsync($@"UPDATE {nameof(SqlVersion)} 
//                    SET {nameof(SqlVersion.IsDeleted)} = 1 
//                    WHERE {nameof(SqlVersion.Id)} > @{nameof(SqlVersion.Id)}", sv);
//
//                trans.Complete();
//            }
//        }
    }
}