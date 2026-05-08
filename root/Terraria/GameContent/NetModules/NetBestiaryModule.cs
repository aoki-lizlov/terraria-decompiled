using System;
using System.IO;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020002DE RID: 734
	public class NetBestiaryModule : NetModule
	{
		// Token: 0x06002623 RID: 9763 RVA: 0x0055DC9C File Offset: 0x0055BE9C
		public static NetPacket SerializeKillCount(int npcNetId, int killcount)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetBestiaryModule>(65530);
			netPacket.Writer.Write(0);
			netPacket.Writer.Write((short)npcNetId);
			netPacket.Writer.Write7BitEncodedInt(killcount);
			return netPacket;
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x0055DCE0 File Offset: 0x0055BEE0
		public static NetPacket SerializeSight(int npcNetId)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetBestiaryModule>(65530);
			netPacket.Writer.Write(1);
			netPacket.Writer.Write((short)npcNetId);
			return netPacket;
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x0055DD14 File Offset: 0x0055BF14
		public static NetPacket SerializeChat(int npcNetId)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetBestiaryModule>(65530);
			netPacket.Writer.Write(2);
			netPacket.Writer.Write((short)npcNetId);
			return netPacket;
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x0055DD48 File Offset: 0x0055BF48
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			if (Main.dedServ)
			{
				return false;
			}
			switch (reader.ReadByte())
			{
			case 0:
			{
				short num = reader.ReadInt16();
				string bestiaryCreditId = ContentSamples.NpcsByNetId[(int)num].GetBestiaryCreditId();
				int num2 = reader.Read7BitEncodedInt();
				Main.BestiaryTracker.Kills.SetKillCountDirectly(bestiaryCreditId, num2);
				break;
			}
			case 1:
			{
				short num3 = reader.ReadInt16();
				string bestiaryCreditId2 = ContentSamples.NpcsByNetId[(int)num3].GetBestiaryCreditId();
				Main.BestiaryTracker.Sights.SetWasSeenDirectly(bestiaryCreditId2);
				break;
			}
			case 2:
			{
				short num4 = reader.ReadInt16();
				string bestiaryCreditId3 = ContentSamples.NpcsByNetId[(int)num4].GetBestiaryCreditId();
				Main.BestiaryTracker.Chats.SetWasChatWithDirectly(bestiaryCreditId3);
				break;
			}
			}
			return true;
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x0055DC93 File Offset: 0x0055BE93
		public NetBestiaryModule()
		{
		}

		// Token: 0x02000826 RID: 2086
		private enum BestiaryUnlockType : byte
		{
			// Token: 0x0400725A RID: 29274
			Kill,
			// Token: 0x0400725B RID: 29275
			Sight,
			// Token: 0x0400725C RID: 29276
			Chat
		}
	}
}
