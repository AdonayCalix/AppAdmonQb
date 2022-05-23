using Microsoft.AspNetCore.Mvc.RazorPages;
using AppAdmonQb.Components.Deposit;
using AppAdmonQb.Components;

namespace AppAdmonQb.Pages
{
    public class DepositModel : PageModel
    {
        private readonly ILogger<DepositModel> _logger;
        public bool HasError;
        public string MessageError;
        public int QuantitySuccess;
        public int QuantityError;

        public DepositModel(ILogger<DepositModel> logger)
        {
            _logger = logger;
            HasError = false;
            MessageError = "";
            QuantitySuccess = 0;
            QuantityError = 0;
        }

        public void OnGet()
        {
            var deposits = (new SourceDeposit()).List();
            var qbManager = new QbManager();

            try
            {
                qbManager.CreateSession()
                    .OpenConnection()
                    .BeginSession()
                    .SendRequest(new DepositAddRequest(qbManager.GetSession(), deposits).Build().Get());

                var response = (new DepositResponse(qbManager.GetResponse(), deposits)).Walk();

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