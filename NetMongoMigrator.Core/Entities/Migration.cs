namespace NetMongoMigrator.Core.Entities
{
    internal record Migration(
        int Id,
        string Name,
        DateTime ExecutionDateTimeUtc);
}
