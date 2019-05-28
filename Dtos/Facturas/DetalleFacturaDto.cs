namespace Dtos.Facturas
{
    public class DetalleFacturaDto
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int DetalleSolicitudId { get; set; }
        public double LavadoSeco { get; set; }
        public double LavadoPlanchado { get; set; }
        public double Planchado { get; set; }
        public double Doblado { get; set; }
        public double Total { get; set; }
    }
}
