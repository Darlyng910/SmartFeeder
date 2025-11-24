using Microsoft.EntityFrameworkCore;
using SmartFeederApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. SQL Server + DbContext
builder.Services.AddDbContext<AlimentadorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Controllers (estilo clase de tu ingeniera)
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 3. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ====================================================================================
// 🔥 BLOQUE DE REINTENTOS PARA ESPERAR A SQL SERVER (como hacía tu ingeniera)
// ====================================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = services.GetRequiredService<AlimentadorContext>();

        int intentos = 0;
        int maxIntentos = 15;
        bool conectado = false;

        while (intentos < maxIntentos && !conectado)
        {
            try
            {
                logger.LogInformation($"Intentando conectar a SQL Server... intento {intentos + 1}/{maxIntentos}");

                context.Database.EnsureCreated();

                conectado = true;
                logger.LogInformation("¡Conexión exitosa! Base de datos inicializada.");
            }
            catch (Exception ex)
            {
                intentos++;
                logger.LogWarning($"SQL Server aún no está listo: {ex.Message}");
                logger.LogWarning("Esperando 2 segundos antes de reintentar...");
                Thread.Sleep(2000);
            }
        }

        if (!conectado)
        {
            throw new Exception("No se pudo conectar a SQL Server tras múltiples intentos.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error crítico durante la inicialización de la base de datos.");
    }
}

// ====================================================================================
// FIN DEL BLOQUE DE REINTENTOS
// ====================================================================================


// 4. Swagger UI solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 5. HTTPS
app.UseHttpsRedirection();

// 6. Mapear controllers automáticamente
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
