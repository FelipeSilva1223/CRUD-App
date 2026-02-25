using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace CRUD_App
{
    internal class DataBase
    {
        private static readonly string pastaDb = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CRUD_App");
        private static readonly string PathDb = Path.Combine(pastaDb, "crud_app.db");
        private static readonly string connStr = $"Data Source={PathDb}";

        internal static void AbrirConexao()
        {
            Directory.CreateDirectory(pastaDb);

            using (var conexao = new SqliteConnection(connStr))
            {
                conexao.Open();

                using (var cmd = conexao.CreateCommand())
                {
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS produtos (" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "nome TEXT NOT NULL," +
                    "valor REAL NOT NULL," +
                    "estoque INTEGER NOT NULL);";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        internal static int CadastrarProduto(string nome, decimal valor)
        {
            using (var conexao = new SqliteConnection(connStr))
            {
                conexao.Open();

                using (var cmd = conexao.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO produtos(nome, valor, estoque) VALUES (@nome, @valor, @estoque)";
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@valor", valor);
                    cmd.Parameters.AddWithValue("@estoque", 0);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"SELECT last_insert_rowid();";

                    long id = (long)cmd.ExecuteScalar();

                    return (int)id;
                }
            }
        }
        internal static List<Produto> ListaProdutos()
        {
            using (var conexao = new SqliteConnection(connStr))
            {
                conexao.Open();

                var lista = new List<Produto>();

                using (var cmd = conexao.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, nome, valor, estoque FROM produtos;";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string nome = reader.GetString(1);
                            decimal valor = Convert.ToDecimal(reader.GetDouble(2));
                            int estoque = reader.GetInt32(3);

                            Produto item = new Produto(id, nome, valor, estoque);
                            lista.Add(item);
                        }
                    }
                    return lista;
                }
            }
        }
        internal static int ApagarProduto(int id)
        {
            using (var conexao = new SqliteConnection(connStr))
            {
                conexao.Open();

                using (var cmd = conexao.CreateCommand())
                {
                    cmd.CommandText = $@"DELETE FROM produtos WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas;
                }
            }
        }
        internal static int EditarNome(int id, string novoNome)
        {
            using (var conexao = new SqliteConnection(connStr))
            {
                conexao.Open();

                using (var cmd = conexao.CreateCommand())
                {
                    cmd.CommandText = "UPDATE produtos SET nome = @nome WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nome", novoNome);

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas;
                }
            }
        }
        internal static int AtualizarValor(int id, decimal novoValor)
        {
            using (var conexao = new SqliteConnection(connStr))
            {
                conexao.Open();

                using (var cmd = conexao.CreateCommand()){
                    cmd.CommandText = @"UPDATE produtos SET valor = @valor WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@valor", novoValor);

                    int resultado = cmd.ExecuteNonQuery();
                    return resultado;
                }
            }
        }
        internal static int AtualizarEstoque(int id, int novoValor)
        {
            using (var conexao = new SqliteConnection(connStr))
            {
                conexao.Open();

                using (var cmd = conexao.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE produtos SET estoque = @novoValor WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@novoValor", novoValor);

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas;
                }
            }
        }
    }
}
