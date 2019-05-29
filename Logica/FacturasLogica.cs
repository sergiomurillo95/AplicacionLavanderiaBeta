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
        public async Task<GuardarFacturaDto> GenerarFacturaDesdeSolicitud(int idSolicitud)
        {
            var solicitud = await _solicitudAccesoBd.ObtenerSolicitudConDetallePorId(idSolicitud);
            if(solicitud != default(SolicitudesConDetallesDto))
            {
                double totalParcial = 0;
                double totalGlobal = 0;
                double totalDoblado = 0;
                double totalSuplemento = 0;

                var listaDetallesFactura = new List<DetalleFacturaDto>();

                foreach (var detalleSolicitud in solicitud.ListadoDetallesSolicitud.DetalleSolicitud)
                {
                    var costo = await _clasificacionPrendasAccesoBd.ObtenerCostoPorIdPrendaClasificacion(detalleSolicitud.PrendasClasificacionId);
                    double lavadoSeco = 0;
                    double lavadoPlanchado = 0;
                    double planchado = 0;
                    double doblado = 0;

                    if (detalleSolicitud.LavadoSeco)
                    {
                        totalParcial += (costo.LavadoSeco * detalleSolicitud.CantidadPrendas);
                        lavadoSeco = costo.LavadoSeco * detalleSolicitud.CantidadPrendas;
                    }
                    if (detalleSolicitud.LavadoPlanchado)
                    {
                        totalParcial += (costo.LavadoPlanchado * detalleSolicitud.CantidadPrendas);
                        lavadoPlanchado = (costo.LavadoPlanchado * detalleSolicitud.CantidadPrendas);
                    }
                    if (detalleSolicitud.Planchado)
                    {
                        totalParcial += (costo.Planchado * detalleSolicitud.CantidadPrendas);
                        planchado = (costo.Planchado * detalleSolicitud.CantidadPrendas);
                    }
                    
                    if (detalleSolicitud.Doblado)
                    {
                        totalParcial += (costo.Doblado * detalleSolicitud.CantidadPrendas);
                        totalDoblado += (costo.Doblado * detalleSolicitud.CantidadPrendas);
                        doblado = (costo.Doblado * detalleSolicitud.CantidadPrendas);
                    }

                    totalGlobal = totalParcial;

                    var detalleFactura = new DetalleFacturaDto
                    {
                         DetalleSolicitudId = detalleSolicitud.Id,
                         Doblado = doblado,
                         LavadoPlanchado = lavadoPlanchado,
                         LavadoSeco = lavadoSeco,
                         Planchado = planchado,
                         Total = (doblado + lavadoPlanchado + lavadoSeco + planchado)
                    };
                    listaDetallesFactura.Add(detalleFactura);
                }
                var listadoDetallesFactura = new ListadoDetallesFacturaDto
                {
                    DetallesFactura = listaDetallesFactura
                };

                if (solicitud.SuplementoEntrega)
                {
                    totalSuplemento = totalGlobal * 0.5;
                    totalGlobal = totalGlobal + totalSuplemento;
                }

                var guardarFactura = new GuardarFacturaDto
                {
                     ClientesId = solicitud.ClienteId,
                     Doblado = totalDoblado,
                     Estado = "Finalizado",
                     SolicitudesId = solicitud.Id,
                     TotalGlobal = totalGlobal,
                     TotalParcial = totalParcial,
                     Suplemento = totalSuplemento,
                     DetallesFacturas = listadoDetallesFactura
                };

                await _facturaAccesoBd.GuardarFactura(guardarFactura);
                return await Task.FromResult(guardarFactura);
            }
            return default(GuardarFacturaDto);
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
