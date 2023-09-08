using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Services;
using Presentacion.Models.ViewModels;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;

        public AlumnoController(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var lista = await _alumnoService.ObtenerTodos();

                return StatusCode(StatusCodes.Status200OK, lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Guardar([FromBody] VMAlumno alumno)
        {
            try
            {
                Alumno alumnoNuevo = new Alumno()
                {
                    Nombre = alumno.Nombre,
                    ApellidoMaterno = alumno.ApellidoMaterno,
                    ApellidoPaterno = alumno.ApellidoPaterno,
                };

                bool respuesta = await _alumnoService.Agregar(alumnoNuevo);

                if (respuesta)
                {
                    return Ok(new { mensaje = "Alumno agregado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo agregar el alumno" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] VMAlumno alumno)
        {
            if (alumno == null)
            {
                return BadRequest("Los datos del alumno no son válidos.");
            }

            Alumno alumnoNuevo = new Alumno()
            {
                IdAlumno = alumno.IdAlumno,
                Nombre = alumno.Nombre,
                ApellidoMaterno = alumno.ApellidoMaterno,
                ApellidoPaterno = alumno.ApellidoPaterno,
            };

            try
            {
                bool respuesta = await _alumnoService.Actualizar(alumnoNuevo);

                if (respuesta)
                {
                    return Ok(new { mensaje = "Alumno actualizado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo actualizar el alumno" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error interno del servidor: " + ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                bool respuesta = await _alumnoService.Eliminar(id);

                if (respuesta)
                {
                    return Ok(new { mensaje = "Alumno eliminado correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo eliminar el alumno" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error interno del servidor: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("Validar")]
        public async Task<IActionResult> ValidarUsuario([FromBody] ValidacionAlumnoRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.ApellidoPaterno))
            {
                return BadRequest(new { mensaje = "Los datos de entrada son inválidos" });
            }

            var resultado = await _alumnoService.ValidarUsuario(request.Nombre, request.ApellidoPaterno);

            if (resultado.AlumnoId.HasValue)
            {
                return Ok(new { mensaje = "Alumno encontrado correctamente" });
            }
            else
            {
                return BadRequest(new { mensaje = "No se pudo encontrar el alumno" });
            }
        }
    }

}
