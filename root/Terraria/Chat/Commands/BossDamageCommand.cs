using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005BC RID: 1468
	[ChatCommand("BossDamage")]
	public class BossDamageCommand : IChatCommand
	{
		// Token: 0x060039F3 RID: 14835 RVA: 0x00654538 File Offset: 0x00652738
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			foreach (NPCDamageTracker npcdamageTracker in NPCDamageTracker.RecentAttempts())
			{
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active)
					{
						ChatHelper.SendChatMessageToClient(npcdamageTracker.GetReport(Main.player[i]), BossDamageCommand.RESPONSE_COLOR, i);
					}
				}
			}
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x0000357B File Offset: 0x0000177B
		public BossDamageCommand()
		{
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x006545B4 File Offset: 0x006527B4
		// Note: this type is marked as 'beforefieldinit'.
		static BossDamageCommand()
		{
		}

		// Token: 0x04005DC3 RID: 24003
		private static readonly Color RESPONSE_COLOR = ChatColors.World;
	}
}
