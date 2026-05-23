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

CREATE TABLE [Productos] (
    [Id_producto] int NOT NULL IDENTITY,
    [Nombre_producto] nvarchar(max) NOT NULL,
    [NroLote] nvarchar(max) NOT NULL,
    [Fec_registro] datetime2 NOT NULL,
    [Costo] decimal(10,2) NOT NULL,
    [PrecioVenta] decimal(10,2) NOT NULL,
    CONSTRAINT [PK_Productos] PRIMARY KEY ([Id_producto])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260522073715_InitialProduct', N'8.0.27');
GO

COMMIT;
GO

