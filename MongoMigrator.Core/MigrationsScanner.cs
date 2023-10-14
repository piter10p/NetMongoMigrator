using MongoMigrator.Core.Exceptions;
using System.Reflection;

namespace MongoMigrator.Core
{
    internal static class MigrationsScanner
    {
        public static IMigration[] GetDeclaredMigrations(Assembly assemblyToScan)
        {
            var migrationTypes = assemblyToScan.GetTypes().Where(x =>
                x.IsClass &&
                !x.IsAbstract &&
                typeof(IMigration).IsAssignableFrom(x)).ToArray();

            var migrations = migrationTypes.Select(x => (IMigration)Activator.CreateInstance(x)!).ToArray();

            if (migrations.GroupBy(x => x.Id).Any(g => g.Count() > 1))
                throw new MultipleMigrationsWithSameIdException();

            return migrations;
        }
    }
}
