using System.Collections.Generic;

namespace sqlcicd.Configuration
{
    public class WithRepoSysIgnoredFileProvider : ISysIgnoredFileProvider
    {
        public IEnumerable<string> GetIgnoredFiles()
        {
            return new List<string>()
            {
                WithRepoConfigurationReader.SQL_IGNORE_CONFIG,
                WithRepoConfigurationReader.SQL_ORDER_CONFIG,
                WithRepoConfigurationReader.BASE_CONFIG
            };
        }
    }
}