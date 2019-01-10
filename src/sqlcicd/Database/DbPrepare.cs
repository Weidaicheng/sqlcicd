using System.Threading.Tasks;
using System.Transactions;

namespace sqlcicd.Database
{
    /// <inheritdoc />
    public class DbPrepare : IDbPrepare
    {
        private readonly DbNegotiator _dbNegotiator;

        public DbPrepare(DbNegotiator dbNegotiator)
        {
            _dbNegotiator = dbNegotiator;
        }

        /// <inheritdoc />
        public async Task Prepare()
        {
            using (var trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // save all records
                var sqlVersions = await _dbNegotiator.GetAllSqlVersions();
            
                // check if table SqlVersion exists
                if (await _dbNegotiator.IsVersionTableExists())
                {
                    // drop table
                    await _dbNegotiator.DropVersionTable();
                }
            
                // create table
                await _dbNegotiator.CreateVersionTable();
            
                // restore all the rocords
                foreach (var sqlVersion in sqlVersions)
                {
                    await _dbNegotiator.InsertSqlVersion(sqlVersion);
                }
                
                trans.Complete();
            }
        }
    }
}