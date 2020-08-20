using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        #region 
         
        static string url = "http://localhost";
        static string userName = "my$username";
        static string password = "my$password";
        static string apikey = "alaskd902lk20aa909322Dj8W6P2klTrDwxO5N4B1N1eGU";

        #endregion

        static void Main(string[] args)
        {
            // var request = new TesteWebRequest(url, apikey, userName, password);
            // request.Execute();

            // var request = new TesteHttpClient(url, apikey, userName, password);
            // Task.WaitAll(request.ExecuteAsync());

            var request = new TesteRestSharp(url, apikey, userName, password);
            request.Execute();

        }

    }
}
