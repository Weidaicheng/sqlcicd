using System;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.InConsole;
using Microsoft.Extensions.DependencyInjection;
using sqlcicd.Commands;
using sqlcicd.Configuration;

namespace sqlcicd
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            try
            {
                // save original command
                Singletons.SetOriginalCommand(args);
                // save command as singleton
                var cmdFactory = new CommandFactory();
                Singletons.Command = cmdFactory.GenerateCommand(args);

                // Startup
                var provider = Startup.ConfigureServices();

                // execute
                var command = provider.GetRequiredService<ICommand>();
                ICommandInvoker invoker = new CMDCommandInvoker(command);
                var result = await invoker.Invoke();

                return result.Success ? ProgramResult.Success : ProgramResult.Fail;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return ProgramResult.Fail;
            }
        }
    }
}
