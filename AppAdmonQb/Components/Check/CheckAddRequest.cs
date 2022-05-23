using QBFC15Lib;

namespace AppAdmonQb.Components.Check
{
    internal class CheckAddRequest
    {

        IMsgSetRequest requestMsgSet = null;
        dynamic Checks = null;

        public CheckAddRequest(QBSessionManager sessionManager, dynamic checks)
        {
            Checks = checks;
            requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
        }

        public CheckAddRequest Build()
        {

            foreach (var check in Checks)
            {
                ICheckAdd CheckAddRq = requestMsgSet.AppendCheckAddRq();
                CheckAddRq.AccountRef.FullName.SetValue(check.AccountRef);
                CheckAddRq.PayeeEntityRef.FullName.SetValue(check.PayeeEntityRef);
                CheckAddRq.RefNumber.SetValue(check.RefNumber);
                CheckAddRq.Memo.SetValue(check.Memo);
                CheckAddRq.Address.Addr1.SetValue(check.Addr1);

                foreach (var expenseLine in check.ExpenseLine)
                {
                    IExpenseLineAdd expenseLineAdd = CheckAddRq.ExpenseLineAddList.Append();
                    expenseLineAdd.AccountRef.ListID.SetValue(expenseLine.AccountRef);
                    expenseLineAdd.Amount.SetValue((double)expenseLine.Amount);
                    expenseLineAdd.Memo.SetValue(expenseLine.Concept);
                    expenseLineAdd.ClassRef.ListID.SetValue(expenseLine.ClassRef);
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
