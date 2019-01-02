using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using sqlcicd.Configuration;
using sqlcicd.Configuration.Entity;
using sqlcicd.Database;
using sqlcicd.Exceptions;
using sqlcicd.Files;
using sqlcicd.Repository;
using sqlcicd.Repository.Entity;
using sqlcicd.Syntax;

namespace sqlcicd.Commands
{
    /// <summary>
    /// Continuous Integrate command
    /// </summary>
    public class IntegrateCommand : ICommand
    {
        private readonly IRepository _repository;
        private readonly IDbNegotiator _dbNegotiator;
        private readonly ISqlSelector _sqlSelector;
        private readonly IGrammerChecker _grammerChecker;
        private readonly IFileReader _fileReader;
        private readonly SqlIgnoreConfiguration _sqlIgnoreConfiguration;

        public IntegrateCommand(IRepository repository,
            IDbNegotiator dbNegotiator,
            ISqlSelector sqlSelector,
            IGrammerChecker grammerChecker,
            IFileReader fileReader,
            SqlIgnoreConfiguration sqlIgnoreConfiguration)
        {
            _repository = repository;
            _dbNegotiator = dbNegotiator;
            _sqlSelector = sqlSelector;
            _grammerChecker = grammerChecker;
            _fileReader = fileReader;
            _sqlIgnoreConfiguration = sqlIgnoreConfiguration;
        }

        /// <summary>
        /// Continuous Integrate
        /// </summary>
        /// <param name="args">arguments, the first argument is the repository path</param>
        /// <returns><see cref="ExecutionResult" /></returns>
        public async Task<ExecutionResult> Execute(string[] args)
        {
            var path = args[0];

            // 1. get newest version from repository
            var newest = _repository.GetNewestCommit(path);

            // 2. get latest version from db
            var latestSqlVersion = _dbNegotiator.GetLatestSqlVersion();
            var latest = RepositoryVersion.GetRepositoryVersion(latestSqlVersion);

            // 3. get changed(added, modified) files from repository
            var changedFiles = _repository.GetChangedFiles(path, newest, latest);

            // 4. exclude ignored files
            _sqlSelector.Exclude(_sqlIgnoreConfiguration, ref changedFiles);

            var hasErr = false;
            var errors = new List<string>();
            // 5. check grammer
            foreach (var file in changedFiles)
            {
                var script = await _fileReader.GetContentAsync(file);
                if (_grammerChecker.Check(script, out string errMsg))
                {
                    errors.Add(errMsg);
                    hasErr = true;
                }
            }

            return new ExecutionResult()
            {
                Success = !hasErr,
                Result = null,
                ErrorMessages = errors
            };
        }
    }
}