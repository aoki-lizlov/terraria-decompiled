using System;
using System.IO;
using Terraria.Net;
using Terraria.Testing.ChatCommands;
using Terraria.UI.Chat;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020002E5 RID: 741
	public class NetDebugModule : NetModule
	{
		// Token: 0x0600263B RID: 9787 RVA: 0x0055E250 File Offset: 0x0055C450
		public static NetPacket Serialize(DebugMessage message)
		{
			NetPacket netPacket = NetModule.CreatePacket<NetDebugModule>(65530);
			message.Serialize(netPacket.Writer);
			return netPacket;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x0055E278 File Offset: 0x0055C478
		public override bool Deserialize(BinaryReader reader, int senderPlayerId)
		{
			DebugMessage debugMessage = DebugMessage.Deserialize((byte)senderPlayerId, reader);
			ChatManager.DebugCommands.Process(debugMessage);
			return true;
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x0055DC93 File Offset: 0x0055BE93
		public NetDebugModule()
		{
		}
	}
}
