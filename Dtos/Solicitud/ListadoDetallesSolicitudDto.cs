using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dtos.Solicitud
{
    public class ListadoDetallesSolicitudDto
    {
        public List<DetalleSolicitudDto> DetalleSolicitud { get; set; } = new List<DetalleSolicitudDto>();
    }
}
