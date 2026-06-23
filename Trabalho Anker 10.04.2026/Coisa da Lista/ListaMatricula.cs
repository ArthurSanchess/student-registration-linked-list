using Trabalho_Anker_10._04._2026.Classes;

namespace Trabalho_Anker_10._04._2026
{
    // Nó que armazena uma Matricula e aponta para o próximo nó
    internal class NoMatricula
    {
        private Matricula elemento;
        private NoMatricula next;

        public NoMatricula(Matricula m, NoMatricula n)
        {
            elemento = m;
            next = n;
        }

        public Matricula getElemento() { return elemento; }
        public NoMatricula getNext() { return next; }
        public void setElemento(Matricula m) { elemento = m; }
        public void setNext(NoMatricula n) { next = n; }
    }

    // Lista encadeada simples de Matriculas
    internal class ListaMatricula
    {
        private NoMatricula cabeca;  // Primeiro nó da lista
        private NoMatricula cauda;   // Último nó da lista
        private int qtdElementos;

        // Cria a lista vazia, sem cabeça nem cauda
        public ListaMatricula()
        {
            cabeca = null;
            cauda = null;
            qtdElementos = 0;
        }

        // Retorna a quantidade de elementos na lista
        public int Count { get { return qtdElementos; } }

        // Insere uma nova matrícula no final da lista
        public void Add(Matricula m)
        {
            NoMatricula novo = new NoMatricula(m, null);

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
        public Matricula this[int indice]
        {
            get
            {
                NoMatricula atual = cabeca;
                for (int i = 0; i < indice; i++)
                    atual = atual.getNext();
                return atual.getElemento();
            }
            set
            {
                NoMatricula atual = cabeca;
                for (int i = 0; i < indice; i++)
                    atual = atual.getNext();
                atual.setElemento(value);
            }
        }

        // Retorna o enumerador para permitir uso do foreach
        public EnumeradorMatricula GetEnumerator()
        {
            return new EnumeradorMatricula(cabeca);
        }
    }

    // Enumerador necessário para o foreach percorrer a lista
    internal class EnumeradorMatricula
    {
        private NoMatricula atual;
        private bool iniciou;

        public EnumeradorMatricula(NoMatricula cabeca)
        {
            atual = cabeca;
            iniciou = false;
        }

        public Matricula Current { get { return atual.getElemento(); } }

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