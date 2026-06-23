using System;
using System.IO;

namespace Trabalho_Anker_10._04._2026.Classes
{
    internal class Aluno
    {
        private int matricula;
        private string nome;
        private int idade;

        public Aluno(int matricula, string nome, int idade)
        {
            this.matricula = matricula;
            this.nome = nome;
            this.idade = idade;
        }

        public int GetMatricula()
        {
            return matricula;
        }

        public string GetNome()
        {
            return nome;
        }

        public int GetIdade()
        {
            return idade;


        }
    }
}
         

        


        
    
