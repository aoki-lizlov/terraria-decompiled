using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x02000559 RID: 1369
	public class PlayerGetItemLogger
	{
		// Token: 0x060037A9 RID: 14249 RVA: 0x0062FBCE File Offset: 0x0062DDCE
		public void Clear()
		{
			this.Entries.Clear();
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x0062FBDC File Offset: 0x0062DDDC
		public void Add(Item[] array, int slot, int itemSlotContext, int stack)
		{
			if (!this._enabled)
			{
				return;
			}
			this.Entries.Add(new PlayerGetItemLogger.GetItemLoggerEntry
			{
				TargetArray = array,
				TargetSlot = slot,
				TargetItemSlotContext = itemSlotContext,
				Stack = stack
			});
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x0062FC27 File Offset: 0x0062DE27
		public void Start()
		{
			this.Clear();
			this._enabled = true;
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x0062FC36 File Offset: 0x0062DE36
		public void Stop()
		{
			this._enabled = false;
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x0062FC3F File Offset: 0x0062DE3F
		public PlayerGetItemLogger()
		{
		}

		// Token: 0x04005BCF RID: 23503
		public List<PlayerGetItemLogger.GetItemLoggerEntry> Entries = new List<PlayerGetItemLogger.GetItemLoggerEntry>();

		// Token: 0x04005BD0 RID: 23504
		private bool _enabled;

		// Token: 0x020009B9 RID: 2489
		public struct GetItemLoggerEntry
		{
			// Token: 0x040076C9 RID: 30409
			public Item[] TargetArray;

			// Token: 0x040076CA RID: 30410
			public int TargetSlot;

			// Token: 0x040076CB RID: 30411
			public int TargetItemSlotContext;

			// Token: 0x040076CC RID: 30412
			public int Stack;
		}
	}
}
