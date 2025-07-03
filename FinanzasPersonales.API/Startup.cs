// FinanciasPersonales.API/Extensions/ServiceCollectionExtensions.cs






using Microsoft.AspNetCore.Identity; 
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;


using FinanzasPersonales.Aplication;

using FluentValidation;

using FinanzasPersonales.Domain;
using FinanzasPersonales.Infrastructure;




namespace FinanzasPersonales.API
{
    public static class ServiceCollectionExtensions
    {
     

        /// <summary>
        /// Configura Swagger con autenticación JWT
        /// </summary>
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Define el esquema de seguridad para Bearer Tokens
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Introduce el token JWT en el formato: Bearer {token}"
                });

                // Requiere el esquema de seguridad en cada operación
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

     

        /// <summary>
        /// Configura CORS para Angular
        /// </summary>
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder
                        //.WithOrigins("http://localhost:4200")
                        .AllowAnyOrigin() // puerto de angular 
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            return services;
        }

        /// <summary>
        /// Registra todos los servicios de aplicación
        /// </summary>
        public static IServiceCollection AddApplicationServicesConfiguration(this IServiceCollection services)
        {
            // Application Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISpendsService, SpendsService>();
            services.AddScoped<IJwtServices, JwtServices>();
            services.AddScoped<IIncomesServices, IncomesServices>();
            services.AddScoped<IBudgetCategoryService, BudgetCategoryService>();
            services.AddScoped<IBudgetService, BudgetService>();
            services.AddScoped<IDataServices, DataService>();

            // Password Hasher
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }

        /// <summary>
        /// Registra todos los repositorios
        /// </summary>
        public static IServiceCollection AddRepositoriesConfiguration(this IServiceCollection services)
        {
            // Generic Repositories
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Spend>, SpendsRepository>();
            services.AddScoped<IRepository<Income>, IncomesRepository>();
            services.AddScoped<IRepository<Budget>, BudgetRepository>();
            services.AddScoped<IRepository<BudgetCategory>, BudgetCategoryRepository>();

            // Specific Repositories
            services.AddScoped<IDataRepository<FullUserDataDTO>, DataRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIncomesRespository, IncomesRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<IBudgetCategoryRepository, BudgetCategoryRepository>();

            // Type Repositories
            services.AddScoped<ITypeRepository<TypeSpends>, TypeSpendsRepository>();
            services.AddScoped<ITypeRepository<TypeIncomes>, TypeIncomesRepository>();

            return services;
        }

        /// <summary>
        /// Registra todos los validadores de FluentValidation
        /// </summary>
        public static IServiceCollection AddValidatorsConfiguration(this IServiceCollection services)
        {
            // User Validators
            services.AddScoped<IValidator<RegisterUserDTO>, UserCreateValidator>();
            services.AddScoped<IValidator<UpdateUserDTO>, UserUpdateValidator>();

            // Type Validators
            services.AddScoped<IValidator<CreateTypeIncomesDTO>, CreateTypeIncomesValidator>();
            services.AddScoped<IValidator<CreateTypeSpendDTO>, CreateTypeSpendsValidator>();

            // Income Validators
            services.AddScoped<IValidator<CreateIncomesDTO>, CreateIncomesValidator>();
            services.AddScoped<IValidator<UpdateIncomesDTO>, UpdateIncomesValidator>();

            // Budget Validators
            services.AddScoped<IValidator<CreateBudgetDTO>, CreateBudgetValidator>();
            services.AddScoped<IValidator<UpdateBudgetDTO>, UpdateBudgetValidator>();
            services.AddScoped<IValidator<CreateBudgetCategoryDTO>, CreateBudgetCategoryValidator>();
            services.AddScoped<IValidator<UpdateBudgetCategoryDTO>, UpdateBudgetCategoryValidator>();

            // Spend Validators
            services.AddScoped<IValidator<CreatedSpendDTO>, CreateSpendsValidator>();

            return services;
        }

        /// <summary>
        /// Configura AutoMapper
        /// </summary>
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            return services;
        }

        /// <summary>
        /// Configura autenticación y autorización JWT
        /// </summary>
        public static IServiceCollection AddJwtAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();

            // SecretKey desde configuración
            var secretJWTKey = configuration["SecretJWTKey"];
            

            if (string.IsNullOrEmpty(secretJWTKey))
            {
                throw new InvalidOperationException("SecretJWTKey no está configurada");
            }

            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:7202";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = "https://localhost:4200",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretJWTKey))
                    };
                });
            
            return services;
        }

        public static IServiceCollection AddDataBaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
             return ConfigurationInfrastructure.AddDatabaseConfiguration(services, configuration);

        }

        /// <summary>
        /// Registra todos los servicios de la aplicación
        /// </summary>
        public static IServiceCollection AddFinanciasPersonalesServices(this IServiceCollection services, IConfigurationBuilder confB, IConfiguration configuration)
        {
            // User Secrets para desarrollo
            confB.AddUserSecrets<Program>();

            return services
                .AddSwaggerConfiguration()
                .AddDataBaseConfig(configuration)
                .AddDatabaseConfiguration(configuration)
                .AddCorsConfiguration()
                .AddApplicationServicesConfiguration()
                .AddRepositoriesConfiguration()
                .AddValidatorsConfiguration()
                .AddAutoMapperConfiguration()
                .AddJwtAuthenticationConfiguration(configuration);
        }
    }
}

