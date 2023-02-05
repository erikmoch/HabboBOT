using System.Net;
using System.Text;
using System.Net.Http;

namespace HabboBOT.Core
{
    internal class Session
    {
        public int Id { get; set; }
        public string SsoToken { get; private set; }

        private string CaptchaToken { get; set; }

        private readonly Account _account;
        private readonly Handler _handler;

        public Session(Account account, Handler handler, string captchaToken, int id)
        {
            _account = account;
            _handler = handler;

            Id = id;
            CaptchaToken = captchaToken;
        }

        public async void Connect()
        {
            try
            {
                using (HttpClient client = new())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "");
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

                            Network network = new(this);
                            _handler.AppendBot(network);
                        }
                    }
                }
            }
            catch { }
        }
    }
}