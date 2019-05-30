using Dtos.Solicitud;
using Persistencia.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistencia.AccesoBD
{
    public interface ISolicitudesAccesoBD
    {
        Task GuardarSolicitudConDetalles(GuardarSolicitudDto solicitud);
        Task<SolicitudesConDetallesDto> ObtenerSolicitudConDetallePorId(int id);
        Task<List<SolicitudDto>> ObtenerTodasSolicitudes();
        Task<SolicitudDto> ObtenerSolicitudPorId(int id);
        Task<List<SolicitudesConDetallesDto>> ObtenerTodasSolicitudesConDetalle();
        Task<List<SolicitudesConDetallesDto>> ConsultarSolicitudConDetallePorEstado(string estado);
        Task ActualizarSolicitud(SolicitudDto solicitud);
        Task EliminarSolicitud(int id);
        Task<IQueryable<Solicitudes>> EncontrarSolicitudes(Expression<Func<Solicitudes, bool>> expresion);
        Task<IQueryable<DetalleSolicitud>> EncontrarDetallesSolicitudes(Expression<Func<DetalleSolicitud, bool>> expresion);
    }
}
