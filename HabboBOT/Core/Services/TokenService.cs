using WebSocketSharp;
using WebSocketSharp.Server;

namespace HabboBOT.Core.Services
{
    internal class TokenService : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e) => AccountHandler.StartLogin(e.Data);
    }
}