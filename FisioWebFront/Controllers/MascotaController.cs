using FisioWebFront.Entities;
using FisioWebFront.Models;
using java.lang;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static FisioWebFront.Enum.Enum;
using Exception = System.Exception;

namespace FisioWebFront.Controllers
{
    public class MascotaController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IConfiguration _config;
        MascotaModel modelMascota = new MascotaModel();
        RazaModel modelRaza = new RazaModel();
        EspeciesModel modelEspecies = new EspeciesModel();
        UsuarioModel modelUsuario = new UsuarioModel();
        ClienteModel modelCliente = new ClienteModel();
        private int idEspecie = 1;
        public MascotaController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public ActionResult ObtenerRaza(int id_especie)
        {
            //===================================================================================================================
            //Dropdown Razas
            var resultadoRaza = modelRaza.ConsultarRazas(_config, id_especie);

            var opcionesRaza = new List<SelectListItem>();
            var razas = new List<(int,string)>();

            foreach (var item in resultadoRaza)
            {
                opcionesRaza.Add(new SelectListItem { Text = item.descripcion_raza, Value = item.id_raza.ToString() });
                razas.Add(new (item.id_raza,item.descripcion_raza));
            }
                
            ViewBag.ComboIdRaza = opcionesRaza;

            //===================================================================================================================
            
            return PartialView("_MascotasPartial");
        }

        [HttpGet]
        public ActionResult RegistrarMascota()
        {
            //===================================================================================================================
            //Dropdown Especies **FALTA HACER FILTRO DE RAZA POR ESPECIE
            var resultadoEspecies = modelEspecies.ConsultarEspecies(_config);

            var opcionesEspecie = new List<SelectListItem>();

            foreach (var item in resultadoEspecies)
                opcionesEspecie.Add(new SelectListItem { Text = item.descripcion_especie, Value = item.id_especie.ToString() });

            ViewBag.ComboIdEspecie = opcionesEspecie;
            
            //===================================================================================================================
           
            //===================================================================================================================
            //Dropdown Razas
            var resultadoRaza = modelRaza.ConsultarRazas(_config, idEspecie);

            var opcionesRaza = new List<SelectListItem>();

            foreach (var item in resultadoRaza)
                opcionesRaza.Add(new SelectListItem { Text = item.descripcion_raza, Value = item.id_raza.ToString() });

            ViewBag.ComboIdRaza = opcionesRaza;
            //===================================================================================================================

            return View();
        }

        [HttpPost]
        public ActionResult RegistrarMascota(string nombre_mascota, int edad_mascota, string detalles_mascota, int id_raza, int id_especie)
        {
            //Registro
            idEspecie = id_especie;
            string id = HttpContext.Session.GetString("id");
            int id_usuario = int.Parse(id);

            ClienteOBJ clienteCredenciales = modelCliente.ConsultarCliente(_config, id_usuario);
            int id_cliente = clienteCredenciales.id_cliente;

            //string correo = HttpContext.Session.GetString("usuario");
            //UsuarioObj usuarioCredenciales = modelUsuario.ConsultarUsuario(_config, correo);
            //int id = usuarioCredenciales.id_usuario;

            MascotaOBJ MascotaInfo = new MascotaOBJ();

            MascotaInfo.nombre_mascota = nombre_mascota;
            MascotaInfo.edad_mascota = edad_mascota;
            MascotaInfo.detalles_mascota = detalles_mascota;
            MascotaInfo.id_raza = id_raza;
            MascotaInfo.id_cliente = id_cliente;
       
            modelMascota.RegistrarMascota(_config, MascotaInfo);

            Alert("Registro de mascota correcto!", NotificationType.success);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult VerMascotas()
        {

			string id = HttpContext.Session.GetString("id");
			int id_usuario = int.Parse(id);

			ClienteOBJ clienteCredenciales = modelCliente.ConsultarCliente(_config, id_usuario);
			int id_cliente = clienteCredenciales.id_cliente;

			var datos = modelMascota.VerMascotasPorCLiente(_config, id_cliente);
            return View(datos);
      
        }

        [HttpGet]
        public ActionResult VerMascotasAdmin(int q)
        {

            var datos = modelMascota.VerMascotasPorCLiente(_config, q);
            return View(datos);

        }


        [HttpGet]
        public ActionResult EditarMascotas(int q)
        {
            try
            {

                //Dropdown Razas
                var resultado = modelRaza.ConsultarRazas(_config,1);

                var opciones = new List<SelectListItem>();

                foreach (var item in resultado)
                    opciones.Add(new SelectListItem { Text = item.descripcion_raza, Value = item.id_raza.ToString() });

                ViewBag.ComboIdRaza = opciones;

                var datos = modelMascota.VerMascota(_config, q);
                return View(datos);

            }
            catch (Exception ex)
            {

                return RedirectToAction("VerMascotas", "Mascota");
            }
        }

        [HttpPost]
        public ActionResult EditarMascotas(MascotaOBJ mascota)
        {
            try
            {
          
                var datos = modelMascota.EditarMascota(_config, mascota);

                Alert("Actualzación de datos correcta!", NotificationType.success);

                if (HttpContext.Session.GetString("rol") == "1" || HttpContext.Session.GetString("rol") == "2")
                {
                    return RedirectToAction("VerListaClientes", "Admin");
                }

                else { return RedirectToAction("VerMascotas", "Mascota"); }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult VerMascota(int q)
        {
            try
            {
                var datos = modelMascota.VerMascota(_config, q);
                return View(datos);

            }
            catch (Exception ex)
            {
                return RedirectToAction("VerMascotas", "Mascota");
            }
        }

    }
}
