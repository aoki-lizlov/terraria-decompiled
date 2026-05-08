using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005C3 RID: 1475
	[ChatCommand("Emote")]
	public class EmoteCommand : IChatCommand
	{
		// Token: 0x06003A10 RID: 14864 RVA: 0x00654B52 File Offset: 0x00652D52
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			if (text != "")
			{
				text = string.Format("*{0} {1}", Main.player[(int)clientId].name, text);
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), EmoteCommand.RESPONSE_COLOR, -1);
			}
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x0000357B File Offset: 0x0000177B
		public EmoteCommand()
		{
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x00654B8B File Offset: 0x00652D8B
		// Note: this type is marked as 'beforefieldinit'.
		static EmoteCommand()
		{
		}

		// Token: 0x04005DCA RID: 24010
		private static readonly Color RESPONSE_COLOR = new Color(200, 100, 0);
	}
}
