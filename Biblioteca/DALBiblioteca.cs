using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;

namespace Biblioteca
{
    public class DALBiblioteca
    {
        //encontrar onde está o arquivo do banco de dados
        public static string Path = Directory.GetCurrentDirectory() + "\\biblioteca.db";
        // na pasta repos/source/Biblioteca/bin/debug/net6.0/bibliteca.db

        //criar uma propriedade do tipo sqliteconnection
        private static SQLiteConnection sqliteConnection;

        //metodo que realiza a conexão
        private static SQLiteConnection DbConnetion()
        {//tem q instanciar e usar o comando Data source como parametro
            sqliteConnection = new SQLiteConnection("Data Source = " + Path);
            sqliteConnection.Open();
            return sqliteConnection;
        }


        public static DataTable GetPessoas()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                using (var cmd = DbConnetion().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM pessoas";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnetion());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
                throw;
            }
        }

        public static List<Pessoa> GetPessoasList()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            try
            {
                using (var cmd = DbConnetion().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM pessoas";// reader é para select
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pessoa pessoa = new Pessoa(Convert.ToInt32(reader["id"]), reader["nome"].ToString());
                            pessoas.Add(pessoa);
                        }
                    }
                }
                return pessoas;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Erro SQL: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro geral: " + ex);
                throw;
            }
            return pessoas;
        }

        public static void InserirPessoa()
        {
            try
            {
                Console.WriteLine("Digite o id da pessoa a ser inserida: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite o nome da passoa a ser inserida: ");
                string nome = Console.ReadLine();

                using (var cmd = DbConnetion().CreateCommand())//conectar com banco de dados
                {
                    //Instrução SQL para inserir uma nova pessoa na tabela 
                    cmd.CommandText = "INSERT INTO pessoas (id, nome) VALUES (@id, @nome)";
                    //adicionar parâmetros personalizados
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.ExecuteNonQuery();// Executar a query SQL
                }
                Console.WriteLine("Dados inseridos com sucesso!");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro ao inserir pessoas: " + ex.Message);
                throw;
            }
        }
        public static void DeletarPessoa()
        {
            try
            { 
                // Solicitar o ID da paessoa a ser deletad
                Console.WriteLine("Digite o ID da pessoa a ser deletada (inteiro):");
                int id = int.Parse(Console.ReadLine());

                using (var cmd = DbConnetion().CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM pessoas WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    int linhasAfetadas = cmd.ExecuteNonQuery();// está sendo armazenado o comando de executar o SQl no int para verificar se as linhas forma afetadas 

                    if (linhasAfetadas > 0)//siginifica sucesso e algumas linhas foram afetadas
                    {
                        Console.WriteLine("Pessoa deletada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Nenhuma pessoa com ID especificado!");
                    }
                }
            }
            catch(Exception ex)// sempre especificar com SQL o exception, se for outra programa coloca ele
            {
                Console.WriteLine("Erro: " + ex);
            }
        }
        public static void AtualizarNomePessoa()
        {
            try
            {
                //Solicitar ao usuário o ID da pessoa a ser atualzida
                Console.WriteLine("Digite o ID da passoa a ser atualizada (inteiro):");
                int id = int.Parse(Console.ReadLine());

                // Solicitar ao usuário o novo nome
                Console.WriteLine("Digite o novo nome da pessoa: ");
                string novoNome = Console.ReadLine();

                using (var cmd = DbConnetion().CreateCommand())
                {
                    cmd.CommandText = "UPDATE pessoas SET nome = @novoNome WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@novoNome", novoNome);
                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if(linhasAfetadas > 0)
                    {
                        Console.WriteLine("Nome atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Nenhuma pessoa com o ID especificado!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
            }
        }
        public static void GetPessoaComParametro()
        {
            try
            {
                Console.WriteLine("Digite o ID da pessoa que você quer o nome: ");
                int id = int.Parse(Console.ReadLine());
                using(var cmd = DbConnetion().CreateCommand())
                {
                    cmd.CommandText = " SELECT * FROM pessoas WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    using(var reader = cmd.ExecuteReader())//reader só para select
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Id: " + reader["id"]);
                            Console.WriteLine("Nome: " + reader["nome"]);
                            Console.WriteLine("");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }
}
