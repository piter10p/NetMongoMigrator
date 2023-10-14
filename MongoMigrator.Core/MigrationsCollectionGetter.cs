using MongoDB.Driver;
using MongoMigrator.Core.Entities;
using MongoMigrator.Core.Extensions;

namespace MongoMigrator.Core
{
    internal static class MigrationsCollectionGetter
    {
        public static IMongoCollection<Migration> GetOrCreateMigrationsCollection(
            IMongoDatabase mongoDatabase, MigratorConfiguration configuration)
        {
            if (!mongoDatabase.CollectionExists(configuration.MigrationsTableName))
            {
                Console.WriteLine("No migrations table. Creating.");
                mongoDatabase.CreateCollection(configuration.MigrationsTableName);
            }

            return mongoDatabase.GetCollection<Migration>(configuration.MigrationsTableName);
        }
    }
}
