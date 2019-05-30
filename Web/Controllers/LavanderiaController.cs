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

        public LavanderiaController(ISolicitudesLogica solicitudLogica)
        {
           
            _solicitudLogica = solicitudLogica;
        }
        // GET: Lavanderia
        public ActionResult Index()
        {
            var cliente = _solicitudLogica.ObtenerTodasSolicitudes().Result;
            
            return View(cliente);
        }
    }
}