using System.Collections.Generic;

namespace HabboBOT.Core
{
    public class AccountHandler
    {
        public  static List<Account> _accounts;
        private static Handler _handler;

        private static int id = 1;

        public AccountHandler(List<Account> accounts, Handler handler)
        {
            _accounts = accounts;
            _handler = handler;
        }

        public static void StartLogin(string token)
        {
            if (_accounts.Count is 0)
                return;

            try 
            {
                Account account = _accounts[0];
                Session session = new(account, _handler, token, id);
                session.Connect();

                _accounts.Remove(account);
                id++;
            }
            catch { }
        }
    }
}