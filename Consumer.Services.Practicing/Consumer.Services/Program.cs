using Consumer.Services.Models;
using System.Net.Http.Json;

namespace Consumer.Services
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            Department department = await client.GetFromJsonAsync<Department>("https://localhost:7068/api/Department/1");

            Console.WriteLine(department.id);
            Console.WriteLine(department.name);
            Console.WriteLine(department.managerName);

        }
    }
}
