using System;
using tabuleiro;


namespace xadrez_console
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab) // Recebendo como parametro a classe Tabuleiro (linhas, colunas)
        {

            for (int x = 0; x < tab.linhas; x++) // Percorrendo as linhas da matriz
            {
                for (int y = 0; y < tab.colunas; y++) // Percorrendo as colunas da matriz
                {
                    if (tab.peca(x, y) == null)  // Se a posição x na coluna y estiver vázio
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tab.peca(x, y) + " "); // Imprimindo uma peça na linha x na coluna y
                    }
                }
                Console.WriteLine();
            }

        }
    }
}
