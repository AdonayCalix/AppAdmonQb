using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace AppAdmonQb.Components.Deposit
{
    internal class SourceDeposit
    {
        public dynamic List()
        {
            var url = "http://localhost/app-admon-api/web/v1/movements/get-deposits";
            dynamic deposits = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization
                          = new AuthenticationHeaderValue("Bearer", "LSBjtdL7eAWDCRMyAUswtuiCAYIDMKMX");
                var response = httpClient.GetAsync(url);

                var result = response.Result.Content.ReadAsStringAsync().Result;

                deposits = JsonConvert.DeserializeObject<dynamic>(result);
            }

            return deposits;
        }
    }
}
