using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    public class GastoAnoTotal
    {
        public string IdDeputado { get; set; }
        public int Ano { get; set; }
        public string TipoGasto { get; set; }
        public object DescricaoGasto { get; set; }
        public string Valor { get; set; }

        public GastoAnoTotal()
        {

        }
    }
}
