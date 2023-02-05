using System;
using System.IO;
using System.Collections.Generic;

using WebSocketSharp.Server;

using HabboBOT.Core;
using HabboBOT.Core.Services;

namespace HabboBOT
{
    class Program
    {
        static void Main() => new Program().Run();

        private Handler handler;
        private WebSocketServer server;

        public void Run()
        {
            Console.Title = "HabboBOT - Connected[0]";
            string[] lines =
            {
                @"  _   _       _     _           ____        _   ",
                @" | | | | __ _| |__ | |__   ___ | __ )  ___ | |_ ",
                @" | |_| |/ _` | '_ \| '_ \ / _ \|  _ \ / _ \| __|",
                @" |  _  | (_| | |_) | |_) | (_) | |_) | (_) | |_ ",
                @" |_| |_|\__,_|_.__/|_.__/ \___/|____/ \___/ \__|", ""
            };
            foreach (string line in lines)
                Console.WriteLine(line);


            handler = new Handler();
            _ = new AccountHandler(LoadAccounts(), handler);

            Config.Load();

            server = new WebSocketServer(8080, false);
            server.AddWebSocketService<TokenService>("/");
            server.Start();

            LogWriter.LogWarning("Waiting captcha token.\n");

            Console.ReadKey();
        }

        private static List<Account> LoadAccounts()
        {
            if (File.Exists("accounts.txt"))
            {
                List<Account> accounts = new();
                foreach (string line in File.ReadAllLines("accounts.txt"))
                {
                    Account account = new Account(line);
                    if (account.IsValid)
                        accounts.Add(account);
                }
                return accounts;
            }
            else
            {
                LogWriter.LogWarning("Account file doesn't exist.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            return null;
        }
    }
}