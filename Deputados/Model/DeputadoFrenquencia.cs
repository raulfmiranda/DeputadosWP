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
    public class DeputadoFrenquencia
    {
        [JsonProperty("idParlamentar")]
        public string IdParlamentar { get; set; }
        [JsonProperty("ano")]
        public int Ano { get; set; }
        [JsonProperty("presencasDias")]
        public int PresencasDias { get; set; }
        [JsonProperty("presencasSessoes")]
        public int PresencasSessoes { get; set; }
        [JsonProperty("ausenciasDias")]
        public int AusenciasDias { get; set; }
        [JsonProperty("ausenciasSessoes")]
        public int AusenciasSessoes { get; set; }
        [JsonProperty("indicePresenca")]
        public double IndicePresenca { get; set; }

        private static void Incluir(DeputadoFrenquencia objDeputadoFrenquencia)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.RunInTransaction(() =>
                {
                    for (int i = 0; i <= 10; i++)
                    {
                        try
                        {
                            conexao.Insert(objDeputadoFrenquencia);
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

        private static void IncluirLista(ObservableCollection<DeputadoFrenquencia> frequencias)
        {
            foreach (DeputadoFrenquencia frequencia in frequencias)
            {
                Incluir(frequencia);
            }

        }

        public static ObservableCollection<DeputadoFrenquencia> ListarFrequenciaDeputado(string idDeputado)
        {
            if (WebServiceHelper.possuiConexaoInternet())
            {
                string jsonString = WebServiceHelper.GetFrequenciaDeputado(idDeputado);
                ObservableCollection<DeputadoFrenquencia> frequencias = JsonConvert.DeserializeObject<ObservableCollection<DeputadoFrenquencia>>(jsonString);
                ObservableCollection<DeputadoFrenquencia> frequenciasClone = JsonConvert.DeserializeObject<ObservableCollection<DeputadoFrenquencia>>(jsonString);
                var t = Task.Run(() => {
                    ExcluirDeputadoFrenquencia(idDeputado);
                    IncluirLista(frequenciasClone);
                });
                
                return frequencias;
            }
            else
            {
                return ListarFrequenciaDeputadoBanco(idDeputado);
            }
        }


        private static ObservableCollection<DeputadoFrenquencia> ListarFrequenciaDeputadoBanco(string IdParlamentar)
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<DeputadoFrenquencia> frequencias = conexao.Query<DeputadoFrenquencia>("select * from DeputadoFrenquencia where IdParlamentar =" + "\"" +  IdParlamentar + "\"").ToList<DeputadoFrenquencia>();
                    ObservableCollection<DeputadoFrenquencia> ListaDeputadoFrenquencia = new ObservableCollection<DeputadoFrenquencia>(frequencias);
                    return ListaDeputadoFrenquencia;
                }
            }
            catch
            {
                return null;
            }
        }


        private static void ExcluirTodasDeputadoFrenquencias()
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                for (int i = 0; i <= 10; i++)
                {
                    try
                    {
                        conexao.DropTable<DeputadoFrenquencia>();
                        conexao.CreateTable<DeputadoFrenquencia>();
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

        private static void ExcluirDeputadoFrenquencia(string IdParlamentar)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                
                for (int i = 0; i <= 10; i++)
                {
                    try
                    {
                        conexao.Execute("delete from DeputadoFrenquencia where IdParlamentar =" + "\"" + IdParlamentar + "\"");
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
