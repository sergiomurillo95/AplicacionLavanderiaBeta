using System;

namespace Dtos.Solicitud
{
    public class SolicitudConClienteDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Habitacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public bool SuplementoEntrega { get; set; }
    }
}
