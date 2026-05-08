using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Terraria.ID;

namespace Terraria.GameContent.Creative
{
	// Token: 0x0200032B RID: 811
	public class ItemsSacrificedUnlocksTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x005695B1 File Offset: 0x005677B1
		// (set) Token: 0x060027D7 RID: 10199 RVA: 0x005695B9 File Offset: 0x005677B9
		public int LastEditId
		{
			[CompilerGenerated]
			get
			{
				return this.<LastEditId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<LastEditId>k__BackingField = value;
			}
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x005695C2 File Offset: 0x005677C2
		public void DismissNewlyUnlockedFromTeamMatesIcon()
		{
			this.AnyNewUnlocksFromTeammates = false;
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x005695CB File Offset: 0x005677CB
		public ItemsSacrificedUnlocksTracker()
		{
			this._sacrificeCountByItemPersistentId = new Dictionary<string, int>();
			this._sacrificesCountByItemIdCache = new Dictionary<int, int>();
			this._unlockedByTeammate = new Dictionary<int, string>();
			this._newlyUnlocked = new HashSet<int>();
			this.LastEditId = 0;
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x00569608 File Offset: 0x00567808
		public int GetSacrificeCount(int itemId)
		{
			int num;
			if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out num))
			{
				itemId = num;
			}
			int num2;
			this._sacrificesCountByItemIdCache.TryGetValue(itemId, out num2);
			return num2;
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x00569638 File Offset: 0x00567838
		public void ForEachItemWithResearchProgress(Action<int> action)
		{
			foreach (KeyValuePair<int, int> keyValuePair in this._sacrificesCountByItemIdCache)
			{
				if (keyValuePair.Value > 0)
				{
					action(keyValuePair.Key);
				}
			}
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x0056969C File Offset: 0x0056789C
		public void CountFullyResearchedItems(out int fullyResearchedItems, out int allItems)
		{
			fullyResearchedItems = 0;
			allItems = 0;
			for (int i = 0; i < (int)ItemID.Count; i++)
			{
				int num;
				int num2;
				if (this.TryGetSacrificeNumbers(i, out num, out num2))
				{
					allItems++;
					if (num >= num2)
					{
						fullyResearchedItems++;
					}
				}
			}
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x005696DC File Offset: 0x005678DC
		public bool TryGetSacrificeNumbers(int itemId, out int amountWeHave, out int amountNeededTotal)
		{
			int num;
			if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out num))
			{
				itemId = num;
			}
			amountWeHave = (amountNeededTotal = 0);
			if (!CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(itemId, out amountNeededTotal))
			{
				return false;
			}
			this._sacrificesCountByItemIdCache.TryGetValue(itemId, out amountWeHave);
			return true;
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x00569724 File Offset: 0x00567924
		public bool IsFullyResearched(int itemId)
		{
			int num;
			int num2;
			return this.TryGetSacrificeNumbers(itemId, out num, out num2) && num >= num2;
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x00569747 File Offset: 0x00567947
		public bool IsNewlyResearched(int itemId)
		{
			return this._newlyUnlocked.Contains(itemId);
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x00569755 File Offset: 0x00567955
		public void ClearNewlyResearchedStatus(int itemId)
		{
			this._newlyUnlocked.Remove(itemId);
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x00569764 File Offset: 0x00567964
		public bool TryGetTeammateUnlockCredit(int itemId, out string teammateName)
		{
			return this._unlockedByTeammate.TryGetValue(itemId, out teammateName);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x00569774 File Offset: 0x00567974
		public void RegisterItemSacrifice(int itemId, int amount, string teammateName = null)
		{
			int num;
			if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(itemId, out num))
			{
				itemId = num;
			}
			string text;
			if (!ContentSamples.ItemPersistentIdsByNetIds.TryGetValue(itemId, out text))
			{
				return;
			}
			int num2;
			if (!CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(itemId, out num2))
			{
				return;
			}
			int num3;
			this._sacrificeCountByItemPersistentId.TryGetValue(text, out num3);
			if (num3 >= num2)
			{
				return;
			}
			num3 = Math.Min(num3 + amount, num2);
			this._sacrificeCountByItemPersistentId[text] = num3;
			this._sacrificesCountByItemIdCache[itemId] = num3;
			this.MarkContentsDirty();
			if (num3 >= num2)
			{
				this._newlyUnlocked.Add(itemId);
				if (teammateName != null)
				{
					this.AnyNewUnlocksFromTeammates = true;
					this._unlockedByTeammate[itemId] = teammateName;
				}
			}
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x0056981C File Offset: 0x00567A1C
		public void SetSacrificeCountDirectly(string persistentId, int sacrificeCount)
		{
			int num = Utils.Clamp<int>(sacrificeCount, 0, 9999);
			this._sacrificeCountByItemPersistentId[persistentId] = num;
			int num2;
			if (!ContentSamples.ItemNetIdsByPersistentIds.TryGetValue(persistentId, out num2))
			{
				return;
			}
			this._sacrificesCountByItemIdCache[num2] = num;
			this.MarkContentsDirty();
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x00569868 File Offset: 0x00567A68
		public void Save(BinaryWriter writer)
		{
			writer.Write(false);
			Dictionary<string, int> dictionary = new Dictionary<string, int>(this._sacrificeCountByItemPersistentId);
			writer.Write(dictionary.Count);
			foreach (KeyValuePair<string, int> keyValuePair in dictionary)
			{
				writer.Write(keyValuePair.Key);
				writer.Write(keyValuePair.Value);
			}
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x005698E8 File Offset: 0x00567AE8
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			if (gameVersionSaveWasMadeOn >= 282)
			{
				reader.ReadBoolean();
			}
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadString();
				int num2 = reader.ReadInt32();
				int num3;
				if (ContentSamples.ItemNetIdsByPersistentIds.TryGetValue(text, out num3))
				{
					int num4;
					if (ContentSamples.CreativeResearchItemPersistentIdOverride.TryGetValue(num3, out num4))
					{
						num3 = num4;
					}
					this._sacrificesCountByItemIdCache[num3] = num2;
					string text2;
					if (ContentSamples.ItemPersistentIdsByNetIds.TryGetValue(num3, out text2))
					{
						text = text2;
					}
				}
				this._sacrificeCountByItemPersistentId[text] = num2;
			}
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x00569978 File Offset: 0x00567B78
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadString();
				reader.ReadInt32();
			}
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x005699A6 File Offset: 0x00567BA6
		public void Reset()
		{
			this._sacrificeCountByItemPersistentId.Clear();
			this._sacrificesCountByItemIdCache.Clear();
			this.AnyNewUnlocksFromTeammates = false;
			this._unlockedByTeammate.Clear();
			this._newlyUnlocked.Clear();
			this.MarkContentsDirty();
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x00009E46 File Offset: 0x00008046
		public void OnPlayerJoining(int playerIndex)
		{
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x005699E4 File Offset: 0x00567BE4
		public void MarkContentsDirty()
		{
			int lastEditId = this.LastEditId;
			this.LastEditId = lastEditId + 1;
		}

		// Token: 0x0400510C RID: 20748
		public const int POSITIVE_SACRIFICE_COUNT_CAP = 9999;

		// Token: 0x0400510D RID: 20749
		private Dictionary<string, int> _sacrificeCountByItemPersistentId;

		// Token: 0x0400510E RID: 20750
		private Dictionary<int, int> _sacrificesCountByItemIdCache;

		// Token: 0x0400510F RID: 20751
		private Dictionary<int, string> _unlockedByTeammate;

		// Token: 0x04005110 RID: 20752
		private HashSet<int> _newlyUnlocked;

		// Token: 0x04005111 RID: 20753
		public bool AnyNewUnlocksFromTeammates;

		// Token: 0x04005112 RID: 20754
		[CompilerGenerated]
		private int <LastEditId>k__BackingField;
	}
}
