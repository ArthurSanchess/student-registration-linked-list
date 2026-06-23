using System;
using System.Text;
using Trabalho_Anker_10._04._2026.Classes;

namespace Trabalho_Anker_10._04._2026
{
    internal class Controller
    {
        // Cadastra um novo aluno gerando a matrícula automaticamente
        public static void CadastroAluno()
        {
            // Encontra o maior número de matrícula existente e soma 1
            int novaMatricula = 1;
            foreach (Aluno a in Model.alunos)
            {
                if (a.GetMatricula() >= novaMatricula)
                    novaMatricula = a.GetMatricula() + 1;
            }

            Console.WriteLine("Digite o nome do aluno:");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite a idade do aluno:");
            int idade = int.Parse(Console.ReadLine());

            // Adiciona o novo aluno ao final da lista
            Model.alunos.Add(new Aluno(novaMatricula, nome, idade));

            Console.WriteLine($"Aluno cadastrado com matrícula {novaMatricula}");
        }

        // Cadastra uma nova disciplina gerando o código automaticamente
        public static void CadastroDisciplina()
        {
            // Encontra o maior código existente e soma 1
            int novoCodigo = 1;
            foreach (Disciplina d in Model.disciplinas)
            {
                if (d.GetCodigo() >= novoCodigo)
                    novoCodigo = d.GetCodigo() + 1;
            }

            string nome;
            while (true)
            {
                Console.WriteLine("Digite o nome da disciplina:");
                nome = Console.ReadLine();

                // Verifica se já existe uma disciplina com esse nome
                bool existe = false;
                foreach (Disciplina d in Model.disciplinas)
                {
                    if (d.GetNome().ToLower() == nome.ToLower())
                    {
                        existe = true;
                        break;
                    }
                }

                if (existe)
                    Console.WriteLine("Essa disciplina já existe. Digite outro nome.");
                else
                    break;
            }

            Console.WriteLine("Digite a nota mínima:");
            double notaMinima = double.Parse(Console.ReadLine());

            // Adiciona a nova disciplina ao final da lista
            Model.disciplinas.Add(new Disciplina(novoCodigo, nome, notaMinima));

            Console.WriteLine($"Disciplina cadastrada com código {novoCodigo}");
        }

        // Realiza a matrícula de um aluno em uma disciplina
        public static void CadastroMatricula()
        {
            int matriculaAluno = -1;
            int codigoDisciplina = -1;

            // Busca o aluno pelo nome ou número de matrícula
            while (true)
            {
                Console.WriteLine("Digite o nome ou matrícula do aluno:");
                string busca = Console.ReadLine();

                foreach (Aluno a in Model.alunos)
                {
                    if (a.GetMatricula().ToString() == busca || a.GetNome().ToLower() == busca.ToLower())
                    {
                        matriculaAluno = a.GetMatricula();
                        Console.WriteLine("Aluno encontrado: " + a.GetNome());
                        break;
                    }
                }

                if (matriculaAluno != -1) break;
                Console.WriteLine("Aluno não encontrado. Digite novamente.");
            }

            // Busca a disciplina pelo nome ou código
            while (true)
            {
                Console.WriteLine("Digite o nome ou código da disciplina:");
                string busca = Console.ReadLine();

                foreach (Disciplina d in Model.disciplinas)
                {
                    if (d.GetCodigo().ToString() == busca || d.GetNome().ToLower() == busca.ToLower())
                    {
                        codigoDisciplina = d.GetCodigo();
                        Console.WriteLine("Disciplina encontrada: " + d.GetNome());
                        break;
                    }
                }

                if (codigoDisciplina != -1) break;
                Console.WriteLine("Disciplina não encontrada. Digite novamente.");
            }

            // Impede que o mesmo aluno seja matriculado duas vezes na mesma disciplina
            foreach (Matricula m in Model.matriculas)
            {
                if (m.GetMatricula() == matriculaAluno && m.GetCodigo() == codigoDisciplina)
                {
                    Console.WriteLine("Esse aluno já está matriculado nessa disciplina.");
                    return;
                }
            }

            // Registra a matrícula com notas zeradas (serão atribuídas depois)
            Model.matriculas.Add(new Matricula(matriculaAluno, codigoDisciplina, 0, 0));

            Console.WriteLine("Matrícula cadastrada com sucesso.");
        }

