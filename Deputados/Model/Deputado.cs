using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Deputados.Model
{
    
    class Deputado
    {
        
        public string Id { get; set; }       
        public string NomeParlamentar { get; set; }        
        public string NomeCompleto { get; set; }        
        public string Cargo { get; set; }        
        public string Partido { get; set; }        
        public string Mandato { get; set; }        
        public string Sexo { get; set; }        
        public string Uf { get; set; }        
        public string Telefone { get; set; }        
        public string Email { get; set; }        
        public string Nascimento { get; set; }        
        public string FotoURL { get; set; }        
        public double GastoTotal { get; set; }        
        public double GastoPorDia { get; set; }
        [Ignore]
        public List<Comissao> Comissoes { get; set; }
        [Ignore]
        public List<Projeto> Projetos { get; set; }
        [Ignore]
        public List<DeputadoFrenquencia> Frequencias { get; set; }
        [Ignore]
        public BitmapImage ImageFromUrl { get; set; }

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
}
