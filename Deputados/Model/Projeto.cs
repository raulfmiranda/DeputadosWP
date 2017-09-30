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

        private static void Incluir(Projeto objProjeto)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.RunInTransaction(() =>
                {
                    
                    for (int i = 0; i <= 10; i++)
                    {
                        try
                        {
                            conexao.Insert(objProjeto);
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

        private static void IncluirLista(ObservableCollection<Projeto> projetos)
        {
            foreach (Projeto projeto in projetos)
            {
                Incluir(projeto);
            }

        }

        public static ObservableCollection<Projeto> ListarProjetoDeputado(string idDeputado)
        {
            if (WebServiceHelper.possuiConexaoInternet())
            {
                string jsonString = WebServiceHelper.GetProjetoDeputado(idDeputado);
                ObservableCollection<Projeto> projetos = JsonConvert.DeserializeObject<ObservableCollection<Projeto>>(jsonString);
                ObservableCollection<Projeto> projetosClone = JsonConvert.DeserializeObject<ObservableCollection<Projeto>>(jsonString);

                var t = Task.Run(() => {
                    ExcluirProjetosDeputado(idDeputado);
                    IncluirLista(projetos);
                });
                
                return projetos;
            }
            else
            {
                return ListarProjetooDeputadoBanco(idDeputado);
            }
        }

        private static ObservableCollection<Projeto> ListarProjetooDeputadoBanco(string IdParlamentarAutor)
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<Projeto> projetos = conexao.Query<Projeto>("select * from Projeto where IdParlamentarAutor =" + "\"" +IdParlamentarAutor + "\"").ToList<Projeto>();
                    ObservableCollection<Projeto> ListaProjetos = new ObservableCollection<Projeto>(projetos);
                    return ListaProjetos;
                }
            }
            catch
            {
                return null;
            }
        }


        private static void ExcluirTodasProjeto()
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                for (int i = 0; i <= 10; i++)
                {
                    try
                    {
                        conexao.DropTable<Projeto>();
                        conexao.CreateTable<Projeto>();
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

        private static void ExcluirProjetosDeputado(string idDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                
                for (int i = 0; i <= 10; i++)
                {
                    try
                    {
                        conexao.Execute("delete from Projeto where IdParlamentarAutor =" + "\""+idDeputado + "\"");
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
