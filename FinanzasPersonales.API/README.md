# FinanciasPersonalesApiRest
This project is a RESTful API built with ASP.NET Core 8 and C#. It enables users to store and manage their financial transactions conveniently. The API is designed to help users track their daily incomes, expenses, and budgets, offering a reliable solution for managing their routine financial activities.


Para crear las entidades de la base de datos se utilizó el siguiente comando:
migraciones

  ## Required NuGet Packages

Para instalar las dependencias necesarias de NuGet para el proyecto .NET, ejecuta los siguientes comandos en la terminal dentro del directorio del proyecto:

``` bash
# Core packages
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Swashbuckle.AspNetCore

  ````


	

 ## Crear la Base de Datos con Entity Framework (Code First)

Para crear la base de datos utilizando **Entity Framework** y el enfoque **Code First**, sigue los siguientes pasos:

### 1. Asegúrate de tener el paquete `Microsoft.EntityFrameworkCore.Tools` instalado:

```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet ef migrations add InitialCreate
dotnet ef database update
````

#### La base de datos se debe llamar `FinanzasPersonalesDB` y se debe configurar en el archivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
	"FinanzasPersonalesDB": "Server=localhost;Database=FinanzasPersonalesDB;Trusted_Connection=True;"
  }
}
```

#### asi mismo la inyeccion de dependencias en el archivo `Startup.cs`:

```csharp
services.AddDbContext<FinanzasPersonalesContext>(options =>
	options.UseSqlServer(Configuration.GetConnectionString("FinanzasPersonalesDB")));
```

###  Ejecuta la aplicación y verifica que la base de datos se haya creado correctamente.

```bash
dotnet run

```





