using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Services
{
    public interface IMateriaService
    {
        Task<IQueryable<Materia>> ObtenerTodos();
        Task<bool> Agregar(Materia entidad);
        Task<bool> Actualizar(Materia entidad);
        Task<bool> Eliminar(int id);
        Task<Materia> Obtener(int id);
    }
}
