using Microsoft.Extensions.DependencyInjection;
using sqlcicd.Commands.Entity;

namespace sqlcicd.DI
{
    /// <summary>
    /// Injector for different command
    /// </summary>
    public interface ICommandInjector
    {
        /// <summary>
        /// Inject
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="command"></param>
        void Inject(IServiceCollection services,
            Command command); // TODO: remove Command argument, after the property injection is supported.
    }
}