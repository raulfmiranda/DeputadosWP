using Deputados.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Deputados.DBHelper
{
    class DatabaseHelper
    {

        private async Task<bool> ExisteArquivo(string nomeArquivo)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(nomeArquivo);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void CriaBancoDeDados(string DB_PATH)
        {
            if (!ExisteArquivo(DB_PATH).Result)
            {
                using (SQLite.Net.SQLiteConnection conexao = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
                {
                    conexao.CreateTable<Deputado>();
                    conexao.CreateTable<Comissao>();
                    conexao.CreateTable<DeputadoFrenquencia>();
                    conexao.CreateTable<GastoCnpj>();
                    conexao.CreateTable<GastosAno>();
                    conexao.CreateTable<GastoTipo>();
                    conexao.CreateTable<Projeto>();
                }
                


            }
        }

        public DatabaseHelper()
        {}


    }
}
