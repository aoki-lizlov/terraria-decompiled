using System;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200032C RID: 812
	public class BestiaryDatabase
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x00569A01 File Offset: 0x00567C01
		public List<BestiaryEntry> Entries
		{
			get
			{
				return this._entries;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060027EB RID: 10219 RVA: 0x00569A09 File Offset: 0x00567C09
		public List<IBestiaryEntryFilter> Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x00569A11 File Offset: 0x00567C11
		public List<IBestiarySortStep> SortSteps
		{
			get
			{
				return this._sortSteps;
			}
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x00569A1C File Offset: 0x00567C1C
		public BestiaryEntry Register(BestiaryEntry entry)
		{
			this._entries.Add(entry);
			for (int i = 0; i < entry.Info.Count; i++)
			{
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement = entry.Info[i] as NPCNetIdBestiaryInfoElement;
				if (npcnetIdBestiaryInfoElement != null)
				{
					this._byNpcId[npcnetIdBestiaryInfoElement.NetId] = entry;
				}
			}
			return entry;
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x00569A73 File Offset: 0x00567C73
		public IBestiaryEntryFilter Register(IBestiaryEntryFilter filter)
		{
			this._filters.Add(filter);
			return filter;
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x00569A82 File Offset: 0x00567C82
		public IBestiarySortStep Register(IBestiarySortStep sortStep)
		{
			this._sortSteps.Add(sortStep);
			return sortStep;
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x00569A94 File Offset: 0x00567C94
		public BestiaryEntry FindEntryByNPCID(int npcNetId)
		{
			BestiaryEntry bestiaryEntry;
			if (this._byNpcId.TryGetValue(npcNetId, out bestiaryEntry))
			{
				return bestiaryEntry;
			}
			this._trashEntry.Info.Clear();
			return this._trashEntry;
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x00569ACC File Offset: 0x00567CCC
		public void Merge(ItemDropDatabase dropsDatabase)
		{
			for (int i = -65; i < (int)NPCID.Count; i++)
			{
				this.ExtractDropsForNPC(dropsDatabase, i);
			}
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x00569AF4 File Offset: 0x00567CF4
		private void ExtractDropsForNPC(ItemDropDatabase dropsDatabase, int npcId)
		{
			BestiaryEntry bestiaryEntry = this.FindEntryByNPCID(npcId);
			if (bestiaryEntry == null)
			{
				return;
			}
			List<IItemDropRule> rulesForNPCID = dropsDatabase.GetRulesForNPCID(npcId, false);
			List<DropRateInfo> list = new List<DropRateInfo>();
			DropRateInfoChainFeed dropRateInfoChainFeed = new DropRateInfoChainFeed(1f);
			foreach (IItemDropRule itemDropRule in rulesForNPCID)
			{
				itemDropRule.ReportDroprates(list, dropRateInfoChainFeed);
			}
			foreach (DropRateInfo dropRateInfo in list)
			{
				bestiaryEntry.Info.Add(new ItemDropBestiaryInfoElement(dropRateInfo));
			}
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x00569BB0 File Offset: 0x00567DB0
		public void ApplyPass(BestiaryDatabase.BestiaryEntriesPass pass)
		{
			for (int i = 0; i < this._entries.Count; i++)
			{
				pass(this._entries[i]);
			}
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x00569BE5 File Offset: 0x00567DE5
		public BestiaryDatabase()
		{
		}

		// Token: 0x04005113 RID: 20755
		private List<BestiaryEntry> _entries = new List<BestiaryEntry>();

		// Token: 0x04005114 RID: 20756
		private List<IBestiaryEntryFilter> _filters = new List<IBestiaryEntryFilter>();

		// Token: 0x04005115 RID: 20757
		private List<IBestiarySortStep> _sortSteps = new List<IBestiarySortStep>();

		// Token: 0x04005116 RID: 20758
		private Dictionary<int, BestiaryEntry> _byNpcId = new Dictionary<int, BestiaryEntry>();

		// Token: 0x04005117 RID: 20759
		private BestiaryEntry _trashEntry = new BestiaryEntry();

		// Token: 0x020008AB RID: 2219
		// (Invoke) Token: 0x060045CB RID: 17867
		public delegate void BestiaryEntriesPass(BestiaryEntry entry);
	}
}
