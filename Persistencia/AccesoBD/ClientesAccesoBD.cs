using Dtos.Clientes;
using System.Collections.Generic;
using System.Linq;
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
    }
}
