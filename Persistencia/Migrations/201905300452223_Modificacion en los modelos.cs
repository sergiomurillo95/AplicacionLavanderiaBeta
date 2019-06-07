namespace Persistencia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modificacionenlosmodelos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SolicitudConClienteDto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombres = c.String(),
                        Habitacion = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        Estado = c.String(),
                        SuplementoEntrega = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SolicitudDto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        SuplementoEntrega = c.Boolean(nullable: false),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SolicitudDto");
            DropTable("dbo.SolicitudConClienteDto");
        }
    }
}
