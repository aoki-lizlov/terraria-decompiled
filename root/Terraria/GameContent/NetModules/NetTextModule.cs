using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.Net;
using Terraria.UI.Chat;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020002E6 RID: 742
	public class NetTextModule : NetModule
	{
		// Token: 0x0600263E RID: 9790 RVA: 0x0055E29C File Offset: 0x0055C49C
		public static NetPacket SerializeClientMessage(ChatMessage message)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetTextModule>(65530);
			message.Serialize(netPacket.Writer);
			return netPacket;
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x0055E2C2 File Offset: 0x0055C4C2
		public static NetPacket SerializeServerMessage(NetworkText text, Color color)
		{
			return NetTextModule.SerializeServerMessage(text, color, byte.MaxValue);
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x0055E2D0 File Offset: 0x0055C4D0
		public static NetPacket SerializeServerMessage(NetworkText text, Color color, byte authorId)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetTextModule>(65530);
			netPacket.Writer.Write(authorId);
			text.Serialize(netPacket.Writer);
			netPacket.Writer.WriteRGB(color);
			return netPacket;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x0055E310 File Offset: 0x0055C510
		private bool DeserializeAsClient(BinaryReader reader, int senderPlayerId)
		{
			byte b = reader.ReadByte();
			NetworkText networkText = NetworkText.Deserialize(reader);
			Color color = reader.ReadRGB();
			ChatHelper.DisplayMessage(networkText, color, b);
			return true;
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x0055E33C File Offset: 0x0055C53C
		private bool DeserializeAsServer(BinaryReader reader, int senderPlayerId)
		{
			ChatMessage chatMessage = ChatMessage.Deserialize(reader);
			ChatManager.Commands.ProcessIncomingMessage(chatMessage, senderPlayerId);
			return true;
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x0055E35D File Offset: 0x0055C55D
		public override bool Deserialize(BinaryReader reader, int senderPlayerId)
		{
			if (Main.dedServ)
			{
				return this.DeserializeAsServer(reader, senderPlayerId);
			}
			return this.DeserializeAsClient(reader, senderPlayerId);
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x0055DC93 File Offset: 0x0055BE93
		public NetTextModule()
		{
		}
	}
}
