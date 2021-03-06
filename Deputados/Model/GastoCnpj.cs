﻿using Deputados.WebserviceHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    class GastoCnpj
    {
        [JsonProperty("idDeputado")]
        public string IdDeputado { get; set; }
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }
        [JsonProperty("descricao")]
        public string Descricao { get; set; }
        [JsonProperty("detalhe")]
        public string Detalhe { get; set; }
        [JsonProperty("totalGasto")]
        public double TotalGasto { get; set; }

        public GastoCnpj()
        {

        }

        private static void Incluir(GastoCnpj objGastoCnpj)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.RunInTransaction(() =>
                {
                    for (int i = 0; i <= 10; i++)
                    {
                        try
                        {
                            conexao.Insert(objGastoCnpj);
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

        private static void IncluirLista(ObservableCollection<GastoCnpj> gastos)
        {
            foreach (GastoCnpj gasto in gastos)
            {
                Incluir(gasto);
            }

        }

        public static ObservableCollection<GastoCnpj> ListarGastoCnpjDeputado(string idDeputado)
        {
            if (WebServiceHelper.possuiConexaoInternet())
            {
                String jsonString = WebServiceHelper.GetGastoCnpjDeputado(idDeputado);
                ObservableCollection<GastoCnpj> gastos = JsonConvert.DeserializeObject<ObservableCollection<GastoCnpj>>(jsonString);
                ObservableCollection<GastoCnpj> gastosClone = JsonConvert.DeserializeObject<ObservableCollection<GastoCnpj>>(jsonString);
                var t = Task.Run(() => {
                    ExcluirGastoCnpjPorDeputado(idDeputado);
                     IncluirLista(gastos);
                });
                
                return gastos;
            }
            else
            {
                return ListarGastoCnpjDeputadoBanco(idDeputado);
            }
        }

        private static ObservableCollection<GastoCnpj> ListarGastoCnpjDeputadoBanco(string idDeputado)
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<GastoCnpj> gastosCnpj = conexao.Query<GastoCnpj>("select * from GastoCnpj where idDeputado =" + "\"" + idDeputado + "\"").ToList<GastoCnpj>();
                    ObservableCollection<GastoCnpj> ListaGastoCnpjj = new ObservableCollection<GastoCnpj>(gastosCnpj);
                    return ListaGastoCnpjj;
                }
            }
            catch
            {
                return null;
            }
        }


        private static void ExcluirTodasGastoCnpj()
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conexao.DropTable<GastoCnpj>();
                conexao.CreateTable<GastoCnpj>();
                conexao.Dispose();
                conexao.Close();
            }
        }

        private static void ExcluirGastoCnpjPorDeputado(string idDeputado)
        {
            using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                
                for (int i = 0; i <= 10; i++)
                {
                    try
                    {
                        conexao.Execute("delete from GastoCnpj where idDeputado = " + "\"" + idDeputado + "\"");
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
