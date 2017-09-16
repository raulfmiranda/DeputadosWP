using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    class GastoCnpj
    {
        public string idDeputado { get; set; }
        public object cnpj { get; set; }
        public string descricao { get; set; }
        public object detalhe { get; set; }
        public double totalGasto { get; set; }

        public GastoCnpj()
        {

        }
    }
}
