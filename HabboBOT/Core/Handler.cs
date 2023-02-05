using System;
using System.Collections.Generic;

namespace HabboBOT.Core
{
    internal class Handler
    {
        public List<Network> bots;

        public Handler()
        {
            bots = new List<Network>();
        }

        public void AppendBot(Network network)
        {
            LogWriter.LogWarning($"[BOT {network.Id}] Connecting...");
            network.OnConnectionStarted += (object sender, EventArgs e) =>
            {
                bots.Add(network);
                LogWriter.LogSuccess($"[BOT {network.Id}] Connected.");

                UpdateConsoleTitle();
            };
            network.OnConnectionStopped += (object sender, string e) =>
            {
                bots.Remove(network);
                LogWriter.LogError($"[BOT {network.Id}] Disconnected.");

                UpdateConsoleTitle();
            };
            network.Connect();
        }

        private void UpdateConsoleTitle() => Console.Title = $"HabboBOT - Connected[{bots.Count}]";
    }
}