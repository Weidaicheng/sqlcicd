using DependencyInjection.InConsole.Injec;
using sqlcicd.Configuration;

namespace sqlcicd.DI
{
    public class DIInjector : Injector
    {
        public override void Inject()
        {
            string command;
            if (Singletons.Args.Length == 0)
            {
                command = CommandInjectorEnum.DEFAULT_CMD; // default command
            }
            else
            {
                command = Singletons.Args[0];
            }

            CommandInjectorEnum
                .GetInjector(command)
                .Inject(services);
        }
    }
}