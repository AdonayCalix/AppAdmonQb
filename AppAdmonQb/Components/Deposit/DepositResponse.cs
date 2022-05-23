using QBFC15Lib;
using AppAdmonQb.Models;

namespace AppAdmonQb.Components.Deposit
{
    internal class DepositResponse
    {
        public List<Response> responseList = new List<Response>();
        public dynamic depositList = null;

        public IMsgSetResponse responseMsgSet;

        public int QuantitySuccess = 0;
        public int QuantityError = 0;
        public DepositResponse(IMsgSetResponse ResponseMsgSet, dynamic deposits)
        {
            responseMsgSet = ResponseMsgSet;
            depositList = deposits;
        }

        public DepositResponse Walk()
        {
            if (responseMsgSet == null) return this;
            IResponseList responseList = responseMsgSet.ResponseList;
            if (responseList == null) return this;

            for (int i = 0; i < responseList.Count; i++)
            {
                IResponse response = responseList.GetAt(i);

                if (response.StatusCode >= 0)
                {

                    ENResponseType responseType = (ENResponseType)response.Type.GetValue();
                    if (responseType == ENResponseType.rtDepositAddRs)
                    {
                        IDepositRet depositRet = (IDepositRet)response.Detail;
                        WalkReturn(depositRet, response.StatusCode, depositList[i]);
                    }

                }
            }

            return this;
        }

        public void WalkReturn(IDepositRet depositRet, int Code, dynamic check)
        {

            if (Code == 0) QuantitySuccess++;
            if (Code > 0) QuantityError++;

            responseList.Add(
                 new Response
                 {
                     Code = Code,
                     Date = check.TxnDate.ToString(),
                     ProjectId = check.ProjectId,
                     MovementDetailId = check.MovementDetailId,
                     MovementId = check.MovementId,
                     ListId = depositRet != null ? (string)depositRet.TxnID.GetValue() : "",
                     Amount = check.Amount,
                     Kind = "Ingreso",
                     NumberRef = check.CheckNumber
                 }
             );
        }

        public List<Response> GetList()
        {
            return responseList;
        }
    }
}
