using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005BD RID: 1469
	[ChatCommand("Death")]
	public class DeathCommand : IChatCommand
	{
		// Token: 0x060039F7 RID: 14839 RVA: 0x006545C0 File Offset: 0x006527C0
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			NetworkText networkText = NetworkText.FromKey("LegacyMultiplayer.23", new object[]
			{
				Main.player[(int)clientId].name,
				Main.player[(int)clientId].numberOfDeathsPVE
			});
			if (Main.player[(int)clientId].numberOfDeathsPVE == 1)
			{
				networkText = NetworkText.FromKey("LegacyMultiplayer.25", new object[]
				{
					Main.player[(int)clientId].name,
					Main.player[(int)clientId].numberOfDeathsPVE
				});
			}
			ChatHelper.BroadcastChatMessage(networkText, DeathCommand.RESPONSE_COLOR, -1);
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x0000357B File Offset: 0x0000177B
		public DeathCommand()
		{
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x00654650 File Offset: 0x00652850
		// Note: this type is marked as 'beforefieldinit'.
		static DeathCommand()
		{
		}

		// Token: 0x04005DC4 RID: 24004
		private static readonly Color RESPONSE_COLOR = ChatColors.Death;
	}
}
