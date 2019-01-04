using NUnit.Framework;
using sqlcicd.Database.Entity;
using sqlcicd.Repository.Entity;

namespace sqlcicd.Tests
{
    public class RepositoryVersionTest
    {
        [Test]
        public void GetRepositoryVersion_PassNull_ReturnsNull()
        {
            var result = RepositoryVersion.GetRepositoryVersion(null);
            
            Assert.IsNull(result);
        }

        [Test]
        public void GetRepositoryVersion_PassEmptyVersion_ReturnsNull()
        {
            var result = RepositoryVersion.GetRepositoryVersion(new SqlVersion()
            {
                Version = string.Empty
            });
            
            Assert.IsNull(result);
        }
    }
}