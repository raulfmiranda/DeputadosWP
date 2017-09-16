using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    class Comissao
    {

        public string IdeCadastroDeputado { get; set; }
        public string SiglaComissao { get; set; }
        public long Entrada { get; set; }
        public string Condicao { get; set; }
        public long Saida { get; set; }
        public string NomeComissao { get; set; }
        public int Orgao { get; set; }
        public string EntradaTxt { get; set; }
        public string SaidaTxt { get; set; }

    }
}
