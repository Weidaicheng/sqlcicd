namespace sqlcicd.Repository
{
    /// <summary>
    /// Repository version
    /// </summary>
    public interface IRepositoryVersion
    {
        /// <summary>
        /// Version id
        /// </summary>
        string Version { get; }
    }
}