using Trabalho_Anker_10._04._2026.Classes;

namespace Trabalho_Anker_10._04._2026
{
    // Nó que armazena uma Disciplina e aponta para o próximo nó
    internal class NoDisciplina
    {
        private Disciplina elemento;
        private NoDisciplina next;

        public NoDisciplina(Disciplina d, NoDisciplina n)
        {
            elemento = d;
            next = n;
        }

        public Disciplina getElemento() { return elemento; }
        public NoDisciplina getNext() { return next; }
        public void setElemento(Disciplina d) { elemento = d; }
        public void setNext(NoDisciplina n) { next = n; }
    }

    // Lista encadeada simples de Disciplinas
    internal class ListaDisciplina
    {
        private NoDisciplina cabeca;  // Primeiro nó da lista
        private NoDisciplina cauda;   // Último nó da lista
        private int qtdElementos;

        // Cria a lista vazia, sem cabeça nem cauda
        public ListaDisciplina()
        {
            cabeca = null;
            cauda = null;
            qtdElementos = 0;
        }

        // Retorna a quantidade de elementos na lista
        public int Count { get { return qtdElementos; } }

        // Insere uma nova disciplina no final da lista
        public void Add(Disciplina d)
        {
            NoDisciplina novo = new NoDisciplina(d, null);

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
        public Disciplina this[int indice]
        {
            get
            {
                NoDisciplina atual = cabeca;
                for (int i = 0; i < indice; i++)
                    atual = atual.getNext();
                return atual.getElemento();
            }
            set
            {
                NoDisciplina atual = cabeca;
                for (int i = 0; i < indice; i++)
                    atual = atual.getNext();
                atual.setElemento(value);
            }
        }

        // Retorna o enumerador para permitir uso do foreach
        public EnumeradorDisciplina GetEnumerator()
        {
            return new EnumeradorDisciplina(cabeca);
        }
    }

    // Enumerador necessário para o foreach percorrer a lista
    internal class EnumeradorDisciplina
    {
        private NoDisciplina atual;
        private bool iniciou;

        public EnumeradorDisciplina(NoDisciplina cabeca)
        {
            atual = cabeca;
            iniciou = false;
        }

        public Disciplina Current { get { return atual.getElemento(); } }

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