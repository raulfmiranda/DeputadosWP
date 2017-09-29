using Deputados.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

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
        


        public static async Task<String> GetRequest(string url)
        {
            Uri geturi = new Uri(url); 
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage responseGet = await client.GetAsync(geturi).ConfigureAwait(continueOnCapturedContext: false);
            return await responseGet.Content.ReadAsStringAsync();
        }
                                                                            
        public static ObservableCollection<Deputado> GetTodoDeputados()
        {
            string jsonString = GetRequest(URL_TODOS_DEPUTADO).Result;
            ObservableCollection<Deputado> rootObject = JsonConvert.DeserializeObject<ObservableCollection<Deputado>>(jsonString);
            return rootObject;
        }

        public static ObservableCollection<Deputado> GetDeputadosPorEstado(string uf)
        {
            string url = String.Format(URL_DEPUTADO_ESTADO, uf);
            string jsonString = GetRequest(url).Result;
            ObservableCollection<Deputado> rootObject = JsonConvert.DeserializeObject<ObservableCollection<Deputado>>(jsonString);
            return rootObject;
        }

        public static Deputado GetDeputado(string idDeputado)
        {
            string url = String.Format(URL_DEPUTADO, idDeputado);
            string jsonString = GetRequest(url).Result;
            Deputado rootObject = JsonConvert.DeserializeObject<Deputado>(jsonString);
            return rootObject;
        }

        public static ObservableCollection<DeputadoFrenquencia> GetFrequenciaDeputado(string idDeputado)
        {
            string url = String.Format(URL_FREQUENCIA_DEPUTADO, idDeputado);
            string jsonString = GetRequest(url).Result;
            ObservableCollection<DeputadoFrenquencia> rootObject = JsonConvert.DeserializeObject<ObservableCollection<DeputadoFrenquencia>>(jsonString);
            return rootObject;
        }

        public static ObservableCollection<Comissao> GetComissaoDeputado(string idDeputado)
        {
            string url = String.Format(URL_COMISSAO_DEPUTADO, idDeputado);
            string jsonString = GetRequest(url).Result;
            ObservableCollection<Comissao> rootObject = JsonConvert.DeserializeObject<ObservableCollection<Comissao>>(jsonString);
            return rootObject;
        }


        public static ObservableCollection<Projeto> GetProjetoDeputado(string idDeputado)
        {
            string url = String.Format(URL_PROJETO_DEPUTADO, idDeputado);
            string jsonString = GetRequest(url).Result;
            ObservableCollection<Projeto> rootObject = JsonConvert.DeserializeObject<ObservableCollection<Projeto>>(jsonString);
            return rootObject;
        }

        public static ObservableCollection<GastoTipo> GetTipoGastoDeputado(string idDeputado)
        {
            string url = String.Format(URL_GASTO_TIPO_DEPUTADO, idDeputado);
            string jsonString = GetRequest(url).Result;
            ObservableCollection<GastoTipo> rootObject = JsonConvert.DeserializeObject<ObservableCollection<GastoTipo>>(jsonString);
            return rootObject;
        }

        public static ObservableCollection<GastosAno> GetGastoAnoDeputado(string idDeputado, string ano)
        {
            string url = String.Format(URL_GASTO_ANO, idDeputado, ano);
            string jsonString = GetRequest(url).Result;
            ObservableCollection<GastosAno> rootObject = JsonConvert.DeserializeObject<ObservableCollection<GastosAno>>(jsonString);
            return rootObject;
        }

        public static ObservableCollection<GastoCnpj> GetGastoCnpjDeputado(string idDeputado)
        {
            string url = String.Format(URL_GASTO_CNPJ, idDeputado);
            string jsonString = GetRequest(url).Result;
            ObservableCollection<GastoCnpj> rootObject = JsonConvert.DeserializeObject<ObservableCollection<GastoCnpj>>(jsonString);
            return rootObject;
        }





    }
}
