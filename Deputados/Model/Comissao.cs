using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    class Comissao
    {
        public string ideCadastroDeputado { get; set; }
        public string siglaComissao { get; set; }
        public long entrada { get; set; }
        public string condicao { get; set; }
        public long saida { get; set; }
        public string nomeComissao { get; set; }
        public int orgao { get; set; }
        public string entradaTxt { get; set; }
        public string saidaTxt { get; set; }

    }
}
