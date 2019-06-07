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
            var list = new SelectList(new[]
                                          {
                                           
                                              new{ID="En proceso",Name="En proceso"},
                                              new{ID="Finalizado",Name="Finalizado"},
                                          },
                            "ID", "Name", 1);
            ViewData["list"] = list;
            ViewData["Estados"] = list;
            var solicitudes = _solicitudLogica.ObtenerSolicitudConDetallePorId(id.Value).Result;

            return View(solicitudes);
        }

        [HttpPost, ActionName("ActualizarEstadoSolicitud")]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarEstado(int id, [Bind(Include = "Estado")] SolicitudDto solicitudEstado)
        {
            if (ModelState.IsValid)
            {
                var estado = solicitudEstado.Estado;
                 solicitudEstado.Id = id;
                _solicitudLogica.CambiarEstadSolicitud(id, estado);
                return RedirectToAction("ActualizarEstadoSolicitud", new {id});
            }

            return View();
        }
        // Se actuliza los estado del detalle de la solicitud
        public ActionResult ActualizarEstadoDetalleSolicitud(int? id)
        {
            ViewData["id"] = id;
            var list = new SelectList(new[]
                                          {
                                        new{ID="En proceso",Name="En proceso"},
                                        new{ID="Finalizado",Name="Finalizado"},
                                          },
                                            "ID", "Name", 1);
            ViewData["Estados"] = list;
            var prenda = _solicitudLogica.ObtenerDetalleSolicitud(id.Value).Result;

            return View(prenda);
        }

        [HttpPost, ActionName("ActualizarEstadoDetalleSolicitud")]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarEstadoDetalle(int id, [Bind(Include = "Estado")] SolicitudDto solicitudEstado)
        {
            if (ModelState.IsValid)
            {
                solicitudEstado.Id = id;
                _solicitudLogica.CambiarEstadoSolicitud(solicitudEstado);
                return RedirectToAction("Index");
            }

            return View();
        }

        

        public ActionResult ListarDetalleSolicitudes(int? id)
        {
            ViewData["id"] = id;
            var solicitudes = _solicitudLogica.ObtenerSolicitudConDetallePorId(id.Value).Result;

            return View(solicitudes);
        }
    }
}