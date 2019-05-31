using Dtos.Solicitud;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lavanderia.Controllers
{
    public class GestionController : Controller
    {
        
        private readonly ISolicitudesLogica _solicitudLogica;
        private readonly IClientesLogica _clientesLogica;
        private readonly IClasificacionPrendasLogica _clasificacionPrendasLogica;

        public GestionController(IClientesLogica clientesLogica,
            ISolicitudesLogica solicitudLogica, IClasificacionPrendasLogica clasificacionPrendasLogica)
        {
            _clientesLogica = clientesLogica;
            _solicitudLogica = solicitudLogica;
            _clasificacionPrendasLogica = clasificacionPrendasLogica;
        }
        // GET: Lavanderia
        public ActionResult Index()
        {
            var solicitudes = _solicitudLogica.ObtenerTodasSolicitudes().Result;
            
            return View(solicitudes);
        }


        public ActionResult ActualizarEstadoSolicitud(int? id)
        {
            ViewData["id"] = id;
            var solicitudes = _solicitudLogica.ObtenerSolicitudConDetallePorId(id.Value).Result;

            return View(solicitudes);
        }



        [HttpGet]
        public ActionResult Crear()
        {
            //Aquí consultas los clientes
            var cliente = _clientesLogica.ObtenerTodosClientes().Result;
            var listaClientes = new SelectList(cliente, "ID", "Nombres", 0);
            ViewData["Clientes"] = listaClientes;

            return View();

        }

        [HttpPost]
        public JsonResult ConsultarCliente(int? id)
        {
            var cliente = _clientesLogica.ObtenerClientePorId(id.Value).Result;

            return Json(cliente);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "ClientesId, Nombres, Identificacion, Habitacion, SuplementoEntrega")] GuardarSolicitudDto guardarSolicitudDto)
        {
            if (ModelState.IsValid)
            {
                _solicitudLogica.GuardarSolicitud(guardarSolicitudDto);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult DetalleSolicitud(int? id)
        {

            ViewData["id"] = id;

            //Aquí consultas los clientes
            var clasificacion = _clasificacionPrendasLogica.ObtenerTodasClasificacion().Result;
            var listaclasificacion = new SelectList(clasificacion, "ID", "Nombre", 0);
            ViewData["clasificacion"] = listaclasificacion;

            
            var prendas = _clasificacionPrendasLogica.ObtenerTodasPrendas().Result;
            var listaprendas = new SelectList(prendas, "ID", "Nombre", 0);
            ViewData["prendas"] = listaprendas;

            return View();

        }


        [HttpPost, ActionName("DetalleSolicitud")]
        [ValidateAntiForgeryToken]
        public ActionResult CrearDetalleSolicitud(int id, [Bind(Include = "SolicitudesId, PrendasId, ClasificacionId, PrendasClasificacionId, LavadoSeco, LavadoPlanchado, Planchado, Doblado, CantidadPrendas")] GuardarDetalleSolicitudDto guardarDetalleSolicitudDto)
        {
            if (ModelState.IsValid)
            {
                guardarDetalleSolicitudDto.SolicitudesId = id;
                _solicitudLogica.GuardarDetalleSolicitud(guardarDetalleSolicitudDto);
                return RedirectToAction("DetalleSolicitud");
            }

            return View();
        }

        [HttpPost]
        public JsonResult ConsultarPrendas(int? id)
        {
            var prendas = _clasificacionPrendasLogica.ObtenerPrendasPorClasificacionId(id.Value).Result;
            return new JsonResult { Data = prendas, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ListarDetalleSolicitudes(int? id)
        {
            ViewData["id"] = id;
            var solicitudes = _solicitudLogica.ObtenerSolicitudConDetallePorId(id.Value).Result;

            return View(solicitudes);
        }
    }
}