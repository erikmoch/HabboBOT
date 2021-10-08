using System.Net;
using System.Text;
using System.Net.Http;

namespace HabboBOT.Core
{
    public class Session
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
                    client.DefaultRequestHeaders.Add("User-Agent", Globals.user_agent);
                    client.DefaultRequestHeaders.Add("Host", Globals.Host);
                    client.DefaultRequestHeaders.Add("Origin", Globals.Origin);
                    client.DefaultRequestHeaders.Add("Referer", Globals.Referer);

                    HttpResponseMessage authenticationResponse = await client.PostAsync(Globals.Authentication_Api, new StringContent("{\"captchaToken\":\"" + CaptchaToken + "\",\"email\":\"" + _account.Email + "\",\"password\":\"" + _account.Password + "\"}", Encoding.UTF8, "application/json"));
                    if (authenticationResponse.StatusCode == HttpStatusCode.OK)
                    {
                        HttpResponseMessage clientResponse = await client.PostAsync(Globals.Clienturl_Api, new StringContent(""));
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