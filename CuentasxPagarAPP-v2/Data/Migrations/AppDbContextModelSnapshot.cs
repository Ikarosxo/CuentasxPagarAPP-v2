﻿// <auto-generated />
using System;
using CuentasxPagarAPP_v2.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CuentasxPagarAPP_v2.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CuentasxPagarAPP_v2.Models.Concepto", b =>
                {
                    b.Property<int>("IdConcepto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdConcepto"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdConcepto");

                    b.ToTable("Conceptos");
                });

            modelBuilder.Entity("CuentasxPagarAPP_v2.Models.DocumentoxPagar", b =>
                {
                    b.Property<int>("NumDocument")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("NumDocument"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaDocumento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("IdProveedor")
                        .HasColumnType("integer");

                    b.Property<decimal>("Monto")
                        .HasColumnType("numeric(10,2)");

                    b.Property<int>("NumFacturaPagar")
                        .HasColumnType("integer");

                    b.HasKey("NumDocument");

                    b.HasIndex("IdProveedor");

                    b.ToTable("DocumentosxPagar");
                });

            modelBuilder.Entity("CuentasxPagarAPP_v2.Models.Proveedor", b =>
                {
                    b.Property<int>("IdProveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdProveedor"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric(10,2)");

                    b.Property<string>("CedulaRNC")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TipoPersona")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdProveedor");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("CuentasxPagarAPP_v2.Models.DocumentoxPagar", b =>
                {
                    b.HasOne("CuentasxPagarAPP_v2.Models.Proveedor", "Proveedor")
                        .WithMany("DocumentosxPagar")
                        .HasForeignKey("IdProveedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("CuentasxPagarAPP_v2.Models.Proveedor", b =>
                {
                    b.Navigation("DocumentosxPagar");
                });
#pragma warning restore 612, 618
        }
    }
}