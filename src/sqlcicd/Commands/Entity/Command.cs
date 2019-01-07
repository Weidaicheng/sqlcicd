namespace sqlcicd.Commands.Entity
{
    /// <summary>
    /// Command entity
    /// </summary>
    public class Command
    {
        /// <summary>
        /// first command, ex: --integrate
        /// </summary>
        public string MainCommand { get; set; }

        /// <summary>
        /// second command, ex: --help
        /// </summary>
        public string SubCommand { get; set; }

        /// <summary>
        /// path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Get command key, style: 'MainCommand,SubCommand'
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{MainCommand},{SubCommand}";
        }
    }
}