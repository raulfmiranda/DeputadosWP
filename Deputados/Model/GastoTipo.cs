using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    class GastoTipo
    {
        public string IdDeputado { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public double Media { get; set; }

        public GastoTipo()
        {

        }
    }
}
