using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace AppAdmonQb.Components.VoluntaryContribution
{
    internal class SourceVoluntaryContribution
    {
        public dynamic List()
        {
            var url = "https://ceprosaf-app-admon-api.azurewebsites.net/web/v1/voluntary-contributions/get";
            dynamic voluntaryContributions = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization
                          = new AuthenticationHeaderValue("Bearer", "LSBjtdL7eAWDCRMyAUswtuiCAYIDMKMX");
                var response = httpClient.GetAsync(url);

                var result = response.Result.Content.ReadAsStringAsync().Result;

                voluntaryContributions = JsonConvert.DeserializeObject<dynamic>(result);
            }

            return voluntaryContributions;
        }
    }
}
