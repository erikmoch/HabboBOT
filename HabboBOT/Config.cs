namespace HabboBOT
{
    public class Config
    {
        public enum Hotels
        {
            BR,
            ES,
            US,
            FR,
            NL,
            IT,
            DE,
            FI,
            TR
        };
        public static void SetGlobals(Hotels hotel)
        {
            switch (hotel)
            {
                case Hotels.BR:
                    Globals.Socket_Url = "game-br.habbo.com";

                    Globals.Authentication_Api = "https://www.habbo.com.br/api/public/authentication/login";
                    Globals.Clienturl_Api = "https://www.habbo.com.br/api/client/clientv2url";
                    Globals.User_Api = "https://www.habbo.com.br/api/public/users?name=";

                    Globals.Host = "www.habbo.com.br";
                    Globals.Origin = "https://www.habbo.com.br";
                    Globals.Referer = "https://www.habbo.com.br/hotelv2";

                    Globals.Domain = "habbo.com.br";
                    break;

                case Hotels.ES:
                    Globals.Socket_Url = "game-es.habbo.com";

                    Globals.Authentication_Api = "https://www.habbo.es/api/public/authentication/login";
                    Globals.Clienturl_Api = "https://www.habbo.es/api/client/clientv2url";
                    Globals.User_Api = "https://www.habbo.es/api/public/users?name=";

                    Globals.Host = "www.habbo.es";
                    Globals.Origin = "https://www.habbo.es";
                    Globals.Referer = "https://www.habbo.es/hotelv2";

                    Globals.Domain = "habbo.es";
                    break;

                case Hotels.US:
                    Globals.Socket_Url = "game-us.habbo.com";

                    Globals.Authentication_Api = "https://www.habbo.com/api/public/authentication/login";
                    Globals.Clienturl_Api = "https://www.habbo.com/api/client/clientv2url";
                    Globals.User_Api = "https://www.habbo.com/api/public/users?name=";

                    Globals.Host = "www.habbo.com";
                    Globals.Origin = "https://www.habbo.com";
                    Globals.Referer = "https://www.habbo.com/hotelv2";

                    Globals.Domain = "habbo.com";
                    break;

                case Hotels.FR:
                    Globals.Socket_Url = "game-fr.habbo.com";

                    Globals.Authentication_Api = "https://www.habbo.fr/api/public/authentication/login";
                    Globals.Clienturl_Api = "https://www.habbo.fr/api/client/clientv2url";
                    Globals.User_Api = "https://www.habbo.fr/api/public/users?name=";

                    Globals.Host = "www.habbo.fr";
                    Globals.Origin = "https://www.habbo.fr";
                    Globals.Referer = "https://www.habbo.fr/hotelv2";

                    Globals.Domain = "habbo.fr";
                    break;

                case Hotels.NL:
                    Globals.Socket_Url = "game-nl.habbo.com";

                    Globals.Authentication_Api = "https://www.habbo.nl/api/public/authentication/login";
                    Globals.Clienturl_Api = "https://www.habbo.nl/api/client/clientv2url";
                    Globals.User_Api = "https://www.habbo.nl/api/public/users?name=";

                    Globals.Host = "www.habbo.nl";
                    Globals.Origin = "https://www.habbo.nl";
                    Globals.Referer = "https://www.habbo.nl/hotelv2";

                    Globals.Domain = "habbo.nl";
                    break;

                case Hotels.IT:
                    Globals.Socket_Url = "game-it.habbo.com";

                    Globals.Authentication_Api = "https://www.habbo.it/api/public/authentication/login";
                    Globals.Clienturl_Api = "https://www.habbo.it/api/client/clientv2url";
                    Globals.User_Api = "https://www.habbo.it/api/public/users?name=";

                    Globals.Host = "www.habbo.it";
                    Globals.Origin = "https://www.habbo.it";
                    Globals.Referer = "https://www.habbo.it/hotelv2";

                    Globals.Domain = "habbo.it";
                    break;

                case Hotels.DE:
                    Globals.Socket_Url = "game-de.habbo.com";

                    Globals.Authentication_Api = "https://www.habbo.de/api/public/authentication/login";
                    Globals.Clienturl_Api = "https://www.habbo.de/api/client/clientv2url";
                    Globals.User_Api = "https://www.habbo.de/api/public/users?name=";

                    Globals.Host = "www.habbo.de";
                    Globals.Origin = "https://www.habbo.de";
                    Globals.Referer = "https://www.habbo.de/hotelv2";

                    Globals.Domain = "habbo.de";
                    break;

                case Hotels.FI:
                    Globals.Socket_Url = "game-fi.habbo.com";

                    Globals.Authentication_Api = "https://www.habbo.fi/api/public/authentication/login";
                    Globals.Clienturl_Api = "https://www.habbo.fi/api/client/clientv2url";
                    Globals.User_Api = "https://www.habbo.fi/api/public/users?name=";

                    Globals.Host = "www.habbo.fi";
                    Globals.Origin = "https://www.habbo.fi";
                    Globals.Referer = "https://www.habbo.fi/hotelv2";

                    Globals.Domain = "habbo.fi";
                    break;

                case Hotels.TR:
                    Globals.Socket_Url = "game-tr.habbo.com";

                    Globals.Authentication_Api = "https://www.habbo.com.tr/api/public/authentication/login";
                    Globals.Clienturl_Api = "https://www.habbo.com.tr/api/client/clientv2url";
                    Globals.User_Api = "https://www.habbo.com.tr/api/public/users?name=";

                    Globals.Host = "www.habbo.com.tr";
                    Globals.Origin = "https://www.habbo.com.tr";
                    Globals.Referer = "https://www.habbo.com.tr/hotelv2";

                    Globals.Domain = "habbo.tr";
                    break;
            }
        }
    }
}