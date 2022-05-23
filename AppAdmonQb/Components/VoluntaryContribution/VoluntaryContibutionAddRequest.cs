using QBFC15Lib;

namespace AppAdmonQb.Components.VoluntaryContribution
{
    internal class VoluntaryContibutionAddRequest
    {
        IMsgSetRequest requestMsgSet = null;
        dynamic VoluntaryContributions = null;

        public VoluntaryContibutionAddRequest(QBSessionManager sessionManager, dynamic voluntaryContributions)
        {
            VoluntaryContributions = voluntaryContributions;
            requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
        }

        public VoluntaryContibutionAddRequest Build()
        {

            foreach (var voluntaryContribution in VoluntaryContributions)
            {
                IDepositAdd DepositAddRq = requestMsgSet.AppendDepositAddRq();
                DepositAddRq.DepositToAccountRef.FullName.SetValue(voluntaryContribution.DepositToAccountRef);
                DepositAddRq.Memo.SetValue(voluntaryContribution.Memo);
                DepositAddRq.TxnDate.SetValue(DateTime.Parse(voluntaryContribution.TxnDate.ToString()));

                foreach (var depositLine in voluntaryContribution.DepositLine)
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
}
