using Deputados.WebserviceHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    class GastosAno
    {
        [JsonProperty("idDeputado")]
        public string IdDeputado { get; set; }
        [JsonProperty("cnpjCpf")]
        public string CnpjCpf { get; set; }
        [JsonProperty("tipoGasto")]
        public string TipoGasto { get; set; }
        [JsonProperty("sescricaoGasto")]
        public string DescricaoGasto { get; set; }
        [JsonProperty("sataEmissao")]
        public string DataEmissao { get; set; }
        [JsonProperty("valor")]
        public double Valor { get; set; }
        [JsonIgnore]
        public string Ano { get; set; }



        public GastosAno()
        { }
        private static void Incluir(GastosAno objGastosAno)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.RunInTransaction(() =>
                {                    
                    for (int i = 0; i <= 10; i++)
                    {
                        try
                        {
                            conexao.Insert(objGastosAno);
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

        private static void IncluirLista(ObservableCollection<GastosAno> gastos)
        {
            foreach (GastosAno gasto in gastos)
            {
                Incluir(gasto);
            }

        }

        public static ObservableCollection<GastosAno> ListarGastosAnoDeputado(string idDeputado, string ano)
        {
            if (WebServiceHelper.possuiConexaoInternet())
            {
                string jsonString = WebServiceHelper.GetGastoAnoDeputado(idDeputado, ano);
                ObservableCollection<GastosAno> gastos = JsonConvert.DeserializeObject<ObservableCollection<GastosAno>>(jsonString);
                ObservableCollection<GastosAno> gastosClone = JsonConvert.DeserializeObject<ObservableCollection<GastosAno>>(jsonString);
                var t = Task.Run(() => {
                    ExcluirGastoAnoPorDeputado(idDeputado, ano);
                    IncluirLista(gastos);
                });
                
                return gastos;
            }
            else
            {
                return ListarGastosAnoDeputadoBanco(idDeputado, ano);
            }
        }



        private static ObservableCollection<GastosAno> ListarGastosAnoDeputadoBanco(string idDeputado, string ano)
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<GastosAno> gastosAno = conexao.Query<GastosAno>(
                       String.Format("select * from GastosAno where idDeputado = {0} and Ano = {1}" , "\"" + idDeputado + "\"", "\"" + ano + "\"")).ToList<GastosAno>();
                    ObservableCollection<GastosAno> ListaGastoAno = new ObservableCollection<GastosAno>(gastosAno);
                    return ListaGastoAno;
                }
            }
            catch
            {
                return null;
            }
        }


        private static void ExcluirTodasGastoAno()
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                for (int i = 0; i <= 10; i++)
                {
                    try
                    {
                        conexao.DropTable<GastosAno>();
                        conexao.CreateTable<GastosAno>();
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

        private static void ExcluirGastoAnoPorDeputado(string idDeputado, string ano)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                
                for (int i = 0; i <= 10; i++)
                {
                    try
                    {                        
                        conexao.Execute(
                            String.Format("delete from GastosAno where idDeputado = {0} and Ano = {1}", "\"" + idDeputado + "\"", "\"" + ano + "\""));
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
