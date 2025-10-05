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
        public static bool TemProduto()
        {
            return Produtos.Count > 0;
        }
        public static void CadastrarProduto()
        {
            int id = Utilitarios.LerInt("Com qual n° de ID deseja cadastrar o produto?");
            string nome = Utilitarios.LerString("Qual o nome do produto?");
            decimal preco = Utilitarios.LerDecimal("Qual o valor?");

            Produto produto = new (id, nome, preco);
            Produtos.Add(produto);
            Interfaces.MensagemColorida($"Produto {produto.Nome} cadastrado com sucesso!", ConsoleColor.Green);
        }
        public static void ListarProdutos()
        {
            Console.Clear();
            Interfaces.Titulo("Lista de produtos");
            if (TemProduto())
            {
                foreach (Produto produto in Produtos)
                {
                    Console.WriteLine($"ID: {produto.Id}\nNome: {produto.Nome}\nValor: R$: {produto.Preco}\nEstoque: {produto.Estoque}\n");
                }
            }
            else
            {
                Console.WriteLine("(Lista Vazia)");
            }
            Console.WriteLine("Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }
        static bool IdExiste(int id)
        {
            Produto? resultado = Produtos.Find(produto => produto.Id == id);
            if (resultado != null)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public static Produto ProdutoAtual()
        {
            while (true)
            {
                int entrada = Utilitarios.LerInt("Qual o ID do produto?");
                if (IdExiste(entrada))
                {
                    Produto resultado = Produtos.Find(produto => produto.Id == entrada);
                    return resultado;
                }
                Interfaces.MensagemColorida("ID não encontrado", ConsoleColor.Yellow);
            }
        }
        public static void MostrarPorID(Produto produto) {
            Console.Clear();
            Console.WriteLine($"ID: {produto.Id}\nNome: {produto.Nome}\nPreço: {produto.Preco}\nEstoque: {produto.Estoque}");
            Console.ReadKey();
        }
        public static void EditarNomeProduto(Produto produto)
        {
            string novoNome = Utilitarios.LerString("Qual o novo nome do produto?");
            produto.Nome = novoNome;
            Interfaces.MensagemColorida($"Nome atualizado para {novoNome}", ConsoleColor.Green);
        }
        public static void AdicionarEstoque(Produto produto)
        {
            while (true)
            {
                Console.WriteLine($"Estoque atual: {produto.Estoque} de {produto.Nome}");
                int unidades = Utilitarios.LerInt("Quantas unidades deseja adicionar?");
                if (unidades > 0)
                {
                    produto.Estoque += unidades;
                    Interfaces.MensagemColorida($"{unidades} unidade{(unidades > 1 ? "s": "")} adicionado{(unidades > 1 ? "s":"")} com sucesso!", ConsoleColor.Green);
                    return;
                }
                else
                {
                    Interfaces.MensagemColorida("Necessário ao menos uma unidade.", ConsoleColor.Yellow);
                }
            }
        }
        public static void RetirarEstoque(Produto produto)
        {
            Console.WriteLine($"Estoque atual do produto: {produto.Estoque} de {produto.Nome}");
            int unidades = Utilitarios.LerInt("Quantas unidades deseja retirar?");
            if (produto.Estoque > 0 && unidades <= produto.Estoque)
            {
                produto.Estoque -= unidades;
                Interfaces.MensagemColorida($"{unidades} unidade{(unidades > 1 ? "s":"")} retirado{(unidades > 1 ? "s":"")}", ConsoleColor.Green);
            } else
            {
                Interfaces.MensagemColorida("Estoque insuficiente", ConsoleColor.Yellow);
            }
        }

        static void Main(String[] args)
        {
            bool continuar = true;
            do
            {
                Console.Clear();
                Interfaces.Titulo("LISTA DE PRODUTOS 1.0");
                Console.WriteLine("Cadastrar produto - 1");
                Console.WriteLine("Ver lista de produtos - 2");
                Console.WriteLine("Ver produto - 3");
                Console.WriteLine("Editar nome de um produto - 4");
                Console.WriteLine("Adicionar estoque - 5");
                Console.WriteLine("Retirar estoque - 6");
                Console.WriteLine("Sair - 0");
                Console.WriteLine(Interfaces.Linhas(23, '='));
                int opicao = Utilitarios.LerInt("O que deseja fazer?");
                switch (opicao)
                {
                    case 1:
                        CadastrarProduto();
                        break;
                    case 2:
                        ListarProdutos();
                        break;
                    case 3:
                        MostrarPorID(ProdutoAtual());
                        break;
                    case 4:
                        EditarNomeProduto(ProdutoAtual());
                        break;
                    case 5:
                        AdicionarEstoque(ProdutoAtual());
                        break;
                    case 6:
                        RetirarEstoque(ProdutoAtual());
                        break;
                    case 0:
                        continuar = false;
                        Console.WriteLine("Obrigado por usar nosso sistema");
                        Thread.Sleep(2000);
                        break;
                }
            } while (continuar);

        }
    }
}