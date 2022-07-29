using System;
using tabuleiro;
using xadrez;


namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez(); // Instânciando uma partida de xadrez

                while (!partida.terminada) // Enquanto a partida de xadrez não tiver acabado (partida = false, com o not se torna true)
                {
                    try
                    {
                        Console.Clear(); // Limpando o terminal
                        Tela.imprimirPartida(partida); // Imprimindo a partida

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao(); // Lendo os dados que o usuário digitar
                        partida.validarPosicaoDeOrigem(origem);

                        bool[,] posicaoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                        Console.Clear(); // Limpando o terminal
                        Tela.imprimirTabuleiro(partida.tab, posicaoesPossiveis); // Imprimindo a partida

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao(); // Lendo os dados que o usuário digitar
                        partida.validarPosicaoDeDestino(origem, destino); //Validando a posição de destino

                        partida.realizaJogada(origem, destino); // Efetuando jogada
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message); // Imprima a messagem do erro
                        Console.Write("Enter to continue: ");
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.imprimirPartida(partida);

            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();

        }
    }
}
