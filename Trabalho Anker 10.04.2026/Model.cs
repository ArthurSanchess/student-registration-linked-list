using System;
using System.IO;
using Trabalho_Anker_10._04._2026.Classes;

namespace Trabalho_Anker_10._04._2026
{
    internal class Model
    {
        // Listas encadeadas que armazenam os dados em memória durante a execução do programa
        public static ListaAluno alunos = new ListaAluno();
        public static ListaDisciplina disciplinas = new ListaDisciplina();
        public static ListaMatricula matriculas = new ListaMatricula();

        // Exibe os resultados de uma disciplina lendo diretamente dos arquivos .dat
        // Usado para mostrar notas e situação de cada aluno matriculado na disciplina
        public static void MostrarResultadosDisciplina(string busca)
        {
            // Lê os dados diretamente dos arquivos
            string[] alunosArq = File.ReadAllLines("alunos.dat");
            string[] matriculasArq = File.ReadAllLines("matricula.dat");
            string[] disciplinasArq = File.ReadAllLines("disciplina.dat");

            int codigoDisciplina = -1;
            double notaMinima = 0;
            string nomeDisciplina = "";

            // Procura a disciplina pelo código ou nome
            for (int i = 0; i < disciplinasArq.Length; i++)
            {
                string[] dadosDisc = disciplinasArq[i].Split(';');

                if (dadosDisc[0] == busca || dadosDisc[1].ToLower() == busca.ToLower())
                {
                    codigoDisciplina = int.Parse(dadosDisc[0]);
                    nomeDisciplina = dadosDisc[1];
                    notaMinima = double.Parse(dadosDisc[2]);
                    break;
                }
            }

            // Se não encontrou a disciplina, encerra o método
            if (codigoDisciplina == -1)
            {
                Console.WriteLine("Disciplina não encontrada.");
                return;
            }

            Console.WriteLine($"Disciplina: {nomeDisciplina}");
            Console.WriteLine("Matricula | Nome | Nota1 | Nota2 | Média | Situação");
            Console.WriteLine("---------------------------------------------------");

            // Percorre as matrículas para encontrar alunos dessa disciplina
            for (int i = 0; i < matriculasArq.Length; i++)
            {
                string[] dadosMat = matriculasArq[i].Split(';');

                int matricula = int.Parse(dadosMat[0]);
                int codigoDisc = int.Parse(dadosMat[1]);

                if (codigoDisc == codigoDisciplina)
                {
                    double nota1 = double.Parse(dadosMat[2]);
                    double nota2 = double.Parse(dadosMat[3]);
                    double media = (nota1 + nota2) / 2;

                    string nomeAluno = "";

                    // Busca o nome do aluno pela matrícula
                    for (int j = 0; j < alunosArq.Length; j++)
                    {
                        string[] dadosAluno = alunosArq[j].Split(';');

                        if (int.Parse(dadosAluno[0]) == matricula)
                        {
                            nomeAluno = dadosAluno[1];
                            break;
                        }
                    }

                    // Verifica se o aluno foi aprovado ou reprovado com base na nota mínima
                    string situacao = media >= notaMinima ? "APROVADO" : "REPROVADO";
                    Console.WriteLine($"{matricula} | {nomeAluno} | N1:{nota1} | N2:{nota2} | Média:{media} | {situacao}");
                }
            }
        }

        // Calcula e exibe a média de todos os alunos em todas as disciplinas
        public static void MediaNota()
        {
            string[] alunosArq = File.ReadAllLines("alunos.dat");
            string[] matriculasArq = File.ReadAllLines("matricula.dat");
            string[] disciplinasArq = File.ReadAllLines("disciplina.dat");

            // Percorre cada matrícula para calcular a média do aluno na disciplina
            for (int i = 0; i < matriculasArq.Length; i++)
            {
                string[] dadosMat = matriculasArq[i].Split(';');

                int matricula = int.Parse(dadosMat[0]);
                int codigoDisc = int.Parse(dadosMat[1]);
                double nota1 = double.Parse(dadosMat[2]);
                double nota2 = double.Parse(dadosMat[3]);
                double media = (nota1 + nota2) / 2;
                double notaMinima = 0;

                // Busca a nota mínima da disciplina correspondente
                for (int j = 0; j < disciplinasArq.Length; j++)
                {
                    string[] dadosDisc = disciplinasArq[j].Split(';');

                    if (int.Parse(dadosDisc[0]) == codigoDisc)
                    {
                        notaMinima = double.Parse(dadosDisc[2]);
                    }
                }

                // Busca o nome do aluno pela matrícula
                string nomeAluno = "";

                for (int j = 0; j < alunosArq.Length; j++)
                {
                    string[] dadosAluno = alunosArq[j].Split(';');

                    if (int.Parse(dadosAluno[0]) == matricula)
                    {
                        nomeAluno = dadosAluno[1];
                    }
                }

                // Exibe o resultado de aprovação ou reprovação
                if (media >= notaMinima)
                    Console.WriteLine(nomeAluno + " Média: " + media + " → APROVADO");
                else
                    Console.WriteLine(nomeAluno + " Média: " + media + " → REPROVADO");
            }
        }

