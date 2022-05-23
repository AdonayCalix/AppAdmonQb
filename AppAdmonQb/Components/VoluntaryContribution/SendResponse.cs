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
            var url = "https://ceprosaf-app-admon-api.azurewebsites.net/web/v1/voluntary-contributions/store-response";
            dynamic checks = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization
                          = new AuthenticationHeaderValue("Bearer", "LSBjtdL7eAWDCRMyAUswtuiCAYIDMKMX");
                var response = httpClient.PostAsJsonAsync(url, responseList);

                var result = response.Result.Content.ReadAsStringAsync().Result;

                Console.WriteLine(JsonConvert.DeserializeObject<dynamic>(result));
            }
            return this;
        }
    }
}
