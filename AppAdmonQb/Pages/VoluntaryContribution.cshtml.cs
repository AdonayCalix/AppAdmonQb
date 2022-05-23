using Microsoft.AspNetCore.Mvc.RazorPages;
using AppAdmonQb.Components.Check;
using AppAdmonQb.Components;



namespace AppAdmonQb.Pages
{
    public class VoluntaryContributionModel : PageModel
    {
        private readonly ILogger<VoluntaryContributionModel> _logger;
        public bool HasError;
        public string MessageError;
        public int QuantitySuccess;
        public int QuantityError;

        public VoluntaryContributionModel(ILogger<VoluntaryContributionModel> logger)
        {
            _logger = logger;
            HasError = false;
            MessageError = "";
            QuantitySuccess = 0;
            QuantityError = 0;
        }

        public void OnGet()
        {                     
            var checks = (new SourceChecks()).List();
            var qbManager = new QbManager();

            try
            {
                qbManager.CreateSession()
                    .OpenConnection()
                    .BeginSession()
                    .SendRequest(new CheckAddRequest(qbManager.GetSession(), checks).Build().Get());

                var response = (new CheckResponse(qbManager.GetResponse(), checks)).Walk();

                QuantityError = response.QuantityError;
                QuantitySuccess = response.QuantitySuccess;

                new SendResponse(response.GetList()).Build();              

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (qbManager.sessionBegun) qbManager.CloseSession();
                if (qbManager.connectionOpen) qbManager.CloseConnection();
            }
        }
    }
}