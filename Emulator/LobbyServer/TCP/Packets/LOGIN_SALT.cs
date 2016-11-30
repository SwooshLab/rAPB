﻿using LobbyServer.SRP;
using FrameWork.Logger;
using FrameWork.NetWork;

namespace LobbyServer.TCP.Packets
{
    static public class LOGIN_SALT
    {
        static public void Send(LobbyClient client)
        {
            Log.Debug("LOGIN_SALT", "Sent to " + client.Account.Username);
            client.serverModulus = Auth.computeServerModulus(client.Verifier);
            PacketOut Out = new PacketOut((uint)Opcodes.LOGIN_SALT);
            Out.WriteUInt32Reverse((uint)client.Account.Index);
            Out.Write(client.serverModulus.B.ToByteArrayUnsigned(), 0, 64);
            Out.WriteByte(0x40);
            Out.WriteByte(0);
            Out.Write(client.Salt, 0, 10);
            client.Send(Out);
        }
    }
}
