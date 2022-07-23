namespace tabuleiro
{
    public class Tabuleiro
    {
        // Todo tabuleiro tem linhas e colunas
        public int linha { get; set; }
        public int coluna { get; set; }
        // O xadrez tem uma matriz de peças
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linha = linhas;
            this.coluna = colunas;
            // As peças tera o número de linhas pelo número de colunas informadas na construtor
            this.pecas = new Peca[linhas, colunas];
        }
    }
}
