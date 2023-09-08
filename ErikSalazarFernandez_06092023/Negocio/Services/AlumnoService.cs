using Datos.DBContext;
using Datos.Repositories;
using Entidades;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Negocio.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IGenericRepository<Alumno> _alumnoRepository;
        private readonly IGenericRepository<Materia> _materiaRepository;
        public AlumnoService(IGenericRepository<Alumno> alumnoRepository)
        {
            _alumnoRepository = alumnoRepository;
        }

        public async Task<bool> Actualizar(Alumno entidad)
        {
            return await _alumnoRepository.Actualizar(entidad);
        }

        public async Task<bool> Agregar(Alumno entidad)
        {
            return await _alumnoRepository.Agregar(entidad);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _alumnoRepository.Eliminar(id);
        }

        public async Task<Alumno> Obtener(int id)
        {
            return await _alumnoRepository.Obtener(id);
        }

        public async Task<IQueryable<Alumno>> ObtenerTodos()
        {
            return await _alumnoRepository.ObtenerTodos();
        }

        public async Task<ValidationResult> ValidarUsuario(string nombre, string apellidoPaterno)
        {
            var alumno = await _alumnoRepository.FirstOrDefaultAsync(a =>
                a.Nombre == nombre && a.ApellidoPaterno == apellidoPaterno);

            if (alumno != null)
            {
                return new ValidationResult
                {
                    AlumnoId = alumno.IdAlumno
                };
            }
            else
            {
                return new ValidationResult
                {
                    AlumnoId = null
                };
            }
        }

        public async Task<decimal> ObtenerCostoTotalMaterias(int idAlumno)
        {
            try
            {
                decimal costoTotal = 0;

                // Utiliza el procedimiento almacenado SumarCostoMaterias
                var idAlumnoParam = new SqlParameter("@IdAlumno", idAlumno);
                var sumaCostoParam = new SqlParameter("@SumaCosto", SqlDbType.Decimal)
                {
                    Precision = 10,
                    Scale = 2,
                    Direction = ParameterDirection.Output
                };

                await _alumnoRepository.ExecuteSqlCommandAsync("SumarCostoMaterias @IdAlumno, @SumaCosto OUTPUT", idAlumnoParam, sumaCostoParam);

                if (sumaCostoParam.Value != DBNull.Value)
                {
                    costoTotal = (decimal)sumaCostoParam.Value;
                }

                return costoTotal;
            }
            catch (Exception ex)
            {
                throw ex; // Maneja la excepción de acuerdo a tus requerimientos
            }
        }

    }
}
