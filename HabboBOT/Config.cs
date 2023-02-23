using System;
using System.IO;
using Newtonsoft.Json;

namespace HabboBOT
{
    internal class Config
    {
        public static string PublicKey { get; private set; }
        public static string SocketUrl { get; private set; }
        public static string ClientVersion { get; private set; }
        public static string AuthenticationApi { get; private set; }
        public static string ClientUrlApi { get; private set; }
        public static string Host { get; private set; }
        public static string Origin { get; private set; }
        public static string Referer { get; private set; }

        public static void Load(string path = "Configuration.json")
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                dynamic jsonData = JsonConvert.DeserializeObject(json);

                PublicKey = jsonData.PublicKey;
                SocketUrl = jsonData.SocketUrl;
                ClientVersion = jsonData.ClientVersion;
                AuthenticationApi = jsonData.AuthenticationApi;
                ClientUrlApi = jsonData.ClientUrlApi;
                Host = jsonData.Host;
                Origin = jsonData.Origin;
                Referer = jsonData.Referer;
            }
            else
            {
                LogWriter.LogError("Could not find a configuration file. Creating a new one...");
                Create();
                if (File.Exists("Configuration.json"))
                    LogWriter.LogSuccess("Done!");
            }
        }

        public static void Create()
        {
            try
            {
                dynamic data = new { PublicKey = "", SocketUrl = "", ClientVersion = "", AuthenticationApi = "", ClientUrlApi = "", Host = "", Origin = "", Referer = "" };
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText("Configuration.json", json);
            }
            catch (Exception e)
            {
                LogWriter.LogError(e.Message);
            }
        }
    }
}