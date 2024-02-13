using FisioWebFront.Entities;
using FisioWebFront.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static FisioWebFront.Enum.Enum;

namespace FisioWebFront.Controllers
{
	public class ServiciosController : BaseController
    {
		public IActionResult Index()
		{
			return View();
		}

        private readonly IConfiguration _config;
        ServiciosModel modelServicios = new ServiciosModel();
        public ServiciosController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public ActionResult VerServicios()
        {
            try
            {
                var datos = modelServicios.ConsultarServicios(_config);
                return View(datos);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult VerListaServicios()
        {
            try
            {
            var datos = modelServicios.ConsultarServicios(_config);
            return View(datos);
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult RegistrarServicio()
        {
            return View();
        }


        [HttpGet]
        public ActionResult EditarServicio(int q)
        {
            try
            {

                var datos = modelServicios.ConsultarServicio(_config, q);
                return View(datos);
            }
            catch (Exception ex)
            {
                return RedirectToAction("VerListaServicios", "Servicios");
            }
        }


        [HttpGet]
        public ActionResult EliminarServicio(int q)
        {
            try
            {

                var datos = modelServicios.ConsultarServicio(_config, q);
                return View(datos);
            }
            catch (Exception ex)
            {
                return RedirectToAction("VerListaServicios", "Servicios");
            }
        }

        [HttpPost]
        public ActionResult RegistrarServicio(string servicio_descripcion, string servicio_detalle)
        {
            //Registro servicio
            try
            {
                ServiciosObj ServiciosInfo = new ServiciosObj();

            ServiciosInfo.servicio_descripcion = servicio_descripcion;
            ServiciosInfo.servicio_detalle = servicio_detalle;

            modelServicios.RegistrarServicio(_config, ServiciosInfo);

            Alert("Registro de servicio correcto!", NotificationType.success);
            return RedirectToAction("VerServicios", "Servicios");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditarServicio(ServiciosObj servicio)
        {
            try
            {
                var datos = modelServicios.EditarServicio(_config, servicio);

                Alert("Actualización de datos correcta!", NotificationType.success);
                return RedirectToAction("VerListaServicios", "Servicios");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult BorrarServicio(ServiciosObj servicio)
        {
            try
            {
                var id = servicio.id_servicio;
                var datos = modelServicios.BorrarServicio(_config, id);

                Alert("Eliminación de servicio correcto!", NotificationType.success);
                return RedirectToAction("VerListaServicios", "Servicios");
            }
            catch (Exception ex)
            {
                return View();
            }
        }



    }
}
