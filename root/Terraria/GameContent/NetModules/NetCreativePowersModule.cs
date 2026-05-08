using System;
using System.IO;
using Terraria.GameContent.Creative;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020002DF RID: 735
	public class NetCreativePowersModule : NetModule
	{
		// Token: 0x06002628 RID: 9768 RVA: 0x0055DE0C File Offset: 0x0055C00C
		public static NetPacket PreparePacket(ushort powerId, int specificInfoBytesInPacketCount)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetCreativePowersModule>(65530);
			netPacket.Writer.Write(powerId);
			return netPacket;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x0055DE34 File Offset: 0x0055C034
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			ushort num = reader.ReadUInt16();
			ICreativePower creativePower;
			if (!CreativePowerManager.Instance.TryGetPower(num, out creativePower))
			{
				return false;
			}
			creativePower.DeserializeNetMessage(reader, userId);
			return true;
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x0055DC93 File Offset: 0x0055BE93
		public NetCreativePowersModule()
		{
		}
	}
}
