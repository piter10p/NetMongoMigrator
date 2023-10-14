using MongoDB.Driver;
using MongoMigrator.Core;

namespace MongoMigrator.Console.Example.Migrations
{
    internal class Migration_0002_DeleteCollection : IMigration
    {
        public int Id => 1;

        public async Task Up(IMongoDatabase mongoDatabase)
        {
            await mongoDatabase.DropCollectionAsync("TestCollection2");
        }
    }
}
