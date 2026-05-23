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
