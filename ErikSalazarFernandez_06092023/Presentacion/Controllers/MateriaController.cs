using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Services;
using Presentacion.Models.ViewModels;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaService _materiaService;

        public MateriaController(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var lista = await _materiaService.ObtenerTodos();

                return StatusCode(StatusCodes.Status200OK, lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Guardar([FromBody] VMMateria materia)
        {
            try
            {
                Materia materiaNueva = new Materia()
                {
                    Nombre = materia.Nombre,
                    Costo = materia.Costo,
                };

                bool respuesta = await _materiaService.Agregar(materiaNueva);

                if (respuesta)
                {
                    return Ok(new { mensaje = "Materia agregada correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo agregar la materia" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error interno del servidor: " + ex.Message });
            }
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] VMMateria materia)
        {
            Materia materiaActualizada = new Materia()
            {
                IdMateria = materia.IdMateria,
                Nombre = materia.Nombre,
                Costo = materia.Costo,
            };

            try
            {
                bool respuesta = await _materiaService.Actualizar(materiaActualizada);

                if (respuesta)
                {
                    return Ok(new { mensaje = "Materia actualizada correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo actualizar la materia" });
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
                bool respuesta = await _materiaService.Eliminar(id);

                if (respuesta)
                {
                    return Ok(new { mensaje = "Materia eliminada correctamente" });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo eliminar la materia" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error interno del servidor: " + ex.Message });
            }
        }
    }
}