        // Atribui ou altera as notas de um aluno em uma disciplina
        public static void AtribuirNota()
        {
            int matriculaAluno = -1;
            int codigoDisciplina = -1;

            // Busca o aluno pelo nome ou número de matrícula
            while (true)
            {
                Console.WriteLine("Digite o nome ou matrícula do aluno:");
                string busca = Console.ReadLine();

                foreach (Aluno a in Model.alunos)
                {
                    if (a.GetMatricula().ToString() == busca || a.GetNome().ToLower() == busca.ToLower())
                    {
                        matriculaAluno = a.GetMatricula();
                        Console.WriteLine("Aluno encontrado: " + a.GetNome());
                        break;
                    }
                }

                if (matriculaAluno != -1) break;
                Console.WriteLine("Aluno não encontrado. Digite novamente.");
            }

            // Busca a disciplina pelo nome ou código
            while (true)
            {
                Console.WriteLine("Digite o nome ou código da disciplina:");
                string busca = Console.ReadLine();

                foreach (Disciplina d in Model.disciplinas)
                {
                    if (d.GetCodigo().ToString() == busca || d.GetNome().ToLower() == busca.ToLower())
                    {
                        codigoDisciplina = d.GetCodigo();
                        Console.WriteLine("Disciplina encontrada: " + d.GetNome());
                        break;
                    }
                }

                if (codigoDisciplina != -1) break;
                Console.WriteLine("Disciplina não encontrada. Digite novamente.");
            }

            bool encontrou = false;

            // Usa índice pois é necessário substituir o objeto na lista ao atualizar as notas
            for (int i = 0; i < Model.matriculas.Count; i++)
            {
                if (Model.matriculas[i].GetMatricula() == matriculaAluno &&
                    Model.matriculas[i].GetCodigo() == codigoDisciplina)
                {
                    encontrou = true;

                    // Se já possui notas, pergunta se deseja alterá-las
                    if (Model.matriculas[i].GetNota1() != 0 || Model.matriculas[i].GetNota2() != 0)
                    {
                        Console.WriteLine($"Esse aluno já possui notas: {Model.matriculas[i].GetNota1()} e {Model.matriculas[i].GetNota2()}");
                        Console.WriteLine("Deseja alterar? (S/N)");
                        string resp = Console.ReadLine().ToUpper();
                        if (resp != "S")
                        {
                            Console.WriteLine("Notas mantidas.");
                            return;
                        }
                    }

                    Console.WriteLine("Digite a Nota 1:");
                    double nota1 = double.Parse(Console.ReadLine());

                    Console.WriteLine("Digite a Nota 2:");
                    double nota2 = double.Parse(Console.ReadLine());

                    // Substitui o objeto antigo por um novo com as notas atualizadas
                    Model.matriculas[i] = new Matricula(matriculaAluno, codigoDisciplina, nota1, nota2);
                    Console.WriteLine("Notas registradas com sucesso.");
                    break;
                }
            }

            if (!encontrou)
                Console.WriteLine("Aluno não está matriculado nessa disciplina.");
        }

        // Exibe todos os alunos cadastrados na lista
        public static void ConsultarAlunos()
        {
            if (Model.alunos.Count == 0)
            {
                Console.WriteLine("Nenhum aluno cadastrado.");
                return;
            }

            foreach (Aluno a in Model.alunos)
            {
                Console.WriteLine($"Matricula: {a.GetMatricula()}");
                Console.WriteLine($"Nome: {a.GetNome()}");
                Console.WriteLine($"Idade: {a.GetIdade()}");
                Console.WriteLine("-----------------------");
            }
        }

