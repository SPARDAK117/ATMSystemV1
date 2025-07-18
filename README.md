ATM API - Sistema de Cajero Automático Simulado
Este repositorio contiene el código fuente de una API RESTful desarrollada en .NET 8 para simular las operaciones de un sistema de cajero automático (ATM). El proyecto está diseñado siguiendo una arquitectura en capas, utilizando SQL Server como base de datos y ADO.NET para el acceso a datos a través de Stored Procedures.

🚀 Tecnologías Utilizadas
Backend: ASP.NET Core 8 Web API (C#)

Base de Datos: SQL Server

Acceso a Datos: ADO.NET (Estrictamente con Stored Procedures)

Autenticación: JSON Web Tokens (JWT)

Validaciones: Data Annotations

Documentación API: Swagger/OpenAPI

Herramientas de Prueba: Postman

Control de Versiones: Git / GitHub

🏗️ Estructura del Proyecto
El proyecto está organizado en 4 capas lógicas, cada una con su propio proyecto físico en la solución de Visual Studio:

ATM.WebAPI (Capa de Presentación):

Proyecto ASP.NET Core Web API.

Maneja las solicitudes HTTP, enrutamiento, serialización/deserialización.

Contiene los controladores (AuthController, AccountController).

Implementa la autenticación JWT y las validaciones de entrada (Data Annotations).

Asegura que todas las respuestas del controlador sean controladas y devuelvan un código de estado HTTP 200 OK, con el resultado lógico (éxito/error) en el cuerpo de la respuesta.

Configuración de Swagger para la documentación interactiva.

ATM.Core (Capa de Lógica de Negocio/Servicios):

Librería de clases que contiene la lógica de negocio central del ATM.

Define interfaces de servicio (ej. IAccountService, IAuthService).

Implementa las reglas de negocio (límites de retiro/depósito, verificación de saldo, lógica de cambio de PIN).

Utiliza un SecurityService para el hashing y verificación de PINs.

ATM.Data (Capa de Acceso a Datos):

Librería de clases que interactúa directamente con la base de datos SQL Server.

Implementa interfaces de repositorio (ej. IAccountRepository).

Todas las operaciones de base de datos se realizan estrictamente a través de Stored Procedures utilizando ADO.NET.

Manejo de conexiones y mapeo de datos.

ATM.Shared (Capa de Modelos/Utilidades Compartidas):

Librería de clases que contiene modelos de dominio (POCOs como Account, Card, Transaction).

Define DTOs comunes para la comunicación entre capas (ej. ApiResponse<T>, ServiceResponse<T>).

Contiene enumeraciones y constantes compartidas.

📋 Requisitos Previos
Antes de ejecutar el proyecto, asegúrate de tener instalado lo siguiente:

Visual Studio 2022 (con las cargas de trabajo de desarrollo web y SQL Server Data Tools - SSDT).

.NET 8 SDK.

SQL Server (versión 2016 o superior, se recomienda SQL Server Express o Developer Edition).

SQL Server Management Studio (SSMS) (para la gestión de la base de datos).

Postman Desktop App (para probar la API).

Git.

⚙️ Configuración del Proyecto
Sigue estos pasos para configurar y ejecutar el proyecto en tu entorno local:

1. Clonar el Repositorio
git clone <URL_DE_TU_REPOSITORIO_GITHUB>
cd ATMSystem

2. Configuración de la Base de Datos
El script de creación de la base de datos se encuentra en la carpeta src/.

Abre SSMS y conéctate a tu instancia de SQL Server.

Abre el archivo src/CreateDatabase.sql en SSMS.

Revisa y ajusta las rutas de los archivos .mdf y .ldf en la sentencia CREATE DATABASE si no coinciden con tu instalación de SQL Server.

Ejecuta el script completo. Este script creará la base de datos ATMSystem con todos sus esquemas (Bank, Audit, Config, Security), tablas, funciones, procedimientos almacenados y datos iniciales de configuración y prueba, todo dentro de una transacción para asegurar la atomicidad.

3. Restaurar Paquetes NuGet
Abre la solución ATMSystem.sln en Visual Studio 2022.

Visual Studio debería restaurar automáticamente los paquetes NuGet. Si no lo hace, haz clic derecho en la solución en el "Explorador de Soluciones" y selecciona "Restore NuGet Packages".

4. Configuración de appsettings.json
Abre el archivo appsettings.json dentro del proyecto ATM.WebAPI.

Cadena de Conexión: Asegúrate de que la cadena de conexión DefaultConnection apunte a tu base de datos SQL Server recién creada.

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ATMSystem;Integrated Security=True;TrustServerCertificate=True;"
  // O tu cadena de conexión real, ej: "Server=.;Database=ATMSystem;Integrated Security=True;TrustServerCertificate=True;"
},

Configuración JWT: La clave JWT debe ser una cadena segura y larga (al menos 32 caracteres).

"Jwt": {
  "Key": "UnaClaveSuperSecretaParaTuAPIATMQueDebeSerLargaYCompleja", // ¡CAMBIA ESTO!
  ""Issuer": "yourdomain.com",
  "Audience": "yourdomain.com",
  "ExpiresInMinutes": "60"
},

🚀 Ejecución de la API
En Visual Studio, asegúrate de que ATM.WebAPI esté configurado como el proyecto de inicio.

Haz clic en el botón "HTML" (o el perfil de lanzamiento que uses) en la barra de herramientas de Visual Studio para iniciar la API.

Se abrirá una ventana del navegador con la interfaz de Swagger UI (normalmente en http://localhost:PUERTO/swagger/index.html).



¡Esperamos que este README te sea de gran utilidad! Si tienes alguna pregunta o necesitas asistencia adicional, no dudes en consultarnos.
