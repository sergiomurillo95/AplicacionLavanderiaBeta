using System.Collections.Generic;

namespace Dtos.Facturas
{
    public class ListadoDetallesFacturaDto
    {
        public List<DetallesFacturaDto> DetallesFactura { get; set; } = new List<DetallesFacturaDto>();
    }
}
