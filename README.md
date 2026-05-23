# SalesSystem - Backend API REST (.NET 8 Microservices)

Sistema Backend API REST desarrollado en **.NET 8** bajo arquitectura de **microservicios** para la gestión de:

- Compras
- Ventas
- Productos
- Movimientos (Kardex)

El sistema implementa autenticación basada en **JWT**, documentación Swagger, persistencia con **Entity Framework Core (Code First)** y base de datos **SQL Server en Docker**.

---

# Arquitectura del Proyecto

El proyecto fue diseñado bajo una arquitectura de **microservicios desacoplados**.

## Microservicios

### Auth.Api
Responsable de:

- Login
- Generación de JWT
- Seguridad de acceso
- Roles

### Product.Api
Responsable de:

- Registrar producto
- Actualizar producto
- Listar productos

### Purchase.Api
Responsable de:

- Registrar compra
- Listar compras
- Generación automática de movimientos de entrada

### Sales.Api
Responsable de:

- Registrar venta
- Validación de stock
- Listar ventas
- Kardex
- Generación automática de movimientos de salida

---

# Tecnologías utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- SQL Server
- Docker Desktop
- JWT Authentication
- Swagger / OpenAPI
- Postman
- CORS
- Visual Studio Community 2026

---

# Patrones de Diseño Aplicados

## Facade Pattern

Se implementó para centralizar lógica de negocio compleja.

Ejemplo:

- `PurchaseFacade`
- `SaleFacade`

Permite desacoplar controladores de la lógica de negocio.

## Decorator Pattern

Aplicado para extender responsabilidades sin modificar clases base.

## Principios SOLID

### S - Single Responsibility Principle
Cada servicio tiene una única responsabilidad.

### O - Open/Closed Principle
Extensible sin modificar implementaciones existentes.

### L - Liskov Substitution Principle
Interfaces desacopladas.

### I - Interface Segregation Principle
Interfaces específicas.

### D - Dependency Inversion Principle
Uso de Inyección de Dependencias.

---

# Seguridad JWT

El sistema utiliza autenticación basada en JWT.

Configuración:

- Expiración del token: **30 minutos**
- Algoritmo: **HS256**
- Roles implementados

### Login

POST:

```http
/api/auth/login
```

Request:

```json
{
  "userName": "admin",
  "password": "Admin123"
}
```

Response:

```json
{
  "token": "jwt-token",
  "expiration": "2026-05-22T07:02:31"
}
```

---

# Base de Datos

La base de datos se ejecuta en **Docker SQL Server**.

## Levantar SQL Server con Docker

Ejecutar:

```bash
docker compose up -d
```

Puerto:

```text
2434
```

Connection String:

```json
"ConnectionStrings": {
  "DefaultConnection":
  "Server=localhost,2434;Database=SalesSystemDB;User Id=sa;Password=SecurePass2026!;TrustServerCertificate=True;"
}
```

---

# Migraciones EF Core

Ejecutar migraciones:

```powershell
Update-Database
```

Generar migración:

```powershell
Add-Migration InitialCreate
```

---

# Swagger

Cada API expone Swagger.

Ejemplo:

```text
https://localhost:{PORT}/swagger
```

---

# CORS

El sistema restringe acceso únicamente al frontend autorizado.

Ejemplo:

```json
"Cors": {
  "AllowedOrigins": [
    "http://localhost:4200"
  ]
}
```

---

# APIs Implementadas

## Productos

- Registrar Producto
- Actualizar Producto
- Listar Producto

## Compras

- Registrar Compra
- Listar Compra

## Ventas

- Registrar Venta
- Listar Venta
- Validar Stock

## Kardex

- Listar Kardex

---

# Reglas de Negocio

## Compra

Al registrar una compra:

- Se registra CompraCab
- Se registra CompraDet
- Se genera Movimiento Entrada automáticamente

## Venta

Al registrar una venta:

- Se valida stock
- Se registra VentaCab
- Se registra VentaDet
- Se genera Movimiento Salida automáticamente

---

# Scripts SQL

Ubicación:

```text
SalesSystem/DatabaseScripts/
```

Archivos:

```text
01_Auth.sql
02_Product.sql
03_Purchase.sql
04_Sales.sql
99_FullDatabase.sql
CRUD_Queries.sql
```

---

# Postman Collection

Ubicación:

```text
SalesSystem/Postman/
SalesSystem_Postman_Collection.json
```

Pasos:

1. Ejecutar Login
2. Token JWT se guarda automáticamente
3. Consumir APIs protegidas

---

# Estructura del Proyecto

```text
SalesSystem
│
├── Services
│   ├── Auth.Api
│   ├── Product.Api
│   ├── Purchase.Api
│   └── Sales.Api
│
├── DatabaseScripts
│
├── Postman
│
└── README.md
```

---

# Autor

**David Silva Bazán**

Ingeniero de Sistemas  
Backend Developer .NET