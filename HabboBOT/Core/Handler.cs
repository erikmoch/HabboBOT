using System;
using System.Collections.Generic;

namespace HabboBOT.Core
{
    public class Handler
    {
        public List<Network> bots;

        public Handler()
        {
            bots = new List<Network>();
        }

        public void AppendBot(Network network)
        {
            Writer.LogWarning($"[BOT {network.Id}] Connecting...");
            network.OnConnectionStarted += (object sender, EventArgs e) =>
            {
                bots.Add(network);
                Writer.LogSuccess($"[BOT {network.Id}] Connected.");

                UpdateConsoleTitle();
            };
            network.OnConnectionStopped += (object sender, EventArgs e) =>
            {
                bots.Remove(network);
                Writer.LogError($"[BOT {network.Id}] Disconnected.");

                UpdateConsoleTitle();
            };
            network.Connect();
        }

        private void UpdateConsoleTitle() => Console.Title = $"HabboBOT - Connected[{bots.Count}]";
    }
}