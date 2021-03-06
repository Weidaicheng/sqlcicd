using System.Collections.Generic;
using sqlcicd.Configuration.Entity;
using System.Linq;

namespace sqlcicd.Configuration
{
    public class SqlSelector : ISqlSelector
    {
        private readonly ISysIgnoredFileProvider _sysIgnoredFileProvider;

        public SqlSelector(ISysIgnoredFileProvider sysIgnoredFileProvider)
        {
            _sysIgnoredFileProvider = sysIgnoredFileProvider;
        }
        
        public void Exclude(SqlIgnoreConfiguration ignoreConfiguration, ref IEnumerable<string> files)
        {
            files = files.Where(f => !_sysIgnoredFileProvider.GetIgnoredFiles().Contains(f));
            files = files.Where(f => !ignoreConfiguration.IgnoredFile.Contains(f));
        }

        public void Sort(SqlOrderConfiguration orderConfiguration, ref IEnumerable<string> files)
        {
            var allFiles = files.ToList();
            var sortedFiles = new List<string>();

            foreach (var file in orderConfiguration.FileOrder)
            {
                if(allFiles.Contains(file))
                {
                    sortedFiles.Add(file);
                    allFiles.Remove(file);
                }
            }

            sortedFiles.AddRange(allFiles);

            files = sortedFiles;
        }
    }
}