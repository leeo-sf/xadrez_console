using System;


namespace tabuleiro
{
    // exceção
    public class TabuleiroException : Exception
    {
        // recebe uma string de mensagem quando instânciada
        public TabuleiroException(string msg) : base(msg) { }
    }
}
