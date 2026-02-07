# Books API (.NET Framework 4.8.1)

API REST desarrollada en ASP.NET Web API con arquitectura en capas y principios SOLID.

## Tecnologías
- .NET Framework 4.8.1
- Entity Framework 6
- SQL Server
- Unity (DI)

## Funcionalidades
- CRUD de Autores
- CRUD de Libros
- Validación de autor existente
- Límite máximo de libros configurable

## Endpoints principales
### Autores
- GET /api/authors
- GET /api/authors/{id}
- POST /api/authors
- PUT /api/authors/{id}
- DELETE /api/authors/{id}

### Libros
- GET /api/books
- GET /api/books/{id}
- POST /api/books
- PUT /api/books/{id}
- DELETE /api/books/{id}

## Configuración
El número máximo de libros se define en:
```xml
<add key="MaxBooksAllowed" value="100" />  
