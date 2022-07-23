namespace Tabuleiro
{
    public class Posicao
    {
        // Uma posição de tabuleiro tem linhas e colunas
        public int linha { get; set; }
        public int coluna { get; set; }

        public Posicao(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public override string ToString()
        {
            return this.linha + ", " + this.coluna;
        }
    }
}
