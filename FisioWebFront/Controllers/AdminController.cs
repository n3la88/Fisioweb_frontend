using Microsoft.AspNetCore.Mvc;
using FisioWebFront.Class;
using FisioWebFront.Models;
using FisioWebFront.Entities;

namespace FisioWebFront.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IConfiguration _config;
        AdminModel model = new AdminModel();
        ClienteModel modelCliente = new ClienteModel();

        public AdminController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public ActionResult VerListaClientes()
        {

            var datos = modelCliente.VerListaClientes(_config);
            return View(datos);
        }


        [HttpGet]
        public ActionResult VerCliente(int q)
        {

            var datos = model.VerCliente(_config, q);
            return View(datos);
        }


        [HttpGet]
        public ActionResult EditarCliente(int q)
        {

            var datos = model.VerCliente(_config, q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult EditarCliente(ClienteUsuarioOBJ cliente)
        {
            try
            {
                var datos = modelCliente.ActualizarCliente(_config, cliente);
                return RedirectToAction("VerListaClientes", "Admin");
            }
            catch (Exception ex)
            {
                return View();
            }
        }


    }
}
