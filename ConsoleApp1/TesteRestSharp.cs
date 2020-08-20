using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TesteRestSharp
    {
        private string url;
        private string apikey;
        private string content;

        public TesteRestSharp(string url, string apiKey, string userName, string password)
        {
            this.url = url;
            this.apikey = apiKey;
            this.content = string.Format("{{ \"username\": \"{0}\", \"password\": \"{1}\" }}", userName, password);
        }

        public void Execute()
        {
            var client = new RestClient(url)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("x-api-key", apikey);
            request.AddHeader("Cookie", "bm_sv=FA0DBE69E0EFD53C576CB2E2ED59FD6C~ySSAIGOzzJFqvIwfZtueJDogk4+rKDkSuI8oT5VVJhbudyFJe4FQgXgczvJgF0ZSMsIMKOquncvGBBdXeCLEMDK6/D65PfGww7owd8d57Lmtb2LZ42eD+05mUnddGnRwiB74fc0hiOee6MOZVsEouw==; ak_bmsc=A653D1981623D5052F4909DCEEF53A9E60067BF5900E00000E543D5FF519C943~plG6wDrIu/htPpBYPGj3p83c6X1q026lMgKR4PB/c2vPAMrag7dF40cZpaoDDVsJGZSqQEXbse/jr9GNLkqNYmsotperBGUv9z3+JTKaNp9Zr5tgE0tn4iAs6STiNQnm6rXx9jzEWtT39E1gDHdzpzNRPWPFRHUucSRI0mQQ8P6Io068ls/NpWLlQTjsa7ooKC+4vaqumtdTWzJ3pMQMJQu+V3i8dAdKrHcSrsKR7WtzM=");
            request.AddParameter("application/json", content, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Console.WriteLine(response.Content);

            Console.WriteLine("Finalizado.");
            Console.ReadKey();

        }

    }
}
