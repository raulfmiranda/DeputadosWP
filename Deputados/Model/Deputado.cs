using Deputados.WebserviceHelper;
using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using  System.Threading;

namespace Deputados.Model
{
    [DataContract]
    public class Deputado //: INotifyPropertyChanged
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("nomeParlamentar")]
        public string NomeParlamentar { get; set; }
        [JsonProperty("nomeCompleto")]
        public string NomeCompleto { get; set; }
        [JsonProperty("cargo")]
        public string Cargo { get; set; }
        [JsonProperty("partido")]
        public string Partido { get; set; }
        [JsonProperty("mandato")]
        public string Mandato { get; set; }
        [JsonProperty("sexo")]
        public string Sexo { get; set; }
        [JsonProperty("uf")]
        public string Uf { get; set; }
        [JsonProperty("telefone")]
        public string Telefone { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("nascimento")]
        public string Nascimento { get; set; }
        [JsonProperty("fotoURL")]
        public string FotoURL { get; set; }
        [JsonProperty("gastoTotal")]
        public double GastoTotal { get; set; }
        [JsonProperty("gastoPorDia")]
        public double GastoPorDia { get; set; }
        [Ignore]
        [JsonIgnore]
        public List<Comissao> Comissoes { get; set; }
        [Ignore]
        [JsonIgnore]
        public List<Projeto> Projetos { get; set; }
        [Ignore]
        [JsonIgnore]
        public List<DeputadoFrenquencia> Frequencias { get; set; }
        [Ignore]
        [JsonIgnore]
        public BitmapImage ImageFromUrl
        {
            get
            {
                return null;// new BitmapImage(new Uri(this.FotoURL, UriKind.Absolute));               
            }
        }

        public static ObservableCollection<Deputado> ListarTodosDeputados()
        {
            if (WebServiceHelper.possuiConexaoInternet())
            {
                string jsonString = WebServiceHelper.GetTodoDeputados();
                ObservableCollection<Deputado> deputados = JsonConvert.DeserializeObject<ObservableCollection<Deputado>>(jsonString);
                ObservableCollection<Deputado> deputadosClone = JsonConvert.DeserializeObject<ObservableCollection<Deputado>>(jsonString);

                var t = Task.Run(() =>
                {
                    ExcluirTodosOsDeputados();
                    IncluirListaDeputados(deputadosClone);
                });
                return deputados;
            }
            else
            {
                return ListarTodosDeputadoBanco();
            }
        }

        public static Deputado ListarDeputado(string idDeputado)
        {
            if (WebServiceHelper.possuiConexaoInternet())
            {
                string jsonString = WebServiceHelper.GetDeputado(idDeputado);
                Deputado deputado = JsonConvert.DeserializeObject<Deputado>(jsonString);
                Deputado deputadoclone = JsonConvert.DeserializeObject<Deputado>(jsonString);
                var t = Task.Run(() =>
                {
                    ExcluirDeputado(idDeputado);
                    Incluir(deputadoclone);
                });

                return deputado;

            }
            else
            {
                return ListarDeputadoBanco(idDeputado);
            }
        }



        public static ObservableCollection<Deputado> ListarDeputadoPorEstado(string uf)
        {
            if (WebServiceHelper.possuiConexaoInternet())
            {

                string jsonString = WebServiceHelper.GetDeputadosPorEstado(uf);
                ObservableCollection<Deputado> deputados = JsonConvert.DeserializeObject<ObservableCollection<Deputado>>(jsonString);
                ObservableCollection<Deputado> deputadosClone = JsonConvert.DeserializeObject<ObservableCollection<Deputado>>(jsonString);

                var t = Task.Run(() =>
                {
                    ExcluirDeputadoPorEstado(uf);
                    IncluirListaDeputados(deputadosClone);
                });

                return deputados;
            }
            else
            {
                return ListarDeputadosPorEstadoBanco(uf);
            }
        }



        private static void Incluir(Deputado objDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                 conexao.RunInTransaction(() =>
                {
                    for (int i = 0; i <= 10; i++)
                    {
                        try
                        {
                            conexao.Insert(objDeputado);
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

        private static void IncluirListaDeputados(ObservableCollection<Deputado> deputados)
        {
            foreach (Deputado deputado in deputados)
            {
                Incluir(deputado);
            }

        }


        private static Deputado ListarDeputadoBanco(string idDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var deputado = conexao.Query<Deputado>("select * from deputado where Id =" + "\"" + idDeputado + "\"").FirstOrDefault();
                return deputado;
            }
        }

        private static ObservableCollection<Deputado> ListarTodosDeputadoBanco()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {

                    List<Deputado> deputados = conexao.Table<Deputado>().ToList<Deputado>();
                    ObservableCollection<Deputado> ListaDeputados = new ObservableCollection<Deputado>(deputados);
                    return ListaDeputados;
                }
            }
            catch
            {
                return null;
            }

        }
        private static ObservableCollection<Deputado> ListarDeputadosPorEstadoBanco(string uf)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var deputados = conexao.Query<Deputado>("select * from deputado where Uf = " + "\"" + uf + "\"").ToList<Deputado>();
                ObservableCollection<Deputado> ListaDeputados = new ObservableCollection<Deputado>(deputados);
                return ListaDeputados;
            }

        }



        private static void ExcluirTodosOsDeputados()
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                for (int i = 0; i <= 10; i++)
                {
                    try
                    {
                        conexao.DropTable<Deputado>();
                        conexao.CreateTable<Deputado>();
                        conexao.Dispose();
                        conexao.Close();
                        break;
                    }
                    catch
                    {
                        Task.Delay(5000);
                        break;
                    }
                }
            }
        }

        private static void ExcluirDeputado(string idDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                for (int i = 0; i <= 10; i++)
                {
                    try
                    {                   
                        conexao.Execute("delete from Deputado where Id =" + "\"" + idDeputado +"\"" );
                        break;
                    }
                    catch
                    {
                        Task.Delay(5000);
                        break;
                    }

                }



            }

        }

        private static void ExcluirDeputadoPorEstado(string uf)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                for (int i=0; i<=10; i++)
                {

                    try
                    {
                       conexao.Execute("delete from Deputado where Uf =" + "\"" + uf + "\"");
                       break;
                    }
                    catch
                    {
                        Task.Delay(10000);
                        break;
                    }
              
                }
            }

        }

    }
}

