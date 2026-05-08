using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x0200023D RID: 573
	public abstract class ConditionalDialogue
	{
		// Token: 0x0600228C RID: 8844 RVA: 0x00539480 File Offset: 0x00537680
		private static void Register(int npcType, ConditionalDialogue dialogue)
		{
			List<ConditionalDialogue> list = ConditionalDialogue._registry[npcType];
			if (list == null)
			{
				list = (ConditionalDialogue._registry[npcType] = new List<ConditionalDialogue>());
			}
			list.Add(dialogue);
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x005394B0 File Offset: 0x005376B0
		public static bool TryGetPendingDialogue(NPC npc, out ConditionalDialogue dialogue)
		{
			dialogue = null;
			List<ConditionalDialogue> list = ConditionalDialogue._registry[npc.type];
			if (list == null)
			{
				return false;
			}
			foreach (ConditionalDialogue conditionalDialogue in list)
			{
				if (conditionalDialogue.ConditionsMet(npc))
				{
					dialogue = conditionalDialogue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600228E RID: 8846 RVA: 0x00539524 File Offset: 0x00537724
		// (set) Token: 0x0600228F RID: 8847 RVA: 0x0053952C File Offset: 0x0053772C
		public bool ShowIndicator
		{
			[CompilerGenerated]
			get
			{
				return this.<ShowIndicator>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ShowIndicator>k__BackingField = value;
			}
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x00539535 File Offset: 0x00537735
		public ConditionalDialogue(Predicate<NPC> condition = null)
		{
			this.ShowIndicator = true;
			Predicate<NPC> predicate = condition;
			if (condition == null && (predicate = ConditionalDialogue.<>c.<>9__9_0) == null)
			{
				predicate = (ConditionalDialogue.<>c.<>9__9_0 = (NPC _) => true);
			}
			this.ConditionsMet = predicate;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0053956E File Offset: 0x0053776E
		public void HideIndicator()
		{
			this.ShowIndicator = false;
		}

		// Token: 0x06002292 RID: 8850
		public abstract string GetChatAndClearCondition(NPC npc);

		// Token: 0x06002293 RID: 8851 RVA: 0x00539577 File Offset: 0x00537777
		public void Register(int npcType)
		{
			ConditionalDialogue.Register(npcType, this);
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x00539580 File Offset: 0x00537780
		internal static void Init()
		{
			new ConditionalDialogue.FreeCakeDialogue().Register(208);
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x00539591 File Offset: 0x00537791
		public static Predicate<NPC> CreateInventoryCondition(RecipeGroup item, int stack)
		{
			return ConditionalDialogue.CreateInventoryCondition(new Recipe.RequiredItemEntry[]
			{
				new Recipe.RequiredItemEntry(item, stack)
			});
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x005395AC File Offset: 0x005377AC
		public static Predicate<NPC> CreateInventoryCondition(params Recipe.RequiredItemEntry[] requiredItems)
		{
			return (NPC _) => Recipe.CollectedEnoughItemsToCraft(requiredItems);
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x005395C5 File Offset: 0x005377C5
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalDialogue()
		{
		}

		// Token: 0x04004D03 RID: 19715
		private static List<ConditionalDialogue>[] _registry = new List<ConditionalDialogue>[(int)NPCID.Count];

		// Token: 0x04004D04 RID: 19716
		[CompilerGenerated]
		private bool <ShowIndicator>k__BackingField;

		// Token: 0x04004D05 RID: 19717
		public readonly Predicate<NPC> ConditionsMet;

		// Token: 0x020007C5 RID: 1989
		public static class ItemGroups
		{
			// Token: 0x06004212 RID: 16914 RVA: 0x006BDAFC File Offset: 0x006BBCFC
			internal static void PostSetupContent()
			{
				foreach (Item item in ContentSamples.ItemsByType.Values)
				{
					if (ProjectileID.Sets.IsAWhip[item.shoot])
					{
						ConditionalDialogue.ItemGroups.Whips.Add(item.type, null);
					}
				}
				foreach (Item item2 in ContentSamples.ItemsByType.Values)
				{
					if (item2.mountType != -1)
					{
						ConditionalDialogue.ItemGroups.Mounts.Add(item2.type, null);
					}
				}
			}

			// Token: 0x06004213 RID: 16915 RVA: 0x006BDBC8 File Offset: 0x006BBDC8
			// Note: this type is marked as 'beforefieldinit'.
			static ItemGroups()
			{
			}

			// Token: 0x040070E7 RID: 28903
			public static RecipeGroup Ore = new RecipeGroup("RecipeGroups.Ore", new int[] { 699, 12, 11, 700, 14, 701, 13, 702 });

			// Token: 0x040070E8 RID: 28904
			public static RecipeGroup Bars = new RecipeGroup("RecipeGroups.Bar", new int[] { 703, 20, 22, 704, 21, 705, 19, 706 });

			// Token: 0x040070E9 RID: 28905
			public static RecipeGroup Anvils = new RecipeGroup("ItemName.IronAnvil", new int[] { 35, 716 });

			// Token: 0x040070EA RID: 28906
			public static RecipeGroup Whips = new RecipeGroup("RecipeGroups.Whip", new int[0]);

			// Token: 0x040070EB RID: 28907
			public static RecipeGroup Mounts = new RecipeGroup("RecipeGroups.Mount", new int[0]);
		}

		// Token: 0x020007C6 RID: 1990
		private class FreeCakeDialogue : ConditionalDialogue
		{
			// Token: 0x06004214 RID: 16916 RVA: 0x006BDC61 File Offset: 0x006BBE61
			public FreeCakeDialogue()
				: base((NPC _) => NPC.freeCake)
			{
			}

			// Token: 0x06004215 RID: 16917 RVA: 0x006BDC88 File Offset: 0x006BBE88
			public override string GetChatAndClearCondition(NPC npc)
			{
				NPC.freeCake = false;
				NetMessage.SendData(51, -1, -1, null, 0, 10f, 0f, 0f, 0, 0, 0);
				Item item = new Item();
				item.SetDefaults(3750, null);
				Main.LocalPlayer.QuickSpawnItem(new EntitySource_Gift(npc), item, GetItemSettings.GiftRecieved);
				return Language.GetTextValue("PartyGirlSpecialText.Cake" + Main.rand.Next(1, 4));
			}

			// Token: 0x02000AB8 RID: 2744
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004C24 RID: 19492 RVA: 0x006DA827 File Offset: 0x006D8A27
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004C25 RID: 19493 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004C26 RID: 19494 RVA: 0x006DA833 File Offset: 0x006D8A33
				internal bool <.ctor>b__0_0(NPC _)
				{
					return NPC.freeCake;
				}

				// Token: 0x04007877 RID: 30839
				public static readonly ConditionalDialogue.FreeCakeDialogue.<>c <>9 = new ConditionalDialogue.FreeCakeDialogue.<>c();

				// Token: 0x04007878 RID: 30840
				public static Predicate<NPC> <>9__0_0;
			}
		}

		// Token: 0x020007C7 RID: 1991
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004216 RID: 16918 RVA: 0x006BDD00 File Offset: 0x006BBF00
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004217 RID: 16919 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004218 RID: 16920 RVA: 0x000379E9 File Offset: 0x00035BE9
			internal bool <.ctor>b__9_0(NPC _)
			{
				return true;
			}

			// Token: 0x040070EC RID: 28908
			public static readonly ConditionalDialogue.<>c <>9 = new ConditionalDialogue.<>c();

			// Token: 0x040070ED RID: 28909
			public static Predicate<NPC> <>9__9_0;
		}

		// Token: 0x020007C8 RID: 1992
		[CompilerGenerated]
		private sealed class <>c__DisplayClass15_0
		{
			// Token: 0x06004219 RID: 16921 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass15_0()
			{
			}

			// Token: 0x0600421A RID: 16922 RVA: 0x006BDD0C File Offset: 0x006BBF0C
			internal bool <CreateInventoryCondition>b__0(NPC _)
			{
				return Recipe.CollectedEnoughItemsToCraft(this.requiredItems);
			}

			// Token: 0x040070EE RID: 28910
			public Recipe.RequiredItemEntry[] requiredItems;
		}
	}
}
