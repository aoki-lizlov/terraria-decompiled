using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent
{
	// Token: 0x0200027C RID: 636
	public class ItemShopSellbackHelper
	{
		// Token: 0x06002468 RID: 9320 RVA: 0x0054D54C File Offset: 0x0054B74C
		public void Add(Item item)
		{
			ItemShopSellbackHelper.ItemMemo itemMemo = this._memos.Find((ItemShopSellbackHelper.ItemMemo x) => x.Matches(item));
			if (itemMemo != null)
			{
				itemMemo.stack += item.stack;
				return;
			}
			this._memos.Add(new ItemShopSellbackHelper.ItemMemo(item));
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x0054D5B0 File Offset: 0x0054B7B0
		public void Clear()
		{
			this._memos.Clear();
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x0054D5C0 File Offset: 0x0054B7C0
		public int GetAmount(Item item)
		{
			ItemShopSellbackHelper.ItemMemo itemMemo = this._memos.Find((ItemShopSellbackHelper.ItemMemo x) => x.Matches(item));
			if (itemMemo != null)
			{
				return itemMemo.stack;
			}
			return 0;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0054D600 File Offset: 0x0054B800
		public int Remove(Item item)
		{
			ItemShopSellbackHelper.ItemMemo itemMemo = this._memos.Find((ItemShopSellbackHelper.ItemMemo x) => x.Matches(item));
			if (itemMemo == null)
			{
				return 0;
			}
			int stack = itemMemo.stack;
			itemMemo.stack -= item.stack;
			if (itemMemo.stack <= 0)
			{
				this._memos.Remove(itemMemo);
				return stack;
			}
			return stack - itemMemo.stack;
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x0054D676 File Offset: 0x0054B876
		public ItemShopSellbackHelper()
		{
		}

		// Token: 0x04004E12 RID: 19986
		private List<ItemShopSellbackHelper.ItemMemo> _memos = new List<ItemShopSellbackHelper.ItemMemo>();

		// Token: 0x020007FF RID: 2047
		private class ItemMemo
		{
			// Token: 0x060042C7 RID: 17095 RVA: 0x006BFEC1 File Offset: 0x006BE0C1
			public ItemMemo(Item item)
			{
				this.type = item.type;
				this.prefix = (int)item.prefix;
				this.stack = item.stack;
			}

			// Token: 0x060042C8 RID: 17096 RVA: 0x006BFEED File Offset: 0x006BE0ED
			public bool Matches(Item item)
			{
				return item.IsConsideredSameItemAsType(this.type) && (int)item.prefix == this.prefix;
			}

			// Token: 0x040071B4 RID: 29108
			public readonly int type;

			// Token: 0x040071B5 RID: 29109
			public readonly int prefix;

			// Token: 0x040071B6 RID: 29110
			public int stack;
		}

		// Token: 0x02000800 RID: 2048
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x060042C9 RID: 17097 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x060042CA RID: 17098 RVA: 0x006BFF0D File Offset: 0x006BE10D
			internal bool <Add>b__0(ItemShopSellbackHelper.ItemMemo x)
			{
				return x.Matches(this.item);
			}

			// Token: 0x040071B7 RID: 29111
			public Item item;
		}

		// Token: 0x02000801 RID: 2049
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x060042CB RID: 17099 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x060042CC RID: 17100 RVA: 0x006BFF1B File Offset: 0x006BE11B
			internal bool <GetAmount>b__0(ItemShopSellbackHelper.ItemMemo x)
			{
				return x.Matches(this.item);
			}

			// Token: 0x040071B8 RID: 29112
			public Item item;
		}

		// Token: 0x02000802 RID: 2050
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x060042CD RID: 17101 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x060042CE RID: 17102 RVA: 0x006BFF29 File Offset: 0x006BE129
			internal bool <Remove>b__0(ItemShopSellbackHelper.ItemMemo x)
			{
				return x.Matches(this.item);
			}

			// Token: 0x040071B9 RID: 29113
			public Item item;
		}
	}
}
