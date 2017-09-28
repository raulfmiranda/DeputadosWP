using Deputados.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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


        public static async Task<String> GetRequest(string url)
        {
            Uri geturi = new Uri(url); 
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage responseGet = await client.GetAsync(geturi).ConfigureAwait(continueOnCapturedContext: false);
            return await responseGet.Content.ReadAsStringAsync();
        }
                                                                            
        public static List<Deputado> GetTodoDeputados()
        {
            string jsonString = GetRequest(URL_TODOS_DEPUTADO).Result;           
            List<Deputado> rootObject = JsonConvert.DeserializeObject<List<Deputado>>(jsonString);
            return rootObject;
        }


    }
}
