using tabuleiro;


namespace xadrez
{
    public class PosicaoXadrez
    {
        // um caractere de destino de 'a' a 'h'
        public char coluna { get; set; }
        // uma linha de destino de 1 a 8
        public int linha { get; set; }

        // quando instânciada recebe a posição
        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        public Posicao toPosicao() // tranforma a posição da matriz
        {
            return new Posicao(8 - this.linha, this.coluna - 'a');
        }

        public override string ToString()
        {
            return "" + this.coluna + this.linha;
        }
    }
}
