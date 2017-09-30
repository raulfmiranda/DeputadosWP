using Deputados.WebserviceHelper;
using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    class GastoTipo
    {
        [JsonProperty("idDeputado")]
        public string IdDeputado { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("valor")]
        public double Valor { get; set; }
        [JsonProperty("media")]
        public double Media { get; set; }

        [Ignore][JsonIgnore]
        public string ValorFormat
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture, "{0:C}", this.Valor);
            }
        }

        [Ignore][JsonIgnore]
        public string MediaFormat
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture, "{0:C}", this.Media);
            }
        }


        public GastoTipo()
        {

        }

        private static void Incluir(GastoTipo objGastoTipo)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.RunInTransaction(() =>
                {                   
                    for (int i = 0; i <= 10; i++)
                    {
                        try
                        {
                            conexao.Insert(objGastoTipo);
                            break;
                        }
                        catch
                        {
                            Task.Delay(5000);
                            continue;
                        }
                    }
                });
            }
        }

        private static void IncluirLista(ObservableCollection<GastoTipo> gastos)
        {
            foreach (GastoTipo gasto in gastos)
            {
                Incluir(gasto);
            }

        }

        public static ObservableCollection<GastoTipo> ListarGastoTipoDeputado(string idDeputado)
        {
            if (WebServiceHelper.possuiConexaoInternet())
            {
                string jsonString = WebServiceHelper.GetTipoGastoDeputado(idDeputado);
                ObservableCollection < GastoTipo > gastos = JsonConvert.DeserializeObject<ObservableCollection<GastoTipo>>(jsonString);
                ObservableCollection<GastoTipo> gastosClone = JsonConvert.DeserializeObject<ObservableCollection<GastoTipo>>(jsonString);
                var t = Task.Run(() => {
                    ExcluirGastoTipoPorDeputado(idDeputado);
                    IncluirLista(gastos);
                });
                return gastos;
            }
            else
            {
                return ListarGastoTipoDeputadoBanco(idDeputado);
            }
        }

        private static ObservableCollection<GastoTipo> ListarGastoTipoDeputadoBanco(string idDeputado)
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<GastoTipo> gastosTipo = conexao.Query<GastoTipo>("select * from GastoTipo where idDeputado =" + "\"" + idDeputado + "\"").ToList<GastoTipo>();
                    ObservableCollection<GastoTipo> ListaGastoCnpjj = new ObservableCollection<GastoTipo>(gastosTipo);
                    return ListaGastoCnpjj;
                }
            }
            catch
            {
                return null;
            }
        }


        private static void ExcluirTodasGastoTipo()
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                for (int i = 0; i <= 10; i++)
                {
                    try
                    {
                        conexao.DropTable<GastoTipo>();
                        conexao.CreateTable<GastoTipo>();
                        conexao.Dispose();
                        conexao.Close();
                        break;
                    }
                    catch
                    {
                        Task.Delay(5000);
                        continue;
                    }
                }
            }
        }

        private static void ExcluirGastoTipoPorDeputado(string idDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                
                for (int i = 0; i <= 10; i++)
                {
                    try
                    {
                        conexao.Execute("delete from GastoTipo where idDeputado =" + "\"" + idDeputado + "\"");
                        break;
                    }
                    catch
                    {
                        Task.Delay(5000);
                        continue;
                    }
                }
            }

        }
    }
}
