namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clasificacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identificacion = c.String(),
                        Nombres = c.String(),
                        Habitacion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Costo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrendasClasificacionId = c.Int(nullable: false),
                        LavadoSeco = c.Double(nullable: false),
                        LavadoPlanchado = c.Double(nullable: false),
                        Planchado = c.Double(nullable: false),
                        Doblado = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrendasClasificacion", t => t.PrendasClasificacionId)
                .Index(t => t.PrendasClasificacionId);
            
            CreateTable(
                "dbo.PrendasClasificacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrendasId = c.Int(nullable: false),
                        ClasificacionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clasificacion", t => t.ClasificacionId)
                .ForeignKey("dbo.Prendas", t => t.PrendasId)
                .Index(t => t.PrendasId)
                .Index(t => t.ClasificacionId);
            
            CreateTable(
                "dbo.Prendas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetalleFactura",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacturaId = c.Int(nullable: false),
                        DetalleSolicitudId = c.Int(nullable: false),
                        LavadoSeco = c.Double(nullable: false),
                        LavadoPlanchado = c.Double(nullable: false),
                        Planchado = c.Double(nullable: false),
                        Doblado = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DetalleSolicitud", t => t.DetalleSolicitudId)
                .ForeignKey("dbo.Factura", t => t.FacturaId)
                .Index(t => t.FacturaId)
                .Index(t => t.DetalleSolicitudId);
            
            CreateTable(
                "dbo.DetalleSolicitud",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SolicitudesId = c.Int(nullable: false),
                        PrendasClasificacionId = c.Int(nullable: false),
                        LavadoSeco = c.Boolean(nullable: false),
                        LavadoPlanchado = c.Boolean(nullable: false),
                        Planchado = c.Boolean(nullable: false),
                        Doblado = c.Boolean(nullable: false),
                        CantidadPrendas = c.Int(nullable: false),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrendasClasificacion", t => t.PrendasClasificacionId)
                .ForeignKey("dbo.Solicitudes", t => t.SolicitudesId)
                .Index(t => t.SolicitudesId)
                .Index(t => t.PrendasClasificacionId);
            
            CreateTable(
                "dbo.Solicitudes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        SuplementoEntrega = c.Boolean(nullable: false),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Factura",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SolicitudesId = c.Int(nullable: false),
                        ClientesId = c.Int(nullable: false),
                        TotalParcial = c.Double(nullable: false),
                        Doblado = c.Double(nullable: false),
                        Suplemento = c.Double(nullable: false),
                        TotalGlobal = c.Double(nullable: false),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClientesId)
                .ForeignKey("dbo.Solicitudes", t => t.SolicitudesId)
                .Index(t => t.SolicitudesId)
                .Index(t => t.ClientesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleFactura", "FacturaId", "dbo.Factura");
            DropForeignKey("dbo.Factura", "SolicitudesId", "dbo.Solicitudes");
            DropForeignKey("dbo.Factura", "ClientesId", "dbo.Clientes");
            DropForeignKey("dbo.DetalleFactura", "DetalleSolicitudId", "dbo.DetalleSolicitud");
            DropForeignKey("dbo.DetalleSolicitud", "SolicitudesId", "dbo.Solicitudes");
            DropForeignKey("dbo.Solicitudes", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.DetalleSolicitud", "PrendasClasificacionId", "dbo.PrendasClasificacion");
            DropForeignKey("dbo.Costo", "PrendasClasificacionId", "dbo.PrendasClasificacion");
            DropForeignKey("dbo.PrendasClasificacion", "PrendasId", "dbo.Prendas");
            DropForeignKey("dbo.PrendasClasificacion", "ClasificacionId", "dbo.Clasificacion");
            DropIndex("dbo.Factura", new[] { "ClientesId" });
            DropIndex("dbo.Factura", new[] { "SolicitudesId" });
            DropIndex("dbo.Solicitudes", new[] { "ClienteId" });
            DropIndex("dbo.DetalleSolicitud", new[] { "PrendasClasificacionId" });
            DropIndex("dbo.DetalleSolicitud", new[] { "SolicitudesId" });
            DropIndex("dbo.DetalleFactura", new[] { "DetalleSolicitudId" });
            DropIndex("dbo.DetalleFactura", new[] { "FacturaId" });
            DropIndex("dbo.PrendasClasificacion", new[] { "ClasificacionId" });
            DropIndex("dbo.PrendasClasificacion", new[] { "PrendasId" });
            DropIndex("dbo.Costo", new[] { "PrendasClasificacionId" });
            DropTable("dbo.Factura");
            DropTable("dbo.Solicitudes");
            DropTable("dbo.DetalleSolicitud");
            DropTable("dbo.DetalleFactura");
            DropTable("dbo.Prendas");
            DropTable("dbo.PrendasClasificacion");
            DropTable("dbo.Costo");
            DropTable("dbo.Clientes");
            DropTable("dbo.Clasificacion");
        }
    }
}
