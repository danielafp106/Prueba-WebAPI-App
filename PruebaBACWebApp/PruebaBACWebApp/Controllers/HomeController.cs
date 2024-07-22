using Microsoft.AspNetCore.Mvc;
using PruebaBACWebApp.Models;
using PruebaBACWebApp.Services;
using System.Diagnostics;

namespace PruebaBACWebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly API_Interface _api;
        public const string SessionUsername = "_Usuario";

        public HomeController(API_Interface api)
        {
            _api = api;
        }


        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ComprobarCredenciales(Usuario credenciales)
        {
            if(ModelState.IsValid)
            {
                bool resultado = await _api.ComprobarCredenciales(credenciales);
                if(resultado)
                {
                    HttpContext.Session.SetString("usuario", credenciales.usuario1);
                    return RedirectToAction("Index", "Preguntas");

                }
                else
                {
                    ViewBag.Error = "Credenciales Incorrectas, intente de nuevo.";
                    return View("Index");
                }
            }
            ViewBag.Error = "Ingreso de datos incorrecto.";
            return View("Index");

        }

        public IActionResult Registrarse()
        {
            return View();
        }

        public async Task<IActionResult> CrearUsuario(Usuario credenciales)
        {
            if (ModelState.IsValid)
            {
                bool resultado = await _api.CrearUsuario(credenciales);
                if (resultado)
                {
                    ViewBag.Exito = "¡Cuenta creada con éxito!";

                }
                else
                {
                    ViewBag.Error = "Ese usuario ya existe, inicie sesión o intente de nuevo.";
                }
            }
            else
            {
                ViewBag.Error = "Ingreso de datos incorrecto.";

            }
            return View("Index");

        }

        public IActionResult CerrarSesion()
        {
            return RedirectToAction("Index", "Home");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}