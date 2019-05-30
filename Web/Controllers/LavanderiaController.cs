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
        /*
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Crear([Bind(Include = "ClientesId, Nombres, Identificacion, Habitacion, SuplementoEntrega")] CrearSolicitudes crear)
                {
                    if (ModelState.IsValid)
                    {

                        var solicitud = new Solicitudes();
                        solicitud.Fecha = DateTime.Now;
                        solicitud.Estado = "Solicitado";
                        solicitud.ClientesId = crear.ClientesId;

                        db.SaveChanges();

                        return RedirectToAction("Index");
        
    }

            return View();
        }*/
    }
}