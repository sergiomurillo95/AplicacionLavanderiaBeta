using Dtos.PrendasClasificacion;
using Persistencia.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistencia.AccesoBD
{
    public class ClasificacionPrendasAccesoBD : IClasificacionPrendasAccesoBD
    {
        private LavanderiaDbContext _context;

        public ClasificacionPrendasAccesoBD(LavanderiaDbContext context)
        {
            _context = context;
        }

        public async Task<List<PrendaClasificacionDto>> ObtenerTodasPrendasConClasificacion()
        {
            var listaPrendasClasificacionDto = new List<PrendaClasificacionDto>();
            var listaPrendasClasificacion = _context.Set<PrendasClasificacion>().ToList();
            foreach(var prendaClasificacion in listaPrendasClasificacion)
            {
                var prendaClasificacionDto = await ObtenerPrendaClasificacionDto(prendaClasificacion.Id, prendaClasificacion.PrendasId, prendaClasificacion.ClasificacionId);
                listaPrendasClasificacionDto.Add(prendaClasificacionDto);
            }
            return listaPrendasClasificacionDto;
        }

        private async Task<PrendaClasificacionDto> ObtenerPrendaClasificacionDto(int idPrendaClasificacion, int prendaId, int clasificacionId)
        {
            var prenda = (await EncontrarPrenda(t => t.Id == prendaId)).FirstOrDefault();
            var clasificacion = (await EncontrarClasificacion(t => t.Id == clasificacionId)).FirstOrDefault();
            if(prenda != default(Prendas) && clasificacion != default(Clasificacion))
            {
                var prendaClasificacionDto = new PrendaClasificacionDto
                {
                    Id = idPrendaClasificacion,
                    NombrePrenda = prenda.Nombre,
                    NombreClasificacion = clasificacion.Nombre
                };
                return prendaClasificacionDto;
            }
            return default(PrendaClasificacionDto);
        }

        public async Task<CostoDto> ObtenerCostoPorIdPrendaClasificacion(int idPrendaClasificacion)
        {
            var costo = (await EncontrarCosto(t => t.PrendasClasificacionId == idPrendaClasificacion)).FirstOrDefault();

            var prendaClasificacion = (await EncontrarPrendasClasificacion(t => t.Id == costo.PrendasClasificacionId)).FirstOrDefault();
            var prendaClasificacionDto = await ObtenerPrendaClasificacionDto(prendaClasificacion.Id, prendaClasificacion.PrendasId, prendaClasificacion.ClasificacionId);
            var costoDto = new CostoDto
            {
                Id = costo.Id,
                PrendaClasificacion = prendaClasificacionDto,
                Doblado = costo.Doblado,
                LavadoPlanchado = costo.LavadoPlanchado,
                LavadoSeco = costo.LavadoSeco,
                Planchado = costo.Planchado
            };

            return costoDto;
        }

        public async Task<List<CostoDto>> ObtenerTodosCostos()
        {
            var listaCostoDto= new List<CostoDto>();
            var listaCosto = _context.Set<Costo>().ToList();
            foreach (var costo in listaCosto)
            {
                var prendaClasificacion = (await EncontrarPrendasClasificacion(t => t.Id == costo.PrendasClasificacionId)).FirstOrDefault();
                var prendaClasificacionDto = await ObtenerPrendaClasificacionDto(prendaClasificacion.Id, prendaClasificacion.PrendasId, prendaClasificacion.ClasificacionId);
                var costoDto = new CostoDto
                {
                     Id = costo.Id,
                     PrendaClasificacion = prendaClasificacionDto,
                     Doblado = costo.Doblado,
                     LavadoPlanchado = costo.LavadoPlanchado,
                     LavadoSeco = costo.LavadoSeco,
                     Planchado = costo.Planchado
                };
                listaCostoDto.Add(costoDto);
            }
            return listaCostoDto;
        }

        public async Task<List<PrendasDto>> ObtenerTodasPrendas()
        {
            var prendas = new List<PrendasDto>();
            var listaPrendas = _context.Set<Prendas>().ToList();
            foreach(var prenda in listaPrendas)
            {
                var prendaDto = new PrendasDto
                {
                     Id = prenda.Id,
                     Nombre = prenda.Nombre
                };
                prendas.Add(prendaDto);
            }
            return await Task.FromResult(prendas);
        }

        public async Task<List<ClasificacionDto>> ObtenerTodasClasificacion()
        {
            var clasificaciones = new List<ClasificacionDto>();
            var listaClasificacion = _context.Set<Clasificacion>().ToList();
            foreach (var clasificacion in listaClasificacion)
            {
                var clasificacionDto = new ClasificacionDto
                {
                    Id = clasificacion.Id,
                    Nombre = clasificacion.Nombre
                };
                clasificaciones.Add(clasificacionDto);
            }
            return await Task.FromResult(clasificaciones);
        }

        public async Task<PrendasDto> ObtenerPrendaPorId(int id)
        {
            var prenda = (await EncontrarPrenda(t => t.Id == id)).FirstOrDefault();
            if(prenda != default(Prendas))
            {
                var predaDto = new PrendasDto
                {
                     Id = prenda.Id,
                     Nombre = prenda.Nombre
                };
                return predaDto;
            }
            return default(PrendasDto);
        }

        public async Task<IQueryable<PrendasClasificacion>> EncontrarPrendasClasificacion(Expression<Func<PrendasClasificacion, bool>> expresion)
        {
            IQueryable<PrendasClasificacion> query = _context.Set<PrendasClasificacion>().Where(expresion);
            return await Task.FromResult(query);
        }

        public async Task<IQueryable<Prendas>> EncontrarPrenda(Expression<Func<Prendas, bool>> expresion)
        {
            IQueryable<Prendas> query = _context.Set<Prendas>().Where(expresion);
            return await Task.FromResult(query);
        }

        public async Task<IQueryable<Costo>> EncontrarCosto(Expression<Func<Costo, bool>> expresion)
        {
            IQueryable<Costo> query = _context.Set<Costo>().Where(expresion);
            return await Task.FromResult(query);
        }

        public async Task<IQueryable<Clasificacion>> EncontrarClasificacion(Expression<Func<Clasificacion, bool>> expresion)
        {
            IQueryable<Clasificacion> query = _context.Set<Clasificacion>().Where(expresion);
            return await Task.FromResult(query);
        }
    }
}
