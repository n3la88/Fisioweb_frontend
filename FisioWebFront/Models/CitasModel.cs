using FisioWebFront.Entities;

namespace FisioWebFront.Models
{
    public class CitasModel
    {

        public CitasObj RegistrarCita(IConfiguration _config, CitasObj cita)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(cita);
                string rutaApi = rutaBase + "api/Cita/RegistrarCita";
                HttpResponseMessage respuesta = client.PostAsync(rutaApi, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<CitasObj>().Result;
                }
                return new CitasObj();
            }
        }

        public CitasObj ConsultarCita(IConfiguration _config, int id_mascota)
        {
            string ruta = _config.GetSection("AppSettings:UrlBackend").Value;
            using (var cliente = new HttpClient())
            {
                string rutaApi = ruta + "api/Cita/ConsultarCita?id_mascota" + id_mascota;
                HttpResponseMessage respuesta = cliente.GetAsync(rutaApi).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<CitasObj>().Result;
                }
                return new CitasObj();
            }
        }

        public List<CitasObj> ConsultarCitas(IConfiguration _config)
        {
            string ruta = _config.GetSection("AppSettings:UrlBackend").Value;
            using (var cliente = new HttpClient())
            {
                string rutaApi = ruta + "api/Cita/ConsultarCitas";
                HttpResponseMessage respuesta = cliente.GetAsync(rutaApi).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<List<CitasObj>>().Result;
                }
                return new List<CitasObj>();
            }
        }

    }

}
