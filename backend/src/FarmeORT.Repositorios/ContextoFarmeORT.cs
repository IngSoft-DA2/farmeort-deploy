using FarmeORT.Dominio;
using Microsoft.EntityFrameworkCore;

namespace FarmeORT.Repositorios;

public class ContextoFarmeORT(DbContextOptions<ContextoFarmeORT> opciones) : DbContext(opciones)
{
    public DbSet<Skill> Skills => Set<Skill>();

    protected override void OnModelCreating(ModelBuilder modelo)
    {
        modelo.Entity<Skill>(skill =>
        {
            skill.ToTable("Skills");
            skill.HasKey(s => s.Id);
            skill.Property(s => s.Codigo).HasMaxLength(Skill.LargoMaximoCodigo).IsRequired();
            skill.HasIndex(s => s.Codigo).IsUnique();
            skill.Property(s => s.Nombre).HasMaxLength(Skill.LargoMaximoNombre).IsRequired();
            skill.Property(s => s.Categoria).HasMaxLength(Skill.LargoMaximoCategoria).IsRequired();

            // Datos de prueba: quedan en la migración, así que una base nueva ya arranca con ellos.
            skill.HasData(
                new { Id = 1, Codigo = "GEST01", Nombre = "Gesto épico", Categoria = "Gestos", Activa = true },
                new { Id = 2, Codigo = "VOCA01", Nombre = "Grito de guerra", Categoria = "Vocales", Activa = true },
                new { Id = 3, Codigo = "BAIL01", Nombre = "Paso prohibido", Categoria = "Baile", Activa = true },
                new { Id = 4, Codigo = "MEME01", Nombre = "Meme a destiempo", Categoria = "Memes", Activa = false });
        });
    }
}
