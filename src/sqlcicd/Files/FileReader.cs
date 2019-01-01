using System;
using System.IO;
using System.Threading.Tasks;
using sqlcicd.Exceptions;

namespace sqlcicd.Files
{
    /// <summary>
    /// File reader
    /// </summary>
    public class FileReader : IFileReader
    {
        /// <summary>
        /// Get file content
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>File content</returns>
        public async Task<string> GetContentAsync(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("File path is not provided.");
            }

            using(FileStream fs = new FileStream(path, FileMode.Open))
            {
                using(StreamReader sr = new StreamReader(fs))
                {
                    string content = await sr.ReadToEndAsync();

                    sr.Close();
                    fs.Close();

                    return content;
                }
            }
        }
    }
}