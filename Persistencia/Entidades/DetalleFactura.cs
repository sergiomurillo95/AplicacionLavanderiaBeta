using System.ComponentModel.DataAnnotations.Schema;

namespace Persistencia.Entidades
{
    public class DetalleFactura
    {
        public int Id { get; set; }

        [ForeignKey("Factura")]
        public int FacturaId { get; set; }
        public Factura Factura { get; set; }

        [ForeignKey("DetalleSolicitud")]
        public int DetalleSolicitudId { get; set; }
        public DetalleSolicitud DetalleSolicitud { get; set; }

        public double LavadoSeco { get; set; }
        public double LavadoPlanchado { get; set; }
        public double Planchado { get; set; }
        public double Doblado { get; set; }
        public double Total { get; set; }
    }
}
