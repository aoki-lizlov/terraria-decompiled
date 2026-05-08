using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x0200023E RID: 574
	public class OneTimeDialogue : ConditionalDialogue
	{
		// Token: 0x06002298 RID: 8856 RVA: 0x005395D8 File Offset: 0x005377D8
		public OneTimeDialogue(string key, Predicate<NPC> condition = null)
			: base((NPC npc) => !Main.LocalPlayer.oneTimeDialoguesSeen.Contains(key) && (condition == null || condition(npc)))
		{
			this.ChatText = Language.GetText(key);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x00539628 File Offset: 0x00537828
		public override string GetChatAndClearCondition(NPC npc)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.oneTimeDialoguesSeen.Add(this.ChatText.Key);
			foreach (Item item in this.Rewards)
			{
				localPlayer.QuickSpawnItem(new EntitySource_Gift(npc), item, GetItemSettings.GiftRecieved);
			}
			return this.ChatText.Value;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x005396B0 File Offset: 0x005378B0
		public OneTimeDialogue WithReward(int itemId, int stack = 1)
		{
			Item item = new Item();
			item.SetDefaults(itemId, null);
			item.stack = stack;
			return this.WithRewards(new Item[] { item });
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x005396E2 File Offset: 0x005378E2
		public OneTimeDialogue WithRewards(params Item[] rewards)
		{
			this.Rewards.AddRange(rewards);
			return this;
		}

		// Token: 0x04004D06 RID: 19718
		public readonly LocalizedText ChatText;

		// Token: 0x04004D07 RID: 19719
		public readonly List<Item> Rewards = new List<Item>();

		// Token: 0x020007C9 RID: 1993
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x0600421B RID: 16923 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x0600421C RID: 16924 RVA: 0x006BDD19 File Offset: 0x006BBF19
			internal bool <.ctor>b__0(NPC npc)
			{
				return !Main.LocalPlayer.oneTimeDialoguesSeen.Contains(this.key) && (this.condition == null || this.condition(npc));
			}

			// Token: 0x040070EF RID: 28911
			public string key;

			// Token: 0x040070F0 RID: 28912
			public Predicate<NPC> condition;
		}
	}
}
