namespace CRUD_App
{
    class Produto
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public decimal Valor { get; set; }

        public int Estoque { get; set; }

        public Produto(int id, string nome, decimal preco, int estoque)
        {
            this.Id = id;
            this.Nome = nome;
            this.Valor = preco;
            this.Estoque = estoque;
        }
    }
    class Program
    {
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
            string nome = Utilitarios.LerString("Qual o nome do produto?");
            decimal valor = Utilitarios.LerDecimal("Qual o valor?");
            Console.WriteLine($"Nome: {nome} - Preço: R$ {valor:F2}");
            if (Confirmar())
            {
                int id = DataBase.CadastrarProduto(nome, valor);
                Interfaces.MensagemColorida($"Produto \"{nome}\" (ID: {id} ) cadastrado com sucesso!", ConsoleColor.Green);
            } else
            {
                Console.WriteLine("Ação cancelada.");
            }
        }
        public static void ListarProdutos()
        {
            Console.Clear();
            List<Produto> produtos = DataBase.ListaProdutos();
            Interfaces.Titulo("Lista de produtos");
            if (produtos.Count > 0)
            {
                foreach (Produto item in produtos)
                {
                    Console.WriteLine($"ID: {item.Id}\nNome: {item.Nome}\nValor: R$: {item.Valor:F2}\nEstoque: {item.Estoque}\n");
                }
            }
            else
            {
                Console.WriteLine("(Lista Vazia)");
            }
        }
        public static Produto? SelecionarProduto()
        {
            while (true)
            {
                var produtos = DataBase.ListaProdutos();
                int idEntrada = Utilitarios.LerInt("Qual o ID do produto?");
                Produto? resultado = produtos.Find(produto => produto.Id == idEntrada);
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
            if(produto == null) { return; }
            Console.Clear();
            Console.WriteLine($"ID: {produto.Id}\nNome: {produto.Nome}\nPreço: {produto.Valor:F2}\nEstoque: {produto.Estoque}");
        }
        public static void EditarNome()
        {
            Produto? produto = SelecionarProduto();
            if(produto == null) { return; }
            string nomeAntigo = produto.Nome;
            string novoNome = Utilitarios.LerString("Qual o novo nome do produto?");
            if (Confirmar())
            {
                int resultado = DataBase.EditarNome(produto.Id, novoNome);
                if (resultado == 0)
                {
                    Interfaces.MensagemColorida("Falha em renomear produto", ConsoleColor.Red);
                } 
                else if (resultado == 1)
                {
                    Interfaces.MensagemColorida($"{nomeAntigo} atualizado para {novoNome}", ConsoleColor.Green);
                } 
                else 
                {
                    Interfaces.Tag("ERRO", ConsoleColor.Red);
                }
            } else
            {
                Interfaces.MensagemColorida("Ação cancelada", ConsoleColor.Yellow);
            } 
        }
        public static void AtualizarValor()
        {
            Produto? produto = SelecionarProduto(); 
            if(produto == null) { return; }
            while (true)
            {
                decimal novoValor = Utilitarios.LerDecimal("Qual o novo valor?");
                if (novoValor < 0)
                {
                    Interfaces.MensagemColorida("Valor não pode ser negativo", ConsoleColor.Yellow);
                    return;
                }
                else if (novoValor == produto.Valor)
                {
                    Interfaces.MensagemColorida("O valor precisa ser diferente do anterior.", ConsoleColor.Yellow);
                    return;
                }
                if (Confirmar())
                {
                    int resultado = DataBase.AtualizarValor(produto.Id, novoValor);
                    if (resultado == 1)
                    {
                        Interfaces.MensagemColorida($"Preço atualizado para: R$ {novoValor:F2}", ConsoleColor.Green);
                        return;
                    }
                    else if (resultado == 0)
                    {
                        Interfaces.MensagemColorida("Falha ao atualizar valor", ConsoleColor.Red);
                        return;
                    }
                    else
                    {
                        Interfaces.Tag("ERRO", ConsoleColor.Red);
                        return;
                    }
                } else
                {
                    Console.WriteLine("Ação cancelada.");
                    return;
                }
            }
        }
        public static void AtualizarEstoque()
        {
            Produto? produto = SelecionarProduto();
            if(produto == null) { return; }
            while (true)
            {
                Console.WriteLine($"Estoque atual de {produto.Nome}: {produto.Estoque}");
                int unidades = Utilitarios.LerInt("Quantas unidades deseja adicionar?");
                if (unidades > 0)
                {
                    if (Confirmar())
                    {
                        int resultado = DataBase.AtualizarEstoque(produto.Id, unidades);
                        if (resultado == 1)
                        {
                            Interfaces.MensagemColorida($"{unidades} unidade{Pluralizar(unidades)} adicionado{Pluralizar(unidades)} a {produto.Nome} com sucesso!", ConsoleColor.Green);
                            return;
                        }
                        else if (resultado == 0)
                        {
                            Interfaces.MensagemColorida("Erro ao atualziar estoque", ConsoleColor.Red);
                            return;
                        }
                        else
                        {
                            Interfaces.Tag("ERRO", ConsoleColor.Red);
                            return;
                        }
                    } else
                    {
                        Interfaces.MensagemColorida("Ação cancelada", ConsoleColor.Yellow);
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
        public static void ApagarProduto()
        {
            Produto? produto = SelecionarProduto();
            if(produto == null) { return; }
            string? nome = produto.Nome;
            if (Confirmar())
            {
                int resultado = DataBase.ApagarProduto(produto.Id);
                if (resultado == 1) {
                    Interfaces.MensagemColorida($"Produto \"{nome}\" foi apagado com sucesso.", ConsoleColor.Green);
                }
                else if (resultado == 0)
                {
                    Interfaces.MensagemColorida("Erro ao apagar produto", ConsoleColor.Red);
                }
                else
                {
                    Interfaces.Tag("ERRO", ConsoleColor.Red);
                }
            }
            else
            {
                Interfaces.MensagemColorida("Ação cancelada", ConsoleColor.Yellow);
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
            DataBase.AbrirConexao();
            do
            {
                Console.Clear();
                Interfaces.Titulo("LISTA DE PRODUTOS 1.0");
                Console.WriteLine("Cadastrar produto - 1");
                Console.WriteLine("Ver lista de produtos - 2");
                Console.WriteLine("Ver produto - 3");
                Console.WriteLine("Editar nome de um produto - 4");
                Console.WriteLine("Editar valor de um produto - 5");
                Console.WriteLine("Atualizar estoque - 6");
                Console.WriteLine("Apagar produto - 7");
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
                        AtualizarValor();
                        break;
                    case 6:
                        AtualizarEstoque();
                        break;
                    case 7:
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