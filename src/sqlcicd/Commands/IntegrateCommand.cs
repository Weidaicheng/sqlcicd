using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sqlcicd.Commands.Entity;
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
        private readonly IGrammarChecker _grammarChecker;
        private readonly IFileReader _fileReader;
        private readonly SqlIgnoreConfiguration _sqlIgnoreConfiguration;

        public IntegrateCommand(IRepository repository,
            IDbNegotiator dbNegotiator,
            ISqlSelector sqlSelector,
            IGrammarChecker grammarChecker,
            IFileReader fileReader,
            SqlIgnoreConfiguration sqlIgnoreConfiguration)
        {
            _repository = repository;
            _dbNegotiator = dbNegotiator;
            _sqlSelector = sqlSelector;
            _grammarChecker = grammarChecker;
            _fileReader = fileReader;
            _sqlIgnoreConfiguration = sqlIgnoreConfiguration;
        }

        /// <summary>
        /// Continuous Integrate
        /// </summary>
        /// <returns><see cref="ExecutionResult" /></returns>
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

            var hasErr = false;
            var errors = new List<string>();
            // 5. check grammar
            foreach (var file in changedFiles)
            {
                if (!file.ToLower().EndsWith(".sql")) continue;
                var script = await _fileReader.GetContentAsync($"{Singletons.GetPath()}/{file}");
                if (_grammarChecker.Check(script, out var errMsg)) continue;
                errors.Add(errMsg);
                hasErr = true;
            }

            return new ExecutionResult()
            {
                Success = !hasErr,
                ErrorMessage = errors.Count == 0 ? string.Empty : errors.Aggregate((prev, next) => $"{prev}\n{next}")
            };
        }
    }
}