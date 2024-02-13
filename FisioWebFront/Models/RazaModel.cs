using FisioWebFront.Entities;

namespace FisioWebFront.Models
{
	public class RazaModel
	{
        public List<RazaOBJ> ConsultarRazas(IConfiguration _config, int id_especie)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {

                string rutaApi = rutaBase + "api/Raza/ConsultarRazas?id_especie="+id_especie;
                HttpResponseMessage respuesta = client.GetAsync(rutaApi).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<List<RazaOBJ>>().Result;
                }
                return new List<RazaOBJ>();
            }
        }

    }
}
