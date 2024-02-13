using FisioWebFront.Class;
using FisioWebFront.Entities;
using FisioWebFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace FisioWebFront.Controllers
{
    public class InformacionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IConfiguration _config;
        InformacionModel model = new InformacionModel();

        public InformacionController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public ActionResult VerSobreNosotros()
        {
            return View();
        }


		[HttpGet]
		public ActionResult VerContacto()
		{
			return View();
		}


		[HttpPost]
		public ActionResult VerContacto( CorreoOBJ correoUsuario)
		{
			EnviarCorreo enviarCorreo = new EnviarCorreo();
            enviarCorreo.ConsultaRealizada(correoUsuario);
			return View();
		}


 

	}

}
