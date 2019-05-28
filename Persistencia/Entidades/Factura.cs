using System.ComponentModel.DataAnnotations.Schema;

namespace Persistencia.Entidades
{
    public class Factura
    {
        public int Id { get; set; }

        [ForeignKey("Solicitudes")]
        public int SolicitudesId { get; set; }
        public Solicitudes Solicitudes { get; set; }

        [ForeignKey("Clientes")]
        public int ClientesId { get; set; }
        public Clientes Clientes { get; set; }

        public double TotalParcial { get; set; }
        public double Doblado { get; set; }
        public double Suplemento { get; set; }
        public double TotalGlobal { get; set; }
        public string Estado { get; set; }
    }
}
