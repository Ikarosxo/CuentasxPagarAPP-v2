using Microsoft.EntityFrameworkCore;
using CuentasxPagarAPP_v2.Models;
using System.Collections.Generic;

namespace CuentasxPagarAPP_v2.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Concepto> Conceptos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<DocumentoxPagar> DocumentosxPagar { get; set; }

    }
}
