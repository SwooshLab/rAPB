﻿using FrameWork.NetWork;
using System.Collections.Generic;

namespace LobbyServer
{
    [PacketHandlerAttribute(PacketHandlerType.TCP, (int)Opcodes.ASK_WORLD_LIST, "onAskWorldList")]
    public class WORLD_LIST : IPacketHandler
    {
        public int HandlePacket(BaseClient client, PacketIn packet)
        {
            LobbyClient cclient = client as LobbyClient;
            Send(cclient);
            return 0;
        }

        public static void Send(LobbyClient cclient)
        {
            PacketOut Out = new PacketOut((uint)Opcodes.WORLD_LIST);
            Out.WriteInt32Reverse((int)ResponseCodes.RC_SUCCESS);
            lock (Program.WorldListener.Worlds)
            {
                Out.WriteUInt16Reverse((ushort)Program.WorldListener.Worlds.Count);
                foreach (KeyValuePair<uint, World.World> info in Program.WorldListener.Worlds)
                {
                    Out.WriteUInt32Reverse(info.Key);
                    Out.WriteParsedString(info.Value.Name, 32);
                    Out.WriteByte((byte)info.Value.Id);
                    Out.WriteByte(0); //m_nPopulation %d
                    Out.WriteByte(0); //m_nEnfFaction %d
                    Out.WriteByte(0); //m_nCrimFaction %d
                    Out.WriteByte(0); //m_nPremiumOnly %d
                    Out.WriteByte(info.Value.IP1);
                    Out.WriteByte(info.Value.IP2);
                    Out.WriteByte(info.Value.IP3);
                    Out.WriteByte(info.Value.IP4);
                }
            }
            cclient.Send(Out);
        }
    }
}
