using System;
using System.Linq;
using System.Buffers;
using System.Threading.Tasks;
using System.Collections.Generic;

using Sulakore.Network;
using Sulakore.Cryptography;
using Sulakore.Network.Protocol;
using Sulakore.Cryptography.Ciphers;

using HabboBOT.Core.Messages;

namespace HabboBOT.Core
{
    public class Network
    {
        public bool IsConnected => _hnode.IsConnected;
        public int Id => _session.Id;

        private string _hexKey;

        private readonly Random _random;
        private readonly Session _session;
        private readonly HKeyExchange _keyExchange;
        private HNode _hnode;

        public event EventHandler OnConnectionStarted;
        public event EventHandler OnConnectionStopped;

        public Network(Session session)
        {
            _session = session;
            _random = new Random();
            _keyExchange = new HKeyExchange(65537, Globals.modulus);
        }

        public async void SendPacket(ushort id, params object[] values) => await _hnode.SendAsync(id, values);

        public async void Connect()
        {
            try
            {
                _hexKey = GetRandomHexNumber();
                _hnode = await HNode.ConnectAsync(Globals.Socket_Url, 30001);

                if (_hnode.IsConnected)
                {
                    _hnode.ReceiveFormat = HFormat.EvaWire;
                    _hnode.SendFormat = HFormat.EvaWire;
                    _hnode.IsWebSocket = true;

                    await _hnode.UpgradeWebSocketAsClientAsync();

                    if (_hnode.IsUpgraded)
                    {
                        await _hnode.SendAsync(Header.GetOutgoingHeader("Hello"), _hexKey, "UNITY1", 0, 0);
                        await _hnode.SendAsync(Header.GetOutgoingHeader("InitDhHandshake"));

                        await HandlePacketAsync(await _hnode.ReceiveAsync());
                    }
                    else
                        Disconnected();
                }
                else
                    Disconnected();
            }
            catch
            {
                Disconnected();
            }
        }

        private async Task HandlePacketAsync(HPacket packet)
        {
            try
            {
                if (packet.Id == Header.GetIncomingHeader("DhInitHandshake"))
                    await VerifyPrimesAsync(packet.ReadUTF8(), packet.ReadUTF8());
                else if (packet.Id == Header.GetIncomingHeader("DhCompleteHandshake"))
                    await EncryptConnection(packet.ReadUTF8());
                else if (packet.Id == Header.GetIncomingHeader("Ping"))
                    await _hnode.SendAsync(Header.GetOutgoingHeader("Pong"));
                else if (packet.Id == Header.GetIncomingHeader("Ok"))
                    Connected();

                await HandlePacketAsync(await _hnode.ReceiveAsync());
            }
            catch
            {
                Disconnected();
            }
        }

        private async Task SendConnectionPackets()
        {
            await _hnode.SendAsync(Header.GetOutgoingHeader("GetIdentityAgreementTypes"));
            await _hnode.SendAsync(Header.GetOutgoingHeader("VersionCheck"), 0, "0.17.0", "");
            await _hnode.SendAsync(Header.GetOutgoingHeader("UniqueMachineId"), GetRandomHexNumber(76).ToLower(), "n/a", "Chrome 88", "n/a");
            await _hnode.SendAsync(Header.GetOutgoingHeader("LoginWithTicket"), _session.SsoToken, 0);
        }

        private async Task EncryptConnection(string key)
        {
            byte[] nonce = GetNonce(_hexKey);
            byte[] numArray = new byte[32];
            byte[] sharedKey = _keyExchange.GetSharedKey(key);
            Buffer.BlockCopy(sharedKey, 0, numArray, 0, sharedKey.Length);

            _hnode.Decrypter = new ChaCha20(numArray, nonce);
            _hnode.Encrypter = new ChaCha20(numArray, nonce);
            await SendConnectionPackets();
        }

        private async Task VerifyPrimesAsync(string p, string g)
        {
            _keyExchange.VerifyDHPrimes(p, g);
            _keyExchange.Padding = PKCSPadding.RandomByte;
            await _hnode.SendAsync(Header.GetOutgoingHeader("CompleteDhHandshake"), _keyExchange.GetPublicKey());
        }

        private string GetRandomHexNumber(int digits = 24)
        {
            byte[] buffer = new byte[digits / 2];
            _random.NextBytes(buffer);
            string str = string.Concat(((IEnumerable<byte>)buffer).Select(x => x.ToString("X2")).ToArray());

            return digits % 2 == 0 ? str : str + _random.Next(16).ToString("X");
        }

        private static byte[] GetNonce(string str)
        {
            string empty = string.Empty;
            for (int index = 0; index < 8; ++index)
                empty += str.Substring(index * 3, 2);
            return Convert.FromHexString(empty);
        }

        private void Disconnected() => OnConnectionStopped?.Invoke(this, EventArgs.Empty);
        private void Connected() => OnConnectionStarted?.Invoke(this, null);
    }
}