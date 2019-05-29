using Dtos.Facturas;
using Dtos.Solicitud;
using Persistencia.AccesoBD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logica
{
    public class FacturasLogica: IFacturasLogica
    {
        private readonly IFacturaAccesoBD _facturaAccesoBd;
        private readonly ISolicitudesAccesoBD _solicitudAccesoBd;
        private readonly IClasificacionPrendasAccesoBD _clasificacionPrendasAccesoBd;

        public FacturasLogica(IFacturaAccesoBD facturaAccesoBd,
            ISolicitudesAccesoBD solicitudAccesoBd,
            IClasificacionPrendasAccesoBD clasificacionPrendasAccesoBd)
        {
            _facturaAccesoBd = facturaAccesoBd;
            _solicitudAccesoBd = solicitudAccesoBd;
            _clasificacionPrendasAccesoBd = clasificacionPrendasAccesoBd;
        }

        /// <summary>
        /// Condiciones de negocio:
        /// 1) Deben existir detalles en la factura
        /// 2) El valor de la factura debe ser mayor que cero
        /// </summary>
        /// <param name="factura"></param>
        /// <returns></returns>
        public async Task GuardarFactura(GuardarFacturaDto factura)
        {
            if(factura.DetallesFacturas.DetallesFactura.Count > 0)
            {
                await _facturaAccesoBd.GuardarFactura(factura);
            } 
        }

        /// <summary>
        /// Condiciones de negocio:
        /// 1) 
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        public async Task GenerarFacturaDesdeSolicitud(int idSolicitud)
        {
            var solicitud = await _solicitudAccesoBd.ObtenerSolicitudConDetallePorId(idSolicitud);
            if(solicitud != default(SolicitudesConDetallesDto))
            {
                foreach(var detalleSolicitud in solicitud.ListadoDetallesSolicitud.DetalleSolicitud)
                {
                    await _clasificacionPrendasAccesoBd.ObtenerCostoPorIdPrendaClasificacion(detalleSolicitud.PrendasClasificacionId);
                }
            }
        }

        public async Task ActualizarFactura(FacturasDto factura)
        {
            await _facturaAccesoBd.ActualizarFactura(factura);
        }

        public async Task<List<FacturasConDetalleDto>> ObtenerTodasFacturasConDetalle()
        {
            return await _facturaAccesoBd.ObtenerTodasFacturasConDetalle();
        }

        public async Task<FacturasDto> ObtenerFacturaPorId(int id)
        {
            return await _facturaAccesoBd.ObtenerFacturaPorId(id);
        }

        public async Task<FacturasConDetalleDto> ObtenerFacturaConDetallesPorId(int id)
        {
            return await _facturaAccesoBd.ObtenerFacturaConDetallesPorId(id);
        }
    }
}
