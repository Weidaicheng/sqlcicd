using System.Linq;
using Microsoft.SqlServer.Management.SqlParser.Parser;

namespace sqlcicd.Syntax
{
    /// <summary>
    /// Microsoft SQL Server grammar checker
    /// </summary>
    public class MssqlGrammarChecker : IGrammarChecker
    {
        public bool Check(string sql, out string errMsg)
        {
            var result = Parser.Parse(sql);
            
            if(result.Errors.Any())
            {
                errMsg = result.Errors
                    .Select(e => e.Message)
                    .Aggregate((prev, next) => $"{prev}\n{next}");
                return false;
            }
            else
            {
                errMsg = string.Empty;
                return true;
            }
        }
    }
}