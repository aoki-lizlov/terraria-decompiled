using System;
using System.IO;
using Terraria.DataStructures;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020002E4 RID: 740
	public class NetTeleportPylonModule : NetModule
	{
		// Token: 0x06002637 RID: 9783 RVA: 0x0055E094 File Offset: 0x0055C294
		public static NetPacket SerializePylonWasAddedOrRemoved(TeleportPylonInfo info, NetTeleportPylonModule.SubPacketType packetType)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetTeleportPylonModule>(65530);
			netPacket.Writer.Write((byte)packetType);
			netPacket.Writer.Write(info.PositionInTiles.X);
			netPacket.Writer.Write(info.PositionInTiles.Y);
			netPacket.Writer.Write((byte)info.TypeOfPylon);
			return netPacket;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x0055E0FC File Offset: 0x0055C2FC
		public static NetPacket SerializeUseRequest(TeleportPylonInfo info)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetTeleportPylonModule>(65530);
			netPacket.Writer.Write(2);
			netPacket.Writer.Write(info.PositionInTiles.X);
			netPacket.Writer.Write(info.PositionInTiles.Y);
			netPacket.Writer.Write((byte)info.TypeOfPylon);
			return netPacket;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x0055E164 File Offset: 0x0055C364
		public override bool Deserialize(BinaryReader reader, int userId)
		{
			switch (reader.ReadByte())
			{
			case 0:
			{
				if (Main.dedServ)
				{
					return false;
				}
				TeleportPylonInfo teleportPylonInfo = default(TeleportPylonInfo);
				teleportPylonInfo.PositionInTiles = new Point16(reader.ReadInt16(), reader.ReadInt16());
				teleportPylonInfo.TypeOfPylon = (TeleportPylonType)reader.ReadByte();
				Main.PylonSystem.AddForClient(teleportPylonInfo);
				break;
			}
			case 1:
			{
				if (Main.dedServ)
				{
					return false;
				}
				TeleportPylonInfo teleportPylonInfo2 = default(TeleportPylonInfo);
				teleportPylonInfo2.PositionInTiles = new Point16(reader.ReadInt16(), reader.ReadInt16());
				teleportPylonInfo2.TypeOfPylon = (TeleportPylonType)reader.ReadByte();
				Main.PylonSystem.RemoveForClient(teleportPylonInfo2);
				break;
			}
			case 2:
			{
				TeleportPylonInfo teleportPylonInfo3 = default(TeleportPylonInfo);
				teleportPylonInfo3.PositionInTiles = new Point16(reader.ReadInt16(), reader.ReadInt16());
				teleportPylonInfo3.TypeOfPylon = (TeleportPylonType)reader.ReadByte();
				Main.PylonSystem.HandleTeleportRequest(teleportPylonInfo3, userId);
				break;
			}
			}
			return true;
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x0055DC93 File Offset: 0x0055BE93
		public NetTeleportPylonModule()
		{
		}

		// Token: 0x02000827 RID: 2087
		public enum SubPacketType : byte
		{
			// Token: 0x0400725E RID: 29278
			PylonWasAdded,
			// Token: 0x0400725F RID: 29279
			PylonWasRemoved,
			// Token: 0x04007260 RID: 29280
			PlayerRequestsTeleport
		}
	}
}
