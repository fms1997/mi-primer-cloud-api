var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/saludo", () =>
{
    return "Hola desde Render!";
});

app.Run();