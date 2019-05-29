using Dtos.PrendasClasificacion;
using Persistencia.AccesoBD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logica
{
    public class ClasificacionPrendasLogica : IClasificacionPrendasLogica
    {
        private readonly IClasificacionPrendasAccesoBD _clasificacionPrendasAccesoBd;

        public ClasificacionPrendasLogica(IClasificacionPrendasAccesoBD clasificacionPrendasAccesoBd)
        {
            _clasificacionPrendasAccesoBd = clasificacionPrendasAccesoBd;
        }

        public async Task<PrendasDto> ObtenerPrendaPorId(int id)
        {
            return await _clasificacionPrendasAccesoBd.ObtenerPrendaPorId(id);
        }

        public async Task<List<PrendaClasificacionDto>> ObtenerTodasPrendasConClasificacion()
        {
            return await _clasificacionPrendasAccesoBd.ObtenerTodasPrendasConClasificacion();
        }

        public async Task<CostoDto> ObtenerCostoPorIdPrendaClasificacion(int idPrendaClasificacion)
        {
            return await _clasificacionPrendasAccesoBd.ObtenerCostoPorIdPrendaClasificacion(idPrendaClasificacion);
        }
    }
}
