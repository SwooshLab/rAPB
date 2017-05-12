﻿using System;

[Flags]
public enum Opcodes : uint
{
    //DONE - GC2LS
    ASK_LOGIN = 0x3E8,
    LOGIN_PROOF = 0x3ED,
    ASK_CHARACTER_INFO = 0x3EE,
    ASK_WORLD_LIST = 0x3EF,
    ASK_CHARACTER_NAME_CHECK = 0x3F0,
    ASK_CHARACTER_NAME_CHANGE = 0x3F1,
    ASK_CHARACTER_CREATE = 0x3F2,
    ASK_CHARACTER_DELETE = 0x3F3,
    ASK_WORLD_ENTER = 0x3F4,
    ASK_CONFIGFILE_LOAD = 0x3F5,
    ASK_CONFIGFILE_SAVE = 0x3F6,

    //DONE - LS2GC
    ERROR = 0x7D0,
    KICK = 0x7D1,
    LOGIN_PUZZLE = 0x7D2,
    LOGIN_SALT = 0x7D3,
    ANS_LOGIN_SUCCESS = 0x7D4,
    ANS_LOGIN_FAILED = 0x7D5,
    CHARACTER_LIST = 0x7D6,
    ANS_CHARACTER_INFO = 0x7D7,
    WORLD_LIST = 0x7D8,
    ANS_CHARACTER_NAME_CHECK = 0x7D9,
    ANS_CHARACTER_NAME_CHANGE = 0x7DA,
    ANS_CHARACTER_CREATE = 0x7DB,
    ANS_CHARACTER_DELETE = 0x7DC,
    ANS_WORLD_ENTER = 0x7DD,
    ANS_CONFIGFILE_LOAD = 0x7DF,
    ANS_CONFIGFILE_SAVE = 0x7E0,
    
    //NOT DONE - GC2LS
    ASK_NUM_ADDITIONAL_CHARACTER_SLOTS = 0x3F7,
    KEY_EXCHANGE = 0x3F8,
    HARDWARE_INFO = 0x3F9,
    ASK_SSO_TOKEN = 0x3FB,
    ASK_PREMIUM_STATUS = 0x3FC,
    TICK_TOGGLE_LOGIN_POPUP = 0x402,

    //NOT DONE - LS2GC
    WORLD_STATUS = 0x7DE,
    ANS_NUM_ADDITIONAL_CHARACTER_SLOTS = 0x7E1,
    ANS_SSO_TOKEN = 0x7E2,
    ANS_PREMIUM_STATUS = 0x7E4,
    WMI_REQUEST = 0x7E5,
};