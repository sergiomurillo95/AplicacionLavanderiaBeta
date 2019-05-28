using System;

namespace Dtos.Solicitud
{
    public class GuardarSolicitudDto
    {
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public bool SuplementoEntrega { get; set; }
        public string Estado { get; set; }
        public ListadoDetallesSolicitudDto DetallesSolicitud { get; set; }
    }
}
