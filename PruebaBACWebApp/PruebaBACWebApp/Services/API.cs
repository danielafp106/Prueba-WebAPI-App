using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PruebaBACWebApp.Models;
using System.Text;

namespace PruebaBACWebApp.Services
{
    public class API : API_Interface
    {
        private static string _baseurl;

        public API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task<List<Pregunta>> ObtenerPreguntas()
        {
            List<Pregunta> lista = new List<Pregunta>();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync("Pregunta/ObtenerPreguntas");
            if (response.IsSuccessStatusCode)
            {
                var json_repuesta = await response.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(json_repuesta);
                var obj = parsedObject["response"].ToString();
                var resultado = JsonConvert.DeserializeObject<List<Pregunta>>(obj);
                lista.AddRange(resultado);
            }
            return lista;
        }

        public async Task<bool> CrearUsuario(Usuario registro)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(registro), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync($"Usuario/CrearUsuario", content);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

        public async Task<bool> ComprobarCredenciales(Usuario registro)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(registro), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync($"Usuario/ComprobarCredenciales", content);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

        public async Task<bool> GuardarPregunta(Pregunta registro)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl); 

            var response = await cliente.PostAsync($"Pregunta/GuardarPregunta?usuario={registro.usuario}&pregunta={registro.pregunta1}",null);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

        public async Task<bool> GuardarRespuesta(Respuesta registro)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var response = await cliente.PostAsync($"Respuesta/GuardarRespuesta?usuario={registro.usuario}&IdPregunta={registro.idPregunta}&respuesta={registro.respuesta1}", null);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

        public async Task<bool> CerrarPregunta(int idPregunta)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var response = await cliente.PutAsync($"Pregunta/CerrarPregunta?idPregunta={idPregunta}", null);

            if (response.IsSuccessStatusCode)
            {
                resultado = true;
            }

            return resultado;
        }

    }
}
