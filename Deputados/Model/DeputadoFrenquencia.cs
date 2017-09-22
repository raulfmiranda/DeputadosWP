using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    public class DeputadoFrenquencia
    {
        public string IdParlamentar { get; set; }
        public int Ano { get; set; }
        public int PresencasDias { get; set; }
        public int PresencasSessoes { get; set; }
        public int AusenciasDias { get; set; }
        public int AusenciasSessoes { get; set; }
        public double IndicePresenca { get; set; }
    }
}
