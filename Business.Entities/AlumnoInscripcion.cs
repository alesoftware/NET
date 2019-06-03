﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class AlumnoInscripcion:BusinessEntity
    {
        private int _IDAlumno;

        public int Alumno
        {
            get { return _IDAlumno; }
            set { _IDAlumno = value; }
        }

        private int _IDCurso;

        public int IDCurso
        {
            get { return _IDCurso; }
            set { _IDCurso = value; }
        }

        private int _Nota;

        public int Nota
        {
            get { return _Nota; }
            set { _Nota = value; }
        }

        private string _Condicion;

        public string Condicion
        {
            get { return _Condicion; }
            set { _Condicion = value; }
        }


    }
}
