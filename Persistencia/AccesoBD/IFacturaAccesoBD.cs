using Dtos.Facturas;
using Persistencia.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistencia.AccesoBD
{
    public interface IFacturaAccesoBD
    {
        Task<int> GuardarFactura(GuardarFacturaDto factura);
        Task ActualizarFactura(FacturasDto factura);
        Task<List<FacturasConDetalleDto>> ObtenerTodasFacturasConDetalle();
        Task<FacturasConDetalleDto> ObtenerFacturaConDetallesPorIdSolicitud(int idSolicitud);
        Task<List<ObtenerFacturasDto>> ObtenerTodasFacturas();
        Task<ObtenerFacturasDto> ObtenerFacturaPorId(int id);
        Task<FacturasConDetalleDto> ObtenerFacturaConDetallesPorId(int id);
        Task<IQueryable<Factura>> EncontrarFactura(Expression<Func<Factura, bool>> expresion);
        Task<IQueryable<DetalleFactura>> EncontrarDetallesFactura(Expression<Func<DetalleFactura, bool>> expresion);
    }
}
