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
    }
}
