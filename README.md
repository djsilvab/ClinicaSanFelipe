
# SalesSystem - FullStack (Microservices + Angular)

Sistema FullStack desarrollado como prueba técnica utilizando arquitectura de microservicios en .NET 8 y Frontend SPA en Angular, orientado a la gestión de compras, ventas, productos y Kardex (movimientos de stock).

El sistema implementa autenticación basada en JWT, arquitectura desacoplada, Entity Framework Core (Code First), SQL Server en Docker, diseño responsivo tipo ERP y control de stock basado en movimientos.

-------------------------------------------------------------------------------

# Arquitectura General

El sistema fue desarrollado bajo una arquitectura FullStack desacoplada.

Angular Frontend (Angular 19)
    |
    | HTTP REST + JWT
    |
.NET 8 Microservices
    |
    |-- Auth.Api
    |-- Product.Api
    |-- Purchase.Api
    |-- Sales.Api
    |-- Kardex.Api
    |
SQL Server Docker + Entity Framework Core

-------------------------------------------------------------------------------

# Backend - Microservicios (.NET 8)

El backend fue implementado utilizando ASP.NET Core Web API (.NET 8) siguiendo una arquitectura desacoplada basada en microservicios.

## Auth.Api

Responsable de:
- Login
- Generación JWT
- Seguridad de acceso
- Roles

## Product.Api

Responsable de:
- Registrar producto
- Actualizar producto
- Listar productos

## Purchase.Api

Responsable de:
- Registrar compras
- CompraCab
- CompraDet
- Movimiento Entrada

## Sales.Api

Responsable de:
- Registrar ventas
- Validación de stock
- VentaCab
- VentaDet
- Movimiento Salida

## Kardex.Api

Responsable de:
- Consulta de movimientos
- Historial de stock
- Kardex de productos

-------------------------------------------------------------------------------

# Frontend - Angular

El frontend fue desarrollado utilizando Angular 19 siguiendo una arquitectura modular escalable.

## Funcionalidades implementadas

### Login JWT

- Login autenticado
- Persistencia token JWT
- HTTP Interceptor
- Route Guards
- Logout seguro

### Compras

Permite:
- Registrar compra
- Agregar múltiples productos
- Modal de registro producto
- Cálculo subtotal, IGV y total
- Actualización automática de costo
- Actualización automática de precio venta

Al registrar:
- CompraCab
- CompraDet
- Movimiento tipo Entrada

### Ventas

Permite:
- Registrar múltiples productos
- Mostrar stock disponible
- Mostrar precio venta
- Validar stock
- Restringir cantidad mayor al stock
- Calcular:
    * Subtotal
    * IGV
    * Total

Al registrar:
- VentaCab
- VentaDet
- Movimiento tipo Salida

### Kardex

Permite visualizar:
- Id Producto
- Nombre Producto
- Stock actual
- Costo
- Precio venta

Incluye:
- Modal historial movimientos
- Entradas y salidas
- Fecha registro
- Tipo movimiento
- Cantidad
- Documento origen

### Navbar ERP

Incluye navegación superior:
- Compras
- Ventas
- Kardex
- Logout

-------------------------------------------------------------------------------

# Arquitectura Frontend

src/app
|
|-- core
|   |-- guards
|   |-- interceptors
|   |-- models
|   |-- services
|
|-- features
|   |-- auth
|   |-- purchase
|   |-- sale
|   |-- kardex
|
|-- shared
|   |-- layout
|
|-- app.routes.ts

-------------------------------------------------------------------------------

# Tecnologías Utilizadas

## Backend

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- SQL Server
- Docker Desktop
- Swagger / OpenAPI
- JWT Authentication
- CORS
- Visual Studio 2022

## Frontend

- Angular 19
- TypeScript
- Angular Material
- Bootstrap 5
- RxJS
- SweetAlert2
- SCSS Responsive UI
- JWT Interceptor
- Route Guards

-------------------------------------------------------------------------------

# Patrones de Diseño Aplicados

## Facade Pattern

Implementado para centralizar lógica compleja.

Ejemplo:
- PurchaseFacade
- SaleFacade

Beneficios:
- Menor acoplamiento
- Código mantenible
- Separación de responsabilidades

## Mapper Pattern

Aplicado para transformación de entidades y DTOs.

