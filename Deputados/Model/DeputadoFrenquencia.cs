using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    class DeputadoFrenquencia
    {
        public string IdParlamentar { get; set; }
        public int Ano { get; set; }
        public int PresencasDias { get; set; }
        public int PresencasSessoes { get; set; }
        public int AusenciasDias { get; set; }
        public int AusenciasSessoes { get; set; }
        public double IndicePresenca { get; set; }

        public static void Incluir(DeputadoFrenquencia objDeputadoFrenquencia)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.RunInTransaction(() =>
                {
                    conexao.Insert(objDeputadoFrenquencia);
                });
            }
        }

        public static ObservableCollection<DeputadoFrenquencia> ListarFrequenciaDeputado(string IdParlamentar)
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<DeputadoFrenquencia> frequencias = conexao.Query<DeputadoFrenquencia>("select * from DeputadoFrenquencia where IdParlamentar =" + IdParlamentar).ToList<DeputadoFrenquencia>();
                    ObservableCollection<DeputadoFrenquencia> ListaDeputadoFrenquencia = new ObservableCollection<DeputadoFrenquencia>(frequencias);
                    return ListaDeputadoFrenquencia;
                }
            }
            catch
            {
                return null;
            }
        }


        public static void ExcluirTodasDeputadoFrenquencias()
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.DropTable<DeputadoFrenquencia>();
                conexao.CreateTable<DeputadoFrenquencia>();
                conexao.Dispose();
                conexao.Close();
            }
        }

        public static void ExcluirDeputadoFrenquencia(string IdParlamentar)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.Execute("delete from DeputadoFrenquencia where IdParlamentar =" + IdParlamentar);
            }

        }

    }
}
