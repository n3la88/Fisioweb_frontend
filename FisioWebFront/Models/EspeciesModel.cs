using FisioWebFront.Entities;

namespace FisioWebFront.Models
{
	public class EspeciesModel
    {
        public List<EspeciesOBJ> ConsultarEspecies(IConfiguration _config)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {

                string rutaApi = rutaBase + "api/Especies/ConsultarEspecies";
                HttpResponseMessage respuesta = client.GetAsync(rutaApi).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<List<EspeciesOBJ>>().Result;
                }
                return new List<EspeciesOBJ>();
            }
        }
    }
}
