using System;
using System.Threading.Tasks;
using DependencyInjection.InConsole;
using Microsoft.Extensions.DependencyInjection;
using sqlcicd.Commands;

namespace sqlcicd
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            try
            {
                // Startup
                var provider = Startup.ConfigureServices(args);

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
