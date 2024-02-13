using com.sun.tools.javac.jvm;
using FisioWebFront.Class;
using FisioWebFront.Entities;
using FisioWebFront.Models;
using java.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace FisioWebFront.Controllers
{
    public class CitasController : Controller
    {

    private readonly IConfiguration _config;
    CitasModel model = new CitasModel();

    public CitasController(IConfiguration config)
    {
        _config = config;
    }

        [HttpGet]
        public ActionResult CalendarioCitas()
        {
            MascotaModel mascotaModel = new MascotaModel();
            ServiciosModel serviciosModel = new ServiciosModel();
            ClienteModel modelCliente = new ClienteModel();

            //=========================================================================================
            //Dropdown Nombre de la mascota
            //=========================================================================================

            string id = HttpContext.Session.GetString("id");
            int id_usuario = int.Parse(id);

            ClienteOBJ clienteCredenciales = modelCliente.ConsultarCliente(_config, id_usuario);
            int id_cliente = clienteCredenciales.id_cliente;

            var resultadoMascota = mascotaModel.VerMascotasPorCLiente(_config, id_cliente);

            var opcionesMascotas = new List<SelectListItem>();

            foreach (var item in resultadoMascota)
                opcionesMascotas.Add(new SelectListItem { Text = item.nombre_mascota, Value = item.id_mascota.ToString() });

            ViewBag.ComboIdMascota = opcionesMascotas;

            //=========================================================================================
            //Dropdown Tipo de servicio
            //=========================================================================================
            var resultadoServicios = serviciosModel.ConsultarServicios(_config);

            var opcionesServicios = new List<SelectListItem>();

            foreach (var item in resultadoServicios)
                opcionesServicios.Add(new SelectListItem { Text = item.servicio_descripcion, Value = item.id_servicio.ToString() });

            ViewBag.ComboIdServicios = opcionesServicios;

            return View();
        }

        [HttpPost]
        public ActionResult CalendarioCitas(string descripcion_cita, int id_mascota, int id_servicio, string horaInicio_cita, string horaFin_cita)
        {

            //=========================================================================================
            //Post al model
            //=========================================================================================
            CitasModel citaModel = new CitasModel();
            CitasObj obj = new CitasObj();
            EnviarCorreo confirmacionCita = new EnviarCorreo();

            obj.descripcion_cita = descripcion_cita;
            obj.id_mascota = id_mascota;
            obj.id_empleado = 1;
            obj.id_servicio = id_servicio;
            obj.horaInicio_cita = DateTime.Parse(horaInicio_cita);
            obj.horaFin_cita = DateTime.Parse(horaFin_cita);

            citaModel.RegistrarCita(_config, obj);

            var correo = HttpContext.Session.GetString("usuario");
            confirmacionCita.CitaConfirmada(correo, horaInicio_cita);

            return RedirectToAction("CalendarioCitas", "Citas");
        }

        [HttpGet]
        public JsonResult ConsultarCitas()
        {
            try
            {
                JsonResult result;

                var datos = model.ConsultarCitas(_config);
                result  = Json(datos, new System.Text.Json.JsonSerializerOptions());
                
                return result;
                //return View(datos);

            }
            catch (Exception ex)
            {
              //  return BadRequest(ex.Message);
              return new JsonResult(ex);
            }
        }

        [HttpGet]
        public ActionResult ConsultarCita(int id_mascota)
        {
            try
            {
                var datos = model.ConsultarCita(_config, 17);
                return View(datos);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
