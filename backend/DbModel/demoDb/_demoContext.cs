using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace DbModel.demoDb;

public partial class _demoContext : DbContext
{
    public _demoContext()
    {
    }

    public _demoContext(DbContextOptions<_demoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Persona { get; set; }

    public virtual DbSet<PersonaTipoDocumento> PersonaTipoDocumento { get; set; }

    // NUEVOS DbSets
    public virtual DbSet<Cancion> Cancion { get; set; }
    public virtual DbSet<GeneroCancion> GeneroCancion { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=demoDb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.45-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.DateUpdate).ValueGeneratedOnAddOrUpdate();

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Persona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_persona_tipo_documento");
        });

        modelBuilder.Entity<PersonaTipoDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.DateUpdate).ValueGeneratedOnAddOrUpdate();
        });

        // NUEVA Configuración para Cancion
        modelBuilder.Entity<Cancion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cancion");

            entity.HasIndex(e => e.IdGeneroCancion, "fk_cancion_genero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Artista)
                .HasMaxLength(255)
                .HasColumnName("artista");
            entity.Property(e => e.FechaLanzamiento)
                .HasColumnType("date")
                .HasColumnName("fecha_lanzamiento");
            entity.Property(e => e.IdGeneroCancion).HasColumnName("id_genero_cancion");

            entity.HasOne(d => d.IdGeneroCancionNavigation)
                .WithMany(p => p.Cancion)
                .HasForeignKey(d => d.IdGeneroCancion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cancion_genero");
        });

        // NUEVA Configuración para GeneroCancion
        modelBuilder.Entity<GeneroCancion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("genero_cancion");

            entity.HasIndex(e => e.Nombre, "nombre").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}