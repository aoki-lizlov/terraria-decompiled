using System;
using System.IO;
using Terraria.GameContent.Creative;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020002E1 RID: 737
	public class NetCreativePowerPermissionsModule : NetModule
	{
		// Token: 0x0600262E RID: 9774 RVA: 0x0055DF28 File Offset: 0x0055C128
		public static NetPacket SerializeCurrentPowerPermissionLevel(ushort powerId, int level)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetCreativePowerPermissionsModule>(65530);
			netPacket.Writer.Write(0);
			netPacket.Writer.Write(powerId);
			netPacket.Writer.Write((byte)level);
			return netPacket;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x0055DF6C File Offset: 0x0055C16C
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			if (reader.ReadByte() == 0)
			{
				ushort num = reader.ReadUInt16();
				int num2 = (int)reader.ReadByte();
				if (Main.netMode == 2)
				{
					return false;
				}
				ICreativePower creativePower;
				if (!CreativePowerManager.Instance.TryGetPower(num, out creativePower))
				{
					return false;
				}
				creativePower.CurrentPermissionLevel = (PowerPermissionLevel)num2;
			}
			return true;
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x0055DC93 File Offset: 0x0055BE93
		public NetCreativePowerPermissionsModule()
		{
		}

		// Token: 0x04005067 RID: 20583
		private const byte _setPermissionLevelId = 0;
	}
}
