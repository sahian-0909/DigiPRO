using Datos.DBContext;
using Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class MateriaRepository : IGenericRepository<Materia>
    {
        private readonly ControlEscolarContext _controlEscolarContext;
        public MateriaRepository(ControlEscolarContext controlEscolarContext)
        {
            _controlEscolarContext = controlEscolarContext;
        }

        public async Task<IQueryable<Materia>> ObtenerTodos()
        {
            try
            {
                var query = _controlEscolarContext.Materias.FromSqlRaw("exec consultarMaterias @id = -1");
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Agregar(Materia entidad)
        {
            try
            {
                var nombreParam = new SqlParameter("@Nombre", entidad.Nombre);
                var costoParam = new SqlParameter("@Costo", entidad.Costo);

                await _controlEscolarContext.Database.ExecuteSqlRawAsync("EXEC sp_AgregarMateria @Nombre, @Costo", nombreParam, costoParam);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Materia> Obtener(int id)
        {
            try
            {
                var idParam = new SqlParameter("@id", id);
                var query = await _controlEscolarContext.Materias.FromSqlRaw("exec consultarMaterias @id", idParam).FirstOrDefaultAsync();

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Actualizar(Materia entidad)
        {
            try
            {
                var idParam = new SqlParameter("@Id", entidad.IdMateria);
                var nombreParam = new SqlParameter("@Nombre", entidad.Nombre);
                var costoParam = new SqlParameter("@Costo", entidad.Costo);

                await _controlEscolarContext.Database.ExecuteSqlRawAsync("EXEC sp_ActualizarMateria @Id, @Nombre, @Costo", idParam, nombreParam, costoParam);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var idParam = new SqlParameter("@Id", id);

                await _controlEscolarContext.Database.ExecuteSqlRawAsync("EXEC sp_EliminarMateria @Id", idParam);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Materia> FirstOrDefaultAsync(Expression<Func<Materia, bool>> predicate)
        {
            return await _controlEscolarContext.Set<Materia>().FirstOrDefaultAsync(predicate);
        }

        public async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return await _controlEscolarContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public IQueryable<Materia> FromSql(string sql, params object[] parameters)
        {
            return _controlEscolarContext.Set<Materia>().FromSqlRaw(sql, parameters);
        }
    }
}
