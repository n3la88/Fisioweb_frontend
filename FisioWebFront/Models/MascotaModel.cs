
using FisioWebFront.Entities;
using FisioWebFront.Controllers;
using System.Net.Http.Headers;

namespace FisioWebFront.Models
{
    public class MascotaModel
    {

        public MascotaRazaEspecieOBJ VerMascota(IConfiguration _config, int id_mascota)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                string rutaServicio = rutaBase + "api/Mascota/ConsultarMascotaYRaza?id_mascota=" + id_mascota;
                HttpResponseMessage respuesta = client.GetAsync(rutaServicio).GetAwaiter().GetResult();
                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<MascotaRazaEspecieOBJ>().Result;
                }

                return new MascotaRazaEspecieOBJ();
            }
        }


        public List<MascotaRazaEspecieOBJ>? VerMascotasPorCLiente(IConfiguration _config, int id_cliente)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                string rutaServicio = rutaBase + "api/Mascota/ConsultarMascotasYRaza?id_cliente=" + id_cliente;
                HttpResponseMessage respuesta = client.GetAsync(rutaServicio).GetAwaiter().GetResult();
                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<List<MascotaRazaEspecieOBJ>>().Result;
                }

                return new List<MascotaRazaEspecieOBJ>();
            }
        }

        public MascotaOBJ RegistrarMascota(IConfiguration _config, MascotaOBJ mascota)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(mascota);

                string rutaServicio = rutaBase + "api/Mascota/RegistrarMascota";

                HttpResponseMessage respuesta = client.PostAsync(rutaServicio, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<MascotaOBJ>().Result;
                }
                return new MascotaOBJ();
            }
        }

        public MascotaOBJ? EditarMascota(IConfiguration _config, MascotaOBJ mascota)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(mascota);
                string rutaServicio = rutaBase + "api/Mascota/ActualizarMascota";

                HttpResponseMessage respuesta = client.PutAsync(rutaServicio, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<MascotaOBJ>().Result;
                }

                return null;
            }
        }





    }


}