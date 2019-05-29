using Dtos.PrendasClasificacion;
using Persistencia.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistencia.AccesoBD
{
    public interface IClasificacionPrendasAccesoBD
    {
        Task<List<PrendaClasificacionDto>> ObtenerTodasPrendasConClasificacion();
        Task<CostoDto> ObtenerCostoPorIdPrendaClasificacion(int idPrendaClasificacion);
        Task<List<CostoDto>> ObtenerTodosCostos();
        Task<PredasDto> ObtenerPrendaPorId(int id);
        Task<IQueryable<Prendas>> EncontrarPrenda(Expression<Func<Prendas, bool>> expresion);
        Task<IQueryable<Clasificacion>> EncontrarClasificacion(Expression<Func<Clasificacion, bool>> expresion);
        Task<IQueryable<PrendasClasificacion>> EncontrarPrendasClasificacion(Expression<Func<PrendasClasificacion, bool>> expresion);
        Task<IQueryable<Costo>> EncontrarCosto(Expression<Func<Costo, bool>> expresion);
    }
}
