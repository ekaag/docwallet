using Microsoft.Extensions.Configuration;

using docwallet.indexer;
using System.Threading.Tasks;

namespace consolemain
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
            try
            {
                BuildStartup().Wait();
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static async Task<docwallet.indexer.Startup> BuildStartup()
        {
            //var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return await Task.FromResult(new docwallet.indexer.Startup());
        }
    }
}