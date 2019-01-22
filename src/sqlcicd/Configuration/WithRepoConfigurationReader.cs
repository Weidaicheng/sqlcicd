using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using sqlcicd.Commands.Entity;
using sqlcicd.Configuration.Entity;
using sqlcicd.Database.Entity;
using sqlcicd.Exceptions;
using sqlcicd.Files;
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
        private readonly Command _command;

        public WithRepoConfigurationReader(IFileReader fileReader, Command command)
        {
            _fileReader = fileReader;
            _command = command;
        }

        public async Task<SqlIgnoreConfiguration> GetSqlIgnoreConfiguration()
        {
            var path = $"{_command.Path}/{SQL_IGNORE_CONFIG}";
            if (!_fileReader.FileExistsCheck(path))
            {
                return new SqlIgnoreConfiguration()
                {
                    IgnoredFile = new List<string>()
                };
            }

            var lines = await _fileReader.GetLinesAsync(path);
            var ignoredFiles = new List<string>();

            foreach (var line in lines)
            {
                if (_fileReader.DirectoryExistsCheck($"{_command.Path}/{line}"))
                {
                    // is a directory
                    var files = await _fileReader.GetFiles($"{_command.Path}/{line}");
                    ignoredFiles.AddRange(files);
                }
                else
                {
                    // is a file
                    ignoredFiles.Add(line);
                }
            }

            return new SqlIgnoreConfiguration()
            {
                IgnoredFile = ignoredFiles
            };
        }

        public async Task<SqlOrderConfiguration> GetSqlOrderConfiguration()
        {
            var path = $"{_command.Path}/{SQL_ORDER_CONFIG}";
            if (!_fileReader.FileExistsCheck(path))
            {
                return new SqlOrderConfiguration()
                {
                    FileOrder = new List<string>()
                };
            }

            var lines = await _fileReader.GetLinesAsync(path);
            var fileOrder = new List<string>();

            foreach (var line in lines)
            {
                if (_fileReader.DirectoryExistsCheck($"{_command.Path}/{line}"))
                {
                    // is a directory
                    var files = await _fileReader.GetFiles($"{_command.Path}/{line}");
                    fileOrder.AddRange(files);
                }
                else
                {
                    // is a file
                    fileOrder.Add(line);
                }
            }

            return new SqlOrderConfiguration()
            {
                FileOrder = fileOrder
            };
        }

        public async Task<BaseConfiguration> GetBaseConfiguration()
        {
            var path = $"{_command.Path}/{BASE_CONFIG}";
            if (!_fileReader.FileExistsCheck(path))
            {
                throw new FileNotFoundException($"{path} hasn't found.");
            }

            try
            {
                var lines = await _fileReader.GetLinesAsync(path);

                // TODO: refactoring
                return new BaseConfiguration()
                {
                    DbType = (DbType) Enum.Parse(typeof(DbType),
                        lines.FirstOrDefault(l => l.StartsWith(nameof(DbType))).Split(':')[1]),
                    RepositoryType =
                        (RepositoryType) Enum.Parse(typeof(RepositoryType),
                            lines.FirstOrDefault(l => l.StartsWith(nameof(RepositoryType))).Split(':')[1]),
                    Server = lines.FirstOrDefault(l => l.StartsWith(nameof(BaseConfiguration.Server))).Split(':')[1],
                    Database =
                        lines.FirstOrDefault(l => l.StartsWith(nameof(BaseConfiguration.Database))).Split(':')[1],
                    UserId = lines.FirstOrDefault(l => l.StartsWith(nameof(BaseConfiguration.UserId))).Split(':')[1],
                    Password =
                        lines.FirstOrDefault(l => l.StartsWith(nameof(BaseConfiguration.Password))).Split(':')[1],
                    BackupPath = lines.FirstOrDefault(l => l.StartsWith(nameof(BaseConfiguration.BackupPath)))
                        ?.Split(':')[1]
                };
            }
            catch
            {
                throw new BaseConfigurationInvalidException("Base configuration is invalid.");
            }
        }
    }
}