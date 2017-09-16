using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    class Projeto
    {
        public string nome { get; set; }
        public int ano { get; set; }
        public string idParlamentarAutor { get; set; }
        public string nomeAutor { get; set; }
        public string dataApresentacao { get; set; }
        public string sigla { get; set; }
        public string ementa { get; set; }
    }
}
