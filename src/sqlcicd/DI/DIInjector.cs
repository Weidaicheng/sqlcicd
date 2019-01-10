using DependencyInjection.InConsole.Injec.Generic;
using Microsoft.Extensions.DependencyInjection;
using sqlcicd.Commands;

namespace sqlcicd.DI
{
    public class DIInjector : Injector<string[]>
    {
        public override void Inject(string[] t)
        {
            //var command = Singletons.Command.ToString();
            // inject Command
            var command = new CommandFactory().GenerateCommand(t);
//            command.OriginalCommand = t.Length == 0 ? string.Empty : t.Aggregate((prev, next) => $"{prev} {next}");
            services.AddSingleton(provider => command);

            CommandInjectorEnum
                .GetInjector(command.ToString())
                .Inject(services, command);
        }
    }
}