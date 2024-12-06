# LearningKurrent.Infrastructure

Provides an implementation of a relational event store to be used with the Kurrent learning project, Entity Framework Core and Microsoft SQL Server.

## Migrations

This project is setup to use migrations. All the commands below must be executed in the solution directory.

### Create a migration

To create a new migration, execute the following command. Do not forget to provide a migration name!

```sh
dotnet ef migrations add <YOUR_MIGRATION_NAME> --context CommerceContext --project src/LearningKurrent.Infrastructure --startup-project src/LearningKurrent
```

### Remove a migration

To remove the latest unapplied migration, execute the following command.

```sh
dotnet ef migrations remove --context CommerceContext --project src/LearningKurrent.Infrastructure --startup-project src/LearningKurrent
```

### Generate a script

To generate a script, execute the following command. Do not forget to provide a source migration name!

```sh
dotnet ef migrations script <SOURCE_MIGRATION> --context CommerceContext --project src/LearningKurrent.Infrastructure --startup-project src/LearningKurrent
```