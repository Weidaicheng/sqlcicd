using System.Data;
using System.Data.SqlClient;
using sqlcicd.Database.Entity;
using Dapper;
using System.Threading.Tasks;
using sqlcicd.Configuration.Entity;

namespace sqlcicd.Database
{
    /// <inheritdoc />
    public class MssqlNegotiator : DbNegotiator
    {
        public MssqlNegotiator(BaseConfiguration baseConfiguration)
            : base(new SqlConnection($@"Server={baseConfiguration.Server};
                                                    Database={baseConfiguration.Database};
                                                    User Id={baseConfiguration.UserId};
                                                    Password={baseConfiguration.Password};"))
        {
        }

        /// <inheritdoc />
        public override async Task<bool> IsVersionTableExists()
        {
            var tableExists =
                await DbConnection.QueryFirstOrDefaultAsync<string>($"SELECT object_id('{nameof(SqlVersion)}')");
            return !string.IsNullOrEmpty(tableExists);
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