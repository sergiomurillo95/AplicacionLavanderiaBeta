using Dtos.Solicitud;
using Persistencia.AccesoBD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logica
{
    public class SolicitudesLogica : ISolicitudesLogica
    {
        private readonly ISolicitudesAccesoBD _solicitudesAccesoBd;

        public SolicitudesLogica(ISolicitudesAccesoBD solicitudesAccesoBd)
        {
            _solicitudesAccesoBd = solicitudesAccesoBd;
        }

        /// <summary>
        /// Condiciones de negocio:
        /// 1) La solicitud debe tener por lo menos un detalle para poder ser almacenada
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        public async Task GuardarSolicitud(GuardarSolicitudDto solicitud)
        {
            if(solicitud.DetallesSolicitud.DetalleSolicitud.Count > 0)
            {
                await _solicitudesAccesoBd.GuardarSolicitudConDetalles(solicitud);
            } 
        }

        /// <summary>
        /// Condiciones de negocio:
        /// 1) No se puede pasar del estado: Facturada a atendida , Facturada a pendiente, atendida a pendiente
        /// 2) Debe existir dicha solicitud
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        public async Task CambiarEstadoSolicitud(SolicitudDto solicitud)
        {
            if(solicitud != default(SolicitudDto))
            {
                var solicitudDto = await _solicitudesAccesoBd.ObtenerSolicitudPorId(solicitud.Id);
                if(solicitudDto != default(SolicitudDto))
                {
                    var estadoAnt = solicitudDto.Estado;
                    var estadoNue = solicitud.Estado;
                    if ((estadoAnt == EstadosSolicitudes.SolicitudFinalizada && estadoNue == EstadosSolicitudes.SolicitudEnProceso) || (estadoAnt == EstadosSolicitudes.SolicitudFinalizada && estadoNue == EstadosSolicitudes.SolicitudSolicitada))
                    {
                        return;
                    }
                    await _solicitudesAccesoBd.ActualizarSolicitud(solicitud);
                }
            }
        }

        public async Task<List<SolicitudDto>> ObtenerTodasSolicitudes()
        {
            return await _solicitudesAccesoBd.ObtenerTodasSolicitudes();
        }

        public async Task<List<SolicitudesConDetallesDto>> ObtenerTodasSolicitudesConDetalle()
        {
            return await _solicitudesAccesoBd.ObtenerTodasSolicitudesConDetalle();
        }

        public async Task<SolicitudesConDetallesDto> ObtenerSolicitudConDetallePorId(int id)
        {
            return await _solicitudesAccesoBd.ObtenerSolicitudConDetallePorId(id);
        }

        public async Task<List<SolicitudesConDetallesDto>> ConsultarSolicitudConDetallePorEstado(string estado)
        {
            return await _solicitudesAccesoBd.ConsultarSolicitudConDetallePorEstado(estado);
        }

        public async Task<SolicitudDto> ObtenerSolicitudPorId(int id)
        {
            return await _solicitudesAccesoBd.ObtenerSolicitudPorId(id);
        }

        public async Task ActualizarSolicitud(SolicitudDto solicitud)
        {
            await _solicitudesAccesoBd.ActualizarSolicitud(solicitud);
        }
    }
}
