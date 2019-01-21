using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using sqlcicd.Commands;
using sqlcicd.Commands.Entity;
using sqlcicd.Configuration;
using sqlcicd.Configuration.Entity;
using sqlcicd.Database;
using sqlcicd.Files;
using sqlcicd.Repository;
using sqlcicd.Repository.Entity;
using sqlcicd.Syntax;

namespace sqlcicd.DI
{
    /// <summary>
    /// DI for continuous delivery
    /// </summary>
    public class DeliveryCommandInjector : ICommandInjector
    {
        public void Inject(IServiceCollection services)
        {
            services.AddTransient<IFileReader, FileReader>();
            services.AddTransient<ISqlConfigurationReader, WithRepoConfigurationReader>();
            services.AddTransient<ISysIgnoredFileProvider, WithRepoSysIgnoredFileProvider>();
            services.AddTransient<ISqlSelector, SqlSelector>();
            services.AddTransient<IDbPrepare, DbPrepare>();

            // add configurations
            services.AddTransient<SqlIgnoreConfiguration>(provider =>
            {
                var configurationReader = provider.GetRequiredService<ISqlConfigurationReader>();
                return configurationReader.GetSqlIgnoreConfiguration().GetAwaiter().GetResult();
            });
            services.AddTransient<SqlOrderConfiguration>(provider =>
            {
                var configurationReader = provider.GetRequiredService<ISqlConfigurationReader>();
                return configurationReader.GetSqlOrderConfiguration().GetAwaiter().GetResult();
            });
            services.AddTransient<BaseConfiguration>(provider =>
            {
                var configurationReader = provider.GetRequiredService<ISqlConfigurationReader>();
                return configurationReader.GetBaseConfiguration().GetAwaiter().GetResult();
            });

            var baseConfig = services
                .BuildServiceProvider()
                .GetRequiredService<BaseConfiguration>();
            switch (baseConfig.RepositoryType)
            {
                case RepositoryType.Git:
                    services.AddTransient<IRepository, GitRepository>();
                    break;
                // TODO: other cases
                default:
                    break;
            }
            switch(baseConfig.DbType)
            {
                case Database.Entity.DbType.Mssql:
                    services.AddTransient<IGrammarChecker, MssqlGrammarChecker>();
                    services.AddTransient<IDbConnection>(provider => new SqlConnection(baseConfig.ConnectionString));
                    services.AddTransient<DbNegotiator, MssqlNegotiator>();
                    break;
                // TODO: other cases
                default:
                    break;
            }

            // add command
            services.AddTransient<ICommand, DeliveryCommand>();
        }
    }
}