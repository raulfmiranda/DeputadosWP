using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    public class DeputadoFrenquencia
    {
        public string idParlamentar { get; set; }
        public int ano { get; set; }
        public int presencasDias { get; set; }
        public int presencasSessoes { get; set; }
        public int ausenciasDias { get; set; }
        public int ausenciasSessoes { get; set; }
        public double indicePresenca { get; set; }
    }
}
