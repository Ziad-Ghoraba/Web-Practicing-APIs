using Consumer.Services.Models;
using System.Net.Http.Json;

namespace Consumer.Services
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            GeneralResponse generalResponse = await client.GetFromJsonAsync<GeneralResponse>("https://localhost:7068/api/Category/2");
            if(generalResponse.IsSuccess)
            {
                Console.WriteLine(generalResponse.Data);
            }
            else
            {
                Console.WriteLine(generalResponse.Data);
            }

        }
    }
}
