using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using sqlcicd.Configuration;
using sqlcicd.Configuration.Entity;

namespace sqlcicd.Tests
{
    public class SqlSelectorTest
    {
        [Test]
        public void Exclude_ProvideTwoFilesAndIgnoreOne_ReturnsOne()
        {
            var selector = new SqlSelector();
            var files = new List<string>()
            {
                "file 1",
                "file 2"
            }.AsEnumerable();
            var ignore = new SqlIgnoreConfiguration()
            {
                IgnoredFile = new List<string>()
                {
                    "file 1"
                }
            };

            selector.Exclude(ignore, ref files);

            Assert.IsTrue(!files.Contains("file 1"));
        }

        [Test]
        public void Sort_ProvideTwoFilesAndSwapOrders_ReturnsDifferentOrder()
        {
            var selector = new SqlSelector();
            var files = new List<string>()
            {
                "file 1",
                "file 2"
            }.AsEnumerable();
            Assert.IsTrue(files.First() == "file 1");

            var order = new SqlOrderConfiguration()
            {
                FileOrder = new List<string>()
                {
                    "file 2"
                }
            };

            selector.Sort(order, ref files);

            Assert.IsTrue(files.First() == "file 2");
        }
    }
}