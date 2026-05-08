using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005C6 RID: 1478
	[ChatCommand("Party")]
	public class PartyChatCommand : IChatCommand
	{
		// Token: 0x06003A1E RID: 14878 RVA: 0x00654D98 File Offset: 0x00652F98
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			int team = Main.player[(int)clientId].team;
			Color color = Main.teamColor[team];
			if (team == 0 || Main.netMode == 0)
			{
				this.SendNoTeamError(clientId);
				return;
			}
			if (text == "")
			{
				return;
			}
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].team == team)
				{
					ChatHelper.SendChatMessageToClientAs(clientId, NetworkText.FromLiteral(text), color, i);
				}
			}
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x00654E0B File Offset: 0x0065300B
		private void SendNoTeamError(byte clientId)
		{
			ChatHelper.SendChatMessageToClient(Lang.mp[10].ToNetworkText(), PartyChatCommand.ERROR_COLOR, (int)clientId);
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x0000357B File Offset: 0x0000177B
		public PartyChatCommand()
		{
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x00654E25 File Offset: 0x00653025
		// Note: this type is marked as 'beforefieldinit'.
		static PartyChatCommand()
		{
		}

		// Token: 0x04005DCD RID: 24013
		private static readonly Color ERROR_COLOR = ChatColors.ServerMessage;
	}
}
