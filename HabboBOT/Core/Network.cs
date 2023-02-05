using System;
using System.Threading.Tasks;

using Sulakore.Network;
using Sulakore.Cryptography;
using Sulakore.Network.Protocol;
using Sulakore.Cryptography.Ciphers;

using Habot.Headers;

namespace HabboBOT.Core
{
    internal class Network
    {
        public bool IsConnected => _hnode.IsConnected;
        public int Id => _session.Id;

        private string _hexKey;

        private readonly Random _random;
        private readonly Session _session;
        private readonly HKeyExchange _keyExchange;
        private HNode _hnode;

        public event EventHandler OnConnectionStarted;
        public event EventHandler<string> OnConnectionStopped;

        public Network(Session session)
        {
            _session = session;
            _random = new Random();
            _keyExchange = new HKeyExchange(65537, Config.PublicKey);
        }

        public async void SendPacket(Outgoing id, params object[] values) => await _hnode.SendAsync((ushort)id, values);

        public async void Connect()
        {
            try
            {
                _hexKey = GetRandomHexNumber();
                _hnode = await HNode.ConnectAsync(Config.SocketUrl, 30001);

                if (_hnode.IsConnected)
                {
                    _hnode.ReceiveFormat = HFormat.EvaWire;
                    _hnode.SendFormat = HFormat.EvaWire;
                    _hnode.IsWebSocket = true;

                    await _hnode.UpgradeWebSocketAsClientAsync();

                    if (_hnode.IsUpgraded)
                    {
                        SendPacket(Outgoing.Hello, _hexKey, "UNITY1", 0, 0);
                        SendPacket(Outgoing.InitDhHandshake);

                        await ConnectionHandlerAsync(await _hnode.ReceiveAsync());
                    }
                    else
                        OnConnectionStopped?.Invoke(this, "Unable to connect to websocket server.");
                }
                else
                    OnConnectionStopped?.Invoke(this, "Unable to upgrade websocket as client.");
            }
            catch (Exception e)
            {
                OnConnectionStopped?.Invoke(this, e.Message);
            }
        }

        private async Task ConnectionHandlerAsync(HPacket packet)
        {
            try
            {
                switch (packet.Id)
                {
                    case (ushort)Incoming.DhInitHandshake:
                        {
                            string p = packet.ReadUTF8();
                            string g = packet.ReadUTF8();
                            _keyExchange.VerifyDHPrimes(p, g);
                            _keyExchange.Padding = PKCSPadding.RandomByte;
                            SendPacket(Outgoing.CompleteDhHandshake, _keyExchange.GetPublicKey());
                            break;
                        }
                    case (ushort)Incoming.DhCompleteHandshake:
                        {
                            byte[] nonce = GetNonce(_hexKey);
                            byte[] sharedKey = _keyExchange.GetSharedKey(packet.ReadUTF8());
                            byte[] numArray = new byte[32];
                            Buffer.BlockCopy(sharedKey, 0, numArray, 0, sharedKey.Length);

                            _hnode.Decrypter = new ChaCha20(numArray, nonce);
                            _hnode.Encrypter = new ChaCha20(numArray, nonce);

                            SendPacket(Outgoing.GetIdentityAgreementTypes);
                            SendPacket(Outgoing.VersionCheck, 0, "0.17.0", "");
                            SendPacket(Outgoing.UniqueMachineId, GetRandomHexNumber(76).ToLower(), "n/a", "Chrome 88", "n/a");
                            SendPacket(Outgoing.LoginWithTicket, _session.SsoToken, 0);
                            break;
                        }
                    case (ushort)Incoming.Ping:
                        SendPacket(Outgoing.Pong);
                        break;
                    case (ushort)Incoming.Ok:
                        OnConnectionStarted?.Invoke(this, null);
                        break;
                }

                await ConnectionHandlerAsync(await _hnode.ReceiveAsync());
            }
            catch(Exception e)
            {
                OnConnectionStopped?.Invoke(this, e.Message);
            }
        }

        private string GetRandomHexNumber(int digits = 24)
        {
            byte[] buffer = new byte[digits / 2];
            _random.NextBytes(buffer);
            return BitConverter.ToString(buffer).Replace("-", "").Substring(0, digits);
        }

        private static byte[] GetNonce(string str)
        {
            string empty = string.Empty;
            for (int index = 0; index < 8; ++index)
                empty += str.Substring(index * 3, 2);
            return Convert.FromHexString(empty);
        }
    }
}