using FisioWebFront.Entities;

namespace FisioWebFront.Models
{
	public class FaqModel
    {
        public List<FaqObj> ConsultarFAQ(IConfiguration _config)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                string rutaApi = rutaBase + "api/Faq/ConsultarFAQ";
                HttpResponseMessage respuesta = client.GetAsync(rutaApi).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<List<FaqObj>>().Result;
                }
                return new List<FaqObj>();
            }
        }

        public FaqObj RegistrarFAQ(IConfiguration _config, FaqObj faq)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(faq);

                string rutaServicio = rutaBase + "api/Faq/RegistrarFAQ";

                HttpResponseMessage respuesta = client.PostAsync(rutaServicio, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<FaqObj>().Result;
                }
                return new FaqObj();
            }
        }

        public FaqObj? EditarFAQ(IConfiguration _config, FaqObj faq)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(faq);
                string rutaServicio = rutaBase + "api/Faq/ActualizarFAQ";
                HttpResponseMessage respuesta = client.PutAsync(rutaServicio, body).GetAwaiter().GetResult();
                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<FaqObj>().Result;
                }

                return null;
            }
        }


        public FaqObj BorrarFAQ(IConfiguration _config, int id_ayuda)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                string rutaServicio = rutaBase + "api/Faq/BorrarFAQ?id_ayuda=" + id_ayuda;
                HttpResponseMessage respuesta = client.DeleteAsync(rutaServicio).GetAwaiter().GetResult();
                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<FaqObj>().Result;
                }

                return new FaqObj();
            }
        }

        public FaqObj ConsultarFAQ(IConfiguration _config, int id_ayuda)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                string rutaServicio = rutaBase + "api/Faq/ConsultarFaqPorId?id_ayuda=" + id_ayuda;
                HttpResponseMessage respuesta = client.GetAsync(rutaServicio).GetAwaiter().GetResult();
                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<FaqObj>().Result;
                }

                return new FaqObj();
            }
        }

    }

}
