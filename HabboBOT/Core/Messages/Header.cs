using System.IO;
using System.Linq;

using Newtonsoft.Json;

namespace HabboBOT.Core.Messages
{
    public static class Header
    {
        private static readonly HeaderModel _headers;

        static Header()
        {
            if (_headers != null)
                return;

            _headers = JsonConvert.DeserializeObject<HeaderModel>(File.ReadAllText("messages.json"));
        }

        private static ushort GetHeader(string headerName, bool incoming) => incoming ? (ushort)_headers.Incoming.First(x => x.Name == headerName).Id : (ushort)_headers.Outgoing.First(x => x.Name == headerName).Id;

        public static ushort GetIncomingHeader(string headerName) => GetHeader(headerName, true);
        public static ushort GetOutgoingHeader(string headerName) => GetHeader(headerName, false);
    }
}