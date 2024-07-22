using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaBACWebAPI.Models;

namespace PruebaBACWebAPI.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaController : Controller
    {
        public readonly PruebaBACContext db;
        public RespuestaController(PruebaBACContext _context)
        {
            db = _context;
        }

        [HttpPost]
        [Route("GuardarRespuesta")]
        public IActionResult GuardarRespuesta(string usuario, int IdPregunta, string respuesta)
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@usuario", usuario));
                parameters.Add(new SqlParameter("@idPregunta", IdPregunta));
                parameters.Add(new SqlParameter("@respuesta", respuesta));

                var ejecutar = db.Database
                                .ExecuteSqlRaw(@"exec GuardarRespuesta @usuario, @idPregunta, @respuesta", parameters.ToArray());
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = true });

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = e.Message, response = false });

            }
        }

        [HttpGet]
        [Route("ObtenerRespuestas")]
        public IActionResult ObtenerRespuestas(int idPregunta)
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@idPregunta", idPregunta));
                List<Respuesta> ejecutar = db.Respuestas
                                .FromSqlRaw<Respuesta>(@"exec ObtenerRespuestas @idPregunta", parameters.ToArray()).ToList();                
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = ejecutar });

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = e.Message, response = false });

            }
        }
    }
}
