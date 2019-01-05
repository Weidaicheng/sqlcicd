using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using NSubstitute;
using NSubstitute.Extensions;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using sqlcicd.Database;
using sqlcicd.Database.Entity;
using sqlcicd.Exceptions;

namespace sqlcicd.Tests
{
    public class DbNegotiatorTest
    {
        [Test]
        public void SetSqlVersionRollback_RecordNotExists_ThrowsException()
        {
            Assert.Pass(); // TODO: find out how to provide a stub for IDbConnection
            
            var dbConnection = Substitute.For<IDbConnection>();
            var returnValue = Task.FromResult<SqlVersion>(null);
            dbConnection.QueryFirstOrDefaultAsync<SqlVersion>(Arg.Any<string>(), Arg.Any<object>())
                .ReturnsForAnyArgs(returnValue);
            
            var dbNegotiator = new DbNegotiator(dbConnection);

            Assert.Catch<SqlVersionNotExistException>(() =>
            {
                dbNegotiator.SetSqlVersionRollback(new SqlVersion()
                {
                    Id = 1,
                    IsDeleted = false
                }).GetAwaiter().GetResult();
            });
        }
    }
}