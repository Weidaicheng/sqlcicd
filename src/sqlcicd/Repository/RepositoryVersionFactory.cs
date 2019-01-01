using sqlcicd.Entity;

namespace sqlcicd.Repository
{
    /// <summary>
    /// Factory for <see cref="IRepositoryVersion" />
    /// </summary>
    public class RepositoryVersionFactory
    {
        /// <summary>
        /// Get an instance of <see cref="IRepositoryVersion" />
        /// </summary>
        /// <param name="sv"><see cref="SqlVersion" /></param>
        /// <returns>An instance of <see cref="IRepositoryVersion" /></returns>
        public IRepositoryVersion GetRepositoryVersion(SqlVersion sv)
        {
            return null;
        }
    }
}