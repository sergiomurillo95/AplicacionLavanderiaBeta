using Dtos.Facturas;
using Persistencia.Entidades;
using System.Threading.Tasks;

namespace Persistencia.AccesoBD
{
    public class FacturaAccesoBD : IFacturaAccesoBD
    {
        private LavanderiaDbContext _context;

        public FacturaAccesoBD(LavanderiaDbContext context)
        {
            _context = context;
        }

        public async Task GuardarFactura(GuardarFacturaDto factura)
        {
            var facturaEntidad = new Factura
            {
               ClientesId = factura.ClientesId,
               Doblado = factura.Doblado,
               Estado = factura.Estado,
               SolicitudesId = factura.SolicitudesId,
               Suplemento = factura.Suplemento,
               TotalGlobal = factura.TotalGlobal,
               TotalParcial = factura.TotalParcial
            };

            var entry = _context.Factura.Add(facturaEntidad);

            foreach (var detalleFactura in factura.DetallesFacturas.DetallesFactura)
            {
                var detalleFacturaEntidad = new DetalleFactura
                {
                     DetalleSolicitudId = detalleFactura.DetalleSolicitudId,
                     Doblado = detalleFactura.Doblado,
                     FacturaId = entry.Id,
                     LavadoPlanchado = detalleFactura.LavadoPlanchado,
                     LavadoSeco = detalleFactura.LavadoSeco,
                     Planchado = detalleFactura.Planchado,
                     Total = detalleFactura.Total
                };

                _context.DetalleFactura.Add(detalleFacturaEntidad);
            }
            await _context.SaveChangesAsync();
        }
    }
}
