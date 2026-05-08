using System;
using Terraria.GameContent.UI;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005C2 RID: 1474
	[ChatCommand("RPS")]
	public class RockPaperScissorsCommand : IChatCommand
	{
		// Token: 0x06003A0D RID: 14861 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessIncomingMessage(string text, byte clientId)
		{
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x00654AC0 File Offset: 0x00652CC0
		public void ProcessOutgoingMessage(ChatMessage message)
		{
			if (Main.netMode != 2 && Main.LocalPlayer.dead)
			{
				message.Consume();
				return;
			}
			int num = Main.rand.NextFromList(new int[] { 37, 38, 36 });
			if (Main.netMode == 0)
			{
				EmoteBubble.NewBubble(num, new WorldUIAnchor(Main.LocalPlayer), 360);
				EmoteBubble.CheckForNPCsToReactToEmoteBubble(num, Main.LocalPlayer);
			}
			else
			{
				NetMessage.SendData(120, -1, -1, null, Main.myPlayer, (float)num, 0f, 0f, 0, 0, 0);
			}
			message.Consume();
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x0000357B File Offset: 0x0000177B
		public RockPaperScissorsCommand()
		{
		}
	}
}
