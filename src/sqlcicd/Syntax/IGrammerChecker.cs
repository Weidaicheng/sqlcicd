namespace sqlcicd.Syntax
{
    /// <summary>
    /// Sql grammer checker
    /// </summary>
    public interface IGrammerChecker
    {
        /// <summary>
        /// Check grammer
        /// </summary>
        /// <param name="sql">Sql syntax that needs to be checked</param>
        /// <param name="errMsg">Error messages</param>
        /// <returns>Check result</returns>
        bool Check(string sql, out string errMsg);
    }
}