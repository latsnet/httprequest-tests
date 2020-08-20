using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TesteHttpClient
    {

        private string url;
        private string apikey;
        private string content;

        public TesteHttpClient(string url, string apiKey, string userName, string password)
        {
            this.url = url;
            this.apikey = apiKey;
            this.content = string.Format("{{ \"username\": \"{0}\", \"password\": \"{1}\" }}", userName, password);
        }

        public async Task ExecuteAsync()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | (SecurityProtocolType)12288;
            var data = new StringContent(content, Encoding.UTF8, "application/json");

            try
            {
                Console.WriteLine("Criando Request");
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                    Headers =
                {
                    { "user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)" },
                    { "x-api-key", apikey }
                },
                    Content = data
                };
                Console.WriteLine("Enviando requisição e aguardando resposta...");
                using (var response = await client.SendAsync(request))
                {
                    Console.WriteLine(string.Format("StatusCode: {0}", response.StatusCode));
                    response.EnsureSuccessStatusCode();

                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("ResponseBody:");
                    Console.WriteLine(body);
                }
            }
            catch (Exception e)
            {
                string erroWebService = string.Empty;
                if (e is WebException)
                {
                    WebException ex = e as WebException;

                    if (ex.Response != null)
                    {
                        erroWebService = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    }
                }

                Console.WriteLine(string.Format("Erro ao executar o serviço: {0} / ", e.Message, erroWebService));
            }

            Console.WriteLine("Finalizado.");
            Console.ReadKey();
        }

    }

}
