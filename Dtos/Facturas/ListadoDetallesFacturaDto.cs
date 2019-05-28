using System.Collections.Generic;

namespace Dtos.Facturas
{
    public class ListadoDetallesFacturaDto
    {
        public List<DetalleFacturaDto> DetallesFactura { get; set; } = new List<DetalleFacturaDto>();
    }
}
