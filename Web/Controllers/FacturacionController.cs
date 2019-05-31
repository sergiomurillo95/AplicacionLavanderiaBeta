using Dtos.Solicitud;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lavanderia.Controllers
{
    public class FacturacionController : Controller
    {
        
        private readonly ISolicitudesLogica _solicitudLogica;
        private readonly IClientesLogica _clientesLogica;
        private readonly IClasificacionPrendasLogica _clasificacionPrendasLogica;
        private readonly IFacturasLogica _facturasLogica;

        public FacturacionController(IClientesLogica clientesLogica,
            ISolicitudesLogica solicitudLogica, IClasificacionPrendasLogica clasificacionPrendasLogica, IFacturasLogica facturasLogica)
        {
            _clientesLogica = clientesLogica;
            _solicitudLogica = solicitudLogica;
            _clasificacionPrendasLogica = clasificacionPrendasLogica;
            _facturasLogica = facturasLogica;
        }
        // GET: Lavanderia
        public ActionResult Index()
        {
            var solicitudes = _solicitudLogica.ObtenerTodasSolicitudes().Result;
            
            return View(solicitudes);
        }


        public ActionResult GenerarFactura (int id)
        {
            ViewData["id"] = id;
            var estado = "Facturado";
            _solicitudLogica.CambiarEstadSolicitud(id, estado);

            var factura = _facturasLogica.GenerarFacturaDesdeSolicitud(id);
            var idFactura = factura.Id;
            var facturaDetalle = _facturasLogica.ObtenerFacturaConDetallesPorId(idFactura).Result;
            //return RedirectToAction("ConsultarFactura", new { id = factura.Id })
            return View(facturaDetalle);
        }

        public ActionResult ConsultarFactura(int id)
        {
            ViewData["id"] = id;

            var factura = _facturasLogica.ObtenerFacturaConDetallesPorId(id).Result;

            return View(factura);
        }


        public ActionResult ListarDetalleSolicitudes(int? id)
        {
            ViewData["id"] = id;
            var solicitudes = _solicitudLogica.ObtenerSolicitudConDetallePorId(id.Value).Result;

            return View(solicitudes);
        }
    }
}