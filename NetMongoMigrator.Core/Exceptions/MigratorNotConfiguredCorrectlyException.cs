namespace NetMongoMigrator.Core.Exceptions
{
    public class MigratorNotConfiguredCorrectlyException : Exception
    {
        public MigratorNotConfiguredCorrectlyException(string reason)
            : base($"Migrator is not configured correctly. Reason: {reason}")
        {
            Reason = reason;
        }

        public string Reason { get; }
    }
}
