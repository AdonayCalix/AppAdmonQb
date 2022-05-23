using QBFC15Lib;

namespace AppAdmonQb.Components.Deposit
{
    internal class DepositAddRequest
    {

        IMsgSetRequest requestMsgSet = null;
        dynamic Deposits = null;

        public DepositAddRequest(QBSessionManager sessionManager, dynamic desposits)
        {
            Deposits = desposits;
            requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
        }

        public DepositAddRequest Build()
        {

            foreach (var deposit in Deposits)
            {
                IDepositAdd DepositAddRq = requestMsgSet.AppendDepositAddRq();
                DepositAddRq.DepositToAccountRef.FullName.SetValue(deposit.DepositToAccountRef);
                DepositAddRq.Memo.SetValue(deposit.Memo);
                DepositAddRq.TxnDate.SetValue(DateTime.Parse(deposit.TxnDate.ToString()));


                foreach (var depositLine in deposit.DepositLine)
                {
                   
                    IDepositLineAdd DepositLine = DepositAddRq.DepositLineAddList.Append();
                    DepositLine.ORDepositLineAdd.DepositInfo.EntityRef.FullName.SetValue(depositLine.EntityRef);
                    DepositLine.ORDepositLineAdd.DepositInfo.AccountRef.ListID.SetValue(depositLine.AccountRef);
                    DepositLine.ORDepositLineAdd.DepositInfo.ClassRef.ListID.SetValue(depositLine.ClassRef);
                    DepositLine.ORDepositLineAdd.DepositInfo.Memo.SetValue(depositLine.Memo);
                    DepositLine.ORDepositLineAdd.DepositInfo.CheckNumber.SetValue(depositLine.CheckNumber);
                    DepositLine.ORDepositLineAdd.DepositInfo.Amount.SetValue((double)(depositLine.Amount));
                }
            }

            return this;
        }

        public IMsgSetRequest Get()
        {         
            return requestMsgSet;
        }
    }
}
