using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace AppAdmonQb.Components.Check
{
    internal class SourceModChecks
    {
        public dynamic List()
        {
            var url = "https://ceprosaf-app-admon-api.azurewebsites.net/web/v1/movements/get-mod-checks";
            dynamic checks = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization
                          = new AuthenticationHeaderValue("Bearer", "LSBjtdL7eAWDCRMyAUswtuiCAYIDMKMX");
                var response = httpClient.GetAsync(url);

                var result = response.Result.Content.ReadAsStringAsync().Result;

                checks = JsonConvert.DeserializeObject<dynamic>(result);
            }

            return checks;
        }
    }
}
