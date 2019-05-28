using Dtos.Clientes;
using Persistencia.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistencia.AccesoBD
{
    public class ClientesAccesoBD: IClientesAccesoBD
    {
        private LavanderiaDbContext _context;

        public ClientesAccesoBD(LavanderiaDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteDto>> ObtenerTodosClientes()
        {
            var listaClientes = new List<ClienteDto>();
            var solicitudes = _context.Clientes.ToList();
            foreach(var cliente in listaClientes)
            {
                var clienteDto = new ClienteDto
                {
                   Id = cliente.Id,
                   Habitacion = cliente.Habitacion,
                   Identificacion = cliente.Identificacion,
                   Nombres = cliente.Nombres
                };
                listaClientes.Add(clienteDto);
            }
            return await Task.FromResult(listaClientes);
        }

        public async Task<ClienteDto> ObtenerClientePorId(int id)
        {
            var cliente = (await EncontrarCliente(t => t.Id == id)).FirstOrDefault();
            if(cliente != default(Clientes))
            {
                var clienteDto = new ClienteDto
                {
                     Id = cliente.Id,
                     Habitacion = cliente.Habitacion,
                     Identificacion = cliente.Identificacion,
                     Nombres = cliente.Nombres
                };
                return clienteDto;
            }
            return default(ClienteDto); 
        }

        public async Task<IQueryable<Clientes>> EncontrarCliente(Expression<Func<Clientes, bool>> expresion)
        {
            IQueryable<Clientes> query = _context.Set<Clientes>().Where(expresion);
            return await Task.FromResult(query);
        }
    }
}
