using Dtos.Solicitud;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logica
{
    public interface ISolicitudesLogica
    {
        Task GuardarSolicitud(GuardarSolicitudDto solicitud);
        Task GuardarSolicitudConDetalle(GuardarSolicitudConDetallesDto solicitud);
        Task GuardarDetalleSolicitud(GuardarDetalleSolicitudDto detalleSolicitud);
        Task CambiarEstadoSolicitud(SolicitudDto solicitud);

        Task<List<SolicitudesConDetallesDto>> ObtenerTodasSolicitudesConDetalle();
        Task<List<SolicitudConClienteDto>> ObtenerTodasSolicitudes();
        Task<SolicitudesConDetallesDto> ObtenerSolicitudConDetallePorId(int id);
        Task<List<DetalleSolicitudDto>> ObtenerDetalleSolicitudPorId(int idSolicitud);

        Task CambiarEstadSolicitud(int id, string estado);
        Task<List<SolicitudesConDetallesDto>> ConsultarSolicitudConDetallePorEstado(string estado);
        Task<SolicitudDto> ObtenerSolicitudPorId(int id);
        Task ActualizarSolicitud(SolicitudDto solicitud);
    }
}
