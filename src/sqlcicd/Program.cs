using System;
using System.Threading.Tasks;
using DependencyInjection.InConsole;
using Microsoft.Extensions.DependencyInjection;
using sqlcicd.Commands;
using sqlcicd.Configuration;
using sqlcicd.Exceptions;

namespace sqlcicd
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            // save args
            Singletons.Args = args;

            // Startup
            var provider = Startup.ConfigureServices();

            // execute
            var command = provider.GetRequiredService<ICommand>();
            ICommandInvoker invoker = new CMDCommandInvoker(command);
            var result = await invoker.Invoke();

            return result.Success ? 0 : -1;
        }
    }
}
