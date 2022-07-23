using tabuleiro;


namespace xadrez
{
    public class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        // Tranforma a posição da matriz
        public Posicao toPosicao()
        {
            return new Posicao(8 - this.linha, this.coluna - 'a');
        }

        public override string ToString()
        {
            return "" + this.coluna + this.linha;
        }
    }
}
