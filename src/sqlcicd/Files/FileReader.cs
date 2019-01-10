using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace sqlcicd.Files
{
    /// <summary>
    /// File reader
    /// </summary>
    public class FileReader : IFileReader
    {
        public bool FileExistsCheck(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// Get file content
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>File content</returns>
        public async Task<string> GetContentAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("File path is not provided.");
            }

            using (var fs = new FileStream(path, FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    var content = await sr.ReadToEndAsync();

                    sr.Close();
                    fs.Close();

                    return content;
                }
            }
        }

        public async Task<IEnumerable<string>> GetLinesAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("File path is not provided.");
            }

            var lines = new List<string>();
            using (var fs = new FileStream(path, FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    var line = await sr.ReadLineAsync();
                    while (line != null)
                    {
                        lines.Add(line);
                        line = await sr.ReadLineAsync();
                    }

                    sr.Close();
                    fs.Close();

                    return lines;
                }
            }
        }
    }
}