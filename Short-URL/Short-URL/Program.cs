using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Short_URL
{
    class Program
    {

        static async void url()
        {
            var payload = new
            {
                destination = "https://docs.google.com/forms/d/1nrW3sdbhgWCFQwKF_VWiJqom7QFH89_eZTvqd0oSaCw/viewform?edit_requested=true",
                domain = new
                {
                    fullName = "rebrand.ly"
                }
            };

            using (var httpClient = new HttpClient { BaseAddress = new Uri("https://api.rebrandly.com") })
            {
                httpClient.DefaultRequestHeaders.Add("apikey", "269285c480d44721a2b25ec54f207d1a");
                httpClient.DefaultRequestHeaders.Add("workspace", "5023336ecbed432fab19c77fa4a7fce9");

                var body = new StringContent(
                    JsonConvert.SerializeObject(payload), UnicodeEncoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("/v1/links", body))
                {
                    response.EnsureSuccessStatusCode();

                    var link = JsonConvert.DeserializeObject<dynamic>(
                        await response.Content.ReadAsStringAsync());

                    Console.WriteLine($"Long URL was {payload.destination}, short URL is {link.shortUrl}");
                }
            }
        }

        static void Main(string[] args)
        {
            url();
            Console.ReadLine();
        }
    }
}
