

 namespace FinanzasPersonales.API;

public class Program
{

   
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();


        builder.Services.AddHttpContextAccessor();

     
        // Configuracion Global del Proyecto
        builder.Services.AddFinanciasPersonalesServices(builder.Configuration, builder.Configuration);


        var app = builder.Build();


        // Configure the HTTP request pipeline.
        // si esta en desarrollo muestra la documentacion de swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


 

        app.UseHttpsRedirection();
        app.UseCors("AllowAllOrigins");
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}