using System;

namespace Dtos.Solicitud
{
    public class SolicitudesConDetallesDto
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public string Nombres { get; set; }
        public string Habitacion { get; set; }

        public DateTime Fecha { get; set; }
        public bool SuplementoEntrega { get; set; }
        public string Estado { get; set; }
        public ListadoDetallesSolicitudDto ListadoDetallesSolicitud { get; set; }
    }
}
