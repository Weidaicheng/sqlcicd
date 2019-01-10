using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using sqlcicd.Commands.Entity;
using sqlcicd.Configuration;
using sqlcicd.Configuration.Entity;
using sqlcicd.Database.Entity;
using sqlcicd.Exceptions;
using sqlcicd.Files;
using sqlcicd.Repository.Entity;

namespace sqlcicd.Tests
{
    public class WithRepoConfigurationReaderTest
    {
        [Test]
        public async Task GetSqlIgnoreConfiguration_FileNotExists_ReturnsEmptyList()
        {
            var fileReader = Substitute.For<IFileReader>();
            var command = Substitute.For<Command>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(false);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            var result = await confReader.GetSqlIgnoreConfiguration();

            Assert.AreEqual(0, result.IgnoredFile.Count());
        }

        [Test]
        public async Task GetSqlIgnoreConfiguration_ReadConfiguration_ReturnsSame()
        {
            var fileReader = Substitute.For<IFileReader>();
            var command = Substitute.For<Command>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                "line 1",
                "line 2"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            var result = await confReader.GetSqlIgnoreConfiguration();

            Assert.AreEqual(lines, result.IgnoredFile);
        }

        [Test]
        public async Task GetSqlIgnoreConfiguration_DirectoryConfigured_ReturnsAllfiles()
        {
            var fileReader = Substitute.For<IFileReader>();
            var command = Substitute.For<Command>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                "path"
            };
            var files = new List<string>()
            {
                "file 1",
                "file 2"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            fileReader.DirectoryExistsCheck(Arg.Any<string>()).Returns(true);
            fileReader.GetFiles(Arg.Any<string>()).Returns(files);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            var result = await confReader.GetSqlIgnoreConfiguration();

            Assert.AreEqual(files, result.IgnoredFile);
        }
        
        [Test]
        public async Task GetSqlIgnoreConfiguration_DirectoryAndFileConfigured_ReturnsAllfiles()
        {
            var command = new Command()
            {
                Path = ""
            };
            var fileReader = Substitute.For<IFileReader>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                "path",
                "file 3"
            };
            var files = new List<string>()
            {
                "file 1",
                "file 2"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            fileReader.DirectoryExistsCheck($"{command.Path}/path").Returns(true);
            fileReader.GetFiles($"{command.Path}/path").Returns(files);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            var result = await confReader.GetSqlIgnoreConfiguration();

            Assert.AreEqual(new List<string>()
            {
                "file 1",
                "file 2",
                "file 3"
            }, result.IgnoredFile);
        }

        [Test]
        public async Task GetSqlOrderConfiguration_FileNotExists_ReturnsEmptyList()
        {
            var fileReader = Substitute.For<IFileReader>();
            var command = Substitute.For<Command>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(false);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            var result = await confReader.GetSqlOrderConfiguration();

            Assert.AreEqual(0, result.FileOrder.Count());
        }

        [Test]
        public async Task GetSqlOrderConfiguration_ReadConfiguration_ReturnsSame()
        {
            var fileReader = Substitute.For<IFileReader>();
            var command = Substitute.For<Command>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                "line 1",
                "line 2"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            var result = await confReader.GetSqlOrderConfiguration();

            Assert.AreEqual(lines, result.FileOrder);
        }
        
        [Test]
        public async Task GetSqlOrderConfiguration_DirectoryConfigured_ReturnsAllfiles()
        {
            var fileReader = Substitute.For<IFileReader>();
            var command = Substitute.For<Command>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                "path"
            };
            var files = new List<string>()
            {
                "file 1",
                "file 2"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            fileReader.DirectoryExistsCheck(Arg.Any<string>()).Returns(true);
            fileReader.GetFiles(Arg.Any<string>()).Returns(files);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            var result = await confReader.GetSqlOrderConfiguration();

            Assert.AreEqual(files, result.FileOrder);
        }
        
        [Test]
        public async Task GetSqlOrderConfiguration_DirectoryAndFileConfigured_ReturnsAllfiles()
        {
            var command = new Command()
            {
                Path = ""
            };
            var fileReader = Substitute.For<IFileReader>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                "path",
                "file 3"
            };
            var files = new List<string>()
            {
                "file 1",
                "file 2"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            fileReader.DirectoryExistsCheck($"{command.Path}/path").Returns(true);
            fileReader.GetFiles($"{command.Path}/path").Returns(files);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            var result = await confReader.GetSqlOrderConfiguration();

            Assert.AreEqual(new List<string>()
            {
                "file 1",
                "file 2",
                "file 3"
            }, result.FileOrder);
        }

        [Test]
        public void GetBaseConfiguration_FileNotExists_ThrowsException()
        {
            var fileReader = Substitute.For<IFileReader>();
            var command = Substitute.For<Command>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(false);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            Assert.Catch<FileNotFoundException>(() =>
            {
                confReader.GetBaseConfiguration().GetAwaiter().GetResult();
            });
        }

        [Test]
        public void GetBaseConfiguration_DbTypeNotConfigured_ThrowsException()
        {
            var fileReader = Substitute.For<IFileReader>();
            var command = Substitute.For<Command>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                $"{nameof(RepositoryType)}:Git",
                $"{WithRepoConfigurationReader.CONNECTION_STRING_KEY}:default"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            Assert.Catch<DbTypeNotConfiguredException>(() =>
            {
                confReader.GetBaseConfiguration().GetAwaiter().GetResult();
            });
        }

        [Test]
        public void GetBaseConfiguration_RepositoryTypeNotConfigured_ThrowsException()
        {
            var fileReader = Substitute.For<IFileReader>();
            var command = Substitute.For<Command>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                $"{nameof(DbType)}:Mssql",
                $"{WithRepoConfigurationReader.CONNECTION_STRING_KEY}:default"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            Assert.Catch<RepositoryTypeNotConfiguredException>(() =>
            {
                confReader.GetBaseConfiguration().GetAwaiter().GetResult();
            });
        }

        [Test]
        public void GetBaseConfiguration_ConnectionStringNotConfigured_ThrowsException()
        {
            var fileReader = Substitute.For<IFileReader>();
            var command = Substitute.For<Command>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                $"{nameof(DbType)}:Mssql",
                $"{nameof(RepositoryType)}:Git"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            Assert.Catch<ConnectionStringNotProvidedException>(() =>
            {
                confReader.GetBaseConfiguration().GetAwaiter().GetResult();
            });
        }

        [Test]
        public async Task GetBaseConfiguration_AllConfigured_ReturnsSame()
        {
            var fileReader = Substitute.For<IFileReader>();
            var command = Substitute.For<Command>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                $"{nameof(DbType)}:Mssql",
                $"{nameof(RepositoryType)}:Git",
                $"{WithRepoConfigurationReader.CONNECTION_STRING_KEY}:default"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            var confReader = new WithRepoConfigurationReader(fileReader, command);

            var result = await confReader.GetBaseConfiguration();

            Assert.AreEqual(DbType.Mssql, result.DbType);
            Assert.AreEqual(RepositoryType.Git, result.RepositoryType);
            Assert.AreEqual("default", result.ConnectionString);
        }
    }
}