using Datos.Repositories;
using Entidades;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly IGenericRepository<Materia> _materiaRepository;
        public MateriaService(IGenericRepository<Materia> materiaRepository)
        {
            _materiaRepository = materiaRepository;
        }
        public async Task<bool> Actualizar(Materia entidad)
        {
            return await _materiaRepository.Actualizar(entidad);
        }

        public async Task<bool> Agregar(Materia entidad)
        {
            return await _materiaRepository.Agregar(entidad);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _materiaRepository.Eliminar(id);
        }

        public async Task<Materia> Obtener(int id)
        {
            return await _materiaRepository.Obtener(id);
        }

        public async Task<IQueryable<Materia>> ObtenerTodos()
        {
            return await _materiaRepository.ObtenerTodos();
        }

        
    }
}
