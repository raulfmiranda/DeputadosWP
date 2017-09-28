using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Deputados.Model
{
    [DataContract]
    public class Deputado
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
        [Ignore][JsonIgnore]
        public List<Comissao> Comissoes { get; set; }
        [Ignore][JsonIgnore]
        public List<Projeto> Projetos { get; set; }
        [Ignore][JsonIgnore]
        public List<DeputadoFrenquencia> Frequencias { get; set; }
        [Ignore]
        [JsonIgnore]
        public BitmapImage ImageFromUrl { get { return new BitmapImage(new Uri(this.FotoURL, UriKind.Absolute)); } }

        public Deputado()
        {

        }

        public static void Incluir(Deputado objDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.RunInTransaction(() =>
                {
                    conexao.Insert(objDeputado);
                });
            }
        }

        public static Deputado ListarDeputado(string idDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var deputado = conexao.Query<Deputado>("select * from Deputado where Id =" + idDeputado).FirstOrDefault();
                return deputado;
            }
        }

        public static ObservableCollection<Deputado> ListarTodosDeputado()
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

        public static void ExcluirTodosOsDeputados()
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.DropTable<Deputado>();
                conexao.CreateTable<Deputado>();
                conexao.Dispose();
                conexao.Close();
            }
        }

        public static void ExcluirDeputado(string idDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var deputado = conexao.Query<Deputado>("select * from Deputado where Id =" + idDeputado).FirstOrDefault();
                if (deputado != null)
                {
                    conexao.RunInTransaction(() =>
                    {
                        conexao.Delete(deputado);
                    });
                }
            }

        }




    }

    public class RootObject
    {
        public List<Deputado> Deputados { get; set; }
    }

}
