using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    public class Projeto
    {
        [JsonProperty("Nome")]
        public string Nome { get; set; }
        [JsonProperty("Ano")]
        public int Ano { get; set; }
        [JsonProperty("IdParlamentarAutor")]
        public string IdParlamentarAutor { get; set; }
        [JsonProperty("NomeAutor")]
        public string NomeAutor { get; set; }
        [JsonProperty("DataApresentacao")]
        public string DataApresentacao { get; set; }
        [JsonProperty("Sigla")]
        public string Sigla { get; set; }
        [JsonProperty("Ementa")]
        public string Ementa { get; set; }

        public Projeto()
        { }

        public static void Incluir(Projeto objProjeto)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.RunInTransaction(() =>
                {
                    conexao.Insert(objProjeto);
                });
            }
        }

        public static ObservableCollection<Projeto> ListarComissaoDeputado(string IdParlamentarAutor)
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<Projeto> projetos = conexao.Query<Projeto>("select * from Projeto where IdParlamentarAutor =" + IdParlamentarAutor).ToList<Projeto>();
                    ObservableCollection<Projeto> ListaProjetos = new ObservableCollection<Projeto>(projetos);
                    return ListaProjetos;
                }
            }
            catch
            {
                return null;
            }
        }


        public static void ExcluirTodasComissoes()
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.DropTable<Projeto>();
                conexao.CreateTable<Projeto>();
                conexao.Dispose();
                conexao.Close();
            }
        }

        public static void ExcluirCommisoesDeputado(string idDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.Execute("delete from Projeto where IdParlamentarAutor =" + idDeputado);
            }

        }
    }
}
