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

        /// <summary>
        /// Check if the file exists
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>if exists</returns>
        bool FileExistsCheck(string path);

        /// <summary>
        /// Check if the directory exists
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>if exists</returns>
        bool DirectoryExistsCheck(string path);

        /// <summary>
        /// Get files under directory
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns>files' name</returns>
        Task<IEnumerable<string>> GetFiles(string directoryPath);
    }
}