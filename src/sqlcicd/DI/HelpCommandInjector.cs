using DependencyInjection.InConsole.Attributes;
using Microsoft.Extensions.DependencyInjection;
using sqlcicd.Commands;
using sqlcicd.Commands.Entity;
using sqlcicd.Exceptions;
using sqlcicd.Help;

namespace sqlcicd.DI
{
    /// <summary>
    /// DI for help
    /// </summary>
    public class HelpCommandInjector : ICommandInjector
    {
        [Inject] public Command Command { get; set; }
        
        public void Inject(IServiceCollection services)
        {
            // sub help
            switch (Command.MainCommand)
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
                case CommandEnum.VERSION_CMD:
                case CommandEnum.VERSION_CMD_SHORT:
                    services.AddTransient<ICMDHelpDisplay, VersionCMDHelpDisplay>();
                    break;
                default:
                    throw new UnSupportedCommandException(
                        $"Command {Command.MainCommand} is not supported, please try again");
            }

            // add command
            services.AddTransient<ICommand, CMDHelpCommand>();
        }
    }
}