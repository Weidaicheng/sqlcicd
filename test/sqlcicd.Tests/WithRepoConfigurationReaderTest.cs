using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
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
        public void GetSqlIgnoreConfiguration_PathNotProvided_ThrowsException()
        {
            var confReader = new WithRepoConfigurationReader(null);
            Singletons.Args = new List<string>()
            {
                ""
            }.ToArray();

            Assert.Catch<PathNotProvidedException>(() => 
            {
                confReader.GetSqlIgnoreConfiguration().GetAwaiter().GetResult();
            });
        }

        [Test]
        public async Task GetSqlIgnoreConfiguration_FileNotExists_ReturnsEmptyList()
        {
            Singletons.Args = new List<string>()
            {
                "",
                ""
            }.ToArray();

            var fileReader = Substitute.For<IFileReader>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(false);
            var confReader = new WithRepoConfigurationReader(fileReader);

            var result = await confReader.GetSqlIgnoreConfiguration();

            Assert.AreEqual(0, result.IgnoredFile.Count());
        }

        [Test]
        public async Task GetSqlIgnoreConfiguration_ReadConfiguration_ReturnsSame()
        {
            Singletons.Args = new List<string>()
            {
                "",
                ""
            }.ToArray();

            var fileReader = Substitute.For<IFileReader>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                "line 1",
                "line 2"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            var confReader = new WithRepoConfigurationReader(fileReader);

            var result = await confReader.GetSqlIgnoreConfiguration();

            Assert.AreEqual(lines, result.IgnoredFile);
        }

        [Test]
        public void GetSqlOrderConfiguration_PathNotProvided_ThrowsException()
        {
            var confReader = new WithRepoConfigurationReader(null);
            Singletons.Args = new List<string>()
            {
                ""
            }.ToArray();

            Assert.Catch<PathNotProvidedException>(() => 
            {
                confReader.GetSqlOrderConfiguration().GetAwaiter().GetResult();
            });
        }

        [Test]
        public async Task GetSqlOrderConfiguration_FileNotExists_ReturnsEmptyList()
        {
            Singletons.Args = new List<string>()
            {
                "",
                ""
            }.ToArray();

            var fileReader = Substitute.For<IFileReader>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(false);
            var confReader = new WithRepoConfigurationReader(fileReader);

            var result = await confReader.GetSqlOrderConfiguration();

            Assert.AreEqual(0, result.FileOrder.Count());
        }

        [Test]
        public async Task GetSqlOrderConfiguration_ReadConfiguration_ReturnsSame()
        {
            Singletons.Args = new List<string>()
            {
                "",
                ""
            }.ToArray();

            var fileReader = Substitute.For<IFileReader>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                "line 1",
                "line 2"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            var confReader = new WithRepoConfigurationReader(fileReader);

            var result = await confReader.GetSqlOrderConfiguration();

            Assert.AreEqual(lines, result.FileOrder);
        }

        [Test]
        public void GetBaseConfiguration_PathNotProvided_ThrowsException()
        {
            var confReader = new WithRepoConfigurationReader(null);
            Singletons.Args = new List<string>()
            {
                ""
            }.ToArray();

            Assert.Catch<PathNotProvidedException>(() => 
            {
                confReader.GetBaseConfiguration().GetAwaiter().GetResult();
            });
        }

        [Test]
        public void GetBaseConfiguration_FileNotExists_ThrowsException()
        {
            Singletons.Args = new List<string>()
            {
                "",
                ""
            }.ToArray();

            var fileReader = Substitute.For<IFileReader>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(false);
            var confReader = new WithRepoConfigurationReader(fileReader);

            Assert.Catch<FileNotFoundException>(() =>
            {
                confReader.GetBaseConfiguration().GetAwaiter().GetResult();
            });
        }

        [Test]
        public void GetBaseConfiguration_DbTypeNotConfigured_ThrowsException()
        {
            Singletons.Args = new List<string>()
            {
                "",
                ""
            }.ToArray();

            var fileReader = Substitute.For<IFileReader>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                $"{nameof(RepositoryType)}:Git",
                $"{WithRepoConfigurationReader.CONNECTION_STRING_KEY}:default"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            var confReader = new WithRepoConfigurationReader(fileReader);

            Assert.Catch<DbTypeNotConfiguredException>(() =>
            {
                confReader.GetBaseConfiguration().GetAwaiter().GetResult();
            });
        }

        [Test]
        public void GetBaseConfiguration_RepositoryTypeNotConfigured_ThrowsException()
        {
            Singletons.Args = new List<string>()
            {
                "",
                ""
            }.ToArray();

            var fileReader = Substitute.For<IFileReader>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                $"{nameof(DbType)}:Mssql",
                $"{WithRepoConfigurationReader.CONNECTION_STRING_KEY}:default"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            var confReader = new WithRepoConfigurationReader(fileReader);

            Assert.Catch<RepositoryTypeNotConfiguredException>(() =>
            {
                confReader.GetBaseConfiguration().GetAwaiter().GetResult();
            });
        }

        [Test]
        public void GetBaseConfiguration_ConnectionStringNotConfigured_ThrowsException()
        {
            Singletons.Args = new List<string>()
            {
                "",
                ""
            }.ToArray();

            var fileReader = Substitute.For<IFileReader>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                $"{nameof(DbType)}:Mssql",
                $"{nameof(RepositoryType)}:Git"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            var confReader = new WithRepoConfigurationReader(fileReader);

            Assert.Catch<ConnectionStringNotProvidedException>(() =>
            {
                confReader.GetBaseConfiguration().GetAwaiter().GetResult();
            });
        }

        [Test]
        public async Task GetBaseConfiguration_AllConfigured_ReturnsSame()
        {
            Singletons.Args = new List<string>()
            {
                "",
                ""
            }.ToArray();

            var fileReader = Substitute.For<IFileReader>();
            fileReader.FileExistsCheck(Arg.Any<string>()).Returns(true);
            var lines = new List<string>()
            {
                $"{nameof(DbType)}:Mssql",
                $"{nameof(RepositoryType)}:Git",
                $"{WithRepoConfigurationReader.CONNECTION_STRING_KEY}:default"
            };
            fileReader.GetLinesAsync(Arg.Any<string>()).Returns(lines);
            var confReader = new WithRepoConfigurationReader(fileReader);

            var result = await confReader.GetBaseConfiguration();

            Assert.AreEqual(DbType.Mssql, result.DbType);
            Assert.AreEqual(RepositoryType.Git, result.RepositoryType);
            Assert.AreEqual("default", result.ConnectionString);
        }
    }
}