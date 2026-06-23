using System;
using System.ComponentModel.Design;
using System.IO;

namespace Trabalho_Anker_10._04._2026.Classes
{
    internal class Matricula
    {
        private int MatriculaAluno;
        private int CodigoDisciplina;
        private double Nota1;
        private double Nota2;

        public Matricula(int MatriculaAluno, int CodigoDisciplina, double Nota1, double Nota2)
        {
            this.MatriculaAluno = MatriculaAluno;
            this.CodigoDisciplina = CodigoDisciplina;
            this.Nota1 = Nota1;
            this.Nota2 = Nota2;
        }

        public int GetMatricula()
        {
            return MatriculaAluno;
        }

        public int GetCodigo()
        {
            return CodigoDisciplina;
        }

        public double GetNota1()
        {
            return Nota1;
        }

        public double GetNota2()
        {
            return Nota2;
        }




    }
}



    

    

       
    