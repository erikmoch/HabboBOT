using System;
using System.Collections.Generic;

namespace HabboBOT.Core
{
    internal class AccountHandler
    {
        private static Queue<Account> _accounts;
        private static Handler _handler;

        private static int id = 1;

        public AccountHandler(Queue<Account> accounts, Handler handler)
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
                Account account = _accounts.Dequeue();
                Session session = new(account, token, id);

                LogWriter.LogWarning($"[{account.Email}] Trying to login.");

                session.Authorized += (object sender, EventArgs e) =>
                {
                    Network network = new(session);
                    _handler.AppendBot(network);
                };
                session.Unauthorized += (object sender, string e) =>
                {
                    LogWriter.LogError($"[{account.Email}] Unauthorized. Error: " + e);
                };

                session.Connect();
                id++;
            }
            catch { }
        }
    }
}