Ejemplo:
- ProductMapper
- PurchaseMapper
- SaleMapper

Beneficios:
- Evita acoplamiento
- Código limpio
- Separación entre capas

## Principios SOLID

S - Single Responsibility Principle
Cada clase tiene responsabilidad única.

O - Open/Closed Principle
Extensible sin modificar implementaciones existentes.

L - Liskov Substitution Principle
Uso correcto de interfaces.

I - Interface Segregation Principle
Interfaces específicas.

D - Dependency Inversion Principle
Uso de Inyección de Dependencias.

-------------------------------------------------------------------------------

# Seguridad JWT

El sistema utiliza JWT Bearer Token.

Características:
- Expiración configurable
- Interceptor Angular automático
- Protección de APIs
- Route Guards

Login Endpoint:

POST /api/auth/login

Request:

{
  "userName": "admin",
  "password": "Admin123"
}

Response:

{
  "token": "jwt-token",
  "expiration": "2026-05-22T07:02:31"
}

-------------------------------------------------------------------------------

# Base de Datos

La base de datos se ejecuta en SQL Server Docker.

## Levantar SQL Server

docker compose up -d

Puerto:
2434

Connection String:

Server=localhost,2434;
Database=SalesSystemDB;
User Id=sa;
Password=SecurePass2026!;
TrustServerCertificate=True;

-------------------------------------------------------------------------------

# Migraciones EF Core

Crear migración:

Add-Migration InitialCreate

Aplicar migración:

Update-Database

-------------------------------------------------------------------------------

# Swagger

Cada API expone Swagger.

Ejemplo:

https://localhost:{PORT}/swagger

-------------------------------------------------------------------------------

# CORS

El sistema restringe acceso únicamente al frontend autorizado.

Origen permitido:

http://localhost:4200

-------------------------------------------------------------------------------

# APIs Implementadas

Productos
- Registrar Producto
- Actualizar Producto
- Listar Productos

Compras
- Registrar Compra
- Listar Compras

Ventas
- Registrar Venta
- Validar Stock
- Listar Ventas

Kardex
- Listar Kardex
- Consultar Movimientos

-------------------------------------------------------------------------------

# Reglas de Negocio

## Compra

Al registrar una compra:

- Se registra CompraCab
- Se registra CompraDet
- Se genera Movimiento Entrada
- Se actualiza costo producto
- Se actualiza precio venta

Formula:

Costo * 1.35

## Venta

Al registrar venta:

- Se valida stock
- Se registra VentaCab
- Se registra VentaDet
- Se genera Movimiento Salida

Restricción:

Cantidad > Stock disponible = No permitido

-------------------------------------------------------------------------------

# Scripts SQL

Ubicación:

SalesSystem/DatabaseScripts/

Archivos:
- 01_Auth.sql
- 02_Product.sql
- 03_Purchase.sql
- 04_Sales.sql
- 99_FullDatabase.sql
- CRUD_Queries.sql

-------------------------------------------------------------------------------

# Postman Collection

Ubicación:

SalesSystem/Postman/

Archivo:
SalesSystem_Postman_Collection.json

Pasos:
1. Ejecutar Login
2. Guardar token JWT
3. Consumir APIs protegidas

-------------------------------------------------------------------------------

# Instalación del Proyecto

## 1. Clonar Repositorio

git clone <repository-url>

## 2. Levantar Base de Datos

docker compose up -d

## 3. Ejecutar Backend

dotnet run

Swagger:
https://localhost:{PORT}/swagger

## 4. Ejecutar Frontend

npm install

ng serve

Frontend:
http://localhost:4200

-------------------------------------------------------------------------------

# Evidencias del Desarrollo

Se incluye:

- Repositorio GitHub
- Video demostrativo
- Evidencias funcionales
- Explicación técnica
- Pruebas de endpoints

-------------------------------------------------------------------------------

# Estructura del Proyecto

SalesSystem
|
|-- Backend
|   |-- Auth.Api
|   |-- Product.Api
|   |-- Purchase.Api
|   |-- Sales.Api
|   |-- Kardex.Api
|
|-- Frontend
|   |-- AngularApp
|
|-- DatabaseScripts
|
|-- Postman
|
|-- README.md

-------------------------------------------------------------------------------

# Autor

David Silva Bazán

Ingeniero de Sistemas
Backend Developer .NET / FullStack Developer
