using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class AlumnosMateria
    {
        public int? IdAlumno { get; set; }
        public int? IdMateria { get; set; }

        public virtual Alumno? IdAlumnoNavigation { get; set; }
        public virtual Materia? IdMateriaNavigation { get; set; }
    }
}
