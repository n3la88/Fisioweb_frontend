using FisioWebFront.Entities;
using FisioWebFront.Controllers;
using System.Net.Http.Headers;

namespace FisioWebFront.Models
{
	public class UsuarioModel
	{

		public UsuarioObj ConsultarUsuario(IConfiguration _config, String correo)
		{
			string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

			using (var client = new HttpClient())
			{
				 
				string rutaApi = rutaBase + "api/Usuario/ConsultarUsuario?correo=" + correo ;
				HttpResponseMessage respuesta = client.GetAsync(rutaApi).GetAwaiter().GetResult();

				if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
				{
					return respuesta.Content.ReadFromJsonAsync<UsuarioObj>().Result;
				}
				return new UsuarioObj();
			}
		}

		public UsuarioObj Login(IConfiguration _config, UsuarioObj usuario)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(usuario);
                string rutaApi= rutaBase + "api/Usuario/Autenticar";
                HttpResponseMessage respuesta = client.PostAsync(rutaApi, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<UsuarioObj>().Result;
                }
                return new UsuarioObj();
            }
        }


		public UsuarioObj CambiarClave(IConfiguration _config, UsuarioObj usuario)
		{
			string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

			using (var client = new HttpClient())
			{
				JsonContent body = JsonContent.Create(usuario);
				string rutaApi = rutaBase + "api/Usuario/CambiarClave";
				HttpResponseMessage respuesta = client.PostAsync(rutaApi, body).GetAwaiter().GetResult();

				if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
				{
					return respuesta.Content.ReadFromJsonAsync<UsuarioObj>().Result;
				}
				return new UsuarioObj();
			}
		}

        public UsuarioObj RecuperarClave(IConfiguration _config, UsuarioObj obj)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;
			try
			{
                using (var client = new HttpClient())
                {
                    JsonContent body = JsonContent.Create(obj);
                    string rutaApi = rutaBase + "api/Usuario/RecuperarContrasena";
                    HttpResponseMessage respuesta = client.PostAsync(rutaApi, body).GetAwaiter().GetResult();

                    if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                    {
                        return respuesta.Content.ReadFromJsonAsync<UsuarioObj>().Result;
                    }
                    return new UsuarioObj();
                }
            }
			catch (Exception ex)
			{
				return new UsuarioObj();
			}
            
        }


    }
}
