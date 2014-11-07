﻿using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ContactHandler
    {
        [Parser(Opcode.CMSG_ADD_IGNORE)]
        [Parser(Opcode.CMSG_ADD_MUTE)]
        public static void HandleAddIgnoreOrMute(Packet packet)
        {
            var bits9 = packet.ReadBits(9);
            packet.ReadWoWString("Name", bits9);
        }

        [Parser(Opcode.CMSG_ADD_FRIEND)]
        public static void HandleAddFriend(Packet packet)
        {
            var bits16 = packet.ReadBits(9);
            var bits10 = packet.ReadBits(10);

            packet.ReadWoWString("Name", bits16);
            packet.ReadWoWString("Notes", bits10);
        }

        [Parser(Opcode.CMSG_DEL_FRIEND)]
        [Parser(Opcode.CMSG_DEL_IGNORE)]
        [Parser(Opcode.CMSG_DEL_MUTE)]
        public static void HandleDeleteFriendOrIgnoreOrMute(Packet packet)
        {
            // QualifiedGUID
            packet.ReadInt32("VirtualRealmAddress");
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_CONTACT_STATUS)]
        public static void HandleContactStatus(Packet packet)
        {
            packet.ReadByte("FriendResult");

            packet.ReadPackedGuid128("Guid");
            packet.ReadPackedGuid128("WowAccount");

            packet.ReadInt32("VirtualRealmAddress");

            packet.ReadEnum<ContactStatus>("Status", TypeCode.Byte);

            packet.ReadEntry<Int32>(StoreNameType.Area, "AreaID");
            packet.ReadInt32("Level");
            packet.ReadEnum<Class>("ClassID", TypeCode.Int32);

            packet.ResetBitReader();

            var bits28 = packet.ReadBits(10);
            packet.ReadWoWString("Notes", bits28);
        }
    }
}