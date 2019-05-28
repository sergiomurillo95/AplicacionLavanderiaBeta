using System.Collections.Generic;

namespace Dtos.Solicitud
{
    public class ListadoDetallesSolicitudDto
    {
        public List<DetalleSolicitudDto> DetalleSolicitud { get; set; } = new List<DetalleSolicitudDto>();
    }
}
