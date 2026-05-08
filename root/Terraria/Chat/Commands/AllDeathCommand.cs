using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005BF RID: 1471
	[ChatCommand("AllDeath")]
	public class AllDeathCommand : IChatCommand
	{
		// Token: 0x060039FF RID: 14847 RVA: 0x006546F8 File Offset: 0x006528F8
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			foreach (Player player in from x in Main.player
				where x != null && x.active
				orderby x.numberOfDeathsPVE descending
				select x)
			{
				NetworkText networkText = NetworkText.FromKey("LegacyMultiplayer.23", new object[] { player.name, player.numberOfDeathsPVE });
				if (player.numberOfDeathsPVE == 1)
				{
					networkText = NetworkText.FromKey("LegacyMultiplayer.25", new object[] { player.name, player.numberOfDeathsPVE });
				}
				ChatHelper.BroadcastChatMessage(networkText, AllDeathCommand.RESPONSE_COLOR, -1);
			}
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x0000357B File Offset: 0x0000177B
		public AllDeathCommand()
		{
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x006547EC File Offset: 0x006529EC
		// Note: this type is marked as 'beforefieldinit'.
		static AllDeathCommand()
		{
		}

		// Token: 0x04005DC6 RID: 24006
		private static readonly Color RESPONSE_COLOR = ChatColors.Death;

		// Token: 0x020009C6 RID: 2502
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004A48 RID: 19016 RVA: 0x006D405C File Offset: 0x006D225C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004A49 RID: 19017 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004A4A RID: 19018 RVA: 0x006D4068 File Offset: 0x006D2268
			internal bool <ProcessIncomingMessage>b__1_0(Player x)
			{
				return x != null && x.active;
			}

			// Token: 0x06004A4B RID: 19019 RVA: 0x006D4075 File Offset: 0x006D2275
			internal int <ProcessIncomingMessage>b__1_1(Player x)
			{
				return x.numberOfDeathsPVE;
			}

			// Token: 0x040076E6 RID: 30438
			public static readonly AllDeathCommand.<>c <>9 = new AllDeathCommand.<>c();

			// Token: 0x040076E7 RID: 30439
			public static Func<Player, bool> <>9__1_0;

			// Token: 0x040076E8 RID: 30440
			public static Func<Player, int> <>9__1_1;
		}
	}
}
