using MongoDB.Driver;
using MongoMigrator.Core;

namespace MongoMigrator.Console.Example.Migrations
{
    internal class Migration_0001_Initial : IMigration
    {
        public int Id => 0;

        public async Task Up(IMongoDatabase mongoDatabase)
        {
            await mongoDatabase.CreateCollectionAsync("TestCollection1");
            await mongoDatabase.CreateCollectionAsync("TestCollection2");
            await mongoDatabase.CreateCollectionAsync("TestCollection3");
        }
    }
}
