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
    public class Comissao
    {

        [JsonProperty("ideCadastroDeputado")]
        public string IdeCadastroDeputado { get; set; }
        [JsonProperty("siglaComissao")]
        public string SiglaComissao { get; set; }
        [JsonProperty("entrada")]
        public long Entrada { get; set; }
        [JsonProperty("condicao")]
        public string Condicao { get; set; }
        [JsonProperty("saida")]
        public long Saida { get; set; }
        [JsonProperty("nomeComissao")]
        public string NomeComissao { get; set; }
        [JsonProperty("orgao")]
        public int Orgao { get; set; }
        [JsonProperty("entradaTxt")]
        public string EntradaTxt { get; set; }
        [JsonProperty("saidaTxt")]
        public string SaidaTxt { get; set; }

        private static void Incluir(Comissao objComissao)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.RunInTransaction(() =>
                {
                    for (int i = 0; i <= 10; i++)
                    {
                        try
                        {
                            conexao.Insert(objComissao);
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

        private static void IncluirLista(ObservableCollection<Comissao> comissoes)
        {
            foreach (Comissao comissao in comissoes)
            {
                Incluir(comissao);
            }

        }

        public static ObservableCollection<Comissao> ListarComissaoDeputado(string idDeputado)
        {
            if (WebServiceHelper.possuiConexaoInternet())
            {
                string jsonString = WebServiceHelper.GetComissaoDeputado(idDeputado);
                ObservableCollection<Comissao> comissoes = JsonConvert.DeserializeObject<ObservableCollection<Comissao>>(jsonString);
                ObservableCollection<Comissao> comissoesClone = JsonConvert.DeserializeObject<ObservableCollection<Comissao>>(jsonString);
                var t = Task.Run(() => {
                    ExcluirCommisoesDeputado(idDeputado);
                    IncluirLista(comissoes);
                });
                
                return comissoes;
            }
            else
            {
                return ListarComissaoDeputadoBanco(idDeputado);
            }
        }


        private static ObservableCollection<Comissao> ListarComissaoDeputadoBanco(string idDeputado)
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<Comissao> comissoes = conexao.Query<Comissao>("select * from Comissao where IdeCadastroDeputado =" + "\"" +  idDeputado+ "\"").ToList<Comissao>();
                    ObservableCollection<Comissao> ListaComissoeDeputados = new ObservableCollection<Comissao>(comissoes);
                    return ListaComissoeDeputados;
                }
            }
            catch
            {
                return null;
            }
        }


        private static void ExcluirTodasComissoes()
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                for (int i = 0; i <= 10; i++)
                {
                    try
                    {
                        conexao.DropTable<Comissao>();
                        conexao.CreateTable<Comissao>();
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

        private static void ExcluirCommisoesDeputado(string idDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {                
                for (int i = 0; i <= 10; i++)
                {
                    try
                    {
                        conexao.Execute("delete from Comissao where IdeCadastroDeputado =" + "\"" + idDeputado + "\"");
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
