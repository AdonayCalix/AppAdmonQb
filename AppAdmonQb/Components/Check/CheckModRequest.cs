using QBFC15Lib;

namespace AppAdmonQb.Components.Check
{
    internal class CheckModRequest
    {

        IMsgSetRequest requestMsgSet = null;
        dynamic Checks = null;

        public CheckModRequest(QBSessionManager sessionManager, dynamic checks)
        {
            Checks = checks;
            requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
        }

        public CheckModRequest Build()
        {

            foreach (var check in Checks)
            {
                ICheckMod CheckModRq = requestMsgSet.AppendCheckModRq();
                CheckModRq.TxnID.SetValue(check.TxnId);
                CheckModRq.EditSequence.SetValue(check.TxnId.ToString());
                CheckModRq.AccountRef.FullName.SetValue(check.AccountRef);
                CheckModRq.PayeeEntityRef.FullName.SetValue(check.PayeeEntityRef);
                CheckModRq.RefNumber.SetValue(check.RefNumber);
                CheckModRq.Memo.SetValue(check.Memo);
                CheckModRq.Address.Addr1.SetValue(check.Addr1);             
            }
                     
            return this;
        }

        public IMsgSetRequest Get()
        {          
            return requestMsgSet;
        }
    }
}
