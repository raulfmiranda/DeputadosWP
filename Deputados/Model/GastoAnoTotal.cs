using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    public class GastoAnoTotal
    {
        public string IdDeputado { get; set; }
        public string Ano { get; set; }
        public string TipoGasto { get; set; }
        public object DescricaoGasto { get; set; }
        public double Valor { get; set; }

        public GastoAnoTotal()
        {

        }

        public static ObservableCollection<GastoAnoTotal> ListarGastosTotais(string idDeputado)
        {
            ObservableCollection<GastosAno> gastosAno = new ObservableCollection<GastosAno>();
            ObservableCollection<GastoAnoTotal> gastosTotais = new ObservableCollection<GastoAnoTotal>();

            for (int i = 2009; i < 2014; i++)
            {
                gastosAno = GastosAno.ListarGastosAnoDeputado(idDeputado, i.ToString());
                int index = -1;
                foreach(GastosAno gas in gastosAno)
                {
                    index = ContemTipoGastoAno(gastosTotais, gas.TipoGasto, i.ToString());
                    if (index >= 0)
                    {
                        gastosTotais[index].Valor += gas.Valor;
                    }
                    else
                    {
                        GastoAnoTotal gasTotal = new GastoAnoTotal();
                        gasTotal.Ano = i.ToString();
                        gasTotal.DescricaoGasto = gas.DescricaoGasto;
                        gasTotal.IdDeputado = gas.IdDeputado;
                        gasTotal.TipoGasto = gas.TipoGasto;
                        gasTotal.Valor = gas.Valor;
                        gastosTotais.Add(gasTotal);
                    }
                }
            }

            return gastosTotais;
        }

        private static int ContemTipoGastoAno(ObservableCollection<GastoAnoTotal> gastosTotais, string tipoGasto, string ano)
        {
            for(int i = 0; i < gastosTotais.Count; i++)
            {
                if (gastosTotais[i].TipoGasto.Equals(tipoGasto) && gastosTotais[i].Ano.Equals(ano))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
