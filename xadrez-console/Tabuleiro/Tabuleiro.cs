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
    }
}
