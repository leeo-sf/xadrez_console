using System;
using xadrez;
using tabuleiro;


namespace xadrez_console
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab) // Recebendo como parametro a classe Tabuleiro (linhas, colunas)
        {
            for (int x = 0; x < tab.linhas; x++) // Percorrendo as linhas da matriz
            {
                Console.Write(8 - x + " ");
                for (int y = 0; y < tab.colunas; y++) // Percorrendo as colunas da matriz
                {
                    if (tab.peca(x, y) == null)  // Se a posição x na coluna y estiver vázio
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirPeca(tab.peca(x, y)); // Imprimindo uma peça na linha x na coluna y
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
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
            if (peca.cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
