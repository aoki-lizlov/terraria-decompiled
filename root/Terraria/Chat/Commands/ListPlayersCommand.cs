using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005C5 RID: 1477
	[ChatCommand("Playing")]
	public class ListPlayersCommand : IChatCommand
	{
		// Token: 0x06003A1A RID: 14874 RVA: 0x00654D18 File Offset: 0x00652F18
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(string.Join(", ", from player in Main.player
				where player.active
				select player.name)), ListPlayersCommand.RESPONSE_COLOR, (int)clientId);
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x0000357B File Offset: 0x0000177B
		public ListPlayersCommand()
		{
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x00654D8C File Offset: 0x00652F8C
		// Note: this type is marked as 'beforefieldinit'.
		static ListPlayersCommand()
		{
		}

		// Token: 0x04005DCC RID: 24012
		private static readonly Color RESPONSE_COLOR = ChatColors.ServerMessage;

		// Token: 0x020009C9 RID: 2505
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004A52 RID: 19026 RVA: 0x006D40BC File Offset: 0x006D22BC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004A53 RID: 19027 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004A54 RID: 19028 RVA: 0x006D40C8 File Offset: 0x006D22C8
			internal bool <ProcessIncomingMessage>b__1_0(Player player)
			{
				return player.active;
			}

			// Token: 0x06004A55 RID: 19029 RVA: 0x006D40D0 File Offset: 0x006D22D0
			internal string <ProcessIncomingMessage>b__1_1(Player player)
			{
				return player.name;
			}

			// Token: 0x040076ED RID: 30445
			public static readonly ListPlayersCommand.<>c <>9 = new ListPlayersCommand.<>c();

			// Token: 0x040076EE RID: 30446
			public static Func<Player, bool> <>9__1_0;

			// Token: 0x040076EF RID: 30447
			public static Func<Player, string> <>9__1_1;
		}
	}
}
