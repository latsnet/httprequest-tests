using System;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApp1
{

    public class TesteWebRequest
    {

        private string url;
        private string apikey;
        private string content;
        
        public TesteWebRequest(string url, string apiKey, string userName, string password)
        {
            this.url = url;
            this.apikey = apiKey;
            this.content = string.Format("{{ \"username\": \"{0}\", \"password\": \"{1}\" }}", userName, password);
        }

        public void Execute()
        {
            try
            {
                Console.WriteLine("Criando Request");
                var request = CreateRequest();

                Console.WriteLine("Escrevendo dados...");
                var bytes = Encoding.UTF8.GetBytes(content);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                Console.WriteLine("Aguardando Response...");
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    Console.WriteLine("Resultado:");
                    Console.WriteLine(string.Format("StatusCode: {0} - {1}", response.StatusCode, response.StatusDescription));
                    Console.WriteLine("ResponseBody: ");
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string line = "";
                        do
                        {
                            line = reader.ReadLine();
                            Console.WriteLine(line);
                        }
                        while (!string.IsNullOrEmpty(line));

                    }
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

        private HttpWebRequest CreateRequest()
        {
            ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(CustomValidation);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.UserAgent = "PostmanRuntime/7.26.3";
            request.ProtocolVersion = HttpVersion.Version11;
            request.Proxy = null;

            request.Headers.Add("x-api-key", apikey);

            return request;
        }

        private static bool CustomValidation(object sender,
                                             System.Security.Cryptography.X509Certificates.X509Certificate cert,
                                             System.Security.Cryptography.X509Certificates.X509Chain chain,
                                             System.Net.Security.SslPolicyErrors error)
        {
            return true;
        }

    }


}
