using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020002E3 RID: 739
	public class NetPingModule : NetModule
	{
		// Token: 0x06002634 RID: 9780 RVA: 0x0055E030 File Offset: 0x0055C230
		public static NetPacket Serialize(Vector2 position)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetPingModule>(65530);
			netPacket.Writer.WriteVector2(position);
			return netPacket;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x0055E058 File Offset: 0x0055C258
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			Vector2 vector = reader.ReadVector2();
			if (Main.dedServ)
			{
				NetManager.Instance.Broadcast(NetPingModule.Serialize(vector), userId);
			}
			else
			{
				Main.Pings.Add(vector);
			}
			return true;
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x0055DC93 File Offset: 0x0055BE93
		public NetPingModule()
		{
		}
	}
}
