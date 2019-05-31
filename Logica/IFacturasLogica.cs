using Dtos.Facturas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logica
{
    public interface IFacturasLogica
    {
        Task<int> GuardarFactura(GuardarFacturaDto factura);
        Task<GuardarFacturaDto> GenerarFacturaDesdeSolicitud(int idSolicitud);
        Task ActualizarFactura(FacturasDto factura);
        Task<List<FacturasConDetalleDto>> ObtenerTodasFacturasConDetalle();
        Task<ObtenerFacturasDto> ObtenerFacturaPorId(int id);
        Task<FacturasConDetalleDto> ObtenerFacturaConDetallesPorId(int id);
        Task<FacturasConDetalleDto> ObtenerFacturaConDetallePorIdSolicitud(int idSolicitud);
    }
}
