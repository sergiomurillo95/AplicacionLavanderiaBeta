using Dtos.Clientes;
using Persistencia.AccesoBD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logica
{
    public class ClientesLogica : IClientesLogica
    {
        private readonly IClientesAccesoBD _clientesAccesoBd;

        public ClientesLogica(IClientesAccesoBD clientesAccesoBd)
        {
            _clientesAccesoBd = clientesAccesoBd;
        }

        public async Task<List<ClienteDto>> ObtenerTodosClientes()
        {
            return await _clientesAccesoBd.ObtenerTodosClientes();
        }

        public async Task<ClienteDto> ObtenerClientePorId(int id)
        {
            return await _clientesAccesoBd.ObtenerClientePorId(id);
        }
    }
}
