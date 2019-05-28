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
        Task GuardarSolicitud(GuardarSolicitudDto solicitud);
        Task<SolicitudesConDetallesDto> ObtenerSolicitudPorId(int id);
        Task<List<SolicitudesConDetallesDto>> ObtenerTodasSolicitudes();
        Task<List<SolicitudesConDetallesDto>> ConsultarSolicitudPorEstado(string estado);
        Task ActualizarSolicitud(SolicitudDto solicitud);
        Task EliminarSolicitud(int id);
        Task<IQueryable<Solicitudes>> EncontrarSolicitudes(Expression<Func<Solicitudes, bool>> expresion);
        Task<IQueryable<DetalleSolicitud>> EncontrarDetallesSolicitudes(Expression<Func<DetalleSolicitud, bool>> expresion);
    }
}