        // Grava todos os dados das listas nos arquivos .dat (sobrescreve o conteúdo anterior)
        public static void Salvar()
        {
            Console.WriteLine($"Na memória --> Alunos: {alunos.Count} | Disciplinas: {disciplinas.Count} | Matrículas: {matriculas.Count}");

            // Salva a lista de alunos no arquivo alunos.dat
            using (StreamWriter arqAlunos = new StreamWriter("alunos.dat", append: false))
            {
                foreach (Aluno a in alunos)
                {
                    arqAlunos.WriteLine($"{a.GetMatricula()};{a.GetNome()};{a.GetIdade()}");
                }
            }

            // Salva a lista de disciplinas no arquivo disciplina.dat
            using (StreamWriter arqDisc = new StreamWriter("disciplina.dat", append: false))
            {
                foreach (Disciplina d in disciplinas)
                {
                    arqDisc.WriteLine($"{d.GetCodigo()};{d.GetNome()};{d.GetNotaMinima()}");
                }
            }

            // Salva a lista de matrículas no arquivo matricula.dat
            using (StreamWriter arqMat = new StreamWriter("matricula.dat", append: false))
            {
                foreach (Matricula m in matriculas)
                {
                    arqMat.WriteLine($"{m.GetMatricula()};{m.GetCodigo()};{m.GetNota1()};{m.GetNota2()}");
                }
            }

            Console.WriteLine("Dados salvos com sucesso.");
        }

        // Lê os arquivos .dat e preenche as listas em memória ao iniciar o programa
        public static void Carregar()
        {
            // Carrega os alunos se o arquivo existir
            if (File.Exists("alunos.dat"))
            {
                string[] linhas = File.ReadAllLines("alunos.dat");
                Console.WriteLine($"Alunos no arquivo: {linhas.Length}");
                foreach (string linha in linhas)
                {
                    string[] campos = linha.Split(';');
                    int matricula = int.Parse(campos[0]);
                    string nome = campos[1];
                    int idade = int.Parse(campos[2]);
                    alunos.Add(new Aluno(matricula, nome, idade));
                }
            }

            // Carrega as disciplinas se o arquivo existir
            if (File.Exists("disciplina.dat"))
            {
                string[] linhas = File.ReadAllLines("disciplina.dat");
                Console.WriteLine($"Disciplinas no arquivo: {linhas.Length}");
                foreach (string linha in linhas)
                {
                    string[] campos = linha.Split(';');
                    int codigo = int.Parse(campos[0]);
                    string nome = campos[1];
                    double notaMinima = double.Parse(campos[2]);
                    disciplinas.Add(new Disciplina(codigo, nome, notaMinima));
                }
            }

            // Carrega as matrículas se o arquivo existir
            if (File.Exists("matricula.dat"))
            {
                string[] linhas = File.ReadAllLines("matricula.dat");
                Console.WriteLine($"Matriculas no arquivo: {linhas.Length}");
                foreach (string linha in linhas)
                {
                    string[] campos = linha.Split(';');
                    int matricula = int.Parse(campos[0]);
                    int codigo = int.Parse(campos[1]);
                    double nota1 = double.Parse(campos[2]);
                    double nota2 = double.Parse(campos[3]);
                    matriculas.Add(new Matricula(matricula, codigo, nota1, nota2));
                }
            }

            Console.WriteLine("Dados carregados com sucesso.");
        }
    }
}