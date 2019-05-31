namespace Dtos.Facturas
{
    public class ObtenerDetalleFacturaDto
    {
        public int Id { get; set; }

        public string Prenda { get; set; }
        public string Clasificacion { get; set; }
        public bool LavadoSecoDetalle { get; set; }
        public bool LavadoPlanchadoDetalle { get; set; }
        public bool PlanchadoDetalle { get; set; }
        public bool DobladoDetalle { get; set; }
        public int CantidadPrendasDetalle { get; set; }
        public string EstadoDetalle { get; set; }

        public double LavadoSeco { get; set; }
        public double LavadoPlanchado { get; set; }
        public double Planchado { get; set; }
        public double Doblado { get; set; }
        public double Total { get; set; }
    }
}
