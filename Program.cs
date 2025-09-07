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
        static void Main(String[] args)
        {
            bool continuar = true;
            do
            {
                string mensagem = Utilitarios.LerDecimal();
                Console.WriteLine(mensagem);
            } while (continuar);

        }
    }
}