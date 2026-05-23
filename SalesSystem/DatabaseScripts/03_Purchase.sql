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

CREATE TABLE [CompraCab] (
    [Id_CompraCab] int NOT NULL IDENTITY,
    [FecRegistro] datetime2 NOT NULL,
    [SubTotal] decimal(10,2) NOT NULL,
    [Igv] decimal(10,2) NOT NULL,
    [Total] decimal(10,2) NOT NULL,
    CONSTRAINT [PK_CompraCab] PRIMARY KEY ([Id_CompraCab])
);
GO

CREATE TABLE [MovimientoCab] (
    [Id_MovimientoCab] int NOT NULL IDENTITY,
    [Fec_registro] datetime2 NOT NULL,
    [Id_TipoMovimiento] int NOT NULL,
    [Id_DocumentoOrigen] int NOT NULL,
    CONSTRAINT [PK_MovimientoCab] PRIMARY KEY ([Id_MovimientoCab])
);
GO

CREATE TABLE [CompraDet] (
    [Id_CompraDet] int NOT NULL IDENTITY,
    [Id_CompraCab] int NOT NULL,
    [Id_producto] int NOT NULL,
    [Cantidad] int NOT NULL,
    [Precio] decimal(18,2) NOT NULL,
    [Sub_Total] decimal(18,2) NOT NULL,
    [Igv] decimal(18,2) NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_CompraDet] PRIMARY KEY ([Id_CompraDet]),
    CONSTRAINT [FK_CompraDet_CompraCab_Id_CompraCab] FOREIGN KEY ([Id_CompraCab]) REFERENCES [CompraCab] ([Id_CompraCab]) ON DELETE CASCADE
);
GO

CREATE TABLE [MovimientoDet] (
    [Id_MovimientoDet] int NOT NULL IDENTITY,
    [Id_movimientocab] int NOT NULL,
    [Id_Producto] int NOT NULL,
    [Cantidad] int NOT NULL,
    CONSTRAINT [PK_MovimientoDet] PRIMARY KEY ([Id_MovimientoDet]),
    CONSTRAINT [FK_MovimientoDet_MovimientoCab_Id_movimientocab] FOREIGN KEY ([Id_movimientocab]) REFERENCES [MovimientoCab] ([Id_MovimientoCab]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CompraDet_Id_CompraCab] ON [CompraDet] ([Id_CompraCab]);
GO

CREATE INDEX [IX_MovimientoDet_Id_movimientocab] ON [MovimientoDet] ([Id_movimientocab]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260522172453_InitialPurchase', N'8.0.27');
GO

COMMIT;
GO

