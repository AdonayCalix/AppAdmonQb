using QBFC15Lib;

namespace AppAdmonQb.Components
{
    internal class QbManager
    {
        public bool sessionBegun = false;
        public bool connectionOpen = false;

         QBSessionManager sessionManager = null;
        public IMsgSetResponse responseMsgSet = null;   

        public QbManager CreateSession()
        {
            sessionManager = new QBSessionManager();
            return this;
        }

        public QbManager OpenConnection()
        {
            sessionManager.OpenConnection("", "CEPROSAF");
            connectionOpen = true;
            return this;
        }

        public QbManager CloseConnection()
        {
            sessionManager.CloseConnection();
            connectionOpen = false;
            return this;
        }

        public QbManager BeginSession()
        {
            sessionManager.BeginSession("", ENOpenMode.omDontCare);
            sessionBegun = true;
            return this;
        }

        public QbManager CloseSession()
        {
            sessionManager.EndSession();
            sessionBegun = false;
            return this;
        }

        public QbManager SendRequest(IMsgSetRequest requestMsgSet)
        {
            responseMsgSet = sessionManager.DoRequests(requestMsgSet);
            Console.WriteLine(responseMsgSet.ToXMLString());
            return this;
        }

        public IMsgSetResponse GetResponse()
        {           
            return responseMsgSet;
        }

        public QBSessionManager GetSession()
        {
            return sessionManager;
        }
    }
}
