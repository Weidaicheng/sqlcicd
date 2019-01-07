using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using sqlcicd.Commands.Entity;
using sqlcicd.Configuration;
using sqlcicd.Configuration.Entity;
using sqlcicd.Database;
using sqlcicd.Database.Entity;
using sqlcicd.Files;
using sqlcicd.Repository;
using sqlcicd.Repository.Entity;
using sqlcicd.Syntax;
using sqlcicd.Utility;

namespace sqlcicd.Commands
{
    /// <summary>
    /// Continuous Delivery command
    /// </summary>
    public class DeliveryCommand : ICommand, ILogEvent
    {
        private readonly IRepository _repository;
        private readonly DbNegotiator _dbNegotiator;
        private readonly ISqlSelector _sqlSelector;
        private readonly IGrammarChecker _grammarChecker;
        private readonly IFileReader _fileReader;
        private readonly SqlIgnoreConfiguration _sqlIgnoreConfiguration;
        private readonly SqlOrderConfiguration _sqlOrderConfiguration;
        private readonly BaseConfiguration _baseConfiguration;

        public DeliveryCommand(IRepository repository,
            DbNegotiator dbNegotiator,
            ISqlSelector sqlSelector,
            IGrammarChecker grammarChecker,
            IFileReader fileReader,
            SqlIgnoreConfiguration sqlIgnoreConfiguration,
            SqlOrderConfiguration sqlOrderConfiguration,
            BaseConfiguration baseConfiguration)
        {
            _repository = repository;
            _dbNegotiator = dbNegotiator;
            _sqlSelector = sqlSelector;
            _grammarChecker = grammarChecker;
            _fileReader = fileReader;
            _sqlIgnoreConfiguration = sqlIgnoreConfiguration;
            _sqlOrderConfiguration = sqlOrderConfiguration;
            _baseConfiguration = baseConfiguration;
        }

        public async Task<ExecutionResult> Execute()
        {
            log($"SQL Continuous Delivery start");
            
            var path = Singletons.Command.Path;
            log($"Path: {path}");

            // 1. get newest version from repository
            log($"get newest version from repository");
            var newest = _repository.GetNewestCommit(path);

            // 2. get latest version from db
            // check if table SqlVersion exists
            if (!await _dbNegotiator.IsVersionTableExists())
            {
                log($"table {nameof(SqlVersion)} doesn't exist");
                // create table
                log($"create table {nameof(SqlVersion)}");
                await _dbNegotiator.CreateVersionTable();
            }
            log($"get last version from database");
            var latestSqlVersion = await _dbNegotiator.GetLatestSqlVersion();
            var latest = RepositoryVersion.GetRepositoryVersion(latestSqlVersion);

            // 3. get changed(added, modified) files from repository
            log($"get changed(added, modified) files");
            var changedFiles = _repository.GetChangedFiles(path, newest, latest);

            // 4. exclude ignored files
            log($"exclude ignored files");
            _sqlSelector.Exclude(_sqlIgnoreConfiguration, ref changedFiles);

            // 5. sort files
            log($"sort files");
            _sqlSelector.Sort(_sqlOrderConfiguration, ref changedFiles);

            // 6. delivery to database
            try
            {
                var scripts = await Task.WhenAll(changedFiles
                    .Where(file => file.ToLower().EndsWith(".sql"))
                    .Select(async file => await _fileReader.GetContentAsync($"{Singletons.Command.Path}/{file}"))
                    .ToList());

                if (!scripts.Any())
                {
                    goto endCommand;
                }

                using (var trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    // execute all scripts
                    var beginTime = TimeUtility.Now;
                    log($"delivery start at {beginTime}");
                    foreach (var script in scripts)
                    {
                        log($"delivery for {string.Concat(script.Take(20))}...");
                        await _dbNegotiator.Execute(script);
                    }

                    var endTime = TimeUtility.Now;
                    log($"delivery end at {endTime}");

                    // insert delivery record
                    log($"insert delivery record");
                    // 1.get the latest version
                    var preVersion = await _dbNegotiator.GetLatestSqlVersion();
                    // 2.update all other records.IsLatest = false
                    await _dbNegotiator.SetAllNonLatest();
                    // 3.insert sv
                    var sv = new SqlVersion(_baseConfiguration.RepositoryType, newest.Version,
                        (endTime - beginTime).TotalMilliseconds) {IsLatest = true, LastVersion = preVersion?.Id};
                    await _dbNegotiator.InsertSqlVersion(sv);

                    trans.Complete();
                }

                endCommand:
                log($"SQL Continuous Delivery finish");
                return new ExecutionResult()
                {
                    Success = true,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new ExecutionResult()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public event Action<string> Log;
        
        /// <summary>
        /// Log
        /// </summary>
        /// <param name="log"></param>
        private void log(string log)
        {
            Log.Invoke($"UTC {TimeUtility.Now.ToLongTimeString()} | {log}");
        }
    }
}