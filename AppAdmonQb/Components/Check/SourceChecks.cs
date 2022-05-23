using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace AppAdmonQb.Components.Check
{
    internal class SourceChecks
    {
        public dynamic List()
        {
            var url = "http://localhost/app-admon-api/web/v1/movements/get-checks";
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
