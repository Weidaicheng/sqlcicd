using System;
using System.Reflection;

namespace sqlcicd.Help
{
    /// <summary>
    /// Display for Version
    /// </summary>
    public class VersionCMDHelpDisplay : ICMDHelpDisplay
    {
        public void Display()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Console.WriteLine(version);
        }
    }
}