using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PruebaBACWebAPI.Models
{
    public partial class PruebaBACContext : DbContext
    {
        public PruebaBACContext()
        {
        }

        public PruebaBACContext(DbContextOptions<PruebaBACContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pregunta> Preguntas { get; set; } = null!;
        public virtual DbSet<Respuesta> Respuestas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-QBNUAIG5\\SQLEXPRESS;Database=PruebaBAC; Integrated Security=true; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.HasKey(e => e.IdPregunta)
                    .HasName("PK__pregunta__623EEC423908AC9D");

                entity.ToTable("preguntas");

                entity.Property(e => e.IdPregunta).HasColumnName("idPregunta");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength();

                entity.Property(e => e.FCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fCreacion");

                entity.Property(e => e.Pregunta1)
                    .HasMaxLength(600)
                    .HasColumnName("pregunta");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(15)
                    .HasColumnName("usuario");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.Usuario)
                    .HasConstraintName("FK__preguntas__usuar__4BAC3F29");
            });

            modelBuilder.Entity<Respuesta>(entity =>
            {
                entity.HasKey(e => e.IdRespuesta)
                    .HasName("PK__respuest__8AB5BFC88466AAA3");

                entity.ToTable("respuestas");

                entity.Property(e => e.IdRespuesta).HasColumnName("idRespuesta");

                entity.Property(e => e.FCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fCreacion");

                entity.Property(e => e.IdPregunta).HasColumnName("idPregunta");

                entity.Property(e => e.Respuesta1)
                    .HasMaxLength(600)
                    .HasColumnName("respuesta");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(15)
                    .HasColumnName("usuario");

                entity.HasOne(d => d.IdPreguntaNavigation)
                    .WithMany(p => p.Respuesta)
                    .HasForeignKey(d => d.IdPregunta)
                    .HasConstraintName("FK__respuesta__idPre__4F7CD00D");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.Respuesta)
                    .HasForeignKey(d => d.Usuario)
                    .HasConstraintName("FK__respuesta__usuar__4E88ABD4");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Usuario1)
                    .HasName("PK__usuarios__9AFF8FC786C5DDCF");

                entity.ToTable("usuarios");

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(15)
                    .HasColumnName("usuario");

                entity.Property(e => e.Contrasena).HasColumnName("contrasena");

                entity.Property(e => e.FCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fCreacion");

                entity.Property(e => e.FModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fModificacion");
            });
            //modelBuilder.Entity<CrearUsuario>(entity => entity.HasNoKey());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

      
    }
}
