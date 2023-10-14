using MongoDB.Driver;

namespace NetMongoMigrator.Core
{
    public interface IMigration
    {
        public int Id { get; }

        public Task Up(IMongoDatabase mongoDatabase);
    }
}
