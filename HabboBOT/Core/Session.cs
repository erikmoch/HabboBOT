using System;
using System.Net;
using System.Text;
using System.Net.Http;

namespace HabboBOT.Core
{
    internal class Session
    {
        public int Id { get; }
        private string CaptchaToken { get; }
        public string SsoToken { get; private set; }

        private readonly Account _account;

        public event EventHandler Authorized;
        public event EventHandler<string> Unauthorized;

        public Session(Account account, string captchaToken, int id)
        {
            _account = account;

            Id = id;
            CaptchaToken = captchaToken;
        }

        public async void Connect()
        {
            try
            {
                using (HttpClient client = new())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36");
                    client.DefaultRequestHeaders.Add("Host", Config.Host);
                    client.DefaultRequestHeaders.Add("Origin", Config.Origin);
                    client.DefaultRequestHeaders.Add("Referer", Config.Referer);

                    HttpResponseMessage authenticationResponse = await client.PostAsync(Config.AuthenticationApi, new StringContent("{\"captchaToken\":\"" + CaptchaToken + "\",\"email\":\"" + _account.Email + "\",\"password\":\"" + _account.Password + "\"}", Encoding.UTF8, "application/json"));
                    if (authenticationResponse.StatusCode == HttpStatusCode.OK)
                    {
                        HttpResponseMessage clientResponse = await client.PostAsync(Config.ClientUrlApi, new StringContent(""));
                        if (clientResponse.StatusCode == HttpStatusCode.OK)
                        {
                            string content = await clientResponse.Content.ReadAsStringAsync();
                            SsoToken = content[0..^2].Split('/')[6];
                            Authorized.Invoke(this, null);
                        }
                    }
                    else if (authenticationResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string content = await authenticationResponse.Content.ReadAsStringAsync();
                        string errorMessage = string.Empty;

                        if (content.Contains("invalid_password"))
                            errorMessage = "Wrong password.";
                        else if (content.Contains("user_banned"))
                            errorMessage = "Banned account.";
                        else if (content.Contains("invalid-captcha."))
                            errorMessage = "Invalid captcha token.";

                        Unauthorized.Invoke(this, errorMessage);
                    }
                }
            }
            catch { }
        }
    }
}