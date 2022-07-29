using tabuleiro;
using System.Collections.Generic;


namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; } // um tabuleiro
        public int turno { get; private set; } // o turno para saber em qual jogada está
        public Cor jogadorAtual { get; private set; } // o jogador (peça preta ou peça branca)
        public bool terminada { get; private set; } // a partida terminou ou não
        private HashSet<Peca> pecas; // coleção de peças
        private HashSet<Peca> capturadas; // coleção de peças que foram capturadas
        public bool xeque { get; private set; } // está em xeque ou não
        public Peca vulneravelEnPassant { get; private set; } // #jogadaEspecial

        public PartidaDeXadrez() // construtor sem argumentos
        {
            this.tab = new Tabuleiro(8, 8); // um tabuleiro de xadrez tem 8 linhas e 8 colunas
            this.turno = 1; // inicia do turno 1
            this.terminada = false; // a partida não está terminada (false = para não terminada)
            this.xeque = false; // uma peça não começa em xeque
            this.vulneravelEnPassant = null; // #jogadaEspecial
            this.jogadorAtual = Cor.Branca; // quem começa jogando é a cor branca
            this.pecas = new HashSet<Peca>(); // coleção de peças vazia
            this.capturadas = new HashSet<Peca>(); // coleção de peças capturadas vazia
            this.colocarPecas(); // já inicia colocando as peças no tabuleiro
        }

        public Peca executaMovimento(Posicao origem, Posicao destino) // método para mover a peça, com (linha e coluna de origem E linha e coluna de destino)
        {
            Peca p = tab.retirarPeca(origem); // retira a peça de onde estava
            p.incrementarQtdMovimentos(); // a peça se move 1 vez
            Peca pecaCapturada = tab.retirarPeca(destino); // capturando uma peça que está na posição de destino
            tab.colocarPeca(p, destino); // colocando a peça na posição de destino
            if (pecaCapturada != null) // se tiver uma peça na posição de destino
            {
                capturadas.Add(pecaCapturada); // adiciona a peça na coleção de peças capturadas
            }

            // jogadaEspecial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            // jogadaEspecial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            // #jogadaEspecial en passant
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) // desfazer o movimento de uma peça com a linha e coluna de origem, linha e coluna de destino
        {
            Peca p = tab.retirarPeca(destino); // retirando a peça da posição de destino
            p.decrementarQtdMovimentos(); // tirando 1 movimento daquela peça
            if (pecaCapturada != null) // se capturou uma peça
            {
                tab.colocarPeca(pecaCapturada, destino); // colocando a peça capturada na posição de destino da peça que estava se movendo
                capturadas.Remove(pecaCapturada); // removendo a peça capturada da coleção de peças capturadas
            }
            tab.colocarPeca(p, origem);

            // jogadaEspecial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                tab.colocarPeca(T, origemT);
            }

            // jogadaEspecial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                tab.colocarPeca(T, origemT);
            }

            // #jogadaEspecial en passant
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if(p.cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                    tab.colocarPeca(peao, posP);
                }
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino) // método realiza jogada com linha e coluna de origem e linha e coluna de destino
        {
            Peca pecaCapturada = executaMovimento(origem, destino); // executando o movimento de uma peça da origem para o destino
            Peca p = tab.peca(destino); // pegando a peça

            if (estaEmXeque(jogadorAtual)) // o jogador atual está em xeque
            {
                desfazMovimento(origem, destino, pecaCapturada); // desfaz o movimento da peça
                throw new TabuleiroException("Você não pode se colocar em xeque!"); // levando uma exceção
            }

            // #jogadaEspecial promocao
            if (p is Peao)
            {
                if ((p.cor == Cor.Branca && destino.linha == 0) || (p.cor == Cor.Preta && destino.linha == 7))
                {
                    p = tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.cor);
                    tab.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (estaEmXeque(adversaria(jogadorAtual))) // peça adversária está em xeque
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (testeXequemate(adversaria(jogadorAtual))) // peça adversária está em xequemate
            {
                terminada = true; // finaliza a partida
            }
            else // caso não esteja em xequemate
            {
                this.turno++; // incrementa 1 turno
                mudaJogador(); // muda o jogador
            }

            // #jogadaEspecial en passant
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }

        }

        public void validarPosicaoDeOrigem(Posicao pos) // validando posição
        {
            if (this.tab.peca(pos) == null) // se a peça retornada for nula
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!"); // levanta exceção
            }
            if (jogadorAtual != this.tab.peca(pos).cor) // se a peça retornada for diferente da cor da peça retornada
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!"); // levanta exceção
            }
            // (peão(1, 4) = false) irá entrar no if, pois a posição da peça não existe movimento
            if (!this.tab.peca(pos).existeMovimentosPossiveis()) // se a peça retornada na posição não existir movimento
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!"); // levanta exceção
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) // validando posição de destino pela linha e coluna de origem e linha e coluna de destino
        {
            if (!this.tab.peca(origem).movimentoPossivel(destino)) // se a peça retornada na origem não existir movimento
            {
                throw new TabuleiroException("Posição de destino inválida!"); // levanta exceção
            }
        }

        private void mudaJogador() // altera jogador
        {
            if (this.jogadorAtual == Cor.Branca) // se o jogador for branca
            {
                this.jogadorAtual = Cor.Preta; // se torna preta
            }
            else // caso contrário
            {
                this.jogadorAtual = Cor.Branca; // o jogador se torna branca
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) // retorna uma coleção de peças 
        {
            HashSet<Peca> aux = new HashSet<Peca>(); // variável para armazenar as peças capturadas
            foreach (Peca x in this.capturadas) // para cada elemento na coleção de capturadas
            {
                if (x.cor == cor) // se o elemento cor for igual a cor de parametro de entrada
                {
                    aux.Add(x); // adiciona na variável
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) // retorna uma coleção de peças
        {
            HashSet<Peca> aux = new HashSet<Peca>(); // variável para armazenar as peças em jogo
            foreach (Peca x in this.pecas) // para cada elemento na coleção de peças
            {
                if (x.cor == cor) // se o elemento cor for igual a cor de parametro de entrada
                {
                    aux.Add(x); // adiciona na variável
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor) // encontra adversário
        {
            if (cor == Cor.Branca) // se a cor de entrada for branca
            {
                return Cor.Preta; // o adversário é preta
            }
            else // caso contrário
            {
                return Cor.Branca; // o adversário é branca
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não há rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i=0; i<tab.linhas; i++)
                {
                    for (int j=0; j<tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) // colocando uma peça nova
        {
            this.tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao()); // colocando uma peça na coluna e linha informada
            pecas.Add(peca); // adicionando a peça na coleção de peças informadas
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
        }
    }
}
