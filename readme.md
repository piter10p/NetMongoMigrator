# NetMongoMigrator

MongoDB migration library for .NET


## Getting Started

Migrator executes script-based migrations defined by classes implementing interface `IMigration`:


    using MongoDB.Driver;
    using NetMongoMigrator.Core;

    namespace NetMongoMigrator.Console.Example.Migrations
    {
        internal class Migration_0001_Example : IMigration
        {
            public int Id => 1;

            public async Task Up(IMongoDatabase mongoDatabase)
            {
                await mongoDatabase.CreateCollectionAsync("ExampleCollection1");
                await mongoDatabase.CreateCollectionAsync("ExampleCollection2");
                await mongoDatabase.CreateCollectionAsync("ExampleCollection3");
            }
        }
    }

Migration definition:
* `Id` - have to be unique. Migrator executes migrations sorting them ascending by id.
* `Up(IMongoDatabase mongoDatabase)` - method called when migration is executed.

Currently library allows you to launch migrations in two ways.
* Using core component directly
* Using console implementation


### I. Using core component directly

Install nuget package `NetMongoMigrator.Core`.

Prepare migrator configuration class `MigratorConfiguration`:

    var configuration = new MigratorConfiguration
    {
        MigrationsTableName = "Example Migrations Table Name",
        ConnectionString = "Example Connection String",
        DatabaseName = "Example Database Name"
    };

Build migrator:

    var migrator = new MigratorBuilder()
        .SetConfiguration(configuration)
        .SetMigrations(new [] { new Migration() })
        .SetLogger(logger)
        .Build();

You can also scan assembly for migrations:

    var migrator = new MigratorBuilder()
        .SetConfiguration(configuration)
        .AddMigrationsFromAssembly(typeof(Program).Assembly)
        .SetLogger(logger)
        .Build();

⚠️ If you not specify configuration, migrations or logger code will throw `MigratorNotConfiguredCorrectlyException`.

Then run migrator:

    await migrator.ExecuteMigrations();


### II. Using console implementation

Create new console project and install nuget package `NetMongoMigrator.Console`.

Inside `Program.cs` launch migrator:

    using NetMongoMigrator.Console;

    var consoleMigrator = new ConsoleMigrator();
    return consoleMigrator.Run(args, typeof(Program).Assembly);

Migrator will automaticly scan for migrations inside specified assembly.

You can find example in `Examples/NetMongoMigrator.Console.Example`.

To launch migrator compile your console app and lanuch it:

    migratorApp.exe up [connection string] [database name]


## Plans for future:

1. Migrating up to specified migration identifier
1. Migrating down to specified migration identifier
1. NetMongoMigrator.MongoDbDriverExtensions - extensions for `MongoDb.Driver` making migrations easier to implement
