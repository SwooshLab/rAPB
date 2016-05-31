﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameWork.NetWork;
using MyDB;

namespace LobbyServer
{
    static public class ANS_CHARACTER_INFO
    {
        static public void Send(LobbyClient client)
        {
            client.Characters = Databases.CharacterTable.Select(c => c.AccountIndex == client.Account.Index);
            PacketOut Out = new PacketOut((UInt32)Opcodes.CHARACTER_LIST);
            Out.WriteByte((Byte)client.Characters.Count);
            lock (Program.worldListener.Worlds)
            {
                foreach (CharacterEntry chr in client.Characters)
                {
                    Out.WriteByte(chr.Slot);
                    Out.WriteByte(chr.Faction);
                    Out.WriteByte(1);
                    Out.WriteUInt32Reverse((uint)chr.World);
                    World.World info = null;
                    Program.worldListener.Worlds.TryGetValue((uint)chr.World, out info);
                    if (info != null) Out.WriteParsedString(info.Name, 32);
                    else Out.WriteParsedString("(undefined)", 32);
                    Out.WriteParsedString(chr.Name, 32);
                }
            }
            client.Send(Out);
            if (client.Characters.Count <= 0) WORLD_LIST.Send(client);
        }
    }
}
