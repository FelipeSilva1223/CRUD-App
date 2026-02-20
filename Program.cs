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
        public static bool TemProduto() => Produtos.Count > 0;
        static bool IdExiste(int id) => Produtos.Any(produto => produto.Id == id);
        static bool Confirmar()
        {
            while (true)
            {
                Interfaces.Tag("ATENÇÃO", ConsoleColor.Yellow);
                string opcao = Utilitarios.LerString("Tem certeza que deseja confirmar ação?(S/N)").ToUpper();
                if (opcao == "S")
                {
                    return true;
                }
                if (opcao == "N")
                {
                    return false;
                }
                Interfaces.MensagemColorida("Opção inválida.", ConsoleColor.Yellow);
            }
        }
        static bool Continuar()
        {
            while (true)
            {
                Interfaces.Tag("ATENÇÃO", ConsoleColor.Yellow);
                string opcao = Utilitarios.LerString("Deseja continuar?(S/N)").ToUpper();
                if (opcao == "S")
                {
                    return true;
                }
                if (opcao == "N")
                {
                    return false;
                }
                Interfaces.MensagemColorida("Opção inválida.", ConsoleColor.Yellow);
            }
        }
        public static void CadastrarProduto()
        {
            int id;
            while (true)
            {
                int entrada = Utilitarios.LerInt("Com qual n° de ID deseja cadastrar o produto?");
                if (IdExiste(entrada))
                {
                    Interfaces.MensagemColorida("ID já cadastrado", ConsoleColor.Yellow);
                    Continuar();
                } else
                {
                    id = entrada;
                    break;
                }
            }
            string nome = Utilitarios.LerString("Qual o nome do produto?");
            decimal preco = Utilitarios.LerDecimal("Qual o preço?");
            Console.WriteLine($"ID: {id} - Nome: {nome} - Preço: R$ {preco:F2}");
            if (Confirmar())
            {
                Produto produto = new(id, nome, preco);
                Produtos.Add(produto);
                Interfaces.MensagemColorida($"Produto \"{produto.Nome}\" cadastrado com sucesso!", ConsoleColor.Green);
            } else
            {
                Console.WriteLine("Ação cancelada.");
            }
        }
        public static void ListarProdutos()
        {
            Console.Clear();
            Interfaces.Titulo("Lista de produtos");
            if (TemProduto())
            {
                foreach (Produto produto in Produtos)
                {
                    Console.WriteLine($"ID: {produto.Id}\nNome: {produto.Nome}\nValor: R$: {produto.Preco:F2}\nEstoque: {produto.Estoque}\n");
                }
            }
            else
            {
                Console.WriteLine("(Lista Vazia)");
            }
        }
        public static Produto SelecionarProduto()
        {
            while (true)
            {
                int idEntrada = Utilitarios.LerInt("Qual o ID do produto?");
                Produto? resultado = Produtos.Find(produto => produto.Id == idEntrada);
                if (resultado != null)
                {
                    return resultado;
                }
                Interfaces.MensagemColorida("Produto não encontrado!", ConsoleColor.Yellow);
                int opcao = Utilitarios.LerInt("Pressione 0 para sair ou 1 para continuar");
                if (opcao == 0)
                {
                    return null;
                }
                else if (opcao == 1)
                {
                    continue;
                }
                else
                {
                    Interfaces.MensagemColorida("Opção inválida!", ConsoleColor.Yellow);
                }
            }
        }
        public static void MostrarPorID() {
            Produto produto = SelecionarProduto();
            Console.Clear();
            Console.WriteLine($"ID: {produto.Id}\nNome: {produto.Nome}\nPreço: {produto.Preco:F2}\nEstoque: {produto.Estoque}");
        }
        public static void EditarNome()
        {
            Produto produto = SelecionarProduto();
            if(produto == null) { return; }
            string novoNome = Utilitarios.LerString("Qual o novo nome do produto?");
            if (Confirmar())
            {
                produto.Nome = novoNome;
                Interfaces.MensagemColorida($"Nome atualizado para {novoNome}", ConsoleColor.Green);
            } else
            {
                Console.WriteLine("Ação cancelada.");
            } 
        }
        public static void EditarPreco()
        {
            Produto produto = SelecionarProduto(); 
            if(produto == null) { return; }
            while (true)
            {
                decimal preco = Utilitarios.LerDecimal("Qual o novo preco?");
                if (preco < 0)
                {
                    Interfaces.MensagemColorida("Preço não pode ser negativo", ConsoleColor.Yellow);
                    return;
                }
                if (preco == produto.Preco)
                {
                    Interfaces.MensagemColorida("O preço precisa ser diferente do anterior.", ConsoleColor.Yellow);
                    return;
                }
                if (Confirmar())
                {
                    produto.Preco = preco;
                    Interfaces.MensagemColorida($"Preço atualizado para: R$ {preco:F2}", ConsoleColor.Green);
                    return;
                } else
                {
                    Console.WriteLine("Ação cancelada.");
                    return;
                }
            }
        }
        public static void AdicionarEstoque()
        {
            Produto produto = SelecionarProduto();
            while (true)
            {
                Console.WriteLine($"Estoque atual de {produto.Nome}: {produto.Estoque}");
                int unidades = Utilitarios.LerInt("Quantas unidades deseja adicionar?");
                if (unidades > 0)
                {
                    if (Confirmar())
                    {
                        produto.Estoque += unidades;
                        Interfaces.MensagemColorida($"{unidades} unidade{Pluralizar(unidades)} adicionado{Pluralizar(unidades)} a {produto.Nome} com sucesso!", ConsoleColor.Green);
                        return;
                    } else
                    {
                        Console.WriteLine("Ação cancelada");
                        return;
                    }
                }
                else
                {
                    Interfaces.MensagemColorida("Necessário adicionar pelo menos uma unidade.", ConsoleColor.Yellow);
                    Continuar();
                }
            }
        }
        public static void RetirarEstoque() 
        {
            Produto produto = SelecionarProduto();
            if(produto == null) { return; }
            if (produto.Estoque == 0)
            {
                Console.WriteLine("(Estoque zerado)");
                return;
            }
            int unidades = Utilitarios.LerInt("Quantas unidades deseja retirar?");
            if (unidades > produto.Estoque)
            {
                Interfaces.MensagemColorida("Estoque insuficiente", ConsoleColor.Yellow);
                return;
            }
            if (unidades < 1)
            {
                Interfaces.MensagemColorida("Retire pelo menos 1 unidade", ConsoleColor.Yellow);
                return;
            }
            if (Confirmar())
            {
                produto.Estoque -= unidades;
                Interfaces.MensagemColorida($"{unidades} unidade{Pluralizar(unidades)} retirado{Pluralizar(unidades)} de {produto.Nome}", ConsoleColor.Green);
            } else
            {
                Console.WriteLine("Ação cancelada.");
            }
        }
        public static void ApagarProduto()
        {
            Produto produto = SelecionarProduto();
            if(produto == null) { return; }
            string? nome = produto.Nome;
            if (Confirmar())
            {
                Produtos.Remove(produto);
                Interfaces.MensagemColorida($"Produto \"{nome}\" foi apagado com sucesso.", ConsoleColor.Green);
            }
            else
            {
                Console.WriteLine("Ação cancelada.");
            }
        }
        public static string Pluralizar(int quantidade)
        {
            if (quantidade == 1)
            {
                return "";
            }
            else
            {
                return "s";
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
                Console.WriteLine("Editar preço de um produto - 5");
                Console.WriteLine("Adicionar estoque - 6");
                Console.WriteLine("Retirar estoque - 7");
                Console.WriteLine("Apagar produto - 8");
                Console.WriteLine("Sair - 0");
                Console.WriteLine(Interfaces.Linhas(23, '='));
                int opcao = Utilitarios.LerInt("O que deseja fazer?");
                switch (opcao)
                {
                    case 1:
                        CadastrarProduto();
                        break;
                    case 2:
                        ListarProdutos();
                        break;
                    case 3:
                        MostrarPorID();
                        break;
                    case 4:
                        EditarNome();
                        break;
                    case 5:
                        EditarPreco();
                        break;
                    case 6:
                        AdicionarEstoque();
                        break;
                    case 7:
                        RetirarEstoque();
                        break;
                    case 8:
                        ApagarProduto();
                            break;
                    case 0:
                        continuar = false;
                        Console.WriteLine("Obrigado por usar nosso sistema");
                        Thread.Sleep(2000);
                        break;
                    default:
                        Interfaces.MensagemColorida("Opção inválida.", ConsoleColor.Yellow);
                        break;
                }
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu inicial"); // So to aqui por causa do console, na interface n vai ter
                Console.ReadKey();
            } while (continuar);

        }
    }
}