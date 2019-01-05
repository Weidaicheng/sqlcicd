using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sqlcicd.Commands.Entity;
using sqlcicd.Configuration;
using sqlcicd.Configuration.Entity;
using sqlcicd.Database;
using sqlcicd.Files;
using sqlcicd.Repository;
using sqlcicd.Repository.Entity;
using sqlcicd.Syntax;

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

        public DeliveryCommand(IRepository repository,
            IDbNegotiator dbNegotiator,
            ISqlSelector sqlSelector,
            IGrammarChecker grammarChecker,
            IFileReader fileReader,
            SqlIgnoreConfiguration sqlIgnoreConfiguration,
            SqlOrderConfiguration sqlOrderConfiguration)
        {
            _repository = repository;
            _dbNegotiator = dbNegotiator;
            _sqlSelector = sqlSelector;
            _grammarChecker = grammarChecker;
            _fileReader = fileReader;
            _sqlIgnoreConfiguration = sqlIgnoreConfiguration;
            _sqlOrderConfiguration = sqlOrderConfiguration;
        }
        
        public async Task<ExecutionResult> Execute()
        {
            var path = Singletons.GetPath();

            // 1. get newest version from repository
            var newest = _repository.GetNewestCommit(path);

            // 2. get latest version from db
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
                    .ToList()) ;
                
                await _dbNegotiator.ExecuteBunch(scripts);
                
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