using System.Globalization;

namespace CRUD_App
{
    public static class Utilitarios
    { 
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

                Interfaces.MensagemColorida("Entrada inválida. Tente novamente.", ConsoleColor.Yellow);
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
                Interfaces.MensagemColorida("Entrada inválida. Tente novamente.", ConsoleColor.Yellow);
            }
        }
        public static decimal LerDecimal(string mensagem = "Digite um número")
        {
            while (true)
            {
                Console.WriteLine(mensagem + ": ");
                string entrada = (Console.ReadLine() ?? "").Trim();
                if (decimal.TryParse(entrada, out decimal decimalValidado))
                {
                    return decimalValidado;
                }
                Interfaces.MensagemColorida("Entrada inválida. Tente novamente.", ConsoleColor.Yellow);
            }
        }

    }
}
