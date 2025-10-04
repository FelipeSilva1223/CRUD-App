namespace CRUD_App
{
    class Produto
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public decimal Preco { get; set; }

        public int Quantidade { get; set; }

        public Produto(int id, string nome, decimal preco, int quantidade)
        {
            this.Id = id;
            this.Nome = nome;
            this.Preco = preco;
            this.Quantidade = quantidade;
        }
    }
    class Program
    {
        static List<Produto> Produtos = new();
        public static void CriarProduto()
        {
            int id = Utilitarios.LerInt("Qual o n° de ID do produto?");
            string nome = Utilitarios.LerString("Qual o nome do produto?");
            decimal preco = Utilitarios.LerDecimal("Qual o valor?");
            int quantidade = Utilitarios.LerInt("Quantos quer adicionar?");

            Produto produto = new (id, nome, preco, quantidade);
            Produtos.Add(produto);
        }

        public static void ListarProdutos()
        {
            Console.Clear();
            Interfaces.Titulo("Lista de produtos");
            if (Produtos.Count > 0)
            {
                foreach (Produto produto in Produtos)
                {
                    Console.WriteLine($"ID: {produto.Id}\nNome: {produto.Nome}\n");
                }
            } else { Console.WriteLine("\n(Lista vazia)"); }
        }
        public static void BuscarPorID() {
            if (Produtos.Count > 0)
            {
                int idEntrada = Utilitarios.LerInt("Qual o ID do produto?");
                Produto? resultado = Produtos.Find(produto => produto.Id == idEntrada);
                if (resultado == null)
                {
                    Console.WriteLine("Produto não encontrado");
                    return;
                }
                Console.WriteLine(resultado.Nome);
            } else Console.WriteLine("\n(Lista vazia)");
        }
        public static void EditarNomeProduto()
        {
            if (Produtos.Count > 0)
            {
                int idEntrada = Utilitarios.LerInt("Qual o n° de ID do produto?");
                Produto? resultado = Produtos.Find(produto => produto.Id == idEntrada);
                if (resultado == null)
                {
                    Console.WriteLine("Produto não encontrado");
                    return;
                } else
                {
                    string novoNome = Utilitarios.LerString("Qual o novo nome do produto?");
                    resultado.Nome = novoNome;
                }
            } else
            {
                Console.WriteLine("(Lista vazia)");
            }
        }

        static void Main(String[] args)
        {
            bool continuar = true;
            do
            {
                CriarProduto();
                BuscarPorID();
                Thread.Sleep(2000);
            } while (continuar);

        }
    }
}