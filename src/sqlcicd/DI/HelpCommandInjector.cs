using Microsoft.Extensions.DependencyInjection;
using sqlcicd.Commands;
using sqlcicd.Commands.Entity;
using sqlcicd.Configuration;
using sqlcicd.Exceptions;
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
            // sub help
            switch (Singletons.GetSubCmd())
            {
                case CommandEnum.HELP_CMD:
                case CommandEnum.HELP_CMD_SHORT:
                    services.AddTransient<ICMDHelpDisplay, GlobalCMDHelpDisplay>();
                    break;
                case CommandEnum.INTEGRATE_CMD:
                case CommandEnum.INTEGRATE_CMD_SHORT:
                    services.AddTransient<ICMDHelpDisplay, IntegrateCMDHelpDisplay>();
                    break;
                case CommandEnum.DELIVERY_CMD:
                case CommandEnum.DELIVERY_CMD_SHORT:
                    services.AddTransient<ICMDHelpDisplay, DeliveryCMDHelpDisplay>();
                    break;
                default:
                    throw new UnSupportedCommandException(
                        $"Command {Singletons.GetSubCmd()} is not supported, please try again");
            }

            // add command
            services.AddTransient<ICommand, CMDHelpCommand>();
        }
    }
}