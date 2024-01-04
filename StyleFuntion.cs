using System;

namespace MeuPrimeiroProjeto
{
    class SytleFuntions
    { 
        public void ColorFunction(string mensagem, ConsoleColor cor)
        {
            Console.BackgroundColor = cor;
            Console.Write(mensagem);
            Console.ResetColor();
        }
    }
}