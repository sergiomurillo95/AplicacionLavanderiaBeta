using Dtos.Clientes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.AccesoBD
{
    public interface IClientesAccesoBD
    {
        Task<List<ClienteDto>> ObtenerTodosClientes();
    }
}
