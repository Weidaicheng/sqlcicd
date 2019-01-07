using DependencyInjection.InConsole.Injec;
using sqlcicd.Configuration;

namespace sqlcicd.DI
{
    public class DIInjector : Injector
    {
        public override void Inject()
        {
            var command = Singletons.Command.ToString();

            CommandInjectorEnum
                .GetInjector(command)
                .Inject(services);
        }
    }
}