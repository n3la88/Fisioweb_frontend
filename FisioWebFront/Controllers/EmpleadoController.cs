using FisioWebFront.Entities;
using FisioWebFront.Models;
using Microsoft.AspNetCore.Mvc;
using FisioWebFront.Class;
using static FisioWebFront.Enum.Enum;
using Newtonsoft.Json.Linq;
using System.Net;

namespace FisioWebFront.Controllers
{
	public class EmpleadoController : BaseController
    {
		public IActionResult Index()
		{
			return View();
		}

        private readonly IConfiguration _config;
        EmpleadoModel modelEmpleado = new EmpleadoModel();
        ClienteModel modelCliente = new ClienteModel();
        UsuarioModel model = new UsuarioModel();

        public EmpleadoController(IConfiguration config)
        {
            _config = config;
        }


        [HttpGet]
        public ActionResult RegistrarEmpleado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarEmpleado(string correo_usuario, string contrasena_usuario, string nombre_empleado, string apellido1_empleado, string apellido2_empleado, string puesto_empleado, int telefono_empleado, string cedula_empleado)
        {
           
                UsuarioObj usuarioInfo = new UsuarioObj();
                EmpleadoOBJ empleadoInfo = new EmpleadoOBJ();
                ClaveEncrypt Encrypt = new ClaveEncrypt();
            EnviarCorreo enviarCorreo = new EnviarCorreo();
            ClaveAleatoria clave = new ClaveAleatoria();
            string claveTemporal = "";

            //Se envia una contraseña temporal para el empleado
            clave.ContrasenaRandom();
            claveTemporal = clave.resultString;
            enviarCorreo.NuevoEmpleado(correo_usuario, claveTemporal);

                usuarioInfo.correo_usuario = correo_usuario;
                usuarioInfo.contrasena_usuario = Encrypt.GetSHA256(claveTemporal);
                usuarioInfo.id_rol = 2;
                usuarioInfo.id_estado = 1;

            empleadoInfo.nombre_empleado = nombre_empleado;
            empleadoInfo.apellido1_empleado = apellido1_empleado;
            empleadoInfo.apellido2_empleado = apellido2_empleado;
            empleadoInfo.puesto_empleado = puesto_empleado;
            empleadoInfo.telefono_empleado = telefono_empleado;
            empleadoInfo.cedula_empleado = cedula_empleado;

            modelCliente.RegistrarUsuario(_config, usuarioInfo);
            modelEmpleado.RegistrarEmpleado(_config, empleadoInfo);

         
                Alert("Se ha registrado correctamente!", NotificationType.success);

            return RedirectToAction("Index", "Home");


        }


      


    }



}
