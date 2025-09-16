using Facturacion.API.Data;
using Microsoft.EntityFrameworkCore;
using Facturacion.API.Repositories;
using Facturacion.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración del DbContext: configuro y uso SQLite embebida para mantener
// el ejercicio sencillo y reproducible. El archivo se crea en la carpeta del
// `facturacion.db`.
var conexionSqlite = "Data Source=facturacion.db";
builder.Services.AddDbContext<FacturaDbContext>(opciones => opciones.UseSqlite(conexionSqlite));

// Registré los repositorios siguiendo el Repository Pattern. Mantengo el
// acceso a datos separado de la lógica de negocio para facilitar pruebas.
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();

// Registré los servicios de negocio (Directorio y Ventas). Los controladores
// consumen estos servicios para realizar las operaciones del dominio.
builder.Services.AddScoped<IDirectorio, Directorio>();
builder.Services.AddScoped<IVentas, Ventas>();

// Añado controladores y Swagger para documentar y probar la API fácilmente.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registré logging básico en consola. Es suficiente para ver eventos
// importantes durante el desarrollo (creación de personas, facturas,
// errores simples, etc.).
builder.Logging.AddConsole();

var app = builder.Build();

// Cree la base de datos y las tablas si no existen. Para un proyecto real
// usaría migraciones (`dotnet ef migrations`) pero aquí evito esa
// dependencia adicional y uso EnsureCreated por simplicidad.
using (var scope = app.Services.CreateScope())
{
    var contexto = scope.ServiceProvider.GetRequiredService<FacturaDbContext>();
    contexto.Database.EnsureCreated();
}

// Solo muestro Swagger en entornos de desarrollo para no exponerlo en
// producción por accidente.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseAuthorization();

// Mapeo los controladores y arranco la aplicación
app.MapControllers();
app.Run();
