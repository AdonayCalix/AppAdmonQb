using Newtonsoft.Json;
using System.Net.Http.Headers;
using AppAdmonQb.Models;

namespace AppAdmonQb.Components.VoluntaryContribution
{
    internal class SendResponse
    {
        public List<Response> responseList = null;

        public SendResponse(List<Response> list)
        {
            responseList = list;
        }

        public SendResponse Build()
        {
            var url = "url_here";
            dynamic checks = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization
                          = new AuthenticationHeaderValue("Bearer", "here");
                var response = httpClient.PostAsJsonAsync(url, responseList);

                var result = response.Result.Content.ReadAsStringAsync().Result;

                Console.WriteLine(JsonConvert.DeserializeObject<dynamic>(result));
            }
            return this;
        }
    }
}
