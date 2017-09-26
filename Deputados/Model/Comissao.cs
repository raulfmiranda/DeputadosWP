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

        public string IdeCadastroDeputado { get; set; }
        public string SiglaComissao { get; set; }
        public long Entrada { get; set; }
        public string Condicao { get; set; }
        public long Saida { get; set; }
        public string NomeComissao { get; set; }
        public int Orgao { get; set; }
        public string EntradaTxt { get; set; }
        public string SaidaTxt { get; set; }

        public static void Incluir(Comissao objComissao)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.RunInTransaction(() =>
                {
                    conexao.Insert(objComissao);
                });
            }
        }

        public static ObservableCollection<Comissao> ListarComissaoDeputado(string idDeputado)
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<Comissao> comissoes = conexao.Query<Comissao>("select * from Comissao where IdeCadastroDeputado =" + idDeputado).ToList<Comissao>();
                    ObservableCollection<Comissao> ListaComissoeDeputados = new ObservableCollection<Comissao>(comissoes);
                    return ListaComissoeDeputados;
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
                conexao.DropTable<Comissao>();
                conexao.CreateTable<Comissao>();
                conexao.Dispose();
                conexao.Close();
            }
        }

        public static void ExcluirCommisoesDeputado(string idDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.Execute("delete from Comissao where IdeCadastroDeputado =" + idDeputado);
            }

        }

    }
}
