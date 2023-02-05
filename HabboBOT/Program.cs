using HabboBOT.Core;
using HabboBOT.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using WebSocketSharp.Server;

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

            Config.SetGlobals(Config.Hotels.BR);

            handler = new Handler();

            _ = new AccountHandler(LoadAccounts(), handler);

            server = new WebSocketServer(8080, false);
            server.AddWebSocketService<TokenService>("/");
            server.Start();

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

            Writer.LogWarning("Waiting captcha token.\n");

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
                Writer.LogWarning("Account file doesn't exist.");
                Console.ReadKey();
                Environment.Exit(0);

                return null;
            }
        }
    }
}