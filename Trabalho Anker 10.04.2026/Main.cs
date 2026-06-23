using System;
using System.IO;
using Trabalho_Anker_10._04._2026;
using Trabalho_Anker_10._04._2026.Classes;

class Program
{
    static void Main()
    {
        Model.Carregar();
        Console.WriteLine("============================\n1 - Consultas\n2 - Cadastros\n3 - Salvar\n4 - Sair\n============================");
       

        while (true)
        {
            int resposta = int.Parse(Console.ReadLine());

            switch (resposta)
            {
                case 1:

                    while (true)
                    {
                        Console.WriteLine("============================");
                        Console.WriteLine("1 - Alunos");
                        Console.WriteLine("2 - Disciplinas");
                        Console.WriteLine("3 - Alunos das Disciplinas");
                        Console.WriteLine("4 - Disciplinas do Aluno");
                        Console.WriteLine("5 - Voltar");
                        Console.WriteLine("============================");

                        int resposta_2 = int.Parse(Console.ReadLine());

                        if (resposta_2 == 1)
                        {
                            Controller.ConsultarAlunos();
                        }
                        else if (resposta_2 == 2)
                        {
                            Controller.ConsultarDisciplina();
                        }
                        else if (resposta_2 == 3)
                        {
                            Console.WriteLine("Digite o nome ou o código da disciplina\nPara voltar ao menu anterior digite 'Sair'");

                            while (true)
                            {
                                string busca = Console.ReadLine();

                                if (busca.ToLower() == "sair")
                                {
                                    break;
                                }

                                bool encontrou = Controller.BuscarAlunosDisciplinas(busca);
                                Model.MostrarResultadosDisciplina(busca);

                                if (encontrou)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Disciplina não encontrada, digite novamente ou 'sair'");
                                }
                            }
                        }
                        else if (resposta_2 == 4)
                        {
                            Controller.ConsultarDisciplinasAluno();
                        }
                        else if (resposta_2 == 5)
                        {
                            Console.WriteLine("============================\n1 - Consultas\n2 - Cadastros\n3 - Salvar\n4 - Sair\n============================");

                            break;
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida.");
                        }
                    }

                    break;

                case 2:

                    while (true)
                    {
                        Console.WriteLine("============================");
                        Console.WriteLine("1 - Alunos");
                        Console.WriteLine("2 - Disciplinas");
                        Console.WriteLine("3 - Matriculas");
                        Console.WriteLine("4 - Atribuir nota ao aluno");
                        Console.WriteLine("5 - Voltar");
                        Console.WriteLine("============================");

                        int resposta_3 = int.Parse(Console.ReadLine());

                        if (resposta_3 == 1)
                        {
                            Controller.CadastroAluno();
                        }
                        else if (resposta_3 == 2)
                        {
                            Controller.CadastroDisciplina();
                        }
                        else if (resposta_3 == 3)
                        {
                            Controller.CadastroMatricula();
                        }
                        else if (resposta_3 == 4)
                        {
                            Controller.AtribuirNota();
                        }
                        else if (resposta_3 == 5)
                        {
                            Console.WriteLine("============================\n1 - Consultas\n2 - Cadastros\n3 - Salvar\n4 - Sair\n============================");
                            ;
                            break;

                        }
                    }
                    break;


                case 3:
                    Model.Salvar();
                    Console.WriteLine("Salvo!");
                    Console.WriteLine("============================\n1 - Consultas\n2 - Cadastros\n3 - Salvar\n4 - Sair\n============================");
                    break;

                case 4:
                    Console.WriteLine("Fechando programa...");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Opção Inválida, tente novamente.");
                    break;
            }
        }
    }
}