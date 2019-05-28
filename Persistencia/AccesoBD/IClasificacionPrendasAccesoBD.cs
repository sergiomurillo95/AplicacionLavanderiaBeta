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
        Task<List<CostoDto>> ObtenerTodosCostos();
        Task<IQueryable<Prendas>> EncontrarPrenda(Expression<Func<Prendas, bool>> expresion);
        Task<IQueryable<Clasificacion>> EncontrarClasificacion(Expression<Func<Clasificacion, bool>> expresion);
    }
}
