using FisioWebFront.Class;
using FisioWebFront.Entities;
using FisioWebFront.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using static FisioWebFront.Enum.Enum;

namespace FisioWebFront.Controllers
{
    public class ClienteController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IConfiguration _config;
        ClienteModel model = new ClienteModel();

        public ClienteController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public ActionResult RegistrarCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCliente(string correo_usuario, string contrasena_usuario, string nombre_cliente, string apellido1_cliente, string apellido2_cliente, int telefono_cliente, string cedula_cliente)
        {
            if(IsReCaptchValid())
            {
                UsuarioObj usuarioInfo = new UsuarioObj();
                ClienteOBJ clienteInfo = new ClienteOBJ();
                ClaveEncrypt Encrypt = new ClaveEncrypt();
                usuarioInfo.correo_usuario = correo_usuario;
                usuarioInfo.contrasena_usuario = Encrypt.GetSHA256(contrasena_usuario);
                usuarioInfo.id_rol = 3;
                usuarioInfo.id_estado = 1;

                clienteInfo.nombre_cliente = nombre_cliente;
                clienteInfo.apellido1_cliente = apellido1_cliente;
                clienteInfo.apellido2_cliente = apellido2_cliente;
                clienteInfo.telefono_cliente = telefono_cliente;
                clienteInfo.cedula_cliente = cedula_cliente;

                model.RegistrarUsuario(_config, usuarioInfo);
                model.RegistrarCliente(_config, clienteInfo);

                Alert("Se ha registrado correctamente!", NotificationType.success);

                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                Alert("El Captcha es obligatorio", NotificationType.error);
                return View();
            }
            
        }

		[HttpGet]
		public ActionResult ActualizarCliente()
		{

            string id = HttpContext.Session.GetString("id");
            int id_usuario = int.Parse(id);
			var datos = model.ConsultarCliente(_config, id_usuario);
			return View(datos);
		}

		[HttpPost]
		public ActionResult ActualizarCliente(ClienteUsuarioOBJ cliente)
		{
			try
			{
				var datos = model.ActualizarCliente(_config, cliente);
				return RedirectToAction("Index", "Home");
			}
			catch
			{
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
