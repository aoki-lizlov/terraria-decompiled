using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x020005C0 RID: 1472
	[ChatCommand("AllPVPDeath")]
	public class AllPVPDeathCommand : IChatCommand
	{
		// Token: 0x06003A03 RID: 14851 RVA: 0x006547F8 File Offset: 0x006529F8
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			foreach (Player player in from x in Main.player
				where x != null && x.active
				orderby x.numberOfDeathsPVP descending
				select x)
			{
				NetworkText networkText = NetworkText.FromKey("LegacyMultiplayer.24", new object[] { player.name, player.numberOfDeathsPVP });
				if (player.numberOfDeathsPVP == 1)
				{
					networkText = NetworkText.FromKey("LegacyMultiplayer.26", new object[] { player.name, player.numberOfDeathsPVP });
				}
				ChatHelper.BroadcastChatMessage(networkText, AllPVPDeathCommand.RESPONSE_COLOR, -1);
			}
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x00009E46 File Offset: 0x00008046
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x0000357B File Offset: 0x0000177B
		public AllPVPDeathCommand()
		{
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x006548EC File Offset: 0x00652AEC
		// Note: this type is marked as 'beforefieldinit'.
		static AllPVPDeathCommand()
		{
		}

		// Token: 0x04005DC7 RID: 24007
		private static readonly Color RESPONSE_COLOR = ChatColors.Death;

		// Token: 0x020009C7 RID: 2503
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004A4C RID: 19020 RVA: 0x006D407D File Offset: 0x006D227D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004A4D RID: 19021 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004A4E RID: 19022 RVA: 0x006D4068 File Offset: 0x006D2268
			internal bool <ProcessIncomingMessage>b__1_0(Player x)
			{
				return x != null && x.active;
			}

			// Token: 0x06004A4F RID: 19023 RVA: 0x006D4089 File Offset: 0x006D2289
			internal int <ProcessIncomingMessage>b__1_1(Player x)
			{
				return x.numberOfDeathsPVP;
			}

			// Token: 0x040076E9 RID: 30441
			public static readonly AllPVPDeathCommand.<>c <>9 = new AllPVPDeathCommand.<>c();

			// Token: 0x040076EA RID: 30442
			public static Func<Player, bool> <>9__1_0;

			// Token: 0x040076EB RID: 30443
			public static Func<Player, int> <>9__1_1;
		}
	}
}
