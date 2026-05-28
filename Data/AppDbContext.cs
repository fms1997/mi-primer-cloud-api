using Microsoft.EntityFrameworkCore;
using MiPrimerCloudApi.Models;
using System.Collections.Generic;

namespace MiPrimerCloudApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tarea> Tareas => Set<Tarea>();
}