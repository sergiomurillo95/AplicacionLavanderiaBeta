using Dtos.Clientes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logica
{
    public interface IClientesLogica
    {
        Task<List<ClienteDto>> ObtenerTodosClientes();
        Task<ClienteDto> ObtenerClientePorId(int id);
    }
}
