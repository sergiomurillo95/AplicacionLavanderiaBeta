using Dtos.Facturas;
using Persistencia.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistencia.AccesoBD
{
    public class FacturaAccesoBD : IFacturaAccesoBD
    {
        private LavanderiaDbContext _context;
        private ISolicitudesAccesoBD _solicitudesAccesoBd;
        private IClasificacionPrendasAccesoBD _clasificacionPrendasAccesoBd;
        private IClientesAccesoBD _clientesAccesoBd;

        public FacturaAccesoBD(LavanderiaDbContext context,
            ISolicitudesAccesoBD solicitudesAccesoBd,
            IClasificacionPrendasAccesoBD clasificacionPrendasAccesoBd,
            IClientesAccesoBD clientesAccesoBd)
        {
            _context = context;
            _solicitudesAccesoBd = solicitudesAccesoBd;
            _clasificacionPrendasAccesoBd = clasificacionPrendasAccesoBd;
            _clientesAccesoBd = clientesAccesoBd;
        }

        public async Task<FacturasConDetalleDto> ObtenerFacturaConDetallesPorId(int id)
        {
            var factura = (await EncontrarFactura(t => t.Id == id)).FirstOrDefault();
            if (factura != default(Factura))
            {
                var listaDetallesFacturaDto = new List<ObtenerDetalleFacturaDto>();
                var listaDetallesFactura = (await EncontrarDetallesFactura(t => t.FacturaId == factura.Id)).ToList();

                var cliente = await _clientesAccesoBd.ObtenerClientePorId(factura.ClientesId);
                var solicitud = await _solicitudesAccesoBd.ObtenerSolicitudPorId(factura.SolicitudesId);

                foreach (var detalleFactura in listaDetallesFactura)
                {
                    var detalleSolicitud = await _solicitudesAccesoBd.ObtenerDetalleSolicitud(detalleFactura.DetalleSolicitudId);

                    var prendaClasificacion = (await _clasificacionPrendasAccesoBd.EncontrarPrendasClasificacion(t => t.Id == detalleSolicitud.PrendasClasificacionId)).FirstOrDefault();

                    var prenda = (await _clasificacionPrendasAccesoBd.EncontrarPrenda(t => t.Id == prendaClasificacion.PrendasId)).FirstOrDefault();
                    var clasificacion = (await _clasificacionPrendasAccesoBd.EncontrarClasificacion(t => t.Id == prendaClasificacion.ClasificacionId)).FirstOrDefault();


                    var detalleFacturaDto = new ObtenerDetalleFacturaDto
                    {
                         Id = detalleFactura.Id,

                         Clasificacion = clasificacion.Nombre,
                         Prenda = prenda.Nombre,

                         CantidadPrendasDetalle = detalleSolicitud.CantidadPrendas,
                         DobladoDetalle = detalleSolicitud.Doblado,
                         EstadoDetalle = detalleSolicitud.Estado,
                         LavadoPlanchadoDetalle = detalleSolicitud.LavadoPlanchado,
                         LavadoSecoDetalle = detalleSolicitud.LavadoSeco,
                         PlanchadoDetalle = detalleSolicitud.Planchado,

                         Doblado = detalleFactura.Doblado,
                         LavadoPlanchado = detalleFactura.LavadoPlanchado,
                         LavadoSeco = detalleFactura.LavadoSeco,
                         Planchado = detalleFactura.Planchado,
                         Total = detalleFactura.Total
                    };
                    listaDetallesFacturaDto.Add(detalleFacturaDto);
                }

                var listadoDetallesFacturas = new ListadoObtenerDetallesFacturaDto
                {
                    DetallesFactura = listaDetallesFacturaDto
                };

                var facturaConDetalleDto = new FacturasConDetalleDto
                {
                     Id = factura.Id,
                     Nombre = cliente.Nombres,
                     Identificacion = cliente.Identificacion,
                     Habitacion = cliente.Habitacion,

                     SuplementoEntrega = solicitud.SuplementoEntrega,
                     Fecha = solicitud.Fecha,

                     Doblado = factura.Doblado,
                     Estado = factura.Estado,
                     Suplemento = factura.Suplemento,
                     TotalGlobal = factura.TotalGlobal,
                     TotalParcial = factura.TotalParcial,
                     DetallesFacturas = listadoDetallesFacturas
                };

                return facturaConDetalleDto;
            }
            return default(FacturasConDetalleDto);
        }

        public async Task<FacturasConDetalleDto> ObtenerFacturaConDetallesPorIdSolicitud(int idSolicitud)
        {
            var factura = (await EncontrarFactura(t => t.SolicitudesId == idSolicitud)).FirstOrDefault();
            if (factura != default(Factura))
            {
                var listaDetallesFacturaDto = new List<ObtenerDetalleFacturaDto>();
                var listaDetallesFactura = (await EncontrarDetallesFactura(t => t.FacturaId == factura.Id)).ToList();

                var cliente = await _clientesAccesoBd.ObtenerClientePorId(factura.ClientesId);
                var solicitud = await _solicitudesAccesoBd.ObtenerSolicitudPorId(factura.SolicitudesId);

                foreach (var detalleFactura in listaDetallesFactura)
                {
                    var detalleSolicitud = await _solicitudesAccesoBd.ObtenerDetalleSolicitud(detalleFactura.DetalleSolicitudId);

                    var prendaClasificacion = (await _clasificacionPrendasAccesoBd.EncontrarPrendasClasificacion(t => t.Id == detalleSolicitud.PrendasClasificacionId)).FirstOrDefault();

                    var prenda = (await _clasificacionPrendasAccesoBd.EncontrarPrenda(t => t.Id == prendaClasificacion.PrendasId)).FirstOrDefault();
                    var clasificacion = (await _clasificacionPrendasAccesoBd.EncontrarClasificacion(t => t.Id == prendaClasificacion.ClasificacionId)).FirstOrDefault();


                    var detalleFacturaDto = new ObtenerDetalleFacturaDto
                    {
                        Id = detalleFactura.Id,

                        Clasificacion = clasificacion.Nombre,
                        Prenda = prenda.Nombre,

                        CantidadPrendasDetalle = detalleSolicitud.CantidadPrendas,
                        DobladoDetalle = detalleSolicitud.Doblado,
                        EstadoDetalle = detalleSolicitud.Estado,
                        LavadoPlanchadoDetalle = detalleSolicitud.LavadoPlanchado,
                        LavadoSecoDetalle = detalleSolicitud.LavadoSeco,
                        PlanchadoDetalle = detalleSolicitud.Planchado,

                        Doblado = detalleFactura.Doblado,
                        LavadoPlanchado = detalleFactura.LavadoPlanchado,
                        LavadoSeco = detalleFactura.LavadoSeco,
                        Planchado = detalleFactura.Planchado,
                        Total = detalleFactura.Total
                    };
                    listaDetallesFacturaDto.Add(detalleFacturaDto);
                }

                var listadoDetallesFacturas = new ListadoObtenerDetallesFacturaDto
                {
                    DetallesFactura = listaDetallesFacturaDto
                };

                var facturaConDetalleDto = new FacturasConDetalleDto
                {
                    Id = factura.Id,
                    Nombre = cliente.Nombres,
                    Identificacion = cliente.Identificacion,
                    Habitacion = cliente.Habitacion,

                    SuplementoEntrega = solicitud.SuplementoEntrega,
                    Fecha = solicitud.Fecha,

                    Doblado = factura.Doblado,
                    Estado = factura.Estado,
                    Suplemento = factura.Suplemento,
                    TotalGlobal = factura.TotalGlobal,
                    TotalParcial = factura.TotalParcial,
                    DetallesFacturas = listadoDetallesFacturas
                };

                return facturaConDetalleDto;
            }
            return default(FacturasConDetalleDto);
        }
        public async Task<List<ObtenerFacturasDto>> ObtenerTodasFacturas()
        {
            var listaFacturas = _context.Set<Factura>().ToList();
            var listaFacturasDto = new List<ObtenerFacturasDto>();
            foreach (var factura in listaFacturas)
            {
                var cliente = await _clientesAccesoBd.ObtenerClientePorId(factura.ClientesId);
                var solicitud = await _solicitudesAccesoBd.ObtenerSolicitudPorId(factura.SolicitudesId);

                var facturaDto = new ObtenerFacturasDto
                {
                    Id = factura.Id,

                    Nombre = cliente.Nombres,
                    Habitacion = cliente.Habitacion,
                    Identificacion = cliente.Identificacion,

                    SuplementoEntrega = solicitud.SuplementoEntrega,
                    Fecha = solicitud.Fecha,

                    Doblado = factura.Doblado,
                    Estado = factura.Estado,
                    Suplemento = factura.Suplemento,
                    TotalGlobal = factura.TotalGlobal,
                    TotalParcial = factura.TotalParcial
                };
                listaFacturasDto.Add(facturaDto);
            }
            return await Task.FromResult(listaFacturasDto);
        }

        public async Task<List<FacturasConDetalleDto>> ObtenerTodasFacturasConDetalle()
        {
            var listaFacturas = _context.Set<Factura>().ToList();
            var listaFactuasConDetalle = new List<FacturasConDetalleDto>();

            foreach (var factura in listaFacturas)
            {
                if (factura != default(Factura))
                {
                    var listaDetallesFacturaDto = new List<ObtenerDetalleFacturaDto>();
                    var listaDetallesFactura = (await EncontrarDetallesFactura(t => t.FacturaId == factura.Id)).ToList();

                    var cliente = await _clientesAccesoBd.ObtenerClientePorId(factura.ClientesId);
                    var solicitud = await _solicitudesAccesoBd.ObtenerSolicitudPorId(factura.SolicitudesId);

                    foreach (var detalleFactura in listaDetallesFactura)
                    {
                        var detalleSolicitud = await _solicitudesAccesoBd.ObtenerDetalleSolicitud(detalleFactura.DetalleSolicitudId);
                        var prendaClasificacion = (await _clasificacionPrendasAccesoBd.EncontrarPrendasClasificacion(t => t.Id == detalleSolicitud.PrendasClasificacionId)).FirstOrDefault();

                        var prenda = (await _clasificacionPrendasAccesoBd.EncontrarPrenda(t => t.Id == prendaClasificacion.PrendasId)).FirstOrDefault();
                        var clasificacion = (await _clasificacionPrendasAccesoBd.EncontrarClasificacion(t => t.Id == prendaClasificacion.ClasificacionId)).FirstOrDefault();

                        var detalleFacturaDto = new ObtenerDetalleFacturaDto
                        {
                            Id = detalleFactura.Id,

                            Clasificacion = clasificacion.Nombre,
                            Prenda = prenda.Nombre,

                            CantidadPrendasDetalle = detalleSolicitud.CantidadPrendas,
                            DobladoDetalle = detalleSolicitud.Doblado,
                            EstadoDetalle = detalleSolicitud.Estado,
                            LavadoPlanchadoDetalle = detalleSolicitud.LavadoPlanchado,
                            LavadoSecoDetalle = detalleSolicitud.LavadoSeco,
                            PlanchadoDetalle = detalleSolicitud.Planchado,

                            Doblado = detalleFactura.Doblado,
                            LavadoPlanchado = detalleFactura.LavadoPlanchado,
                            LavadoSeco = detalleFactura.LavadoSeco,
                            Planchado = detalleFactura.Planchado,
                            Total = detalleFactura.Total
                        };

                        listaDetallesFacturaDto.Add(detalleFacturaDto);
                    }

                    var listadoDetallesFacturas = new ListadoObtenerDetallesFacturaDto
                    {
                        DetallesFactura = listaDetallesFacturaDto
                    };

                    var facturaConDetalleDto = new FacturasConDetalleDto
                    {
                        Id = factura.Id,
                        Nombre = cliente.Nombres,
                        Identificacion = cliente.Identificacion,
                        Habitacion = cliente.Habitacion,

                        SuplementoEntrega = solicitud.SuplementoEntrega,
                        Fecha = solicitud.Fecha,

                        Doblado = factura.Doblado,
                        Estado = factura.Estado,
                        Suplemento = factura.Suplemento,
                        TotalGlobal = factura.TotalGlobal,
                        TotalParcial = factura.TotalParcial,
                        DetallesFacturas = listadoDetallesFacturas
                    };
                    listaFactuasConDetalle.Add(facturaConDetalleDto);
                }
            }
            return listaFactuasConDetalle;
        }

        public async Task<ObtenerFacturasDto> ObtenerFacturaPorId(int id)
        {
            var factura = (await EncontrarFactura(t => t.Id == id)).FirstOrDefault();
            if(factura != default(Factura))
            {
                var cliente = await _clientesAccesoBd.ObtenerClientePorId(factura.ClientesId);
                var solicitud = await _solicitudesAccesoBd.ObtenerSolicitudPorId(factura.SolicitudesId);

                var facturaDto = new ObtenerFacturasDto
                {
                    Id = factura.Id,

                    Nombre = cliente.Nombres,
                    Habitacion = cliente.Habitacion,
                    Identificacion = cliente.Identificacion,

                    SuplementoEntrega = solicitud.SuplementoEntrega,
                    Fecha = solicitud.Fecha,

                    Doblado = factura.Doblado,
                    Estado = factura.Estado,
                    Suplemento = factura.Suplemento,
                    TotalGlobal = factura.TotalGlobal,
                    TotalParcial = factura.TotalParcial
                };
                return facturaDto;
            }
            return default(ObtenerFacturasDto);
        }

        public async Task<int> GuardarFactura(GuardarFacturaDto factura)
        {
            var facturaEntidad = new Factura
            {
               ClientesId = factura.ClientesId,
               Doblado = factura.Doblado,
               Estado = factura.Estado,
               SolicitudesId = factura.SolicitudesId,
               Suplemento = factura.Suplemento,
               TotalGlobal = factura.TotalGlobal,
               TotalParcial = factura.TotalParcial
            };

            _context.Set<Factura>().Add(facturaEntidad);
            await _context.SaveChangesAsync();

            foreach (var detalleFactura in factura.DetallesFacturas.DetallesFactura)
            {
                var detalleFacturaEntidad = new DetalleFactura
                {
                     DetalleSolicitudId = detalleFactura.DetalleSolicitudId,
                     Doblado = detalleFactura.Doblado,
                     FacturaId = facturaEntidad.Id,
                     LavadoPlanchado = detalleFactura.LavadoPlanchado,
                     LavadoSeco = detalleFactura.LavadoSeco,
                     Planchado = detalleFactura.Planchado,
                     Total = detalleFactura.Total
                };

                _context.Set<DetalleFactura>().Add(detalleFacturaEntidad);
                await _context.SaveChangesAsync();
            }
            return facturaEntidad.Id;
        }

        public async Task ActualizarFactura(FacturasDto factura)
        {
            var facturaEntidad = _context.Set<Factura>().First(a => a.Id == factura.Id);
            if (facturaEntidad != default(Factura))
            {
                facturaEntidad.ClientesId = factura.ClientesId;
                facturaEntidad.Doblado = factura.Doblado;
                facturaEntidad.Estado = factura.Estado;
                facturaEntidad.SolicitudesId = factura.SolicitudesId;
                facturaEntidad.Suplemento = factura.Suplemento;
                facturaEntidad.TotalGlobal = factura.TotalGlobal;
                facturaEntidad.TotalParcial = factura.TotalParcial;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IQueryable<Factura>> EncontrarFactura(Expression<Func<Factura, bool>> expresion)
        {
            IQueryable<Factura> query = _context.Set<Factura>().Where(expresion);
            return await Task.FromResult(query);
        }

        public async Task<IQueryable<DetalleFactura>> EncontrarDetallesFactura(Expression<Func<DetalleFactura, bool>> expresion)
        {
            IQueryable<DetalleFactura> query = _context.Set<DetalleFactura>().Where(expresion);
            return await Task.FromResult(query);
        }
    }
}
