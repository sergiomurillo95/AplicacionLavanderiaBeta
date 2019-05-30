using System;

namespace Dtos.Solicitud
{
    public class GuardarSolicitudConDetallesDto
    {
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public bool SuplementoEntrega { get; set; } = false;
        public string Estado { get; set; }
        public ListadoDetallesSolicitudDto DetallesSolicitud { get; set; }
    }
}
