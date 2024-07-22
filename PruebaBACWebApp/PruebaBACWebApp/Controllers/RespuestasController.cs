using Microsoft.AspNetCore.Mvc;
using PruebaBACWebApp.Models;
using PruebaBACWebApp.Services;

namespace PruebaBACWebApp.Controllers
{
    public class RespuestasController : Controller
    {
        private readonly API_Interface _api;
        public const string SessionUsername = "_Usuario";

        public RespuestasController(API_Interface api)
        {
            _api = api;
        }
        public IActionResult Index()
        {
            ViewData["usuario"] = HttpContext.Session.GetString("usuario").ToString();

            return View();
        }

        public IActionResult IngresarRespuesta(Respuesta registro)
        {
            ViewData["usuario"] = HttpContext.Session.GetString("usuario").ToString();

            return PartialView("IngresarRespuesta",registro);
        }
        public async Task<IActionResult> GuardarRespuesta(Respuesta registro)
        {
            ViewData["usuario"] = HttpContext.Session.GetString("usuario").ToString();

            if (ModelState.IsValid)
            {
                bool resultado = await _api.GuardarRespuesta(registro);
                if (resultado)
                {
                    ViewBag.Exito = "¡Respuesta publicada con éxito!";

                }
                else
                {
                    ViewBag.Error = "Algo salió mal, intente de nuevo";
                }
            }
            else
            {
                ViewBag.Error = "Ingreso de datos incorrecto.";

            }
            List<Pregunta> registros = await _api.ObtenerPreguntas();
            foreach (var item in registros)
            {
                Respuesta reg = new Respuesta();
                reg.idPregunta = item.idPregunta;
                reg.usuario = @ViewData["usuario"].ToString();

                item.registro = reg;
            }

            return RedirectToAction("Index", "Preguntas",registros);
        }
    }
}
