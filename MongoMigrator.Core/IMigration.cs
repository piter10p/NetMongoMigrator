using MongoDB.Driver;

namespace MongoMigrator.Core
{
    public interface IMigration
    {
        public int Id { get; }

        public Task Up(IMongoDatabase mongoDatabase);
    }
}
