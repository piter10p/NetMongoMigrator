using MongoDB.Driver;
using MongoMigrator.Core.Entities;

namespace MongoMigrator.Core
{
    internal static class ExecutedMigrationsGetter
    {
        public static int[] GetExecutedMigrations(IMongoCollection<Migration> migrationsCollection)
        {
            var migrations = migrationsCollection.Find(Builders<Migration>.Filter.Empty).ToList();
            return migrations.Select(x => x.Id).ToArray();
        }
    }
}
