IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219164424_Initial'
)
BEGIN
    CREATE TABLE [Conferences] (
        [ConferenceID] int NOT NULL IDENTITY,
        [ConferenceName] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Conferences] PRIMARY KEY ([ConferenceID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219164424_Initial'
)
BEGIN
    CREATE TABLE [Teams] (
        [TeamID] int NOT NULL IDENTITY,
        [TeamName] nvarchar(100) NOT NULL,
        [Coach] nvarchar(max) NULL,
        [City] nvarchar(100) NOT NULL,
        [ConferenceID] int NULL,
        CONSTRAINT [PK_Teams] PRIMARY KEY ([TeamID]),
        CONSTRAINT [FK_Teams_Conferences_ConferenceID] FOREIGN KEY ([ConferenceID]) REFERENCES [Conferences] ([ConferenceID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219164424_Initial'
)
BEGIN
    CREATE TABLE [Players] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [Height] decimal(18,2) NOT NULL,
        [Weight] decimal(18,2) NOT NULL,
        [DateOfBirth] datetime2 NOT NULL,
        [Position] nvarchar(max) NULL,
        [PointsPerGame] decimal(18,2) NOT NULL,
        [ReboundsPerGame] decimal(18,2) NOT NULL,
        [AssistsPerGame] decimal(18,2) NOT NULL,
        [TeamID] int NULL,
        CONSTRAINT [PK_Players] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Players_Teams_TeamID] FOREIGN KEY ([TeamID]) REFERENCES [Teams] ([TeamID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219164424_Initial'
)
BEGIN
    CREATE INDEX [IX_Players_TeamID] ON [Players] ([TeamID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219164424_Initial'
)
BEGIN
    CREATE INDEX [IX_Teams_ConferenceID] ON [Teams] ([ConferenceID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219164424_Initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231219164424_Initial', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231220152012_FormatDateChange'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231220152012_FormatDateChange', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231220152622_PlayersDecimalsChange'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'Weight');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Players] ALTER COLUMN [Weight] decimal(5,1) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231220152622_PlayersDecimalsChange'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'ReboundsPerGame');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Players] ALTER COLUMN [ReboundsPerGame] decimal(3,1) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231220152622_PlayersDecimalsChange'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'PointsPerGame');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Players] ALTER COLUMN [PointsPerGame] decimal(3,1) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231220152622_PlayersDecimalsChange'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'Height');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Players] ALTER COLUMN [Height] decimal(3,2) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231220152622_PlayersDecimalsChange'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'AssistsPerGame');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Players] ALTER COLUMN [AssistsPerGame] decimal(3,1) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231220152622_PlayersDecimalsChange'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231220152622_PlayersDecimalsChange', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221150505_CorrectIdsNames'
)
BEGIN
    ALTER TABLE [Players] DROP CONSTRAINT [FK_Players_Teams_TeamID];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221150505_CorrectIdsNames'
)
BEGIN
    ALTER TABLE [Teams] DROP CONSTRAINT [FK_Teams_Conferences_ConferenceID];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221150505_CorrectIdsNames'
)
BEGIN
    EXEC sp_rename N'[Teams].[ConferenceID]', N'ConferenceId', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221150505_CorrectIdsNames'
)
BEGIN
    EXEC sp_rename N'[Teams].[TeamID]', N'Id', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221150505_CorrectIdsNames'
)
BEGIN
    EXEC sp_rename N'[Teams].[IX_Teams_ConferenceID]', N'IX_Teams_ConferenceId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221150505_CorrectIdsNames'
)
BEGIN
    EXEC sp_rename N'[Players].[TeamID]', N'TeamId', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221150505_CorrectIdsNames'
)
BEGIN
    EXEC sp_rename N'[Players].[IX_Players_TeamID]', N'IX_Players_TeamId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221150505_CorrectIdsNames'
)
BEGIN
    EXEC sp_rename N'[Conferences].[ConferenceID]', N'Id', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221150505_CorrectIdsNames'
)
BEGIN
    ALTER TABLE [Players] ADD CONSTRAINT [FK_Players_Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Teams] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221150505_CorrectIdsNames'
)
BEGIN
    ALTER TABLE [Teams] ADD CONSTRAINT [FK_Teams_Conferences_ConferenceId] FOREIGN KEY ([ConferenceId]) REFERENCES [Conferences] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221150505_CorrectIdsNames'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231221150505_CorrectIdsNames', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222150439_AddedForeignKeysAsProps'
)
BEGIN
    ALTER TABLE [Players] DROP CONSTRAINT [FK_Players_Teams_TeamId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222150439_AddedForeignKeysAsProps'
)
BEGIN
    ALTER TABLE [Teams] DROP CONSTRAINT [FK_Teams_Conferences_ConferenceId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222150439_AddedForeignKeysAsProps'
)
BEGIN
    DROP INDEX [IX_Teams_ConferenceId] ON [Teams];
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Teams]') AND [c].[name] = N'ConferenceId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Teams] DROP CONSTRAINT [' + @var5 + '];');
    EXEC(N'UPDATE [Teams] SET [ConferenceId] = 0 WHERE [ConferenceId] IS NULL');
    ALTER TABLE [Teams] ALTER COLUMN [ConferenceId] int NOT NULL;
    ALTER TABLE [Teams] ADD DEFAULT 0 FOR [ConferenceId];
    CREATE INDEX [IX_Teams_ConferenceId] ON [Teams] ([ConferenceId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222150439_AddedForeignKeysAsProps'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Teams]') AND [c].[name] = N'Coach');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Teams] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Teams] ALTER COLUMN [Coach] nvarchar(50) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222150439_AddedForeignKeysAsProps'
)
BEGIN
    DROP INDEX [IX_Players_TeamId] ON [Players];
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'TeamId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var7 + '];');
    EXEC(N'UPDATE [Players] SET [TeamId] = 0 WHERE [TeamId] IS NULL');
    ALTER TABLE [Players] ALTER COLUMN [TeamId] int NOT NULL;
    ALTER TABLE [Players] ADD DEFAULT 0 FOR [TeamId];
    CREATE INDEX [IX_Players_TeamId] ON [Players] ([TeamId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222150439_AddedForeignKeysAsProps'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Players]') AND [c].[name] = N'Position');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Players] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Players] ALTER COLUMN [Position] nvarchar(50) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222150439_AddedForeignKeysAsProps'
)
BEGIN
    ALTER TABLE [Players] ADD CONSTRAINT [FK_Players_Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Teams] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222150439_AddedForeignKeysAsProps'
)
BEGIN
    ALTER TABLE [Teams] ADD CONSTRAINT [FK_Teams_Conferences_ConferenceId] FOREIGN KEY ([ConferenceId]) REFERENCES [Conferences] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222150439_AddedForeignKeysAsProps'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231222150439_AddedForeignKeysAsProps', N'8.0.0');
END;
GO

COMMIT;
GO

