using Microsoft.AspNetCore.Mvc;
using PruebaBACWebApp.Models;
using PruebaBACWebApp.Services;

namespace PruebaBACWebApp.Controllers
{
    public class PreguntasController : Controller
    {
        private readonly API_Interface _api;
        public const string SessionUsername = "_Usuario";

        public PreguntasController(API_Interface api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["usuario"] = HttpContext.Session.GetString("usuario").ToString();
            List<Pregunta> registros = await _api.ObtenerPreguntas();
            foreach (var item in registros)
            {
                Respuesta reg = new Respuesta();
                reg.idPregunta = item.idPregunta;
                reg.usuario = ViewData["usuario"].ToString();

                item.registro = reg;
            }
            return View(registros);
        }

        public IActionResult HacerPregunta()
        {
            ViewData["usuario"] = HttpContext.Session.GetString("usuario").ToString();

            return View();
        }

        public async Task<IActionResult> GuardarPregunta(Pregunta registro)
        {
            ViewData["usuario"] = HttpContext.Session.GetString("usuario").ToString();

            if (ModelState.IsValid)
            {
                bool resultado = await _api.GuardarPregunta(registro);
                if (resultado)
                {
                    ViewBag.Exito = "¡Pregunta publicada con éxito!";

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
            ModelState.Clear();
            return View("Index", registros);
        }

        public async Task<IActionResult> CerrarPregunta(int idPregunta)
        {
            ViewData["usuario"] = HttpContext.Session.GetString("usuario").ToString();

            if (idPregunta!=0)
            {
                bool resultado = await _api.CerrarPregunta(idPregunta);
                if (resultado)
                {
                    ViewBag.Exito = $"¡Pregunta P#{idPregunta} cerrada con éxito!";

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
            ModelState.Clear();
            return View("Index", registros);
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
            ModelState.Clear();

            return View("Index", registros);
        }
    }
}
