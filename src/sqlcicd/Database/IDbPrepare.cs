using System.Threading.Tasks;

namespace sqlcicd.Database
{
    /// <summary>
    /// Database preparation
    /// </summary>
    public interface IDbPrepare
    {
        /// <summary>
        /// Do the preparation job
        /// </summary>
        /// <returns></returns>
        Task Prepare();
    }
}