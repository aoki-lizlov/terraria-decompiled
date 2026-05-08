using System;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005C8 RID: 1480
	[ChatCommand("Say")]
	public class SayChatCommand : IChatCommand
	{
		// Token: 0x06003A27 RID: 14887 RVA: 0x00654E9E File Offset: 0x0065309E
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			ChatHelper.BroadcastChatMessageAs(clientId, NetworkText.FromLiteral(text), Main.player[(int)clientId].ChatColor(), -1);
			if (Main.dedServ)
			{
				Console.WriteLine("<{0}> {1}", Main.player[(int)clientId].name, text);
			}
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x0000357B File Offset: 0x0000177B
		public SayChatCommand()
		{
		}
	}
}
