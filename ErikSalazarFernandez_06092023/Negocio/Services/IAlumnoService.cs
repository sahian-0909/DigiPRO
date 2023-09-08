using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Services
{
    public interface IAlumnoService
    {
        Task<IQueryable<Alumno>> ObtenerTodos();
        Task<bool> Agregar(Alumno entidad);
        Task<bool> Actualizar(Alumno entidad);
        Task<bool> Eliminar(int id);
        Task<Alumno> Obtener(int id);

        Task<ValidationResult> ValidarUsuario(string nombre, string apellidoPaterno);
    }
}
