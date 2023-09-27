using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using Biblioteca;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        /*
        try
        {
            DataTable dt = new DataTable();
            dt = DALBiblioteca.GetPessoas();

            //igual a procura na matriz
            foreach(DataRow row in dt.Rows)//dentro da linha
            {
                foreach(DataColumn column in dt.Columns)//dentro da coluna
                {
                    Console.WriteLine(column.ColumnName + ": " + row[column]);
                }
                Console.WriteLine();
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Erro: " + ex);
        }
        */

        try
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            pessoas = DALBiblioteca.GetPessoasList();

            foreach (Pessoa pessoa in pessoas)
            {
                Console.WriteLine("ID: {0}", pessoa.Id);
                Console.WriteLine("Nome: {0}", pessoa.Nome);
                Console.WriteLine();
            }

            DALBiblioteca.GetPessoaComParametro();

            DALBiblioteca.InserirPessoa();
            List<Pessoa> pessoas2 = new List<Pessoa>();
            pessoas2 = DALBiblioteca.GetPessoasList();

            foreach (Pessoa pessoa in pessoas2)
            {
                Console.WriteLine("ID: {0}", pessoa.Id);
                Console.WriteLine("Nome: {0}", pessoa.Nome);
            }

            DALBiblioteca.DeletarPessoa();
            List<Pessoa> pessoa3 = new List<Pessoa>();
            pessoa3 = DALBiblioteca.GetPessoasList();

            foreach (Pessoa pessoa in pessoa3)
            {
                Console.WriteLine("ID: {0}", pessoa.Id);
                Console.WriteLine("Nome: {0}", pessoa.Nome);
            }

            DALBiblioteca.AtualizarNomePessoa();
            List<Pessoa> pessoa4 = new List<Pessoa>();
            pessoa4 = DALBiblioteca.GetPessoasList();

            foreach (Pessoa pessoa in pessoa4)
            {
                Console.WriteLine("ID: {0}", pessoa.Id);
                Console.WriteLine("Nome: {0}", pessoa.Nome);
            }

        }
        catch(Exception ex)
        {
            Console.WriteLine("Erro: " + ex);
            throw;
        }
    }
}