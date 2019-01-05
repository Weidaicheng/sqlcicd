using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class DeliveryCommand : ICommand
    {
        private readonly IRepository _repository;
        private readonly IDbNegotiator _dbNegotiator;
        private readonly ISqlSelector _sqlSelector;
        private readonly IGrammarChecker _grammarChecker;
        private readonly IFileReader _fileReader;
        private readonly SqlIgnoreConfiguration _sqlIgnoreConfiguration;
        private readonly SqlOrderConfiguration _sqlOrderConfiguration;
        private readonly BaseConfiguration _baseConfiguration;

        public DeliveryCommand(IRepository repository,
            IDbNegotiator dbNegotiator,
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
            var path = Singletons.GetPath();

            // 1. get newest version from repository
            var newest = _repository.GetNewestCommit(path);

            // 2. get latest version from db
            // check if table SqlVersion exists
            if (!await _dbNegotiator.IsVersionTableExists())
            {
                // create table
                await _dbNegotiator.CreateVersionTable();
            }

            var latestSqlVersion = await _dbNegotiator.GetLatestSqlVersion();
            var latest = RepositoryVersion.GetRepositoryVersion(latestSqlVersion);

            // 3. get changed(added, modified) files from repository
            var changedFiles = _repository.GetChangedFiles(path, newest, latest);

            // 4. exclude ignored files
            _sqlSelector.Exclude(_sqlIgnoreConfiguration, ref changedFiles);

            // 5. sort files
            _sqlSelector.Sort(_sqlOrderConfiguration, ref changedFiles);

            // 6. delivery to database
            try
            {
                var scripts = await Task.WhenAll(changedFiles
                    .Where(file => file.ToLower().EndsWith(".sql"))
                    .Select(async file => await _fileReader.GetContentAsync($"{Singletons.GetPath()}/{file}"))
                    .ToList());

                if (!scripts.Any())
                {
                    goto goto_end;
                }

                using (var trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    // execute all scripts
                    var beginTime = TimeUtility.Now;
                    foreach (var script in scripts)
                    {
                        await _dbNegotiator.Execute(script);
                    }

                    var endTime = TimeUtility.Now;

                    // insert delivery record
                    var sv = new SqlVersion(_baseConfiguration.RepositoryType, newest.Version,
                        0); // TODO: change 0 to a real number

                    // 1.get the latest version
                    var preVersion = await _dbNegotiator.GetLatestSqlVersion();
                    // 2.set sv.LastVersion = lastV.Id, sv.IsLatest = true
                    sv.IsLatest = true;
                    sv.LastVersion = preVersion?.Id;
                    // 3.update all other records.IsLatest = false
                    await _dbNegotiator.SetAllNonLatest();
                    // 4.insert sv
                    // set transaction cost
                    sv.TransactionCost = (endTime - beginTime).TotalMilliseconds;
                    await _dbNegotiator.InsertSqlVersion(sv);

                    trans.Complete();
                }

                goto_end:
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
    }
}