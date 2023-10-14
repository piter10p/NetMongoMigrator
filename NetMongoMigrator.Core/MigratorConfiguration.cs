namespace NetMongoMigrator.Core
{
    public class MigratorConfiguration
    {
        public string MigrationsTableName { get; init; } = "Migrations";
        public string ConnectionString { get; init; } = string.Empty;
        public string DatabaseName { get; init; } = string.Empty;
    }
}
