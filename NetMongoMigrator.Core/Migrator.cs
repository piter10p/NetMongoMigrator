using Amazon.Runtime.Internal.Util;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NetMongoMigrator.Core.Entities;

namespace NetMongoMigrator.Core
{
    public class Migrator
    {
        private readonly MigratorConfiguration _migratorConfiguration;
        private readonly IMigration[] _migrations;
        private readonly ILogger<Migrator> _logger;

        public Migrator(
            MigratorConfiguration migratorConfiguration,
            IMigration[] migrations,
            ILogger<Migrator> logger)
        {
            _migratorConfiguration = migratorConfiguration;
            _migrations = migrations;
            _logger = logger;
        }

        public async Task ExecuteMigrations()
        {
            _logger.LogInformation("Connectiong to database.");
            var client = new MongoClient(_migratorConfiguration.ConnectionString);

            _logger.LogInformation($"Getting or creating '{_migratorConfiguration.DatabaseName}' database.");
            var database = client.GetDatabase(_migratorConfiguration.DatabaseName);

            var migrationsCollection = MigrationsCollectionGetter.GetOrCreateMigrationsCollection(database, _migratorConfiguration);
            var executedMigrations = ExecutedMigrationsGetter.GetExecutedMigrations(migrationsCollection);

            var migrationsToExecute = _migrations.Where(x => !executedMigrations.Contains(x.Id))
                .OrderBy(x => x.Id)
                .ToArray();

            _logger.LogInformation($"Detected {migrationsToExecute.Length} migrations to execute.");

            foreach (var migration in migrationsToExecute)
            {
                var migrationName = migration.GetType().Name;
                _logger.LogInformation($"Executing migration {migration.Id} '{migrationName}'");
                await migration.Up(database);
                migrationsCollection.InsertOne(new Migration(migration.Id, migrationName, DateTime.UtcNow));
            }

            _logger.LogInformation("Migrations executed.");
        }
    }
}
