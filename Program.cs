using Microsoft.EntityFrameworkCore;
using MiPrimerCloudApi.Data;
using MiPrimerCloudApi.Models;

var builder = WebApplication.CreateBuilder(args);

var frontendUrl = builder.Configuration["FRONTEND_URL"] ?? "http://localhost:5173";

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins(frontendUrl)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseCors("FrontendPolicy");

app.MapGet("/", () => "API funcionando en Render");

app.MapGet("/saludo", () => "Hola desde Render!");

app.MapGet("/tareas", async (AppDbContext db) =>
{
    return await db.Tareas.ToListAsync();
});

app.MapPost("/tareas", async (Tarea tarea, AppDbContext db) =>
{
    db.Tareas.Add(tarea);
    await db.SaveChangesAsync();
    return Results.Created($"/tareas/{tarea.Id}", tarea);
});

app.Run();
