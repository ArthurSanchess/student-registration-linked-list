using System;
using System.IO;

namespace Trabalho_Anker_10._04._2026.Classes
{
    internal class Disciplina
    {
        private int Codigo;
        private string Nome;
        private double notaMinima;

        public Disciplina(int Codigo, string Nome, double notaMinima)
        {
            this.Codigo = Codigo;
            this.Nome = Nome;
            this.notaMinima = notaMinima;
        }

        //descobrimos que podemos fazer assim
        public int GetCodigo() => Codigo;
        public string GetNome() => Nome;
        public double GetNotaMinima() => notaMinima;



    }
}



    

