using System.Collections.Generic;

namespace sqlcicd.Configuration
{
    /// <summary>
    /// Provide system ignored files
    /// </summary>
    public interface ISysIgnoredFileProvider
    {
        /// <summary>
        /// Get system ignored files
        /// </summary>
        /// <returns>file list</returns>
        IEnumerable<string> GetIgnoredFiles();
    }
}