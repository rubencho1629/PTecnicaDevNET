# 📚 Books Application – Prueba Técnica .NET

Aplicación desarrollada como **prueba técnica para Desarrollador .NET**, que implementa una **API REST** y un **Front-End MVC**, utilizando **arquitectura en capas**, **principios SOLID**, **Entity Framework 6**, **SQL Server** y **buenas prácticas de desarrollo**.

---

## 📑 Tabla de Contenidos

- [Tecnologías Utilizadas](#️-tecnologías-utilizadas)
- [Arquitectura del Proyecto](#-arquitectura-del-proyecto)
- [Funcionalidades](#-funcionalidades)
- [Reglas de Negocio](#️-reglas-de-negocio-implementadas)
- [Base de Datos](#️-base-de-datos-sql-server)
- [Cómo Ejecutar](#️-cómo-ejecutar-el-proyecto)
- [API Endpoints](#-api-endpoints)
- [Pruebas](#-pruebas)

---

## 🛠️ Tecnologías Utilizadas

| Tecnología | Versión/Descripción |
|-----------|---------------------|
| **Lenguaje** | C# |
| **Framework** | .NET Framework 4.8.1 |
| **Back-End** | ASP.NET Web API |
| **Front-End** | ASP.NET MVC + Bootstrap 5 |
| **ORM** | Entity Framework 6 (Code First + Migrations) |
| **Base de Datos** | SQL Server |
| **IoC Container** | Unity |
| **Control de Versiones** | Git |

---

## 🧱 Arquitectura del Proyecto

La solución está organizada siguiendo una **arquitectura en capas**, separando responsabilidades y aplicando **principios SOLID**:

```
📦 Books.Api.sln
│
├── 📂 Books.Domain                    # Capa de Dominio
│   ├── Entities/                      # Entidades del negocio
│   ├── Interfaces/                    # Contratos de repositorios
│   └── Exceptions/                    # Excepciones de dominio
│
├── 📂 Books.Application               # Capa de Aplicación
│   ├── DTOs/                          # Data Transfer Objects
│   ├── Interfaces/                    # Contratos de servicios
│   ├── Services/                      # Lógica de negocio
│   └── Settings/                      # Configuraciones
│
├── 📂 Books.Infrastructure            # Capa de Infraestructura
│   ├── Persistence/                   # DbContext de EF
│   ├── Repositories/                  # Implementación de repositorios
│   └── Migrations/                    # Migraciones de BD
│
├── 📂 Books.Api                       # API REST
│   └── Controllers/                   # Controladores de API
│
└── 📂 Books.Front                     # Front-End MVC
    ├── Controllers/                   # Controladores MVC
    ├── Views/                         # Vistas Razor
    └── Services/                      # Servicios de consumo API
```

### ✅ Principios Aplicados

- ✔️ **Separación de responsabilidades** por capas
- ✔️ **Principios SOLID** en diseño de clases
- ✔️ **Dominio desacoplado** de infraestructura
- ✔️ **Reglas de negocio centralizadas** en Application
- ✔️ **Front-End consumiendo la API** (sin lógica duplicada)
- ✔️ **Inyección de dependencias** con Unity

---

## 📋 Funcionalidades

### 👤 Gestión de Autores

**Operaciones CRUD completas:**
- ✅ Crear nuevo autor
- ✅ Listar todos los autores
- ✅ Editar información del autor
- ✅ Eliminar autor (con validación de libros asociados)

**Campos del Autor:**

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `FullName` | `string` | Nombre completo (requerido) |
| `BirthDate` | `DateTime` | Fecha de nacimiento (requerido) |
| `City` | `string` | Ciudad de procedencia (requerido) |
| `Email` | `string` | Correo electrónico único (requerido) |

### 📖 Gestión de Libros

**Operaciones CRUD completas:**
- ✅ Crear nuevo libro
- ✅ Listar todos los libros
- ✅ Editar información del libro
- ✅ Eliminar libro

**Campos del Libro:**

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `Title` | `string` | Título del libro (requerido) |
| `Year` | `int` | Año de publicación (requerido) |
| `Genre` | `string` | Género literario (requerido) |
| `Pages` | `int` | Número de páginas (requerido) |
| `AuthorId` | `int` | Referencia al autor (requerido) |

---

## ⚖️ Reglas de Negocio Implementadas

| Regla | Descripción |
|-------|-------------|
| **Validación de Campos** | Todos los campos obligatorios son validados en API y Front-End |
| **Integridad Referencial** | Un libro debe estar asociado a un autor existente |
| **Límite de Libros** | Control del número máximo de libros permitidos (configurable) |
| **Email Único** | No se permiten autores con emails duplicados |
| **Validación de Autor** | Al crear un libro, se valida que el autor exista |

### 🚨 Mensajes de Error Específicos

```
❌ "No es posible registrar el libro, se alcanzó el máximo permitido."
❌ "El autor no está registrado"
```

---

## 🗄️ Base de Datos (SQL Server)

### 📋 Requisitos

- SQL Server (Express / Developer / Standard)
- Permisos para crear bases de datos

### 📊 Estructura

La base de datos se genera automáticamente mediante **Entity Framework Migrations**:

| Tabla | Descripción |
|-------|-------------|
| `Authors` | Información de autores |
| `Books` | Información de libros |
| `__MigrationHistory` | Historial de migraciones de EF |

> ⚠️ **Nota:** No es necesario crear tablas manualmente. Las migraciones generan todo automáticamente.

---

## ⚙️ Cómo Ejecutar el Proyecto

### 1️⃣ Clonar el Repositorio

```bash
git clone <url-del-repositorio>
cd Books.Api
```

Abrir la solución `Books.Api.sln` en **Visual Studio 2022** o superior.

### 2️⃣ Configurar la Cadena de Conexión

En el proyecto `Books.Api`, editar el archivo `Web.config`:

```xml
<connectionStrings>
  <add name="BooksDbContext"
       connectionString="Data Source=.;Initial Catalog=BooksDb;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

> ⚠️ **Importante:** Ajustar `Data Source` si SQL Server no está en local (ej: `localhost\SQLEXPRESS`).

### 3️⃣ Configurar el Límite Máximo de Libros

En el mismo `Web.config` del proyecto `Books.Api`:

```xml
<appSettings>
  <add key="MaxBooksAllowed" value="100" />
</appSettings>
```

> 💡 **Tip:** Puedes ajustar este valor según tus necesidades.

### 4️⃣ Crear la Base de Datos (Migraciones)

En **Visual Studio**:

1. Abrir **Tools** → **NuGet Package Manager** → **Package Manager Console**
2. Seleccionar como **Default Project**: `Books.Infrastructure`
3. Ejecutar el siguiente comando:

```powershell
Update-Database -ProjectName Books.Infrastructure -StartupProjectName Books.Api
```

✅ Esto creará la base de datos `BooksDb` con todas sus tablas.

### 5️⃣ Configurar el Front-End para Consumir la API

En el proyecto `Books.Front`, editar el archivo `Web.config`:

```xml
<appSettings>
  <add key="ApiBaseUrl" value="https://localhost:44354/" />
</appSettings>
```

> ⚠️ **Importante:** El puerto debe coincidir con el puerto donde se ejecute `Books.Api`.

### 6️⃣ Ejecutar la Solución

Configurar **Multiple Startup Projects**:

1. Click derecho en la solución → **Properties**
2. En **Startup Project**, seleccionar **Multiple startup projects**
3. Configurar:
   - `Books.Api` → **Start**
   - `Books.Front` → **Start**
4. Presionar **F5** para ejecutar

### 7️⃣ Probar la Aplicación

🌐 **Front-End (Interfaz de Usuario):**
```
https://localhost:<puerto-front>/
```

🚀 **API REST:**
```
https://localhost:<puerto-api>/api/authors
https://localhost:<puerto-api>/api/books
```

**Desde la interfaz web puedes:**
- ✅ Gestionar autores (crear, listar, editar, eliminar)
- ✅ Gestionar libros (crear, listar, editar, eliminar)
- ✅ Validar reglas de negocio y mensajes de error

---

## 🚀 API Endpoints

### 👤 Autores (`/api/authors`)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/authors` | Obtener todos los autores |
| `GET` | `/api/authors/{id}` | Obtener un autor por ID |
| `POST` | `/api/authors` | Crear un nuevo autor |
| `PUT` | `/api/authors/{id}` | Actualizar un autor existente |
| `DELETE` | `/api/authors/{id}` | Eliminar un autor |

### 📖 Libros (`/api/books`)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/books` | Obtener todos los libros |
| `GET` | `/api/books/{id}` | Obtener un libro por ID |
| `POST` | `/api/books` | Crear un nuevo libro |
| `PUT` | `/api/books/{id}` | Actualizar un libro existente |
| `DELETE` | `/api/books/{id}` | Eliminar un libro |

---

## 🧪 Pruebas

### 💻 Pruebas desde la Interfaz Web

La aplicación incluye un **Front-End completo en ASP.NET MVC** que permite probar todas las funcionalidades:

1. Navegar a `https://localhost:<puerto-front>/`
2. Usar las interfaces de Usuario para CRUD de Autores y Libros
3. Verificar validaciones y mensajes de error

### 🛠️ Pruebas con Postman o Herramientas API

Puedes probar los endpoints directamente con herramientas como:
- **Postman**
- **Thunder Client** (extensión de VS Code)
- **curl** desde terminal

**Ejemplo de Solicitud POST para Crear un Autor:**

```json
POST https://localhost:44354/api/authors
Content-Type: application/json

{
  "FullName": "Gabriel García Márquez",
  "BirthDate": "1927-03-06",
  "City": "Aracataca",
  "Email": "[email protected]"
}
```

**Ejemplo de Solicitud POST para Crear un Libro:**

```json
POST https://localhost:44354/api/books
Content-Type: application/json

{
  "Title": "Cien años de soledad",
  "Year": 1967,
  "Genre": "Realismo mágico",
  "Pages": 471,
  "AuthorId": 1
}
```

---

## 📝 Licencia

Este proyecto fue desarrollado como **prueba técnica** con fines educativos y de evaluación.

---

## 👤 Autor

Desarrollado como prueba técnica para **Ruben Hernandez**

---


⭐ **¡Gracias por revisar este proyecto!** ⭐
