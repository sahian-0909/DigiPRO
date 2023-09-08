using Microsoft.EntityFrameworkCore;
using Datos.DBContext;
using Datos.Repositories;
using Entidades;
using Negocio.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ControlEscolarContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddScoped<IGenericRepository<Alumno>, AlumnoRepository>();
builder.Services.AddScoped<IAlumnoService, AlumnoService>();

builder.Services.AddScoped<IGenericRepository<Materia>, MateriaRepository>();
builder.Services.AddScoped<IMateriaService, MateriaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
