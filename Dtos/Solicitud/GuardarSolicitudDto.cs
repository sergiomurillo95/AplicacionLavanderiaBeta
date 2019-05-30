namespace Dtos.Solicitud
{
    public class GuardarSolicitudDto
    {
        public int ClientesId { get; set; }
        public string Nombres { get; set; }
        public string Identificacion { get; set; }
        public string Habitacion { get; set; }
        public bool SuplementoEntrega { get; set; } = false;
    }
}
