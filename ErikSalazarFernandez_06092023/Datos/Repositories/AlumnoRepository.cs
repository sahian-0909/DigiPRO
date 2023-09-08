using Datos.DBContext;
using Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class AlumnoRepository : IGenericRepository<Alumno>
    {
        private readonly ControlEscolarContext _controlEscolarContext;
        public AlumnoRepository(ControlEscolarContext controlEscolarContext)
        {
            _controlEscolarContext = controlEscolarContext;
        }
        public async Task<IQueryable<Alumno>> ObtenerTodos()
        {
            try
            {
                var query = _controlEscolarContext.Alumnos.FromSqlRaw("exec consultarAlumnos @id = -1");
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Agregar(Alumno entidad)
        {
            try
            {
                var nombreParam = new SqlParameter("@Nombre", entidad.Nombre);
                var apellidoPaternoParam = new SqlParameter("@ApellidoPaterno", entidad.ApellidoPaterno);
                var apellidoMaternoParam = new SqlParameter("@ApellidoMaterno", entidad.ApellidoMaterno);
                var registradoParam = new SqlParameter("@Registrado", SqlDbType.Bit);
                registradoParam.Direction = ParameterDirection.Output;
                var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 50);
                mensajeParam.Direction = ParameterDirection.Output;

                await _controlEscolarContext.Database.ExecuteSqlRawAsync("EXEC sp_RegistrarAlumno @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Registrado OUTPUT, @Mensaje OUTPUT",
                    nombreParam, apellidoPaternoParam, apellidoMaternoParam, registradoParam, mensajeParam);

                var registrado = Convert.ToBoolean(registradoParam.Value);
                var mensaje = mensajeParam.Value.ToString();

                if (registrado)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Alumno> Obtener(int id)
        {
            try
            {
                var idParam = new SqlParameter("@id", id);
                var query = await _controlEscolarContext.Alumnos.FromSqlRaw("exec consultarAlumnos @id", idParam).FirstOrDefaultAsync();

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Actualizar(Alumno entidad)
        {
            try
            {
                var idParam = new SqlParameter("@Id", entidad.IdAlumno); // Supongo que existe una propiedad "Id" en la clase Alumno
                var nombreParam = new SqlParameter("@Nombre", entidad.Nombre);
                var apellidoPaternoParam = new SqlParameter("@ApellidoPaterno", entidad.ApellidoPaterno);
                var apellidoMaternoParam = new SqlParameter("@ApellidoMaterno", entidad.ApellidoMaterno);
                var actualizadoParam = new SqlParameter("@Actualizado", SqlDbType.Bit);
                actualizadoParam.Direction = ParameterDirection.Output;
                var mensajeParam = new SqlParameter("@Mensaje", SqlDbType.VarChar, 50);
                mensajeParam.Direction = ParameterDirection.Output;

                await _controlEscolarContext.Database.ExecuteSqlRawAsync("EXEC sp_ActualizarAlumnos @Id, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Actualizado OUTPUT, @Mensaje OUTPUT",
                    idParam, nombreParam, apellidoPaternoParam, apellidoMaternoParam, actualizadoParam, mensajeParam);

                var actualizado = Convert.ToBoolean(actualizadoParam.Value);
                var mensaje = mensajeParam.Value.ToString();

                if (actualizado)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

                await _controlEscolarContext.Database.ExecuteSqlRawAsync("EXEC sp_EliminarAlumno @Id", idParam);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Alumno> FirstOrDefaultAsync(Expression<Func<Alumno, bool>> predicate)
        {
            return await _controlEscolarContext.Set<Alumno>().FirstOrDefaultAsync(predicate);
        }

        public async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return await _controlEscolarContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public IQueryable<Alumno> FromSql(string sql, params object[] parameters)
        {
            return _controlEscolarContext.Set<Alumno>().FromSqlRaw(sql, parameters);
        }
    }
}
