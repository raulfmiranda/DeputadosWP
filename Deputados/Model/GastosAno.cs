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
        public object DescricaoGasto { get; set; }
        [JsonProperty("sataEmissao")]
        public object DataEmissao { get; set; }
        [JsonProperty("valor")]
        public double Valor { get; set; }

        public GastosAno()
        { }
        public static void Incluir(GastosAno objGastosAno)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.RunInTransaction(() =>
                {
                    conexao.Insert(objGastosAno);
                });
            }
        }

        public static ObservableCollection<GastosAno> ListarFrequenciaDeputado(string idDeputado)
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<GastosAno> gastosAno = conexao.Query<GastosAno>("select * from GastosAno where idDeputado =" + idDeputado).ToList<GastosAno>();
                    ObservableCollection<GastosAno> ListaGastoAno = new ObservableCollection<GastosAno>(gastosAno);
                    return ListaGastoAno;
                }
            }
            catch
            {
                return null;
            }
        }


        public static void ExcluirTodasGastoAno()
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.DropTable<GastosAno>();
                conexao.CreateTable<GastosAno>();
                conexao.Dispose();
                conexao.Close();
            }
        }

        public static void ExcluirGastoCnpjPorDeputado(string idDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.Execute("delete from GastosAno where idDeputado =" + idDeputado);
            }

        }

    }
}
