using FisioWebFront.Entities;

namespace FisioWebFront.Models
{
    public class AdminModel
    {

        public ClienteUsuarioOBJ VerCliente(IConfiguration _config, int q)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                string rutaServicio = rutaBase + "api/Cliente/ConsultarClientePorID?id_usuario=" + q;
                HttpResponseMessage respuesta = client.GetAsync(rutaServicio).GetAwaiter().GetResult();
                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<ClienteUsuarioOBJ>().Result;
                }

                return new ClienteUsuarioOBJ();
            }
        }




    }
}
