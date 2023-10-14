using MongoMigrator.Console;

var consoleMigrator = new ConsoleMigrator();
return consoleMigrator.Run(args, typeof(Program).Assembly);
