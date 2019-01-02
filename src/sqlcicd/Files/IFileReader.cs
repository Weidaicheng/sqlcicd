using System.Collections.Generic;
using System.Threading.Tasks;

namespace sqlcicd.Files
{
    /// <summary>
    /// File reader
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Get file content
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>File content</returns>
        Task<string> GetContentAsync(string path);

        /// <summary>
        /// Get file lines
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>File lines</returns>
        Task<IEnumerable<string>> GetLinesAsync(string path);
    }
}