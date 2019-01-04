namespace sqlcicd.Syntax
{
    /// <summary>
    /// Sql grammar checker
    /// </summary>
    public interface IGrammarChecker
    {
        /// <summary>
        /// Check grammar
        /// </summary>
        /// <param name="sql">Sql syntax that needs to be checked</param>
        /// <param name="errMsg">Error messages</param>
        /// <returns>Check result</returns>
        bool Check(string sql, out string errMsg);
    }
}