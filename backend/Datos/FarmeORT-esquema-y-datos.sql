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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111015712_Inicial'
)
BEGIN
    CREATE TABLE [Skills] (
        [Id] int NOT NULL IDENTITY,
        [Codigo] nvarchar(10) NOT NULL,
        [Nombre] nvarchar(50) NOT NULL,
        [Categoria] nvarchar(30) NOT NULL,
        [Activa] bit NOT NULL,
        CONSTRAINT [PK_Skills] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111015712_Inicial'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Activa', N'Categoria', N'Codigo', N'Nombre') AND [object_id] = OBJECT_ID(N'[Skills]'))
        SET IDENTITY_INSERT [Skills] ON;
    EXEC(N'INSERT INTO [Skills] ([Id], [Activa], [Categoria], [Codigo], [Nombre])
    VALUES (1, CAST(1 AS bit), N''Gestos'', N''GEST01'', N''Gesto épico''),
    (2, CAST(1 AS bit), N''Vocales'', N''VOCA01'', N''Grito de guerra''),
    (3, CAST(1 AS bit), N''Baile'', N''BAIL01'', N''Paso prohibido''),
    (4, CAST(0 AS bit), N''Memes'', N''MEME01'', N''Meme a destiempo'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Activa', N'Categoria', N'Codigo', N'Nombre') AND [object_id] = OBJECT_ID(N'[Skills]'))
        SET IDENTITY_INSERT [Skills] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111015712_Inicial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Skills_Codigo] ON [Skills] ([Codigo]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261111015712_Inicial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261111015712_Inicial', N'10.0.15');
END;

COMMIT;
GO

