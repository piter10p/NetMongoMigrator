using Microsoft.Extensions.Logging;
using NetMongoMigrator.Core;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;

namespace NetMongoMigrator.Console.Up
{
    internal class UpCommand : Command<UpSettings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] UpSettings settings)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            builder.AddConsole());

            var logger = loggerFactory.CreateLogger<Migrator>();

            var configuration = new MigratorConfiguration
            {
                ConnectionString = settings.DatabaseConnectionString,
                DatabaseName = settings.DatabaseName
            };

            var migrator = new MigratorBuilder()
                .SetConfiguration(configuration)
                .AddMigrationsFromAssembly(ConsoleMigratorSettings.AssemblyToScan!)
                .SetLogger(logger)
                .Build();

            var migrateTask = migrator.ExecuteMigrations();
            migrateTask.Wait();

            return 0;
        }
    }
}
