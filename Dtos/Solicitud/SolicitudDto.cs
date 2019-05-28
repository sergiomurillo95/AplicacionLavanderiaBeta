using System;

namespace Dtos.Solicitud
{
    public class SolicitudDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public bool SuplementoEntrega { get; set; }
        public string Estado { get; set; }
    }
}
