    using FisioWebFront.Entities;
using System.Net.Http.Headers;

namespace FisioWebFront.Models
{
    public class ClienteModel
    {

        public ClienteOBJ ConsultarCliente(IConfiguration _config, int id_usuario)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {

                string rutaApi = rutaBase + "api/Cliente/ConsultarClientePorID?id_usuario=" + id_usuario;
                HttpResponseMessage respuesta = client.GetAsync(rutaApi).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<ClienteOBJ>().Result;
                }
                return new ClienteOBJ();
            }
        }

        public List<ClienteUsuarioOBJ>? VerListaClientes(IConfiguration _config)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                string rutaServicio = rutaBase + "api/Cliente/ConsultarClientes";
                HttpResponseMessage respuesta = client.GetAsync(rutaServicio).GetAwaiter().GetResult();
                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<List<ClienteUsuarioOBJ>>().Result;
                }

                return new List<ClienteUsuarioOBJ>();
            }
        }

        public ClienteOBJ RegistrarCliente(IConfiguration _config, ClienteOBJ cliente)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(cliente);
                string rutaApi = rutaBase + "api/Cliente/RegistrarCliente";
                HttpResponseMessage respuesta = client.PostAsync(rutaApi, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<ClienteOBJ>().Result;
                }
                return new ClienteOBJ();
            }
        }

        public UsuarioObj RegistrarUsuario(IConfiguration _config, UsuarioObj usuario)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(usuario);
                string rutaApi = rutaBase + "api/Usuario/RegistrarUsuario";
                HttpResponseMessage respuesta = client.PostAsync(rutaApi, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<UsuarioObj>().Result;
                }
                return new UsuarioObj();
            }
        }

        public ClienteUsuarioOBJ? ActualizarCliente(IConfiguration _config, ClienteUsuarioOBJ cliente)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(cliente);
                string rutaApi = rutaBase + "api/Cliente/ActualizarCliente";
                HttpResponseMessage respuesta = client.PutAsync(rutaApi, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<ClienteUsuarioOBJ>().Result;
                }

                return new ClienteUsuarioOBJ();
            }
        }

        public ClienteOBJ ConsultarClientePorCorreo(IConfiguration _config, string correo_usuario)
        {
            string rutaBase = _config.GetSection("AppSettings:UrlBackend").Value;

            using (var client = new HttpClient())
            {

                string rutaApi = rutaBase + "api/Cliente/ConsultarClientePorCorreo?correo_usuario=" + correo_usuario;
                HttpResponseMessage respuesta = client.GetAsync(rutaApi).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode && respuesta.Content != null)
                {
                    return respuesta.Content.ReadFromJsonAsync<ClienteOBJ>().Result;
                }
                return new ClienteOBJ();
            }
        }

    }
}
