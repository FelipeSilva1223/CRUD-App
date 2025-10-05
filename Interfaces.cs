namespace CRUD_App
{
    internal class Interfaces
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
            Console.Write($"[{mensagem}] ");
            Console.ForegroundColor = corAntiga;
        }
        public static string Linhas(int quantidade, char caractere = '=')
        {
            return new string(caractere, quantidade);
        }
        public static void Titulo(string texto, char caractere = '=')
        {
            Console.WriteLine(Linhas(texto.Length + 2, '='));
            Console.WriteLine(texto.ToUpper());
            Console.WriteLine(Linhas(texto.Length + 2, '='));
        }
    }

}
