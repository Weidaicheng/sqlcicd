using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    /// Continuous Integrate command
    /// </summary>
    public class IntegrateCommand : ICommand, ILogEvent
    {
        private readonly IRepository _repository;
        private readonly DbNegotiator _dbNegotiator;
        private readonly ISqlSelector _sqlSelector;
        private readonly IGrammarChecker _grammarChecker;
        private readonly IFileReader _fileReader;
        private readonly SqlIgnoreConfiguration _sqlIgnoreConfiguration;
        private readonly Command _command;

        public IntegrateCommand(IRepository repository,
            DbNegotiator dbNegotiator,
            ISqlSelector sqlSelector,
            IGrammarChecker grammarChecker,
            IFileReader fileReader,
            SqlIgnoreConfiguration sqlIgnoreConfiguration,
            Command command)
        {
            _repository = repository;
            _dbNegotiator = dbNegotiator;
            _sqlSelector = sqlSelector;
            _grammarChecker = grammarChecker;
            _fileReader = fileReader;
            _sqlIgnoreConfiguration = sqlIgnoreConfiguration;
            _command = command;
        }

        /// <summary>
        /// Continuous Integrate
        /// </summary>
        /// <returns><see cref="ExecutionResult" /></returns>
        public async Task<ExecutionResult> Execute()
        {
            log($"SQL Continuous Integrate start");
            
            var path = _command.Path;
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

            var hasErr = false;
            var errors = new List<string>();
            // 5. check grammar
            foreach (var file in changedFiles)
            {
                if (!file.ToLower().EndsWith(".sql")) continue;
                log($"check grammar for {file}");
                var script = await _fileReader.GetContentAsync($"{_command.Path}/{file}");
                if (_grammarChecker.Check(script, out var errMsg)) continue;
                log($"error(s) found in {file}");
                errors.Add(errMsg);
                hasErr = true;
            }
            
            log($"SQL Continuous Integrate finish");
            return new ExecutionResult()
            {
                Success = !hasErr,
                ErrorMessage = errors.Count == 0 ? string.Empty : errors.Aggregate((prev, next) => $"{prev}\n{next}")
            };
        }

        public event Action<string> Log;

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="log"></param>
        private void log(string log)
        {
            Log?.Invoke($"UTC {TimeUtility.Now.ToLongTimeString()} | {log}");
        }
    }
}