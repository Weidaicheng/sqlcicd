using Microsoft.Extensions.DependencyInjection;
using sqlcicd.Commands;
using sqlcicd.Commands.Entity;
using sqlcicd.Configuration;
using sqlcicd.Help;

namespace sqlcicd.DI
{
    /// <summary>
    /// DI for help
    /// </summary>
    public class HelpCommandInjector : ICommandInjector
    {
        public void Inject(IServiceCollection services)
        {
            if ((Singletons.GetCmd().ToLower() != CommandEnum.HELP_CMD &&
                 Singletons.GetCmd().ToLower() != CommandEnum.HELP_CMD_SHORT) &&
                (Singletons.GetSubCmd().ToLower() == CommandEnum.HELP_CMD ||
                 Singletons.GetSubCmd().ToLower() == CommandEnum.HELP_CMD_SHORT))
            {
                // sub help
                switch (Singletons.GetCmd())
                {
                    case CommandEnum.INTEGRATE_CMD:
                    case CommandEnum.INTEGRATE_CMD_SHORT:
                        services.AddTransient<ICMDHelpDisplay, IntegrateCMDHelpDisplay>();
                        break;
                    case CommandEnum.DELIVERY_CMD:
                    case CommandEnum.DELIVERY_CMD_SHORT:
                        services.AddTransient<ICMDHelpDisplay, DeliveryCMDHelpDisplay>();
                        break;
                }
            }
            else
            {
                // global help
                services.AddTransient<ICMDHelpDisplay, GlobalCMDHelpDisplay>();
            }

            // add command
            services.AddTransient<ICommand, CMDHelpCommand>();
        }
    }
}