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

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260522044853_InitialCreate', N'8.0.27');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO


                IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE UserName = 'admin')
                BEGIN
                    INSERT INTO dbo.Users (UserName, PasswordHash, Role, CreatedAt)
                    VALUES ('admin', '$2a$11$Ad4mVpUfjuIC.5IZJWCtAe21Jw9i4a9IEQTCPJ2eb5JPcX5IH/oLO', 'Admin', GETDATE());
                END
                ELSE
                BEGIN
                    UPDATE dbo.Users 
                    SET PasswordHash = '$2a$11$Ad4mVpUfjuIC.5IZJWCtAe21Jw9i4a9IEQTCPJ2eb5JPcX5IH/oLO',
                        Role = 'Admin'
                    WHERE UserName = 'admin';
                END
            
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260522072944_CheckAndSeedAdminUser', N'8.0.27');
GO

COMMIT;
GO

