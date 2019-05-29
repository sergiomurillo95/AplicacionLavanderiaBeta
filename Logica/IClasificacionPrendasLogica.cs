using Dtos.PrendasClasificacion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logica
{
    public interface IClasificacionPrendasLogica
    {
        Task<PrendasDto> ObtenerPrendaPorId(int id);
        Task<List<PrendaClasificacionDto>> ObtenerTodasPrendasConClasificacion();
        Task<CostoDto> ObtenerCostoPorIdPrendaClasificacion(int idPrendaClasificacion);
    }
}
