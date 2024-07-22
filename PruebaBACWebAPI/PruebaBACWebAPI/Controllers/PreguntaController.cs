using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaBACWebAPI.Models;
using System.Data;

namespace PruebaBACWebAPI.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaController : Controller
    {
        public readonly PruebaBACContext db;
        public PreguntaController(PruebaBACContext _context)
        {
            db = _context;
        }

        [HttpPost]
        [Route("GuardarPregunta")]
        public IActionResult GuardarPregunta(string usuario, string pregunta)
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@usuario", usuario));
                parameters.Add(new SqlParameter("@pregunta", pregunta));

                var ejecutar = db.Database
                                .ExecuteSqlRaw(@"exec GuardarPregunta @usuario, @pregunta", parameters.ToArray());
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = true });

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = e.Message, response = false });

            }
        }

        [HttpPut]
        [Route("CerrarPregunta")]
        public IActionResult CerrarPregunta(int idPregunta)
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@idPregunta", idPregunta));

                var ejecutar = db.Database
                                .ExecuteSqlRaw(@"exec CerrarPregunta @idPregunta", parameters.ToArray());
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = true });

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = e.Message, response = false });

            }
        }

        [HttpGet]
        [Route("ObtenerPreguntas")]
        public IActionResult ObtenerPreguntas()
        {
            try
            {
                List<Pregunta> ejecutar = db.Preguntas
                                .FromSqlRaw<Pregunta>(@"exec ObtenerPreguntas").ToList();
                foreach(var obj in ejecutar)
                {
                    var parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@idPregunta", obj.IdPregunta));
                    obj.Respuesta = db.Respuestas
                                .FromSqlRaw<Respuesta>(@"exec ObtenerRespuestas @idPregunta", parameters.ToArray()).ToList();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = ejecutar });

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = e.Message, response = false });

            }
        }
    }
}
