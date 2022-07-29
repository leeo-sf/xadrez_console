namespace tabuleiro
{
    abstract class Peca
    {
        // toda peça tem uma posição
        public Posicao posicao { get; set; }
        // toda peça tem uma cor {a cor de uma peça não pode ser alterada, inicia a partida de uma cor e termina com determinada cor}
        public Cor cor { get; private set; }
        // quantidade de vezes que a peça se movimentou
        public int qtdMovimentos { get; protected set; }
        // toda peça está em um tabuleiro
        public Tabuleiro tab { get; set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null; // a posição se inicia nula
            this.tab = tab;
            this.cor = cor; 
            this.qtdMovimentos = 0; // e a peça não se mecheu ainda
        }

        public void incrementarQtdMovimentos() // incremento de atributo quando a peça se move
        {
            qtdMovimentos++;
        }

        public void decrementarQtdMovimentos() // decremento de atributo quando a peça não pode ser movida para aquela determinada posição
        {
            qtdMovimentos--;
        }

        public bool existeMovimentosPossiveis() // checa se a peça pode se mover
        {
            bool[,] mat = movimentosPossiveis(); // matriz que guarda movimentos possíveis que a peça pode fazer
            for (int i=0; i<this.tab.linhas; i++) // percorrendo as linhas
            {
                for (int j=0; j<this.tab.colunas; j++) // percorrendo as colunas
                {
                    if (mat[i, j]) // se a linha e a coluna estiver vazia
                    {
                        return true; // a peça pode mover para a posição
                    }
                }
            }
            return false;
        }

        public bool movimentoPossivel(Posicao pos) // checa se existe movimento na posição informada
        {
            return movimentosPossiveis()[pos.linha, pos.coluna]; // retorna true ou false para a determinada posição
        }

        public abstract bool[,] movimentosPossiveis();
    }
}
