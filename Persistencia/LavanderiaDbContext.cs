using Persistencia.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Persistencia
{
    public class LavanderiaDbContext : DbContext
    {
        public LavanderiaDbContext() : base("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;")
        {
        }

        public DbSet<Clasificacion> Clasificacion { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Costo> Costo { get; set; }
        public DbSet<DetalleFactura> DetalleFactura { get; set; }
        public DbSet<DetalleSolicitud> DetalleSolicitud { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<Prendas> Prendas { get; set; }
        public DbSet<PrendasClasificacion> PrendasClasificacion { get; set; }
        public DbSet<Solicitudes> Solicitudes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clasificacion>().HasKey(t => t.Id);
            modelBuilder.Entity<Clasificacion>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Clientes>().HasKey(t => t.Id);
            modelBuilder.Entity<Clientes>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Costo>().HasKey(t => t.Id);
            modelBuilder.Entity<Costo>().Property(t => t.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<DetalleFactura>().HasKey(t => t.Id);
            modelBuilder.Entity<DetalleFactura>().Property(t => t.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<DetalleSolicitud>().HasKey(t => t.Id);
            modelBuilder.Entity<DetalleSolicitud>().Property(t => t.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Factura>().HasKey(t => t.Id);
            modelBuilder.Entity<Factura>().Property(t => t.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Prendas>().HasKey(t => t.Id);
            modelBuilder.Entity<Prendas>().Property(t => t.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<PrendasClasificacion>().HasKey(t => t.Id);
            modelBuilder.Entity<PrendasClasificacion>().Property(t => t.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Solicitudes>().HasKey(t => t.Id);
            modelBuilder.Entity<Solicitudes>().Property(t => t.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
