namespace tabuleiro
{
    class Peca
    {
        // Toda peça tem uma posição
        public Posicao posicao { get; set; }
        // Toda peça tem uma cor {pode ser acessada e alterada por ela mesmo ou por subclasses}
        public Cor cor { get; protected set; }
        // Quantidade de vezes que a peça se movimentou
        public int qtdMovimentos { get; protected set; }
        // Toda peça está em um tabuleiro {pode ser acessada e alterada por ela mesmo ou por subclasses}
        public Tabuleiro tab { get; set; }

        public Peca(Posicao posicao, Tabuleiro tab, Cor cor)
        {
            this.posicao = posicao;
            this.tab = tab;
            this.cor = cor;
            this.qtdMovimentos = 0;
        }
    }
}
