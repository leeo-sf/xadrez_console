namespace tabuleiro
{
    class Tabuleiro
    {
        // Todo tabuleiro tem linhas e colunas
        public int linhas { get; set; }
        public int colunas { get; set; }
        // O xadrez tem uma matriz de peças
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            // As peças tera o número de linhas pelo número de colunas informadas na construtor
            this.pecas = new Peca[linhas, colunas];
        }

        public Peca peca(int linha, int coluna) // Método que irá retornar uma peça
        {
            return this.pecas[linha, coluna];
        }

        public Peca peca(Posicao pos)
        {
            return this.pecas[pos.linha, pos.coluna];
        }

        public bool existePeca(Posicao pos) // Checa se existe a posição
        {
            this.validarPosicao(pos); // Caso de erro de validação de posição, irá levantar um erro
            return this.peca(pos) != null;
        }

        public void colocarPeca(Peca p, Posicao pos)
        {
            if (this.existePeca(pos)) // Se existir peça na posição
            {
                // Levante o erro
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            // Colocando a peca "p" na posição informada no parâmetro
            this.pecas[pos.linha, pos.coluna] = p;
            p.posicao = pos;
        }

        public Peca retirarPeca(Posicao pos)
        {
            if (this.peca(pos) == null)
            {
                return null;
            }
            Peca aux = this.peca(pos);
            aux.posicao = null;
            this.pecas[pos.linha, pos.coluna] = null;

            return aux;
        }

        public bool posicaoValida(Posicao pos)
        {
            if (pos.linha < 0 || pos.linha >= this.linhas || pos.coluna < 0 || pos.coluna >= this.colunas)
            {
                return false;
            }
            return true;
        }

        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos)) // Se a minha posicaoValida não for válida
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}
