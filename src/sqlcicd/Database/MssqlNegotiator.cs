using System.Collections.Generic;
using System.Data;
using sqlcicd.Database.Entity;
using Dapper;
using System.Threading.Tasks;

namespace sqlcicd.Database
{
    public class MssqlNegotiator : IDbNegotiator // TODO: implicate
    {
        private readonly IDbConnection _dbConnection;

        public MssqlNegotiator(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Execute(string sqlScript)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SqlVersion> GetLatestSqlVersion()
        {
            return await _dbConnection.QueryFirstAsync<SqlVersion>($@"SELECT 
                {nameof(SqlVersion.Id)}, 
                {nameof(SqlVersion.RepositoryType)},
                {nameof(SqlVersion.Version)},
                {nameof(SqlVersion.DeliveryTime)},
                {nameof(SqlVersion.TransactionCost)},
                {nameof(SqlVersion.LastVersion)},
                {nameof(SqlVersion.IsLatest)},
                {nameof(SqlVersion.IsRollbacked)} 
                FROM {nameof(SqlVersion)} 
                WHERE {nameof(SqlVersion.IsLatest)} = TRUE");
        }

        public async Task<IEnumerable<SqlVersion>> GetSqlVersions()
        {
            throw new System.NotImplementedException();
        }

        public async Task InsertSqlVersion(SqlVersion sv)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetSqlVersionRollback(SqlVersion sv)
        {
            throw new System.NotImplementedException();
        }
    }
}