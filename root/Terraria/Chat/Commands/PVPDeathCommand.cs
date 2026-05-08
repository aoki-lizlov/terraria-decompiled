using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005BE RID: 1470
	[ChatCommand("PVPDeath")]
	public class PVPDeathCommand : IChatCommand
	{
		// Token: 0x060039FB RID: 14843 RVA: 0x0065465C File Offset: 0x0065285C
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			NetworkText networkText = NetworkText.FromKey("LegacyMultiplayer.24", new object[]
			{
				Main.player[(int)clientId].name,
				Main.player[(int)clientId].numberOfDeathsPVP
			});
			if (Main.player[(int)clientId].numberOfDeathsPVP == 1)
			{
				networkText = NetworkText.FromKey("LegacyMultiplayer.26", new object[]
				{
					Main.player[(int)clientId].name,
					Main.player[(int)clientId].numberOfDeathsPVP
				});
			}
			ChatHelper.BroadcastChatMessage(networkText, PVPDeathCommand.RESPONSE_COLOR, -1);
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x0000357B File Offset: 0x0000177B
		public PVPDeathCommand()
		{
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x006546EC File Offset: 0x006528EC
		// Note: this type is marked as 'beforefieldinit'.
		static PVPDeathCommand()
		{
		}

		// Token: 0x04005DC5 RID: 24005
		private static readonly Color RESPONSE_COLOR = ChatColors.Death;
	}
}
