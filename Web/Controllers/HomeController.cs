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

        public HomeController(IClientesLogica clientesLogica)
        {
            _clientesLogica = clientesLogica;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var cliente = _clientesLogica.ObtenerClientePorId(1).Result;
            ViewBag.Message = "Your application description page." + cliente.Nombres;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}