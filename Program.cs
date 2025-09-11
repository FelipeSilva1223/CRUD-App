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
            int id = Utilitarios.LerInt("Qual o ID do produto?");
            string nome = Utilitarios.LerString("Qual o nome do produto?");
            decimal preco = Utilitarios.LerDecimal("Qual o valor?");
            int quantidade = Utilitarios.LerInt("Quantos quer adicionar?");

            Produto prdt = new Produto(id, nome, preco, quantidade);
            Produtos.Add(prdt);
        }

        public static void MostrarProdutos()
        {
            Console.Clear();
            Interfaces.Titulo("Lista de produtos");
            foreach (Produto produto in Produtos)
            {
                Console.WriteLine($"ID: {produto.Id}\nNome: {produto.Nome}\n");
            }
        }
        static void Main(String[] args)
        {
            bool continuar = true;
            do
            {
                CriarProduto();
                CriarProduto();
                CriarProduto();
                MostrarProdutos();
            } while (continuar);

        }
    }
}