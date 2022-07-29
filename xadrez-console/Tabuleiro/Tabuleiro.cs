namespace tabuleiro
{
    class Tabuleiro
    {
        // todo tabuleiro tem linhas e colunas
        public int linhas { get; set; }
        public int colunas { get; set; }
        // o xadrez tem uma matriz de peças
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            // matriz de pecas terá o número de linhas pelo número de colunas
            this.pecas = new Peca[linhas, colunas];
        }

        public Peca peca(int linha, int coluna) // método que irá retornar uma peça
        {
            return this.pecas[linha, coluna]; // retorna a peça na matriz de peça através da linha e coluna
        }

        public Peca peca(Posicao pos)
        {
            return this.pecas[pos.linha, pos.coluna];
        }

        public bool existePeca(Posicao pos) // checa se existe uma peça na posição
        {
            this.validarPosicao(pos); // caso de erro de validação de posição, irá levantar um erro
            return this.peca(pos) != null; // caso contrário existe uma peça
        }

        public void colocarPeca(Peca p, Posicao pos) // coloca uma peça na linha e coluna informada
        {
            if (this.existePeca(pos)) // se existir uma peça na posição
            {
                // levante o erro
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            // colocando a peca "p" na linha e coluna informada
            this.pecas[pos.linha, pos.coluna] = p;
            p.posicao = pos;
        }

        public Peca retirarPeca(Posicao pos) // retira uma peça da linha e coluna
        {
            if (this.peca(pos) == null) // se não existir uma peça na posição
            {
                return null;
            }
            Peca aux = this.peca(pos); // pegando a posição da peça
            aux.posicao = null;
            this.pecas[pos.linha, pos.coluna] = null; // retirando a peça da posição na matriz

            return aux;
        }

        public bool posicaoValida(Posicao pos) // checa posição
        {
            // se a linha ou coluna for menor ou maior que 0
            if (pos.linha < 0 || pos.linha >= this.linhas || pos.coluna < 0 || pos.coluna >= this.colunas) 
            {
                return false;
            }
            return true;
        }

        public void validarPosicao(Posicao pos) // valida posição na linha e coluna
        {
            if (!posicaoValida(pos)) // se a posição não for válida (posicaoValida=false, então se torna true e entra no if)
            {
                throw new TabuleiroException("Posição inválida!"); // levantando exceção
            }
        }
    }
}
