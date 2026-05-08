using System;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000347 RID: 839
	public class CommonEnemyUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x06002861 RID: 10337 RVA: 0x0057323C File Offset: 0x0057143C
		public CommonEnemyUICollectionInfoProvider(string persistentId, bool quickUnlock)
		{
			this._persistentIdentifierToCheck = persistentId;
			this._quickUnlock = quickUnlock;
			this._killCountNeededToFullyUnlock = CommonEnemyUICollectionInfoProvider.GetKillCountNeeded(persistentId);
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x00573260 File Offset: 0x00571460
		public static int GetKillCountNeeded(string persistentId)
		{
			int defaultKillsForBannerNeeded = ItemID.Sets.DefaultKillsForBannerNeeded;
			int num;
			if (!ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(persistentId, out num))
			{
				return defaultKillsForBannerNeeded;
			}
			NPC npc;
			if (!ContentSamples.NpcsByNetId.TryGetValue(num, out npc))
			{
				return defaultKillsForBannerNeeded;
			}
			int num2 = BannerSystem.BannerToItem(BannerSystem.NPCtoBanner(npc.BannerID()));
			return ItemID.Sets.KillsToBanner[num2];
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x005732B0 File Offset: 0x005714B0
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			int killCount = Main.BestiaryTracker.Kills.GetKillCount(this._persistentIdentifierToCheck);
			BestiaryEntryUnlockState unlockStateByKillCount = this.GetUnlockStateByKillCount(killCount, this._quickUnlock);
			return new BestiaryUICollectionInfo
			{
				UnlockState = unlockStateByKillCount
			};
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x005732F4 File Offset: 0x005714F4
		public BestiaryEntryUnlockState GetUnlockStateByKillCount(int killCount, bool quickUnlock)
		{
			int killCountNeededToFullyUnlock = this._killCountNeededToFullyUnlock;
			return CommonEnemyUICollectionInfoProvider.GetUnlockStateByKillCount(killCount, quickUnlock, killCountNeededToFullyUnlock);
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x00573310 File Offset: 0x00571510
		public static BestiaryEntryUnlockState GetUnlockStateByKillCount(int killCount, bool quickUnlock, int fullKillCountNeeded)
		{
			int num = fullKillCountNeeded / 2;
			int num2 = fullKillCountNeeded / 5;
			BestiaryEntryUnlockState bestiaryEntryUnlockState;
			if (quickUnlock && killCount > 0)
			{
				bestiaryEntryUnlockState = BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
			}
			else if (killCount >= fullKillCountNeeded)
			{
				bestiaryEntryUnlockState = BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
			}
			else if (killCount >= num)
			{
				bestiaryEntryUnlockState = BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3;
			}
			else if (killCount >= num2)
			{
				bestiaryEntryUnlockState = BestiaryEntryUnlockState.CanShowStats_2;
			}
			else if (killCount >= 1)
			{
				bestiaryEntryUnlockState = BestiaryEntryUnlockState.CanShowPortraitOnly_1;
			}
			else
			{
				bestiaryEntryUnlockState = BestiaryEntryUnlockState.NotKnownAtAll_0;
			}
			return bestiaryEntryUnlockState;
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x00076333 File Offset: 0x00074533
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04005141 RID: 20801
		private string _persistentIdentifierToCheck;

		// Token: 0x04005142 RID: 20802
		private bool _quickUnlock;

		// Token: 0x04005143 RID: 20803
		private int _killCountNeededToFullyUnlock;
	}
}
