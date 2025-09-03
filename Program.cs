namespace CRUD_App
{
    class Interfaces
    {


        public static int ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("=== GERENCIADOR DE TAREFAS ===");
            Console.WriteLine("1 - Adicionar tarefa");
            Console.WriteLine("2 - Listar tarefas");
            Console.WriteLine("3 - Editar tarefa");
            Console.WriteLine("4 - Excluir tarefa");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            string entrada = Utilitarios.LerString();

            return int.TryParse(entrada, out int opcao) ? opcao : -1;

        }
    }
    class Program
    {


        static void Main(String[] args)
        {
            bool continuar = true;
            do
            {
                string mensagem = Utilitarios.LerString();
                Console.WriteLine(mensagem);
            } while (continuar);

        }
    }
}