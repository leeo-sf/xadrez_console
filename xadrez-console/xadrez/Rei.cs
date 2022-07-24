using System;
using tabuleiro;


namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            // acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);

            if (this.tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);

            if (this.tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);

            if (this.tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // suldeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);

            if (this.tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);

            if (this.tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // sudo oeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);

            if (this.tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);

            if (this.tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // noro oeste
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);

            if (this.tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
