using System;
using System.Threading.Tasks;
using NUnit.Framework;
using sqlcicd.Files;

namespace Tests
{
    public class Tests
    {
        private IFileReader reader;

        [SetUp]
        public void Setup()
        {
            reader = new FileReader();
        }

        [Test]
        public async Task GetContentAsync_PassNull_ThrowsException()
        {
            try
            {
                await reader.GetContentAsync(string.Empty);
            }
            catch(Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(ArgumentNullException));
            }
        }

        [Test]
        public async Task GetContentAsync_PassEmptyString_ThrowsException()
        {
            try
            {
                await reader.GetContentAsync(null);
            }
            catch(Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(ArgumentNullException));
            }
        }
    }
}