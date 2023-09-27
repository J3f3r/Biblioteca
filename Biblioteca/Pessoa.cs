using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Pessoa(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }
    }
}
