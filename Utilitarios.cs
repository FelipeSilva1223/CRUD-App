using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_App
{
    public static class Utilitarios
    {
        public static void MensagemColorida(string mensagem, ConsoleColor cor)
        {
            var corAntiga = Console.ForegroundColor;
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ForegroundColor = corAntiga;
        }
        public static void Tag(string mensagem, ConsoleColor cor)
        {
            var corAntiga = Console.ForegroundColor;
            Console.ForegroundColor = cor;
            Console.Write($"[{mensagem}]");
            Console.ForegroundColor = corAntiga;
        }
        public static string LerString(string mensagem = "Digite algo")
        {
            Console.Write(mensagem + ": ");
            string mensagemValidada = (Console.ReadLine() ?? "").Trim();
            if (string.IsNullOrWhiteSpace(mensagemValidada))
            {
                MensagemColorida("Entrada inválida. Tente novamente.", ConsoleColor.Yellow);
                return LerString(mensagem);
            }
            return mensagemValidada;
        }
    }
}
