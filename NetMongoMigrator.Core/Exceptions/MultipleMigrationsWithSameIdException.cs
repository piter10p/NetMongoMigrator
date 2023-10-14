namespace NetMongoMigrator.Core.Exceptions
{
    public class MultipleMigrationsWithSameIdException : Exception
    {
        public MultipleMigrationsWithSameIdException()
            : base($"Multiple migrations with same id detected.")
        {
        }
    }
}
