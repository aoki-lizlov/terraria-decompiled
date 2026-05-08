using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000337 RID: 823
	public class BestiaryEntry
	{
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x005725AC File Offset: 0x005707AC
		// (set) Token: 0x06002836 RID: 10294 RVA: 0x005725B4 File Offset: 0x005707B4
		public List<IBestiaryInfoElement> Info
		{
			[CompilerGenerated]
			get
			{
				return this.<Info>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Info>k__BackingField = value;
			}
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x005725BD File Offset: 0x005707BD
		public BestiaryEntry()
		{
			this.Info = new List<IBestiaryInfoElement>();
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x005725D0 File Offset: 0x005707D0
		public static BestiaryEntry Enemy(int npcNetId)
		{
			NPC npc = ContentSamples.NpcsByNetId[npcNetId];
			List<IBestiaryInfoElement> list = new List<IBestiaryInfoElement>
			{
				new NPCNetIdBestiaryInfoElement(npcNetId),
				new NamePlateInfoElement(Lang.GetNPCName(npcNetId).Key, npcNetId),
				new NPCPortraitInfoElement(new int?(ContentSamples.NpcBestiaryRarityStars[npcNetId])),
				new NPCKillCounterInfoElement(npcNetId)
			};
			list.Add(new NPCStatsReportInfoElement(npcNetId));
			if (npc.rarity != 0)
			{
				list.Add(new RareSpawnBestiaryInfoElement(npc.rarity));
			}
			IBestiaryUICollectionInfoProvider bestiaryUICollectionInfoProvider;
			if (npc.boss || NPCID.Sets.ShouldBeCountedAsBossForBestiary[npc.type])
			{
				list.Add(new BossBestiaryInfoElement());
				bestiaryUICollectionInfoProvider = new CommonEnemyUICollectionInfoProvider(npc.GetBestiaryCreditId(), true);
			}
			else
			{
				bestiaryUICollectionInfoProvider = new CommonEnemyUICollectionInfoProvider(npc.GetBestiaryCreditId(), false);
			}
			string text = Lang.GetNPCName(npc.netID).Key;
			text = text.Replace("NPCName.", "");
			string text2 = "Bestiary_FlavorText.npc_" + text;
			if (Language.Exists(text2))
			{
				list.Add(new FlavorTextBestiaryInfoElement(text2));
			}
			return new BestiaryEntry
			{
				Icon = new UnlockableNPCEntryIcon(npcNetId, 0f, 0f, 0f, 0f, null),
				Info = list,
				UIInfoProvider = bestiaryUICollectionInfoProvider
			};
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x00572714 File Offset: 0x00570914
		public static BestiaryEntry TownNPC(int npcNetId)
		{
			NPC npc = ContentSamples.NpcsByNetId[npcNetId];
			List<IBestiaryInfoElement> list = new List<IBestiaryInfoElement>
			{
				new NPCNetIdBestiaryInfoElement(npcNetId),
				new NamePlateInfoElement(Lang.GetNPCName(npcNetId).Key, npcNetId),
				new NPCPortraitInfoElement(new int?(ContentSamples.NpcBestiaryRarityStars[npcNetId])),
				new NPCKillCounterInfoElement(npcNetId)
			};
			string text = Lang.GetNPCName(npc.netID).Key;
			text = text.Replace("NPCName.", "");
			string text2 = "Bestiary_FlavorText.npc_" + text;
			if (Language.Exists(text2))
			{
				list.Add(new FlavorTextBestiaryInfoElement(text2));
			}
			return new BestiaryEntry
			{
				Icon = new UnlockableNPCEntryIcon(npcNetId, 0f, 0f, 0f, 0f, null),
				Info = list,
				UIInfoProvider = new TownNPCUICollectionInfoProvider(npc.GetBestiaryCreditId())
			};
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x00572800 File Offset: 0x00570A00
		public static BestiaryEntry Critter(int npcNetId)
		{
			NPC npc = ContentSamples.NpcsByNetId[npcNetId];
			List<IBestiaryInfoElement> list = new List<IBestiaryInfoElement>
			{
				new NPCNetIdBestiaryInfoElement(npcNetId),
				new NamePlateInfoElement(Lang.GetNPCName(npcNetId).Key, npcNetId),
				new NPCPortraitInfoElement(new int?(ContentSamples.NpcBestiaryRarityStars[npcNetId])),
				new NPCKillCounterInfoElement(npcNetId)
			};
			string text = Lang.GetNPCName(npc.netID).Key;
			text = text.Replace("NPCName.", "");
			string text2 = "Bestiary_FlavorText.npc_" + text;
			if (Language.Exists(text2))
			{
				list.Add(new FlavorTextBestiaryInfoElement(text2));
			}
			return new BestiaryEntry
			{
				Icon = new UnlockableNPCEntryIcon(npcNetId, 0f, 0f, 0f, 0f, null),
				Info = list,
				UIInfoProvider = new CritterUICollectionInfoProvider(npc.GetBestiaryCreditId())
			};
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x005728E9 File Offset: 0x00570AE9
		public static BestiaryEntry Biome(string nameLanguageKey, string texturePath, Func<bool> unlockCondition)
		{
			return new BestiaryEntry
			{
				Icon = new CustomEntryIcon(nameLanguageKey, texturePath, unlockCondition),
				Info = new List<IBestiaryInfoElement>()
			};
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x00572909 File Offset: 0x00570B09
		public void AddTags(params IBestiaryInfoElement[] elements)
		{
			this.Info.AddRange(elements);
		}

		// Token: 0x04005127 RID: 20775
		public IEntryIcon Icon;

		// Token: 0x04005128 RID: 20776
		[CompilerGenerated]
		private List<IBestiaryInfoElement> <Info>k__BackingField;

		// Token: 0x04005129 RID: 20777
		public IBestiaryUICollectionInfoProvider UIInfoProvider;
	}
}
