using MongoDB.Driver;
using NetMongoMigrator.Core;

namespace NetMongoMigrator.Console.Example.Migrations
{
    internal class Migration_0002_DeleteCollection : IMigration
    {
        public int Id => 2;

        public async Task Up(IMongoDatabase mongoDatabase)
        {
            await mongoDatabase.DropCollectionAsync("TestCollection2");
        }
    }
}
