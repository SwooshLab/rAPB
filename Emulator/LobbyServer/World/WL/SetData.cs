﻿using FrameWork.Logger;

namespace LobbyServer.World.WL
{
    class SetData : Packet, IPacket
    {
        public void Handle(World world)
        {
            Position = 0;
            byte IP1, IP2, IP3, IP4;
            IP1 = (byte)ReadByte();
            IP2 = (byte)ReadByte();
            IP3 = (byte)ReadByte();
            IP4 = (byte)ReadByte();
            uint port = ReadD();
            world.IP1 = IP1;
            world.IP2 = IP2;
            world.IP3 = IP3;
            world.IP4 = IP4;
            world.Port = port;
            Log.Succes(world.Name, "Address " + IP1 + "." + IP2 + "." + IP3 + "." + IP4 + ":" + port);
            lock (Program.WorldListener.Worlds)
            {
                if (Program.WorldListener.Worlds.ContainsKey(world.Id))
                {
                    Log.Error("SetData", "This world server is already registered!");
                    world.tcp.Close();
                }
                else Program.WorldListener.Worlds.Add(world.Id, world);
            }
        }
    }
}
