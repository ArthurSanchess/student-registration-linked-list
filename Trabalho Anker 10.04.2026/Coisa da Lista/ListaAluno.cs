using Trabalho_Anker_10._04._2026.Classes;

namespace Trabalho_Anker_10._04._2026
{
    // Nó que armazena um Aluno e aponta para o próximo nó
    internal class NoAluno
    {
        private Aluno elemento;
        private NoAluno next;

        public NoAluno(Aluno a, NoAluno n)
        {
            elemento = a;
            next = n;
        }

        public Aluno getElemento() { return elemento; }
        public NoAluno getNext() { return next; }
        public void setElemento(Aluno a) { elemento = a; }
        public void setNext(NoAluno n) { next = n; }
    }

    // Lista encadeada simples de Alunos
    internal class ListaAluno
    {
        private NoAluno cabeca;  // Primeiro nó da lista
        private NoAluno cauda;   // Último nó da lista
        private int qtdElementos;

        // Cria a lista vazia, sem cabeça nem cauda
        public ListaAluno()
        {
            cabeca = null;
            cauda = null;
            qtdElementos = 0;
        }

        // Retorna a quantidade de elementos na lista
        public int Count { get { return qtdElementos; } }

        // Insere um novo aluno no final da lista
        public void Add(Aluno a)
        {
            NoAluno novo = new NoAluno(a, null);

            // Se a lista estiver vazia, cabeça e cauda apontam para o mesmo nó
            if (cabeca == null)
            {
                cabeca = novo;
                cauda = novo;
            }
            else
            {
                // O último nó passa a apontar para o novo, que vira a nova cauda
                cauda.setNext(novo);
                cauda = novo;
            }

            qtdElementos++;
        }

        // Acessa ou substitui o elemento em uma posição específica
        public Aluno this[int indice]
        {
            get
            {
                NoAluno atual = cabeca;
                for (int i = 0; i < indice; i++)
                    atual = atual.getNext();
                return atual.getElemento();
            }
            set
            {
                NoAluno atual = cabeca;
                for (int i = 0; i < indice; i++)
                    atual = atual.getNext();
                atual.setElemento(value);
            }
        }

        // Retorna o enumerador para permitir uso do foreach
        public EnumeradorAluno GetEnumerator()
        {
            return new EnumeradorAluno(cabeca);
        }
    }

    // Enumerador necessário para o foreach percorrer a lista
    internal class EnumeradorAluno
    {
        private NoAluno atual;
        private bool iniciou;

        public EnumeradorAluno(NoAluno cabeca)
        {
            atual = cabeca;
            iniciou = false;
        }

        public Aluno Current { get { return atual.getElemento(); } }

        public bool MoveNext()
        {
            if (!iniciou)
            {
                iniciou = true;
                return atual != null;
            }
            atual = atual.getNext();
            return atual != null;
        }
    }
}