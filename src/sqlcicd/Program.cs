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
            // try
            // {
            // save args
            Singletons.Args = args;

            // Startup
            var provider = Startup.ConfigureServices();

            // execute
            var command = provider.GetRequiredService<ICommand>();
            var invoker = new CommandInvoker(command);
            var result = await invoker.Invoke();

            return result.Success ? 0 : -1;
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine(ex.Message);
            //     return -1;
            // }
        }
    }
}
