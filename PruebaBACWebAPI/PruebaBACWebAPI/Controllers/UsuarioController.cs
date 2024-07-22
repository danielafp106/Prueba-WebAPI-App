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
    public class UsuarioController : Controller
    {
        public readonly PruebaBACContext db;
        public UsuarioController(PruebaBACContext _context)
        {
            db = _context;
        }

        [HttpPost]
        [Route("CrearUsuario")]
        public IActionResult CrearUsuario([FromBody] Usuario datos)
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@usuario", datos.Usuario1));
                parameters.Add(new SqlParameter("@contrasena", datos.Contrasena));
                parameters.Add(new SqlParameter("@r", SqlDbType.Bit) { Direction = ParameterDirection.Output });

                var ejecutar = db.Database
                                .ExecuteSqlRaw(@"exec CrearUsuario @usuario, @contrasena, @r out", parameters.ToArray());

                if (Convert.ToBoolean(parameters[2].Value) == true)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = Convert.ToBoolean(parameters[2].Value) });
                }
                else
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, new { mensaje = "Error", response = false });
                }
            }            
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = e.Message, response = false });

            }
        }

        [HttpPost]
        [Route("ComprobarCredenciales")]
        public IActionResult ComprobarCredenciales([FromBody] Usuario credenciales)
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@usuario", credenciales.Usuario1));
                parameters.Add(new SqlParameter("@contrasena", credenciales.Contrasena));
                parameters.Add(new SqlParameter("@r", SqlDbType.Bit) { Direction = ParameterDirection.Output });

                var ejecutar = db.Database
                                .ExecuteSqlRaw(@"exec ComprobarCredenciales @usuario, @contrasena, @r out", parameters.ToArray());

                if (Convert.ToBoolean(parameters[2].Value) == true)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = Convert.ToBoolean(parameters[2].Value) });
                }
                else
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, new { mensaje = "Error", response = false });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = e.Message, response = false });

            }

        }

    }
}
