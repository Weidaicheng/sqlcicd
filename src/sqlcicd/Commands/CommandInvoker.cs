namespace sqlcicd.Commands
{
    /// <summary>
    /// Command invoker
    /// </summary>
    public class CommandInvoker
    {
        private ICommand _command;

        public CommandInvoker(ICommand command)
        {
            _command = command;
        }

        /// <summary>
        /// Command
        /// </summary>
        public ICommand Command
        {
            set
            {
                _command = value;
            }
        }

        /// <summary>
        /// Execute command
        /// </summary>
        public void Invoke(string[] args)
        {
            _command.Execute(args);
        }
    }
}