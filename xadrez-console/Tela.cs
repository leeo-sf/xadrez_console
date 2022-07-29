using System;
using xadrez;
using tabuleiro;
using System.Collections.Generic;


namespace xadrez_console
{
    class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            imprimirTabuleiro(partida.tab); // imprimindo o tabuleiro
            Console.WriteLine();
            imprimirPecasCapturadas(partida); // imprimindo peças que foram capturadas
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno); // imprimindo o turno da partida
            if (!partida.terminada) // se a partida não estiver terminada
            {
                Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual); // aguradando a jogada do jogador atual
                if (partida.xeque) // se o jogador estiver em xeque
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else // caso a partida estiver terminado
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.jogadorAtual); // jogador vencedor
            }
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida) // imprimindo as peças que foram capturadas
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca)); // imprimindo peças brancas
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta)); // imprimindo peças pretas
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto) // recebe um conjunto de peças capturadas
        {
            Console.Write("[");
            foreach (Peca x in conjunto) // para cada elemento nessas peças capturadas
            {
                Console.Write(x + " "); // imprima a peça e um espaço
            }
            Console.Write("]");
        }

        public static void imprimirTabuleiro(Tabuleiro tab) // recebendo como parametro a classe Tabuleiro (linhas, colunas)
        {
            for (int x = 0; x < tab.linhas; x++) // percorrendo as linhas da matriz
            {
                Console.Write(8 - x + " ");
                for (int y = 0; y < tab.colunas; y++) // percorrendo as colunas da matriz
                {
                    imprimirPeca(tab.peca(x, y)); // imprimindo uma peça na linha x na coluna y
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis) // recebendo como parametro a classe Tabuleiro (linhas, colunas)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            for (int x = 0; x < tab.linhas; x++) // percorrendo as linhas da matriz
            {
                Console.Write(8 - x + " ");
                for (int y = 0; y < tab.colunas; y++) // percorrendo as colunas da matriz
                {
                    if (posicoesPossiveis[x, y]) // para cada posição possível nas linhas e colunas
                    {
                        Console.BackgroundColor = fundoAlterado; // alterando o fundo de possições possíveis de outra cor para ajuda ao usuário
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(x, y)); // imprimindo uma peça na linha x na coluna y
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca == null) // se não tiver peça na linha e coluna
            {
                Console.Write("- ");
            }
            else // caso tenha
            {
                if (peca.cor == Cor.Branca) // se a peça for da cor branca
                {
                    Console.Write(peca); // imprima a peça
                }
                else // caso seja de outra cor
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca); // imprima de amarelo
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
