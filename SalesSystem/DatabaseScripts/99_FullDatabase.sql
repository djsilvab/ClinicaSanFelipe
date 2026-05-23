/*=========================================================
  SALES SYSTEM DATABASE
  Autor: David Silva Bazán
  Tecnología: .NET 8 + EF Core + SQL Server + Docker
  Arquitectura: Microservicios
=========================================================*/

USE master;
GO

/*=========================================================
  CREAR BASE DE DATOS
=========================================================*/
IF DB_ID('SalesSystemDB') IS NULL
BEGIN
    CREATE DATABASE SalesSystemDB;
END
GO

USE SalesSystemDB;
GO

/*=========================================================
  AUTH MODULE
=========================================================*/

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

/*=========================================================
  PRODUCT MODULE
=========================================================*/


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

/*=========================================================
  PURCHASE MODULE
=========================================================*/



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

/*=========================================================
  SALES MODULE
=========================================================*/



BEGIN TRANSACTION;
GO

CREATE TABLE [VentaCab] (
    [Id_VentaCab] int NOT NULL IDENTITY,
    [fecRegistro] datetime2 NOT NULL,
    [SubTotal] decimal(18,2) NOT NULL,
    [Igv] decimal(18,2) NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_VentaCab] PRIMARY KEY ([Id_VentaCab])
);
GO

CREATE TABLE [VentaDet] (
    [Id_VentaDet] int NOT NULL IDENTITY,
    [Id_VentaCab] int NOT NULL,
    [Id_producto] int NOT NULL,
    [Cantidad] int NOT NULL,
    [Precio] decimal(18,2) NOT NULL,
    [Sub_Total] decimal(18,2) NOT NULL,
    [Igv] decimal(18,2) NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_VentaDet] PRIMARY KEY ([Id_VentaDet]),
    CONSTRAINT [FK_VentaDet_VentaCab_Id_VentaCab] FOREIGN KEY ([Id_VentaCab]) REFERENCES [VentaCab] ([Id_VentaCab]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_VentaDet_Id_VentaCab] ON [VentaDet] ([Id_VentaCab]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260522202829_InitialSales', N'8.0.27');
GO

COMMIT;
GO

/*=========================================================
  SEED DATA
=========================================================*/

/******************************************/
/* 1. PRODUCTOS - CRUD */
/******************************************/
/* INSERT */
INSERT INTO Productos
(
    Nombre_producto,
    NroLote,
    Fec_registro,
    Costo,
    PrecioVenta
)
VALUES
(
    'Laptop Lenovo',
    'LT-001',
    GETDATE(),
    2500.00,
    3200.00
);

/* LISTAR */
SELECT
    Id_producto,
    Nombre_producto,
    NroLote,
    Fec_registro,
    Costo,
    PrecioVenta
FROM Productos;

/* ACTUALIZAR */
UPDATE Productos
SET
    Nombre_producto = 'Laptop Lenovo i7',
    Costo = 2800.00,
    PrecioVenta = 3500.00
WHERE Id_producto = 1;

/* ELIMINAR */
DELETE FROM Productos
WHERE Id_producto = 1;

/******************************************/
/* 2. COMPRAS - INSERTAR/LISTAR */
/******************************************/
/* INSERT CABECERA */
INSERT INTO CompraCab
(
    FecRegistro,
    SubTotal,
    Igv,
    Total
)
VALUES
(
    GETDATE(),
    100,
    18,
    118
);

/* INSERT DETALLE */
INSERT INTO CompraDet
(
    Id_CompraCab,
    Id_producto,
    Cantidad,
    Precio,
    Sub_Total,
    Igv,
    Total
)
VALUES
(
    1,
    1,
    5,
    100,
    500,
    90,
    590
);

/* LISTAR COMPRAS */
SELECT
    c.Id_CompraCab,
    c.FecRegistro,
    c.SubTotal,
    c.Igv,
    c.Total,
    d.Id_producto,
    d.Cantidad,
    d.Precio
FROM CompraCab c
INNER JOIN CompraDet d
    ON c.Id_CompraCab =
       d.Id_CompraCab;

/******************************************/
/* 3. VENTAS - INSERTAR/LISTAR */
/******************************************/
/* INSERT CABECERA */
INSERT INTO VentaCab
(
    FecRegistro,
    SubTotal,
    Igv,
    Total
)
VALUES
(
    GETDATE(),
    200,
    36,
    236
);

/* INSERT DETALLE */
INSERT INTO VentaDet
(
    Id_VentaCab,
    Id_producto,
    Cantidad,
    Precio,
    Sub_Total,
    Igv,
    Total
)
VALUES
(
    1,
    1,
    2,
    100,
    200,
    36,
    236
);

/* LISTAR VENTAS */
SELECT
    v.Id_VentaCab,
    v.FecRegistro,
    v.SubTotal,
    v.Igv,
    v.Total,
    d.Id_producto,
    d.Cantidad,
    d.Precio
FROM VentaCab v
INNER JOIN VentaDet d
    ON v.Id_VentaCab =
       d.Id_VentaCab;

/******************************************/
/* 4. MOVIMIENTOS - LISTAR(KARDEX) */
/******************************************/
SELECT
    md.Id_Producto,
    mc.Fec_registro,

    CASE
        WHEN mc.Id_TipoMovimiento = 1
        THEN 'Entrada'
        ELSE 'Salida'
    END AS TipoMovimiento,

    md.Cantidad,
    mc.Id_DocumentoOrigen
FROM MovimientoDet md
INNER JOIN MovimientoCab mc
    ON md.Id_movimientocab =
       mc.Id_MovimientoCab
ORDER BY mc.Fec_registro;

/******************************************/
/* 5. STOCK ACTUAL */
/******************************************/
SELECT
    md.Id_Producto,

    SUM(
        CASE
            WHEN mc.Id_TipoMovimiento = 1
            THEN md.Cantidad
            ELSE -md.Cantidad
        END
    ) AS StockActual
FROM MovimientoDet md
INNER JOIN MovimientoCab mc
    ON md.Id_movimientocab =
       mc.Id_MovimientoCab
GROUP BY md.Id_Producto;

GO

PRINT 'SalesSystemDB creada correctamente';
GO