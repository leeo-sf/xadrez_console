namespace tabuleiro
{
    abstract class Peca
    {
        // Toda peça tem uma posição
        public Posicao posicao { get; set; }
        // Toda peça tem uma cor {pode ser acessada e alterada por ela mesmo ou por subclasses}
        public Cor cor { get; protected set; }
        // Quantidade de vezes que a peça se movimentou
        public int qtdMovimentos { get; protected set; }
        // Toda peça está em um tabuleiro {pode ser acessada e alterada por ela mesmo ou por subclasses}
        public Tabuleiro tab { get; set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null;
            this.tab = tab;
            this.cor = cor;
            this.qtdMovimentos = 0;
        }

        public void incrementarQtdMovimentos()
        {
            qtdMovimentos++;
        }

        public void decrementarQtdMovimentos()
        {
            qtdMovimentos--;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for (int i=0; i<this.tab.linhas; i++)
            {
                for (int j=0; j<this.tab.colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool podeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }

        public abstract bool[,] movimentosPossiveis();
    }
}
