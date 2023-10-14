using Spectre.Console.Cli;

namespace MongoMigrator.Console.Up
{
    internal class UpSettings : CommandSettings
    {
        [CommandArgument(0, "[Database Connection String]")]
        public string DatabaseConnectionString { get; set; } = string.Empty;
        [CommandArgument(1, "[Database Name]")]
        public string DatabaseName { get; set; } = string.Empty;
    }
}
