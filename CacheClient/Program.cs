using CacheCow.Client;
using CacheCow.Client.RedisCacheStore;
using System;
using System.Net.Http;

namespace CacheClient
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.Clear();
            var client = ClientExtensions.CreateClient(new RedisStore("localhost:6379"));
            client.BaseAddress = new Uri("http://localhost:1337");

            while(true)
            {
                Console.WriteLine("Hit Enter to get the Info");
                Console.ReadLine();
                var response = await client.GetAsync("/caching/info");
                response.EnsureSuccessStatusCode(); // blow up if it isn't 200-299
                Console.WriteLine(response.Headers.CacheControl.ToString());
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);

            }
        }
    }
}
