namespace Dtos.Facturas
{
    public class GuardarFacturaDto
    {
        public int SolicitudesId { get; set; }
        public int ClientesId { get; set; }
        public double TotalParcial { get; set; }
        public double Doblado { get; set; }
        public double Suplemento { get; set; }
        public double TotalGlobal { get; set; }
        public string Estado { get; set; }
        public ListadoDetallesFacturaDto DetallesFacturas { get; set; }
    }
}
