using Dtos.Facturas;
using Persistencia.AccesoBD;
using System.Threading.Tasks;

namespace Logica
{
    public class FacturasLogica: IFacturasLogica
    {
        private readonly IFacturaAccesoBD _facturaAccesoBd;

        public FacturasLogica(IFacturaAccesoBD facturaAccesoBd)
        {
            _facturaAccesoBd = facturaAccesoBd;
        }

        /// <summary>
        /// Condiciones de negocio:
        /// 1) Deben existir detalles en la factura
        /// 2) El valor de la factura debe ser mayor que cero
        /// </summary>
        /// <param name="factura"></param>
        /// <returns></returns>
        public async Task GuardarFactura(GuardarFacturaDto factura)
        {
            if(factura.DetallesFacturas.DetallesFactura.Count > 0)
            {
                await _facturaAccesoBd.GuardarFactura(factura);
            } 
        }
    }
}
