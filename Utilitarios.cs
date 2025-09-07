using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_App
{
    public static class Utilitarios
    {
        public static void MensagemColorida(string mensagem, ConsoleColor cor)
        {
            var corAntiga = Console.ForegroundColor; // Salva a cor atual em uma variavel
            Console.ForegroundColor = cor; // seta a cor nova
            Console.WriteLine(mensagem);
            Console.ForegroundColor = corAntiga; // volta pra cor antiga
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
            while (true)
            {
                Console.Write(mensagem + ": ");
                string entrada = (Console.ReadLine() ?? "").Trim();
                if (!string.IsNullOrWhiteSpace(entrada))
                {
                    return entrada;
                }

                MensagemColorida("Entrada inválida. Tente novamente.", ConsoleColor.Yellow);
            }
        }
        public static int LerInt(string mensagem = "Digite um número")
        {
            while (true)
            {
                Console.Write(mensagem + ": ");
                string entrada = (Console.ReadLine() ?? "").Trim();
                if (int.TryParse(entrada, out int inteiroValidado))
                {
                    return inteiroValidado;
                }
                MensagemColorida("Entrada inválida. Tente novamente.", ConsoleColor.Yellow);
            }
        }
        public static string LerDecimal(string mensagem = "Digite um número")
        {
            var pt = new CultureInfo("pt-BR");
            while (true)
            {
                Console.WriteLine(mensagem + ": ");
                string entrada = (Console.ReadLine() ?? "").Trim();
                if (decimal.TryParse(entrada, out decimal decimalValidado))
                {
                    return decimalValidado.ToString("F2");
                }
                MensagemColorida("Entrada inválida. Tente novamente.", ConsoleColor.Yellow);
            }
        }
    }
}
