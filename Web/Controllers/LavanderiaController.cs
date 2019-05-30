using Dtos.Solicitud;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lavanderia.Controllers
{
    public class LavanderiaController : Controller
    {
        
        private readonly ISolicitudesLogica _solicitudLogica;
        private readonly IClientesLogica _clientesLogica;

        public LavanderiaController(IClientesLogica clientesLogica,
            ISolicitudesLogica solicitudLogica)
        {
            _clientesLogica = clientesLogica;
            _solicitudLogica = solicitudLogica;
        }
        // GET: Lavanderia
        public ActionResult Index()
        {
            var solicitudes = _solicitudLogica.ObtenerTodasSolicitudes().Result;
            
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
            ViewBag.SolicitudId = id;

            //Aquí consultas los clientes
            
            
            var clasificacion = _clientesLogica.ObtenerTodosClientes().Result;
            var listaclasificacion = new SelectList(clasificacion, "ID", "Nombres", 0);
            ViewData["clasificacion"] = listaclasificacion;

            
            var prendas = _clientesLogica.ObtenerTodosClientes().Result;
            var listaprendas = new SelectList(prendas, "ID", "Nombres", 0);
            ViewData["prendas"] = listaprendas;

            return View();

        }

        public ActionResult ListarDetalleSolicitudes(int? id)
        {
            var solicitudes = _solicitudLogica.ObtenerSolicitudConDetallePorId(id.Value).Result;

            return View(solicitudes);
        }
    }
}