        // Exibe todas as disciplinas cadastradas na lista
        public static void ConsultarDisciplina()
        {
            if (Model.disciplinas.Count == 0)
            {
                Console.WriteLine("Nenhuma disciplina cadastrada.");
                return;
            }

            foreach (Disciplina d in Model.disciplinas)
            {
                Console.WriteLine($"Codigo: {d.GetCodigo()}");
                Console.WriteLine($"Nome: {d.GetNome()}");
                Console.WriteLine($"Nota mínima: {d.GetNotaMinima()}");
                Console.WriteLine("-----------------------");
            }
        }

        // Busca e exibe os alunos matriculados em uma disciplina específica
        public static bool BuscarAlunosDisciplinas(string busca)
        {
            int codigoDisciplina = -1;
            string nomeDisciplina = "";

            // Localiza a disciplina pelo código ou nome
            foreach (Disciplina d in Model.disciplinas)
            {
                if (d.GetCodigo().ToString() == busca || d.GetNome().ToLower() == busca.ToLower())
                {
                    codigoDisciplina = d.GetCodigo();
                    nomeDisciplina = d.GetNome();
                    break;
                }
            }

            // Retorna false se a disciplina não foi encontrada
            if (codigoDisciplina == -1) return false;

            Console.WriteLine("\nDisciplina: " + nomeDisciplina);

            // Percorre as matrículas e exibe o nome de cada aluno matriculado
            foreach (Matricula m in Model.matriculas)
            {
                if (m.GetCodigo() == codigoDisciplina)
                {
                    foreach (Aluno a in Model.alunos)
                    {
                        if (a.GetMatricula() == m.GetMatricula())
                        {
                            Console.WriteLine(a.GetNome());
                        }
                    }
                }
            }

            return true;
        }

        // Exibe as disciplinas e notas de um aluno específico
        public static void ConsultarDisciplinasAluno()
        {
            while (true)
            {
                Console.WriteLine("Digite o nome ou a matrícula do aluno\nPara voltar ao menu anterior digite 'Sair'");
                string busca = Console.ReadLine();

                if (busca.ToLower() == "sair") return;

                int matriculaAluno = -1;
                string nomeAluno = "";

                // Localiza o aluno pelo nome ou matrícula
                foreach (Aluno a in Model.alunos)
                {
                    if (a.GetMatricula().ToString() == busca || a.GetNome().ToLower() == busca.ToLower())
                    {
                        matriculaAluno = a.GetMatricula();
                        nomeAluno = a.GetNome();
                        break;
                    }
                }

                if (matriculaAluno == -1)
                {
                    Console.WriteLine("Aluno não existe. Tente novamente.");
                    continue;
                }

                Console.WriteLine($"\nAluno: {nomeAluno}");
                Console.WriteLine("Disciplina | Nota1 | Nota2 | Média | Situação");
                Console.WriteLine("---------------------------------------------");

                // Percorre as matrículas do aluno e exibe os dados de cada disciplina
                foreach (Matricula m in Model.matriculas)
                {
                    if (m.GetMatricula() == matriculaAluno)
                    {
                        int codigoDisc = m.GetCodigo();
                        double nota1 = m.GetNota1();
                        double nota2 = m.GetNota2();
                        double media = (nota1 + nota2) / 2;

                        string nomeDisc = "";
                        double notaMinima = 0;

                        // Busca o nome e a nota mínima da disciplina correspondente
                        foreach (Disciplina d in Model.disciplinas)
                        {
                            if (d.GetCodigo() == codigoDisc)
                            {
                                nomeDisc = d.GetNome();
                                notaMinima = d.GetNotaMinima();
                                break;
                            }
                        }

                        // Determina a situação do aluno com base na média e na nota mínima
                        string situacao = media >= notaMinima ? "APROVADO" : "REPROVADO";
                        Console.WriteLine($"{nomeDisc} | N1:{nota1} | N2:{nota2} | Média:{media} | {situacao}");
                    }
                }

                break;
            }
        }
    }
}