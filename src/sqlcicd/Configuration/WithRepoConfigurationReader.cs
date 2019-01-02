using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using sqlcicd.Configuration.Entity;
using sqlcicd.Files;
using sqlcicd.Repository;
using sqlcicd.Repository.Entity;

namespace sqlcicd.Configuration
{
    /// <summary>
    /// Configurations under repository root path
    /// </summary>
    public class WithRepoConfigurationReader : ISqlConfigurationReader
    {
        #region Const
        /// <summary>
        /// Default sql order configuration file name
        /// </summary>
        public const string SQL_ORDER_CONFIG = ".sqlorder";

        /// <summary>
        /// Default sql ignore configuration file name
        /// </summary>
        public const string SQL_IGNORE_CONFIG = ".sqlignore";

        /// <summary>
        /// Base configuration file includes database type, repository type
        /// </summary>
        public const string BASE_CONFIG = ".sqlcicd";
        #endregion

        private readonly IFileReader _fileReader;

        public WithRepoConfigurationReader(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        /// <summary>
        /// Check if the file exists
        /// </summary>
        /// <param name="path">File path</param>
        private void fileExistsCheck(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"{path} didn't find.");
            }
        }

        public async Task<DbType> GetDbTypeConfiguration()
        {
            var path = $"{Singletons.Path}/{BASE_CONFIG}";
            fileExistsCheck(path);

            var lines = await _fileReader.GetLinesAsync(path);
            var dbTypeConfig = lines.First(l => l.StartsWith(nameof(DbType)));

            return (DbType)Enum.Parse(typeof(DbType), dbTypeConfig.Split(':')[1]);
        }

        public async Task<RepositoryType> GetRepositoryTypeConfiguration()
        {
            var path = $"{Singletons.Path}/{BASE_CONFIG}";
            fileExistsCheck(path);

            var lines = await _fileReader.GetLinesAsync(path);
            var dbTypeConfig = lines.First(l => l.StartsWith(nameof(DbType)));

            return (RepositoryType)Enum.Parse(typeof(RepositoryType), dbTypeConfig.Split(':')[1]);
        }

        public async Task<SqlIgnoreConfiguration> GetSqlIgnoreConfiguration()
        {
            var path = $"{Singletons.Path}/{SQL_IGNORE_CONFIG}";
            fileExistsCheck(path);

            var lines = await _fileReader.GetLinesAsync(path);

            return new SqlIgnoreConfiguration()
            {
                IgnoredFile = lines
            };
        }

        public async Task<SqlOrderConfiguration> GetSqlOrderConfiguration()
        {
            var path = $"{Singletons.Path}/{SQL_ORDER_CONFIG}";
            fileExistsCheck(path);

            var lines = await _fileReader.GetLinesAsync(path);

            return new SqlOrderConfiguration()
            {
                FileOrder = lines
            };
        }
    }
}