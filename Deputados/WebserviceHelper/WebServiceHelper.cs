using Deputados.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage.Provider;
using System.Net.NetworkInformation;

namespace Deputados.WebserviceHelper
{
    static class WebServiceHelper
    {

        public const string URL_TODOS_DEPUTADO = "http://meucongressonacional.com/api/001/deputado";
        public const string URL_DEPUTADO_ESTADO = "http://meucongressonacional.com/api/001/deputado/estado/{0}";
        public const string URL_DEPUTADO = "http://meucongressonacional.com/api/001/deputado/{0}";
        public const string URL_FREQUENCIA_DEPUTADO = "http://meucongressonacional.com/api/001/deputado/{0}/freq";
        public const string URL_COMISSAO_DEPUTADO = "http://meucongressonacional.com/api/001/deputado/{0}/comissoes";
        public const string URL_PROJETO_DEPUTADO = "http://meucongressonacional.com/api/001/deputado/{0}/projetos";
        public const string URL_GASTO_TIPO_DEPUTADO = "http://meucongressonacional.com/api/001/deputado/{0}/gastos/tipo";
        public const string URL_GASTO_ANO = "http://meucongressonacional.com/api/001/deputado/{0}/gastos/{1}";
        public const string URL_GASTO_CNPJ = "http://meucongressonacional.com/api/001/deputado/{0}/gastos/cnpj";
        

        public static Boolean possuiConexaoInternet()
        {
            //    Boolean hasConnection = false;
            //    //Checa adaptadores de rede
            //    if (NetworkInterface.GetIsNetworkAvailable())
            //    {
            //        hasConnection = true;
            //    }
            //    return hasConnection;
            //
            return false;
        }


        private static async Task<String> GetRequest(string url)
        {
            Uri geturi = new Uri(url); 
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage responseGet = await client.GetAsync(geturi).ConfigureAwait(continueOnCapturedContext: false);
            return await responseGet.Content.ReadAsStringAsync();
        }


        public static string GetTodoDeputados()
        {
            string jsonString = GetRequest(URL_TODOS_DEPUTADO).Result;           
            return jsonString;
        }


        public static string GetDeputadosPorEstado(string uf)
        {
            string url = String.Format(URL_DEPUTADO_ESTADO, uf);
            string jsonString = GetRequest(url).Result;            
            return jsonString;
        }

        public static string GetDeputado(string idDeputado)
        {
            string url = String.Format(URL_DEPUTADO, idDeputado);
            string jsonString = GetRequest(url).Result;
            //Deputado rootObject = JsonConvert.DeserializeObject<Deputado>(jsonString);
            return jsonString;
        }

        public static string GetFrequenciaDeputado(string idDeputado)
        {
            string url = String.Format(URL_FREQUENCIA_DEPUTADO, idDeputado);
            string jsonString = GetRequest(url).Result;
           // ObservableCollection<DeputadoFrenquencia> rootObject = JsonConvert.DeserializeObject<ObservableCollection<DeputadoFrenquencia>>(jsonString);
            return jsonString;
        }

        public static string GetComissaoDeputado(string idDeputado)
        {
            string url = String.Format(URL_COMISSAO_DEPUTADO, idDeputado);
            string jsonString = GetRequest(url).Result;
            //ObservableCollection<Comissao> rootObject = JsonConvert.DeserializeObject<ObservableCollection<Comissao>>(jsonString);
            return jsonString;
        }


        public static string GetProjetoDeputado(string idDeputado)
        {
            string url = String.Format(URL_PROJETO_DEPUTADO, idDeputado);
            string jsonString = GetRequest(url).Result;
            //ObservableCollection<Projeto> rootObject = JsonConvert.DeserializeObject<ObservableCollection<Projeto>>(jsonString);
            return jsonString;
        }

        public static string GetTipoGastoDeputado(string idDeputado)
        {
            string url = String.Format(URL_GASTO_TIPO_DEPUTADO, idDeputado);
            string jsonString = GetRequest(url).Result;
            //ObservableCollection<GastoTipo> rootObject = JsonConvert.DeserializeObject<ObservableCollection<GastoTipo>>(jsonString);
            return jsonString;
        }

        public static string GetGastoAnoDeputado(string idDeputado, string ano)
        {
            string url = String.Format(URL_GASTO_ANO, idDeputado, ano);
            string jsonString = GetRequest(url).Result;
            //ObservableCollection<GastosAno> rootObject = JsonConvert.DeserializeObject<ObservableCollection<GastosAno>>(jsonString);
            return jsonString;
        }

        public static string GetGastoCnpjDeputado(string idDeputado)
        {
            string url = String.Format(URL_GASTO_CNPJ, idDeputado);
            string jsonString = GetRequest(url).Result;
           // ObservableCollection<GastoCnpj> rootObject = JsonConvert.DeserializeObject<ObservableCollection<GastoCnpj>>(jsonString);
            return jsonString;
        }





    }
}
