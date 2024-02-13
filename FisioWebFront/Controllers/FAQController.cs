using FisioWebFront.Entities;
using FisioWebFront.Models;
using Microsoft.AspNetCore.Mvc;
using static FisioWebFront.Enum.Enum;

namespace FisioWebFront.Controllers
{
    public class FaqController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IConfiguration _config;
        FaqModel modelFaq = new FaqModel();
        public FaqController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public ActionResult VerFaq()
        {
            try
            {
                var datos = modelFaq.ConsultarFAQ(_config);
                return View(datos);
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult VerListaFaq()
        {
            try
            {
                var datos = modelFaq.ConsultarFAQ(_config);
                return View(datos);
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult RegistrarFaq()
        {
            return View();
        }


        [HttpGet]
        public ActionResult EditarFaq(int q)
        {
            try
            {

                var datos = modelFaq.ConsultarFAQ(_config, q);
                return View(datos);
            }
            catch (Exception ex)
            {
                return RedirectToAction("VerListaFaq", "FAQ");
            }
        }


        [HttpGet]
        public ActionResult EliminarFaq(int q)
        {
            try
            {

                var datos = modelFaq.ConsultarFAQ(_config, q);
                return View(datos);
            }
            catch (Exception ex)
            {
                return RedirectToAction("VerListaFaq", "FAQ");
            }
        }

        [HttpPost]
        public ActionResult RegistrarFaq(string pregunta_titulo, string pregunta_respuesta)
        {
            //Registro servicio
            try
            {
                FaqObj FaqInfo = new FaqObj();

                FaqInfo.pregunta_titulo = pregunta_titulo;
                FaqInfo.pregunta_respuesta = pregunta_respuesta;

                modelFaq.RegistrarFAQ(_config, FaqInfo);

                Alert("Registro de FAQ correcto!", NotificationType.success);
                return RedirectToAction("VerListaFaq", "FAQ");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditarFaq(FaqObj faq)
        {
            try
            {
                var datos = modelFaq.EditarFAQ(_config, faq);

                Alert("Actualización de datos correcta!", NotificationType.success);
                return RedirectToAction("VerListaFaq", "FAQ");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult BorrarFaq(FaqObj faq)
        {
            try
            {
                var id = faq.id_ayuda;
                var datos = modelFaq.BorrarFAQ(_config, id);

                Alert("Eliminación de FAQ correcto!", NotificationType.success);
                return RedirectToAction("VerListaFaq", "FAQ");
            }
            catch (Exception ex)
            {
                return View();
            }
        }


    }
}
