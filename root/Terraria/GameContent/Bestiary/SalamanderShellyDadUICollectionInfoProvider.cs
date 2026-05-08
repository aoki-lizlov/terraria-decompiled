using System;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200034A RID: 842
	public class SalamanderShellyDadUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x0600286E RID: 10350 RVA: 0x00573502 File Offset: 0x00571702
		public SalamanderShellyDadUICollectionInfoProvider(string persistentId)
		{
			this._persistentIdentifierToCheck = persistentId;
			this._killCountNeededToFullyUnlock = CommonEnemyUICollectionInfoProvider.GetKillCountNeeded(persistentId);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x00573520 File Offset: 0x00571720
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			BestiaryEntryUnlockState bestiaryEntryUnlockState = CommonEnemyUICollectionInfoProvider.GetUnlockStateByKillCount(Main.BestiaryTracker.Kills.GetKillCount(this._persistentIdentifierToCheck), false, this._killCountNeededToFullyUnlock);
			if (!this.IsIncludedInCurrentWorld())
			{
				bestiaryEntryUnlockState = this.GetLowestAvailableUnlockStateFromEntriesThatAreInWorld(bestiaryEntryUnlockState);
			}
			return new BestiaryUICollectionInfo
			{
				UnlockState = bestiaryEntryUnlockState
			};
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x00573570 File Offset: 0x00571770
		private BestiaryEntryUnlockState GetLowestAvailableUnlockStateFromEntriesThatAreInWorld(BestiaryEntryUnlockState unlockstatus)
		{
			BestiaryEntryUnlockState bestiaryEntryUnlockState = BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
			int[,] cavernMonsterType = NPC.cavernMonsterType;
			for (int i = 0; i < cavernMonsterType.GetLength(0); i++)
			{
				for (int j = 0; j < cavernMonsterType.GetLength(1); j++)
				{
					string text = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[cavernMonsterType[i, j]];
					BestiaryEntryUnlockState unlockStateByKillCount = CommonEnemyUICollectionInfoProvider.GetUnlockStateByKillCount(Main.BestiaryTracker.Kills.GetKillCount(text), false, this._killCountNeededToFullyUnlock);
					if (bestiaryEntryUnlockState > unlockStateByKillCount)
					{
						bestiaryEntryUnlockState = unlockStateByKillCount;
					}
				}
			}
			return bestiaryEntryUnlockState;
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x005735E8 File Offset: 0x005717E8
		private bool IsIncludedInCurrentWorld()
		{
			int num = ContentSamples.NpcNetIdsByPersistentIds[this._persistentIdentifierToCheck];
			int[,] cavernMonsterType = NPC.cavernMonsterType;
			for (int i = 0; i < cavernMonsterType.GetLength(0); i++)
			{
				for (int j = 0; j < cavernMonsterType.GetLength(1); j++)
				{
					if (ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[cavernMonsterType[i, j]] == this._persistentIdentifierToCheck)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x00076333 File Offset: 0x00074533
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04005148 RID: 20808
		private string _persistentIdentifierToCheck;

		// Token: 0x04005149 RID: 20809
		private int _killCountNeededToFullyUnlock;
	}
}
