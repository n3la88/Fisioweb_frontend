using FisioWebFront.Entities;
using FisioWebFront.Models;
using Microsoft.AspNetCore.Mvc;
using FisioWebFront.Class;
using static FisioWebFront.Enum.Enum;
using Newtonsoft.Json.Linq;
using System.Net;
using java.lang;

namespace FisioWebFront.Controllers
{
	public class UsuarioController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}

		private readonly IConfiguration _config;
		UsuarioModel modelUsuario = new UsuarioModel();
        BitacoraModel modelBitacora = new BitacoraModel();  

		const string usuario = "_usuario";

		public UsuarioController(IConfiguration config)
		{
			_config = config;
		}


		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(string correo_usuario, string contrasena_usuario)
		{
			UsuarioObj usuarioCredenciales = new UsuarioObj();
            ClienteModel modelCliente = new ClienteModel();
            EmpleadoModel modelEmpleado = new EmpleadoModel();
            ClaveEncrypt Encrypt = new ClaveEncrypt();
            usuarioCredenciales.correo_usuario = correo_usuario;
			usuarioCredenciales.contrasena_usuario = Encrypt.GetSHA256(contrasena_usuario);
			var user = modelUsuario.Login(_config, usuarioCredenciales);


            //if (user.id_estado == 2)
            //{
            //    ViewBag.error = "Cuenta Desactivada. Contacte al Administrador";
            //    HttpContext.Session.Clear();
            //    Alert("Cuenta Desactivada. Contacte al Administrador", NotificationType.error);
            //    return View(user);
            //}


            //else if (user.correo_usuario.Equals(""))
            //{
            //    ViewBag.error = "Usuario o contraseña incorrectos.";
            //    HttpContext.Session.Clear();

            //    //====================================================================
            //    //===================INSERTAR DATOS EN BITACORA=======================
            //    BitacoraOBJ bitacora = new BitacoraOBJ();
            //    bitacora.codigo_error_p = "401-Acceso denegado";
            //    bitacora.error_descripcion_p = "Credenciale de usuario no validas";
            //    string time = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
            //    bitacora.fecha_error_p = DateTime.Parse(time);
            //    bitacora.origen_error_p = "LOGIN";
            //    bitacora.id_usuario_p = 6;
            //    BitacoraOBJ insertarBitacora = modelBitacora.DatosBitacora(_config, bitacora);

            //    //====================================================================

            //    Alert("Combinacion de Usuario y contraseña incorrectos", NotificationType.error);
            //    return View(user);
            //}

            ////===================================================================================
            ////=============================INICIO DE SESION CORRECTO=============================
            //else
            //{
            //    ViewBag.error = "";
            //    Alert("Inicio de sesión correcto!", NotificationType.success);
            //    HttpContext.Session.SetString("usuario", user.correo_usuario);
            //    HttpContext.Session.SetString("id", user.id_usuario.ToString());
            //    HttpContext.Session.SetString("rol", user.id_rol.ToString());

            //    //El siguiente código recupera el nombre y apellido del usuario dependiendo si es cliente o empleado
            //    if (user.id_rol == 2)
            //    {
            //        var empleadoInfo = modelEmpleado.ConsultarEmpleadoPorCorreo(_config, correo_usuario);
            //        HttpContext.Session.SetString("nombreEmpleado", empleadoInfo.nombre_empleado);
            //        HttpContext.Session.SetString("apellidoEmpleado", empleadoInfo.apellido1_empleado);
            //    }
            //    else
            //    {
            //        var clienteInfo = modelCliente.ConsultarClientePorCorreo(_config, correo_usuario);
            //        HttpContext.Session.SetString("nombreCliente", clienteInfo.nombre_cliente);
            //        HttpContext.Session.SetString("apellidoCliente", clienteInfo.apellido1_cliente);

            //    }

            //    return RedirectToAction("CalendarioCitas", "Citas");
            //}

            if (IsReCaptchValid())
            {
                if (user.id_estado == 2)
                {
                    ViewBag.error = "Cuenta Desactivada. Contacte al Administrador";
                    HttpContext.Session.Clear();
                    Alert("Cuenta Desactivada. Contacte al Administrador", NotificationType.error);
                    return View(user);
                }


                else if (user.correo_usuario.Equals(""))
                {
                    ViewBag.error = "Usuario o contraseña incorrectos.";
                    HttpContext.Session.Clear();

                    //====================================================================
                    //===================INSERTAR DATOS EN BITACORA=======================
                    BitacoraOBJ bitacora = new BitacoraOBJ();
                    bitacora.codigo_error_p = "401-Acceso denegado";
                    bitacora.error_descripcion_p = "Credenciale de usuario no validas";
                    string time = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
                    bitacora.fecha_error_p = DateTime.Parse(time);
                    bitacora.origen_error_p = "LOGIN";
                    bitacora.id_usuario_p = 6;
                    BitacoraOBJ insertarBitacora = modelBitacora.DatosBitacora(_config, bitacora);

                    //====================================================================

                    Alert("Combinacion de Usuario y contraseña incorrectos", NotificationType.error);
                    return View(user);
                }

                //===================================================================================
                //=============================INICIO DE SESION CORRECTO=============================
                else
                {
                    ViewBag.error = "";
                    Alert("Inicio de sesión correcto!", NotificationType.success);
                    HttpContext.Session.SetString("usuario", user.correo_usuario);
                    HttpContext.Session.SetString("id", user.id_usuario.ToString());
                    HttpContext.Session.SetString("rol", user.id_rol.ToString());

                    //El siguiente código recupera el nombre y apellido del usuario dependiendo si es cliente o empleado
                    if (user.id_rol == 2)
                    {
                        var empleadoInfo = modelEmpleado.ConsultarEmpleadoPorCorreo(_config, correo_usuario);
                        HttpContext.Session.SetString("nombreEmpleado", empleadoInfo.nombre_empleado);
                        HttpContext.Session.SetString("apellidoEmpleado", empleadoInfo.apellido1_empleado);
                    }
                    else
                    {
                        var clienteInfo = modelCliente.ConsultarClientePorCorreo(_config, correo_usuario);
                        HttpContext.Session.SetString("nombreCliente", clienteInfo.nombre_cliente);
                        HttpContext.Session.SetString("apellidoCliente", clienteInfo.apellido1_cliente);

                    }

                    return RedirectToAction("CalendarioCitas", "Citas");
                }
            }
            else
            {
                Alert("El Captcha es obligatorio", NotificationType.error);
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }


        [HttpGet]
		public ActionResult CambiarClave()
		{
			return View();
		}


		[HttpPost]
		public ActionResult CambiarClave(string contrasena_anterior, string contrasena_nueva, string contrasena_confirmacion)
		{
			string correo = HttpContext.Session.GetString("usuario");
			UsuarioObj usuarioCredenciales = modelUsuario.ConsultarUsuario(_config, correo);
            ClaveEncrypt Encrypt = new ClaveEncrypt();

            if ((Encrypt.GetSHA256(contrasena_nueva) == Encrypt.GetSHA256(contrasena_confirmacion)) & (Encrypt.GetSHA256(contrasena_anterior) == usuarioCredenciales.contrasena_usuario))

			{
				usuarioCredenciales.contrasena_usuario = Encrypt.GetSHA256(contrasena_nueva);
                modelUsuario.CambiarClave(_config, usuarioCredenciales);

				ViewBag.error = "";
                Alert("Cambio de contraseña correcto!", NotificationType.success);
                return RedirectToAction("Index", "Home");

			}

            else
			{
				ViewBag.error = "Usuario o contraseña incorrectos.";
                Alert("Las contraseñas no coinciden!", NotificationType.error);
                return View();
			}
	
		}

        [HttpGet]
        public ActionResult RecuperarClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarClave(string correo_usuario)
        {
			UsuarioObj obj = new UsuarioObj();
			EnviarCorreo enviarCorreo = new EnviarCorreo();
			ClaveAleatoria clave = new ClaveAleatoria();
            ClaveEncrypt Encrypt = new ClaveEncrypt();
            string claveTemporal = "";

            UsuarioObj credenciales = modelUsuario.ConsultarUsuario(_config, correo_usuario);

            if(IsReCaptchValid())
            {
                if (credenciales.correo_usuario == correo_usuario)
                {
                    UsuarioModel model = new UsuarioModel();

                    clave.ContrasenaRandom();
                    claveTemporal = clave.resultString;
                    enviarCorreo.RecoveryPassword(correo_usuario, claveTemporal);

                    obj.correo_usuario = correo_usuario;
                    obj.contrasena_usuario = Encrypt.GetSHA256(claveTemporal);

                    Alert("Proceso realizado con exito!", NotificationType.error);

                    model.RecuperarClave(_config, obj);
                    //Console.WriteLine(claveTemporal);
                    return RedirectToAction("Login", "Usuario");
                }
                else
                {
                    Alert("El correo digitado, no se encuentra registrado", NotificationType.error);
                    return View();
                }
            }
            else
            {
                Alert("El Captcha es obligatorio", NotificationType.error);
                return View();
            }


        }

        //=========================================================================================
        //=============Metodo para invocar el API de Google y ejecutar el reCaptcha v2=============
        //=========================================================================================
        public bool IsReCaptchValid()
        {
            var result = false;
            var captchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = _config.GetSection("SecretKey:secret").Value;
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }

    }
}
