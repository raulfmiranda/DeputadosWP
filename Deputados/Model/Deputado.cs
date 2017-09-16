using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    class Deputado
    {
        public string id { get; set; }
        public string nomeParlamentar { get; set; }
        public string nomeCompleto { get; set; }
        public string cargo { get; set; }
        public string partido { get; set; }
        public string mandato { get; set; }
        public string sexo { get; set; }
        public string uf { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public string nascimento { get; set; }
        public string fotoURL { get; set; }
        public double gastoTotal { get; set; }
        public double gastoPorDia { get; set; }
        public List<Comissao> comissoes { get; set; }
        public List<Projeto> projetos { get; set; }


    }
}
