using System.Threading.Tasks;
using sqlcicd.Configuration;
using sqlcicd.DbNegotiator;
using sqlcicd.Exceptions;
using sqlcicd.Files;
using sqlcicd.Repository;
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
        private readonly RepositoryVersionFactory _repositoryVFactory;
        private readonly IFileReader _fileReader;

        public IntegrateCommand(IRepository repository,
            IDbNegotiator dbNegotiator,
            ISqlSelector sqlSelector,
            IGrammerChecker grammerChecker,
            RepositoryVersionFactory repositoryVFactory,
            IFileReader fileReader)
        {
            _repository = repository;
            _dbNegotiator = dbNegotiator;
            _sqlSelector = sqlSelector;
            _grammerChecker = grammerChecker;
            _repositoryVFactory = repositoryVFactory;
            _fileReader = fileReader;
        }

        /// <summary>
        /// Continuous Integrate
        /// </summary>
        /// <param name="args">arguments, the first argument is the repository path</param>
        public async Task Execute(string[] args)
        {
            var path = args[0];

            // 1. get newest version from repository
            var newest = _repository.GetNewestCommit(path);

            // 2. get latest version from db
            var latestSqlVersion = _dbNegotiator.GetLatestSqlVersion();
            var latest = _repositoryVFactory.GetRepositoryVersion(latestSqlVersion);

            // 3. get changed(added, modified) files from repository
            var changedFiles = _repository.GetChangedFiles(path, newest, latest);

            // 4. exclude ignored files
            _sqlSelector.Exclude(SqlConfigurations.SqlIgnore, ref changedFiles);

            var hasErr = false;
            var error = "";
            // 5. check grammer
            foreach (var file in changedFiles)
            {
                var script = await _fileReader.GetContentAsync(file);
                hasErr = _grammerChecker.Check(script, out string errMsg);
                error += errMsg;
            }

            if (hasErr)
                throw new SqlSyntaxErrorException(error);
        }
    }
}