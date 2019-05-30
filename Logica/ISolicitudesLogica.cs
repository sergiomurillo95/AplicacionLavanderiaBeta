using Dtos.Solicitud;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logica
{
    public interface ISolicitudesLogica
    {
        Task GuardarSolicitud(GuardarSolicitudConDetallesDto solicitud);
        Task GuardarDetalleSolicitud(GuardarDetalleSolicitudDto detalleSolicitud);
        Task CambiarEstadoSolicitud(SolicitudDto solicitud);
        Task<List<SolicitudesConDetallesDto>> ObtenerTodasSolicitudesConDetalle();
        Task<List<SolicitudConClienteDto>> ObtenerTodasSolicitudes();
        Task<SolicitudesConDetallesDto> ObtenerSolicitudConDetallePorId(int id);
        Task<List<SolicitudesConDetallesDto>> ConsultarSolicitudConDetallePorEstado(string estado);
        Task<SolicitudDto> ObtenerSolicitudPorId(int id);
        Task ActualizarSolicitud(SolicitudDto solicitud);
    }
}
