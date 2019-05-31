using System.Collections.Generic;

namespace Dtos.Facturas
{
    public class ListadoObtenerDetallesFacturaDto
    {
        public List<ObtenerDetalleFacturaDto> DetallesFactura { get; set; } = new List<ObtenerDetalleFacturaDto>();
    }
}
