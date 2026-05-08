using System;
using Terraria.ID;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000349 RID: 841
	public class GoldCritterUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x0600286A RID: 10346 RVA: 0x005733D0 File Offset: 0x005715D0
		public GoldCritterUICollectionInfoProvider(int[] normalCritterPersistentId, string goldCritterPersistentId)
		{
			this._normalCritterPersistentId = new string[normalCritterPersistentId.Length];
			for (int i = 0; i < normalCritterPersistentId.Length; i++)
			{
				this._normalCritterPersistentId[i] = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[normalCritterPersistentId[i]];
			}
			this._goldCritterPersistentId = goldCritterPersistentId;
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x0057341C File Offset: 0x0057161C
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			BestiaryEntryUnlockState unlockStateForCritter = this.GetUnlockStateForCritter(this._goldCritterPersistentId);
			BestiaryEntryUnlockState bestiaryEntryUnlockState = BestiaryEntryUnlockState.NotKnownAtAll_0;
			if (unlockStateForCritter > bestiaryEntryUnlockState)
			{
				bestiaryEntryUnlockState = unlockStateForCritter;
			}
			foreach (string text in this._normalCritterPersistentId)
			{
				BestiaryEntryUnlockState unlockStateForCritter2 = this.GetUnlockStateForCritter(text);
				if (unlockStateForCritter2 > bestiaryEntryUnlockState)
				{
					bestiaryEntryUnlockState = unlockStateForCritter2;
				}
			}
			BestiaryUICollectionInfo bestiaryUICollectionInfo = new BestiaryUICollectionInfo
			{
				UnlockState = bestiaryEntryUnlockState
			};
			if (bestiaryEntryUnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return bestiaryUICollectionInfo;
			}
			if (!this.TryFindingOneGoldCritterThatIsAlreadyUnlocked())
			{
				return new BestiaryUICollectionInfo
				{
					UnlockState = BestiaryEntryUnlockState.NotKnownAtAll_0
				};
			}
			return bestiaryUICollectionInfo;
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x005734A4 File Offset: 0x005716A4
		private bool TryFindingOneGoldCritterThatIsAlreadyUnlocked()
		{
			for (int i = 0; i < NPCID.Sets.GoldCrittersCollection.Count; i++)
			{
				int num = NPCID.Sets.GoldCrittersCollection[i];
				string text = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[num];
				if (this.GetUnlockStateForCritter(text) > BestiaryEntryUnlockState.NotKnownAtAll_0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x005734EB File Offset: 0x005716EB
		private BestiaryEntryUnlockState GetUnlockStateForCritter(string persistentId)
		{
			if (!Main.BestiaryTracker.Sights.GetWasNearbyBefore(persistentId))
			{
				return BestiaryEntryUnlockState.NotKnownAtAll_0;
			}
			return BestiaryEntryUnlockState.CanShowDropsWithDropRates_4;
		}

		// Token: 0x04005146 RID: 20806
		private string[] _normalCritterPersistentId;

		// Token: 0x04005147 RID: 20807
		private string _goldCritterPersistentId;
	}
}
