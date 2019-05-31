using Dtos.Solicitud;
using Persistencia.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistencia.AccesoBD
{
    public class SolicitudesAccesoBD : ISolicitudesAccesoBD
    {
        private LavanderiaDbContext _context;
        private readonly IClasificacionPrendasAccesoBD _clasificacionPrendasAccesoBd;
        private readonly IClientesAccesoBD _clienteAccesoBd;

        public SolicitudesAccesoBD(LavanderiaDbContext context,
            IClasificacionPrendasAccesoBD clasificacionPrendasAccesoBd,
            IClientesAccesoBD clienteAccesoBd)
        {
            _context = context;
            _clasificacionPrendasAccesoBd = clasificacionPrendasAccesoBd;
            _clienteAccesoBd = clienteAccesoBd;
        }

        public async Task GuardarSolicitud(GuardarSolicitudDto solicitud)
        {
            var solicitudEntidad = new Solicitudes
            {
                ClienteId = solicitud.ClientesId,
                Estado = EstadosSolicitudes.SolicitudSolicitada,
                Fecha = DateTime.Now,
                SuplementoEntrega = solicitud.SuplementoEntrega
            };
            _context.Set<Solicitudes>().Add(solicitudEntidad);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarDetalleSolicitud(GuardarDetalleSolicitudDto detalleSolicitud)
        {
            var prendasClasificacion = (await _clasificacionPrendasAccesoBd.EncontrarPrendasClasificacion(t => t.ClasificacionId == detalleSolicitud.ClasificacionId && t.PrendasId == detalleSolicitud.PrendasId)).FirstOrDefault();
            var detalleSolicitudEntidad = new DetalleSolicitud
            {
                SolicitudesId = detalleSolicitud.SolicitudesId,
                Doblado = detalleSolicitud.Doblado,
                LavadoPlanchado = detalleSolicitud.LavadoPlanchado,
                LavadoSeco = detalleSolicitud.LavadoSeco,
                Planchado = detalleSolicitud.Planchado,
                Estado = EstadosSolicitudes.SolicitudSolicitada,
                CantidadPrendas = detalleSolicitud.CantidadPrendas,
                PrendasClasificacionId = prendasClasificacion.Id
            };
            _context.Set<DetalleSolicitud>().Add(detalleSolicitudEntidad);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarSolicitudConDetalles(GuardarSolicitudConDetallesDto solicitud)
        {
            var solicitudEntidad = new Solicitudes
            {
                ClienteId = solicitud.ClienteId,
                Estado = solicitud.Estado,
                Fecha = solicitud.Fecha,
                SuplementoEntrega = solicitud.SuplementoEntrega
            };
             _context.Set<Solicitudes>().Add(solicitudEntidad);
            await _context.SaveChangesAsync();

            foreach (var detalleSolicitud in solicitud.DetallesSolicitud.DetalleSolicitud)
            {
                if(detalleSolicitud.CantidadPrendas > 0)
                {
                    var detalleSolicitudEntidad = new DetalleSolicitud
                    {
                        SolicitudesId = solicitudEntidad.Id,
                        Doblado = detalleSolicitud.Doblado,
                        LavadoPlanchado = detalleSolicitud.LavadoPlanchado,
                        LavadoSeco = detalleSolicitud.LavadoSeco,
                        Planchado = detalleSolicitud.Planchado,
                        Estado = detalleSolicitud.Estado,
                        CantidadPrendas = detalleSolicitud.CantidadPrendas,
                        PrendasClasificacionId = detalleSolicitud.PrendasClasificacionId
                    };
                    _context.Set<DetalleSolicitud>().Add(detalleSolicitudEntidad);
                    await _context.SaveChangesAsync();
                } 
            }
        }

        public async Task<List<SolicitudConClienteDto>> ObtenerTodasSolicitudes()
        {
            var listaSolicitudesDto = new List<SolicitudConClienteDto>();

            var listaSolicitudes = _context.Set<Solicitudes>().ToList();
            foreach(var solicitud in listaSolicitudes)
            {
                var cliente = (await EncontrarCliente(t => t.Id == solicitud.ClienteId)).FirstOrDefault();
                var solicitudDto = new SolicitudConClienteDto
                {
                     Estado = solicitud.Estado,
                     Fecha = solicitud.Fecha,
                     Id = solicitud.Id,
                     SuplementoEntrega = solicitud.SuplementoEntrega,
                     Nombres = cliente.Nombres,
                     Habitacion = cliente.Habitacion
                };
                listaSolicitudesDto.Add(solicitudDto);
            }
            return await Task.FromResult(listaSolicitudesDto);
        }

        public async Task<List<DetalleSolicitudDto>> ObtenerDetalleSolicitudPorId(int idSolicitud)
        {
            var listaDetallesSolicitudesDto = new List<DetalleSolicitudDto>();
            var detalleSolicitudes = (await EncontrarDetallesSolicitudes(t => t.SolicitudesId == idSolicitud)).ToList();
            foreach(var detalle in detalleSolicitudes)
            {
                var detalleDto = new DetalleSolicitudDto
                {
                    Id = detalle.Id,
                    Estado = detalle.Estado,
                    Doblado = detalle.Doblado,
                    LavadoPlanchado = detalle.LavadoPlanchado,
                    LavadoSeco = detalle.LavadoSeco,
                    Planchado = detalle.Planchado,
                    CantidadPrendas = detalle.CantidadPrendas,
                    PrendasClasificacionId = detalle.PrendasClasificacionId,
                    SolicitudesId = detalle.SolicitudesId
                };
                listaDetallesSolicitudesDto.Add(detalleDto);
            }
            return listaDetallesSolicitudesDto;
        }

        public async Task<SolicitudDto> ObtenerSolicitudPorId(int id)
        {
            var solicitud = (await EncontrarSolicitudes(t => t.Id == id)).FirstOrDefault();
            if(solicitud != default(Solicitudes))
            {
                var solicitudDto = new SolicitudDto
                {
                     Id = solicitud.Id,
                     ClienteId = solicitud.ClienteId,
                     Estado = solicitud.Estado,
                     Fecha = solicitud.Fecha,
                     SuplementoEntrega = solicitud.SuplementoEntrega
                };
                return solicitudDto;
            }
            return default(SolicitudDto);
        }

        public async Task<SolicitudesConDetallesDto> ObtenerSolicitudConDetallePorId(int id)
        {
            var solicitud = (await EncontrarSolicitudes(t => t.Id == id)).FirstOrDefault();
            if(solicitud != default(Solicitudes))
            {
                var listaDetallesDto = new List<DetalleSolicitudDto>();
                var listaDetalles = (await EncontrarDetallesSolicitudes(t => t.SolicitudesId == solicitud.Id)).ToList();

                foreach (var detalle in listaDetalles)
                {
                    var prendaClasificacion = (await _clasificacionPrendasAccesoBd.EncontrarPrendasClasificacion(t => t.Id == detalle.PrendasClasificacionId)).FirstOrDefault();

                    var prenda = (await _clasificacionPrendasAccesoBd.EncontrarPrenda(t => t.Id == prendaClasificacion.PrendasId)).FirstOrDefault();
                    var clasificacion = (await _clasificacionPrendasAccesoBd.EncontrarClasificacion(t => t.Id == prendaClasificacion.ClasificacionId)).FirstOrDefault();

                    var detalleDto = new DetalleSolicitudDto
                    {
                        Id = detalle.Id,
                        Estado = detalle.Estado,
                        Doblado = detalle.Doblado,
                        LavadoPlanchado = detalle.LavadoPlanchado,
                        LavadoSeco = detalle.LavadoSeco,
                        Planchado = detalle.Planchado,
                        CantidadPrendas = detalle.CantidadPrendas,
                        PrendasClasificacionId = detalle.PrendasClasificacionId,
                        SolicitudesId = detalle.SolicitudesId,
                        Prenda = prenda.Nombre,
                        Clasificacion = clasificacion.Nombre
                    };
                    listaDetallesDto.Add(detalleDto);
                }

                var listadoDetalles = new ListadoDetallesSolicitudDto
                {
                    DetalleSolicitud = listaDetallesDto
                };
                var cliente = await _clienteAccesoBd.ObtenerClientePorId(solicitud.ClienteId);
                var solicitudConDetalle = new SolicitudesConDetallesDto
                {
                    ClienteId = solicitud.ClienteId,
                    Estado = solicitud.Estado,
                    Fecha = solicitud.Fecha,
                    Id = solicitud.Id,
                    SuplementoEntrega = solicitud.SuplementoEntrega,
                    ListadoDetallesSolicitud = listadoDetalles,
                    Habitacion = cliente.Habitacion,
                    Nombres = cliente.Nombres
                };
                return solicitudConDetalle;
            }
            return default(SolicitudesConDetallesDto);
        }

        public async Task<List<SolicitudesConDetallesDto>> ObtenerTodasSolicitudesConDetalle()
        {
            var listaSolicitudConDetalles = new List<SolicitudesConDetallesDto>();
            var solicitudes = _context.Set<Solicitudes>().ToList();
            foreach (var solicitud in solicitudes)
            {
                var listaDetalles = (await EncontrarDetallesSolicitudes(t => t.SolicitudesId == solicitud.Id)).ToList();
                var listaDetallesDto = new List<DetalleSolicitudDto>();

                foreach (var detalle in listaDetalles)
                {
                    var prendaClasificacion = (await _clasificacionPrendasAccesoBd.EncontrarPrendasClasificacion(t => t.Id == detalle.PrendasClasificacionId)).FirstOrDefault();

                    var prenda = (await _clasificacionPrendasAccesoBd.EncontrarPrenda(t => t.Id == prendaClasificacion.PrendasId)).FirstOrDefault();
                    var clasificacion = (await _clasificacionPrendasAccesoBd.EncontrarClasificacion(t => t.Id == prendaClasificacion.ClasificacionId)).FirstOrDefault();

                    var detalleDto = new DetalleSolicitudDto
                    {
                        Id = detalle.Id,
                        Estado = detalle.Estado,
                        Doblado = detalle.Doblado,
                        LavadoPlanchado = detalle.LavadoPlanchado,
                        LavadoSeco = detalle.LavadoSeco,
                        Planchado = detalle.Planchado,
                        CantidadPrendas = detalle.CantidadPrendas,
                        PrendasClasificacionId = detalle.PrendasClasificacionId,
                        SolicitudesId = detalle.SolicitudesId,
                        Clasificacion = clasificacion.Nombre,
                        Prenda = prenda.Nombre
                    };
                    listaDetallesDto.Add(detalleDto);
                }

                var listadoDetalles = new ListadoDetallesSolicitudDto
                {
                    DetalleSolicitud = listaDetallesDto
                };
                var cliente = await _clienteAccesoBd.ObtenerClientePorId(solicitud.ClienteId);

                var solicitudConDetalle = new SolicitudesConDetallesDto
                {
                    ClienteId = solicitud.ClienteId,
                    Estado = solicitud.Estado,
                    Fecha = solicitud.Fecha,
                    Id = solicitud.Id,
                    SuplementoEntrega = solicitud.SuplementoEntrega,
                    ListadoDetallesSolicitud = listadoDetalles,
                    Habitacion = cliente.Habitacion,
                    Nombres = cliente.Nombres
                };
                listaSolicitudConDetalles.Add(solicitudConDetalle);
            }
            return await Task.FromResult(listaSolicitudConDetalles);
        }

        public async Task<List<SolicitudesConDetallesDto>> ConsultarSolicitudConDetallePorEstado(string estado)
        {
            var solicitudes = (await EncontrarSolicitudes(t => t.Estado == estado)).ToList();
            var listaSolicitudConDetalles = new List<SolicitudesConDetallesDto>();

            foreach (var solicitud in solicitudes)
            {
                var listaDetalles = (await EncontrarDetallesSolicitudes(t => t.SolicitudesId == solicitud.Id)).ToList();
                var listaDetallesDto = new List<DetalleSolicitudDto>();

                foreach (var detalle in listaDetalles)
                {
                    var prendaClasificacion = (await _clasificacionPrendasAccesoBd.EncontrarPrendasClasificacion(t => t.Id == detalle.PrendasClasificacionId)).FirstOrDefault();

                    var prenda = (await _clasificacionPrendasAccesoBd.EncontrarPrenda(t => t.Id == prendaClasificacion.PrendasId)).FirstOrDefault();
                    var clasificacion = (await _clasificacionPrendasAccesoBd.EncontrarClasificacion(t => t.Id == prendaClasificacion.ClasificacionId)).FirstOrDefault();

                    var detalleDto = new DetalleSolicitudDto
                    {
                        Id = detalle.Id,
                        Estado = detalle.Estado,
                        Doblado = detalle.Doblado,
                        LavadoPlanchado = detalle.LavadoPlanchado,
                        LavadoSeco = detalle.LavadoSeco,
                        Planchado = detalle.Planchado,
                        CantidadPrendas = detalle.CantidadPrendas,
                        PrendasClasificacionId = detalle.PrendasClasificacionId,
                        SolicitudesId = detalle.SolicitudesId,
                        Clasificacion = clasificacion.Nombre,
                        Prenda = prenda.Nombre
                    };
                    listaDetallesDto.Add(detalleDto);
                }

                var listadoDetalles = new ListadoDetallesSolicitudDto
                {
                    DetalleSolicitud = listaDetallesDto
                };
                var cliente = await _clienteAccesoBd.ObtenerClientePorId(solicitud.ClienteId);

                var solicitudConDetalle = new SolicitudesConDetallesDto
                {
                    ClienteId = solicitud.ClienteId,
                    Estado = solicitud.Estado,
                    Fecha = solicitud.Fecha,
                    Id = solicitud.Id,
                    SuplementoEntrega = solicitud.SuplementoEntrega,
                    ListadoDetallesSolicitud = listadoDetalles,
                    Habitacion = cliente.Habitacion,
                    Nombres = cliente.Nombres
                };
                listaSolicitudConDetalles.Add(solicitudConDetalle);
            }
            return await Task.FromResult(listaSolicitudConDetalles);
        }

        public async Task ActualizarSolicitud(SolicitudDto solicitud)
        {
            var solicitudEntidad = _context.Set<Solicitudes>().First(a => a.Id == solicitud.Id);
            if(solicitudEntidad != default(Solicitudes))
            {
                solicitudEntidad.ClienteId = solicitud.ClienteId;
                solicitudEntidad.Estado = solicitud.Estado;
                solicitudEntidad.Fecha = solicitud.Fecha;
                solicitudEntidad.SuplementoEntrega = solicitud.SuplementoEntrega;

                await _context.SaveChangesAsync();
            } 
        }

        public async Task ActualizarDetalleSolicitud(DetalleSolicitudDto detalle)
        {
            var detalleEntidad = _context.Set<DetalleSolicitud>().First(a => a.Id == detalle.Id);
            if (detalleEntidad != default(DetalleSolicitud))
            {
                detalleEntidad.Id = detalle.Id;
                detalleEntidad.CantidadPrendas = detalle.CantidadPrendas;
                detalleEntidad.Doblado = detalle.Doblado;
                detalleEntidad.Estado = detalle.Estado;
                detalleEntidad.LavadoPlanchado = detalle.LavadoPlanchado;
                detalleEntidad.LavadoSeco = detalle.LavadoSeco;
                detalleEntidad.Planchado = detalle.Planchado;
                detalleEntidad.PrendasClasificacionId = detalle.PrendasClasificacionId;
                detalleEntidad.SolicitudesId = detalle.SolicitudesId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarSolicitud(int id)
        {
            _context.Solicitudes.Remove(_context.Solicitudes.Single(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<Solicitudes>> EncontrarSolicitudes(Expression<Func<Solicitudes, bool>> expresion)
        {
            IQueryable<Solicitudes> query = _context.Set<Solicitudes>().Where(expresion);
            return await Task.FromResult(query);
        }

        public async Task<IQueryable<DetalleSolicitud>> EncontrarDetallesSolicitudes(Expression<Func<DetalleSolicitud, bool>> expresion)
        {
            IQueryable<DetalleSolicitud> query = _context.Set<DetalleSolicitud>().Where(expresion);
            return await Task.FromResult(query);
        }

        public async Task<IQueryable<Clientes>> EncontrarCliente(Expression<Func<Clientes, bool>> expresion)
        {
            IQueryable<Clientes> query = _context.Set<Clientes>().Where(expresion);
            return await Task.FromResult(query);
        }
    }
}
