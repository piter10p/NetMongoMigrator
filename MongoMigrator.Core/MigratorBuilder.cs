using Microsoft.Extensions.Logging;
using MongoMigrator.Core.Exceptions;
using System.Reflection;

namespace MongoMigrator.Core
{
    public class MigratorBuilder
    {
        private MigratorConfiguration? _migratorConfiguration;
        private IMigration[]? _migrations;
        private ILogger<Migrator>? _logger;

        public MigratorBuilder SetConfiguration(MigratorConfiguration migratorConfiguration)
        {
            if (string.IsNullOrEmpty(migratorConfiguration.MigrationsTableName))
                throw new ArgumentException("MigrationsTableName can not be null or empty.", nameof(migratorConfiguration));

            if (string.IsNullOrEmpty(migratorConfiguration.ConnectionString))
                throw new ArgumentException("ConnectionString can not be null or empty.", nameof(migratorConfiguration));

            if (string.IsNullOrEmpty(migratorConfiguration.DatabaseName))
                throw new ArgumentException("DatabaseName can not be null or empty.", nameof(migratorConfiguration));

            _migratorConfiguration = migratorConfiguration;
            return this;
        }

        public MigratorBuilder AddMigrationsFromAssembly(Assembly assemblyToScan)
        {
            _migrations = MigrationsScanner.GetDeclaredMigrations(assemblyToScan);
            return this;
        }

        public MigratorBuilder SetMigrations(IMigration[] migrations)
        {
            _migrations = migrations;
            return this;
        }

        public MigratorBuilder SetLogger(ILogger<Migrator> logger)
        {
            _logger = logger;
            return this;
        }

        public Migrator Build()
        {
            if (_migratorConfiguration is null)
                throw new MigratorNotConfiguredCorrectlyException("Migrator configuration not set.");

            if (_migrations is null)
                throw new MigratorNotConfiguredCorrectlyException("Migrations not set.");

            if (_logger is null)
                throw new MigratorNotConfiguredCorrectlyException("Logger not set.");

            return new Migrator(_migratorConfiguration, _migrations, _logger);
        }
    }
}
