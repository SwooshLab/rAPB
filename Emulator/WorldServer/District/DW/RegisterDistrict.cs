﻿using FrameWork.Logger;
using FrameWork;
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using WorldServer.Districts.WD;
using MyDB;

namespace WorldServer.Districts.DW
{
    class RegisterDistrict : Packet, IPacket
    {
        public RegisterDistrict() : base() { }

        public void Handle(District district)
        {
            Position = 0;
            Byte type = (Byte)ReadByte();
            Byte ID = (Byte)ReadByte();
            LanguageCodes language = (LanguageCodes)ReadByte();
            TcpClient client = district.tcp;
            String regpass = ReadS();
            String IP = ReadS();
            String Port = ReadS();
            String Token = ReadS();
            switch ((DistrictTypes)type)
            {
                case DistrictTypes.FINANCIAL:
                    district = new FinancialDistrict(ID, language);
                    break;
                case DistrictTypes.FINANCIAL_HARDCORE:
                    district = new FinancialDistrict(ID, language, true);
                    break;
                case DistrictTypes.SOCIAL:
                    district = new SocialDistrict(ID);
                    break;
                case DistrictTypes.TUTORIAL:
                    district = new TutorialDistrict(ID);
                    break;
                case DistrictTypes.WATERFRONT:
                    district = new WaterFrontDistrict(ID, language);
                    break;
                case DistrictTypes.WATERFRONT_HARDCORE:
                    district = new WaterFrontDistrict(ID, language, true);
                    break;
            }

            district.tcp = client;
            Log.Info("RegisterDistrict", district.ToString() + " tries to register.");
            if (ID != 0)
            {
                AccountEntry acc = Databases.AccountTable.SingleOrDefault(a => a.Token == Token);
                if(acc.CanHostDistrict == 0 || acc.Index < 1)
                {
                    district.Send(new MessageInfo("You can not host a district instance!"));
                    district.tcp.Client.Disconnect(true);
                    return;
                }
                district.Id = ID;
                district.IP = IP;
                district.Port = Convert.ToUInt16(Port);
                lock (Program.districtsListener.Districts)
                {
                    UInt32 code = (UInt32)(district.Type << 24);
                    code += district.Id;
                    foreach (KeyValuePair<UInt32, District> dist in Program.districtsListener.Districts)
                    {
                        if (dist.Value.IP == IP && Program.districtsListener.Districts.ContainsKey(code))
                        {
                            Program.districtsListener.Districts.Remove(code);
                            break;
                        }
                        else if (dist.Value.IP != IP && Program.districtsListener.Districts.ContainsKey(code))
                        {
                            Log.Error("RegisterDistrict", "Fail try of district registration that already exists!");
                            break;
                        }
                    }
                    Program.districtsListener.Districts.Add(code, district);
                    Program.districtsListener.DistrictsTcp.Add(district.tcp, code);
                }
                Log.Succes("RegisterDistrict", district + " was registered! (" + IP + ":" + Port + ")");
                
                district.Send(new MessageInfo("Token check complete. You are allowed to host a district!"));
            }
            else
            {
                district.Send(new MessageInfo("Invalid ID! Please choose an ID that's not 0."));
            }
        }
    }
}
