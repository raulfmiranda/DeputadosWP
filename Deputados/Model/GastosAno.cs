using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    class GastosAno
    {
        public string idDeputado { get; set; }
        public string cnpjCpf { get; set; }
        public string tipoGasto { get; set; }
        public object descricaoGasto { get; set; }
        public object dataEmissao { get; set; }
        public double valor { get; set; }
    }
}
