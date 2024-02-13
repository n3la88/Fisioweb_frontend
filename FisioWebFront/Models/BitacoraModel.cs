using FisioWebFront.Entities;

namespace FisioWebFront.Models
{
    public class BitacoraModel
    {
        public BitacoraOBJ DatosBitacora(IConfiguration _config, BitacoraOBJ bitacora)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(bitacora);
                string rutaApi = rutaBase + "api/Bitacora/RegistrarBitacora";
                HttpResponseMessage respuesta = client.PostAsync(rutaApi, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<BitacoraOBJ>().Result;
                }
                return new BitacoraOBJ();
            }
        }
    }
}
