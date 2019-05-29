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

        public FacturaAccesoBD(LavanderiaDbContext context)
        {
            _context = context;
        }

        public async Task<FacturasConDetalleDto> ObtenerFacturaConDetallesPorId(int id)
        {
            var factura = (await EncontrarFactura(t => t.Id == id)).FirstOrDefault();
            if (factura != default(Factura))
            {
                var listaDetallesFacturaDto = new List<DetalleFacturaDto>();
                var listaDetallesFactura = (await EncontrarDetallesFactura(t => t.FacturaId == factura.Id)).ToList();
                foreach(var detalleFactura in listaDetallesFactura)
                {
                    var detalleFacturaDto = new DetalleFacturaDto
                    {
                         Id = detalleFactura.Id,
                         DetalleSolicitudId = detalleFactura.DetalleSolicitudId,
                         Doblado = detalleFactura.Doblado,
                         FacturaId = detalleFactura.FacturaId,
                         LavadoPlanchado = detalleFactura.LavadoPlanchado,
                         LavadoSeco = detalleFactura.LavadoSeco,
                         Planchado = detalleFactura.Planchado,
                         Total = detalleFactura.Total
                    };
                    listaDetallesFacturaDto.Add(detalleFacturaDto);
                }

                var listadoDetallesFacturas = new ListadoDetallesFacturaDto
                {
                    DetallesFactura = listaDetallesFacturaDto
                };

                var facturaConDetalleDto = new FacturasConDetalleDto
                {
                     Id = factura.Id,
                     ClientesId = factura.ClientesId,
                     Doblado = factura.Doblado,
                     Estado = factura.Estado,
                     SolicitudesId = factura.SolicitudesId,
                     Suplemento = factura.Suplemento,
                     TotalGlobal = factura.TotalGlobal,
                     TotalParcial = factura.TotalParcial,
                     DetallesFacturas = listadoDetallesFacturas
                };

                return facturaConDetalleDto;
            }
            return default(FacturasConDetalleDto);
        }

        public async Task<List<FacturasDto>> ObtenerTodasFacturas()
        {
            var listaFacturas = _context.Set<Factura>().ToList();
            var listaFacturasDto = new List<FacturasDto>();
            foreach (var factura in listaFacturas)
            {
                var facturaDto = new FacturasDto
                {
                    Id = factura.Id,
                    ClientesId = factura.ClientesId,
                    Doblado = factura.Doblado,
                    Estado = factura.Estado,
                    SolicitudesId = factura.SolicitudesId,
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
                    var listaDetallesFacturaDto = new List<DetalleFacturaDto>();
                    var listaDetallesFactura = (await EncontrarDetallesFactura(t => t.FacturaId == factura.Id)).ToList();
                    foreach (var detalleFactura in listaDetallesFactura)
                    {
                        var detalleFacturaDto = new DetalleFacturaDto
                        {
                            Id = detalleFactura.Id,
                            DetalleSolicitudId = detalleFactura.DetalleSolicitudId,
                            Doblado = detalleFactura.Doblado,
                            FacturaId = detalleFactura.FacturaId,
                            LavadoPlanchado = detalleFactura.LavadoPlanchado,
                            LavadoSeco = detalleFactura.LavadoSeco,
                            Planchado = detalleFactura.Planchado,
                            Total = detalleFactura.Total
                        };
                        listaDetallesFacturaDto.Add(detalleFacturaDto);
                    }

                    var listadoDetallesFacturas = new ListadoDetallesFacturaDto
                    {
                        DetallesFactura = listaDetallesFacturaDto
                    };

                    var facturaConDetalleDto = new FacturasConDetalleDto
                    {
                        Id = factura.Id,
                        ClientesId = factura.ClientesId,
                        Doblado = factura.Doblado,
                        Estado = factura.Estado,
                        SolicitudesId = factura.SolicitudesId,
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

        public async Task<FacturasDto> ObtenerFacturaPorId(int id)
        {
            var factura = (await EncontrarFactura(t => t.Id == id)).FirstOrDefault();
            if(factura != default(Factura))
            {
                var facturaDto = new FacturasDto
                {
                     Id = factura.Id,
                     ClientesId = factura.ClientesId,
                     Doblado = factura.Doblado,
                     Estado = factura.Estado,
                     SolicitudesId = factura.SolicitudesId,
                     Suplemento = factura.Suplemento,
                     TotalGlobal = factura.TotalGlobal,
                     TotalParcial = factura.TotalParcial
                };
                return facturaDto;
            }
            return default(FacturasDto);
        }

        public async Task GuardarFactura(GuardarFacturaDto factura)
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
