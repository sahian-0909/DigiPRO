using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Entidades;

namespace Datos.DBContext
{
    public partial class ControlEscolarContext : DbContext
    {
        public ControlEscolarContext()
        {
        }

        public ControlEscolarContext(DbContextOptions<ControlEscolarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumnos { get; set; } = null!;
        public virtual DbSet<AlumnosMateria> AlumnosMaterias { get; set; } = null!;
        public virtual DbSet<Materia> Materias { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.IdAlumno)
                    .HasName("PK__alumnos__43FBBAC7D9B6BB40");

                entity.ToTable("alumnos");

                entity.Property(e => e.IdAlumno).HasColumnName("idAlumno");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .HasColumnName("apellidoMaterno");

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .HasColumnName("apellidoPaterno");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<AlumnosMateria>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("alumnos_materias");

                entity.Property(e => e.IdAlumno).HasColumnName("idAlumno");

                entity.Property(e => e.IdMateria).HasColumnName("idMateria");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdAlumno)
                    .HasConstraintName("FK__alumnos_m__idAlu__3A81B327");

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMateria)
                    .HasConstraintName("FK__alumnos_m__idMat__3B75D760");
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasKey(e => e.IdMateria)
                    .HasName("PK__materias__4B740AB34AED734B");

                entity.ToTable("materias");

                entity.Property(e => e.IdMateria).HasColumnName("idMateria");

                entity.Property(e => e.Costo)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("costo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
