using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        Task<IQueryable<TEntityModel>> ObtenerTodos();
        Task<bool> Agregar(TEntityModel entidad);
        Task<bool> Actualizar(TEntityModel entidad);
        Task<bool> Eliminar(int id);
        Task<TEntityModel> Obtener(int id);
        Task<TEntityModel> FirstOrDefaultAsync(Expression<Func<TEntityModel, bool>> predicate);

        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
        IQueryable<TEntityModel> FromSql(string sql, params object[] parameters);
    }
}
