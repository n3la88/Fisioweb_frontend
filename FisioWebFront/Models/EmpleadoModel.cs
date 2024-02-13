using FisioWebFront.Entities;

namespace FisioWebFront.Models
{
	public class EmpleadoModel
	{

        public EmpleadoOBJ RegistrarEmpleado(IConfiguration _config, EmpleadoOBJ empleado)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(empleado);
                string rutaApi = rutaBase + "api/Empleado/RegistrarEmpleado";
                HttpResponseMessage respuesta = client.PostAsync(rutaApi, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<EmpleadoOBJ>().Result;
                }
                return new EmpleadoOBJ();
            }
        }

        public EmpleadoOBJ ConsultarEmpleadoPorCorreo(IConfiguration _config, string correo_usuario)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {

                string rutaApi = rutaBase + "api/Empleado/ConsultarEmpleadoPorCorreo?correo_usuario=" + correo_usuario;
                HttpResponseMessage respuesta = client.GetAsync(rutaApi).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<EmpleadoOBJ>().Result;
                }
                return new EmpleadoOBJ();
            }
        }


    }
}
