﻿namespace MyDB
{
    [DatabaseTable("accounts")]
    public struct AccountEntry
    {
        public int Index;
        public string Username;
        public string Password;
        public string Verifier;
        public string Salt;
        public byte Threat;
        public byte IsAdmin;
        public byte IsBanned;
        public byte InUse;
        public byte CanHostDistrict;
        public string Token;
    }
}
