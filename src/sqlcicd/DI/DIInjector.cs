using DependencyInjection.InConsole.Injec;
using sqlcicd.Configuration;

namespace sqlcicd.DI
{
    public class DIInjector : Injector
    {
        public override void Inject()
        {
            var command = Singletons.Args.Length == 0 ? CommandInjectorEnum.DEFAULT_CMD : Singletons.Args[0];

            CommandInjectorEnum
                .GetInjector(command)
                .Inject(services);
        }
    }
}