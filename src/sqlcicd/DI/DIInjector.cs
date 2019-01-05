using System;
using DependencyInjection.InConsole.Injec;
using sqlcicd.Configuration;

namespace sqlcicd.DI
{
    public class DIInjector : Injector
    {
        public override void Inject()
        {
            string command;
            try
            {
                command = Singletons.GetCmd();
            }
            catch (ArgumentNullException)
            {
                command = CommandInjectorEnum.DEFAULT_CMD;
            }

            CommandInjectorEnum
                .GetInjector(command)
                .Inject(services);
        }
    }
}