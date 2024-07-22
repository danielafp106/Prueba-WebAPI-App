using PruebaBACWebApp.Models;

namespace PruebaBACWebApp.Services
{
    public interface API_Interface
    {
        //Preguntas
        Task<List<Pregunta>> ObtenerPreguntas();
        Task<bool> GuardarPregunta(Pregunta registro);
        Task<bool> CerrarPregunta(int idPregunta);

        ////Respuestas
       // Task<List<Respuesta>> ObtenerRespuetas(int idPregunta);
        Task<bool> GuardarRespuesta(Respuesta registro);

        //Usuarios
        Task<bool> CrearUsuario(Usuario registro);
        Task<bool> ComprobarCredenciales(Usuario datos);
    }
}
