using System;
using System.IO;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020002E0 RID: 736
	public class NetCreativeUnlocksPlayerReportModule : NetModule
	{
		// Token: 0x0600262B RID: 9771 RVA: 0x0055DE64 File Offset: 0x0055C064
		public static NetPacket SerializeSacrificeRequest(int userId, int itemId, int amount)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetCreativeUnlocksPlayerReportModule>(65530);
			netPacket.Writer.Write((byte)userId);
			netPacket.Writer.Write((ushort)itemId);
			netPacket.Writer.Write((ushort)amount);
			return netPacket;
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x0055DEA8 File Offset: 0x0055C0A8
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			int num = (int)reader.ReadByte();
			int num2 = (int)reader.ReadUInt16();
			int num3 = (int)reader.ReadUInt16();
			if (Main.dedServ)
			{
				NetManager.Instance.Broadcast(NetCreativeUnlocksPlayerReportModule.SerializeSacrificeRequest(userId, num2, num3), userId);
				return true;
			}
			Player player = Main.player[num];
			if (Main.LocalPlayer.team > 0 && Main.LocalPlayer.team == player.team)
			{
				Main.LocalPlayerCreativeTracker.ItemSacrifices.RegisterItemSacrifice(num2, num3, player.name);
			}
			return true;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x0055DC93 File Offset: 0x0055BE93
		public NetCreativeUnlocksPlayerReportModule()
		{
		}
	}
}
