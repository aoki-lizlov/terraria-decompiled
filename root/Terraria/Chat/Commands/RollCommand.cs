using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005C7 RID: 1479
	[ChatCommand("Roll")]
	public class RollCommand : IChatCommand
	{
		// Token: 0x06003A23 RID: 14883 RVA: 0x00654E34 File Offset: 0x00653034
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			int num = Main.rand.Next(1, 101);
			ChatHelper.BroadcastChatMessage(NetworkText.FromFormattable("*{0} {1} {2}", new object[]
			{
				Main.player[(int)clientId].name,
				Lang.mp[9].ToNetworkText(),
				num
			}), RollCommand.RESPONSE_COLOR, -1);
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x0000357B File Offset: 0x0000177B
		public RollCommand()
		{
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x00654E92 File Offset: 0x00653092
		// Note: this type is marked as 'beforefieldinit'.
		static RollCommand()
		{
		}

		// Token: 0x04005DCE RID: 24014
		private static readonly Color RESPONSE_COLOR = ChatColors.ServerMessage;
	}
}
