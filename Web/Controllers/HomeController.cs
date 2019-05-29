using Dtos.Solicitud;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lavanderia.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientesLogica _clientesLogica;
        private readonly ISolicitudesLogica _solicitudLogica;

        public HomeController(IClientesLogica clientesLogica,
            ISolicitudesLogica solicitudLogica)
        {
            _clientesLogica = clientesLogica;
            _solicitudLogica = solicitudLogica;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var detallesSolicitudes = new List<DetalleSolicitudDto>();

            var detalleSolicitud = new DetalleSolicitudDto
            {
                 CantidadPrendas = 2,
                 Doblado = true,
                 LavadoPlanchado = true,
                 LavadoSeco = true,
                 Planchado = true,
                 Estado = "Pendiente",
                 PrendasClasificacionId = 1
            };

            detallesSolicitudes.Add(detalleSolicitud);

            var listadoDetallesSolicitud = new ListadoDetallesSolicitudDto {
                 DetalleSolicitud = detallesSolicitudes
            };

            var guardarSolicitud = new GuardarSolicitudDto
            {
                 ClienteId = 1,
                 Estado = "Pendiente",
                 Fecha = DateTime.Now,
                 SuplementoEntrega = true,
                 DetallesSolicitud = listadoDetallesSolicitud
            };

            _solicitudLogica.GuardarSolicitud(guardarSolicitud);

            var cliente = _clientesLogica.ObtenerTodosClientes().Result;
            ViewBag.Message = "Your application description page." + cliente.Count;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}