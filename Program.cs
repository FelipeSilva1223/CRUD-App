namespace CRUD_App
{
    class Produto
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public decimal Preco { get; set; }

        public int Estoque { get; set; }

        public Produto(int id, string nome, decimal preco)
        {
            this.Id = id;
            this.Nome = nome;
            this.Preco = preco;
            this.Estoque = 0;
        }
    }
    class Program
    {
        static List<Produto> Produtos = new();
        public static bool VerificarLista()
        {
            return Produtos.Count > 0;
        }
        public static void CadastrarProduto()
        {
            int id = Utilitarios.LerInt("Qual o n° de ID do produto?");
            string nome = Utilitarios.LerString("Qual o nome do produto?");
            decimal preco = Utilitarios.LerDecimal("Qual o valor?");

            Produto produto = new (id, nome, preco);
            Produtos.Add(produto);
        }
        public static void ListarProdutos()
        {
            Console.Clear();
            Interfaces.Titulo("Lista de produtos");
            foreach (Produto produto in Produtos)
            {
                Console.WriteLine($"ID: {produto.Id}\nNome: {produto.Nome}\nValor: R$: {produto.Preco}");
            }
        }
        public static void BuscarPorID() {
            int idEntrada = Utilitarios.LerInt("Qual o ID do produto?");
            Produto? resultado = Produtos.Find(produto => produto.Id == idEntrada);
            if (resultado == null)
            {
                Console.WriteLine("Produto não encontrado");
                return;
            }
            Console.WriteLine($"ID: {resultado.Id}\nNome: {resultado.Nome}\n");
        }
        public static void EditarNomeProduto()
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
        }
        public static void AdicionarProdutos()
        {

        }

        static void Main(String[] args)
        {
            bool continuar = true;
            do
            {
                CadastrarProduto();
                if (VerificarLista())
                {
                    ListarProdutos();
                } else
                {
                    Console.WriteLine("(Lista Vazia)");
                }
            } while (continuar);

        }
    }
}