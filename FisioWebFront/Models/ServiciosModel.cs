using FisioWebFront.Entities;

namespace FisioWebFront.Models
{
    public class ServiciosModel
    {
        public List<ServiciosObj> ConsultarServicios(IConfiguration _config)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                string rutaApi = rutaBase + "api/Servicios/ConsultarServicios";
                HttpResponseMessage respuesta = client.GetAsync(rutaApi).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<List<ServiciosObj>>().Result;
                }
                return new List<ServiciosObj>();
            }
        }

        public ServiciosObj RegistrarServicio(IConfiguration _config, ServiciosObj servicio)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(servicio);

                string rutaServicio = rutaBase + "api/Servicios/RegistrarServicio";

                HttpResponseMessage respuesta = client.PostAsync(rutaServicio, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<ServiciosObj>().Result;
                }
                return new ServiciosObj();
            }
        }

        public ServiciosObj? EditarServicio(IConfiguration _config, ServiciosObj servicio)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(servicio);
                string rutaServicio = rutaBase + "api/Servicios/ActualizarServicio";
                HttpResponseMessage respuesta = client.PutAsync(rutaServicio, body).GetAwaiter().GetResult();
                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<ServiciosObj>().Result;
                }

                return null;
            }
        }


        public ServiciosObj BorrarServicio(IConfiguration _config, int servicio_id)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                string rutaServicio = rutaBase + "api/Servicios/BorrarServicio?id_servicio=" + servicio_id;
                HttpResponseMessage respuesta = client.DeleteAsync(rutaServicio).GetAwaiter().GetResult();
                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<ServiciosObj>().Result;
                }

                return new ServiciosObj();
            }
        }

        public ServiciosObj ConsultarServicio(IConfiguration _config, int servicio_id)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                string rutaServicio = rutaBase + "api/Servicios/ConsultarServicioPorId?id_servicio=" + servicio_id;
                HttpResponseMessage respuesta = client.GetAsync(rutaServicio).GetAwaiter().GetResult();
                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<ServiciosObj>().Result;
                }

                return new ServiciosObj();
            }
        }


    }
}
