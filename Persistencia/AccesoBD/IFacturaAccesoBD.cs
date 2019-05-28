using Dtos.Facturas;
using System.Threading.Tasks;

namespace Persistencia.AccesoBD
{
    public interface IFacturaAccesoBD
    {
        Task GuardarFactura(GuardarFacturaDto factura);
    }
}
