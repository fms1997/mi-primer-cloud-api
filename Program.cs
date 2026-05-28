using Microsoft.EntityFrameworkCore;
using MiPrimerCloudApi.Data;
using MiPrimerCloudApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins(
                "https://mi-primer-cloud-front-git-main-franco-sassi-s-projects.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("Frontend");

app.MapGet("/", () => "API funcionando");

app.MapGet("/saludo", () => "Hola desde Render!");

app.MapGet("/tareas", async (AppDbContext db) =>
{
    return await db.Tareas.ToListAsync();
});
//.
app.MapPost("/tareas", async (Tarea tarea, AppDbContext db) =>
{
    db.Tareas.Add(tarea);
    await db.SaveChangesAsync();
    return Results.Ok(tarea);
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();