using QBFC15Lib;
using AppAdmonQb.Models;

namespace AppAdmonQb.Components.Check
{
    internal class CheckResponse
    {
        public List<Response> responseList = new List<Response>();
        public dynamic checkList = null;

        public IMsgSetResponse responseMsgSet;

        public int QuantitySuccess = 0;
        public int QuantityError = 0;
        public CheckResponse(IMsgSetResponse ResponseMsgSet, dynamic checks)
        {
            responseMsgSet = ResponseMsgSet;
            checkList = checks;
        }

        public CheckResponse Walk()
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
                    if (responseType == ENResponseType.rtCheckAddRs)
                    {
                        ICheckRet checkRet = (ICheckRet)response.Detail;
                        WalkReturn(checkRet, response.StatusCode, checkList[i]);
                    }

                }
            }

            return this;
        }

        public void WalkReturn(ICheckRet checkRet, int Code, dynamic check)
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
                     ListId = checkRet != null ? (string)checkRet.TxnID.GetValue() : "",
                     Amount = check.Amount,
                     Kind = "Egreso",
                     NumberRef = check.RefNumber
                 }
             );
        }

        public List<Response> GetList()
        {
            return responseList;
        }
    }
}
