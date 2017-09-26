using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    public class Projeto
    {
        public string Nome { get; set; }
        public int Ano { get; set; }
        public string IdParlamentarAutor { get; set; }
        public string NomeAutor { get; set; }
        public string DataApresentacao { get; set; }
        public string Sigla { get; set; }
        public string Ementa { get; set; }

        public Projeto()
        {

        }
    }
}
