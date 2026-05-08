using System;
using System.Collections.Generic;

namespace Terraria.GameContent
{
	// Token: 0x02000246 RID: 582
	public class ItemTrader
	{
		// Token: 0x060022ED RID: 8941 RVA: 0x0053BB58 File Offset: 0x00539D58
		public void AddOption_Interchangable(int itemType1, int itemType2)
		{
			this.AddOption_OneWay(itemType1, 1, itemType2, 1);
			this.AddOption_OneWay(itemType2, 1, itemType1, 1);
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x0053BB70 File Offset: 0x00539D70
		public void AddOption_CyclicLoop(params int[] typesInOrder)
		{
			for (int i = 0; i < typesInOrder.Length - 1; i++)
			{
				this.AddOption_OneWay(typesInOrder[i], 1, typesInOrder[i + 1], 1);
			}
			this.AddOption_OneWay(typesInOrder[typesInOrder.Length - 1], 1, typesInOrder[0], 1);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x0053BBB0 File Offset: 0x00539DB0
		public void AddOption_FromAny(int givingItemType, params int[] takingItemTypes)
		{
			for (int i = 0; i < takingItemTypes.Length; i++)
			{
				this.AddOption_OneWay(takingItemTypes[i], 1, givingItemType, 1);
			}
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x0053BBD7 File Offset: 0x00539DD7
		public void AddOption_OneWay(int takingItemType, int takingItemStack, int givingItemType, int givingItemStack)
		{
			this._options.Add(new ItemTrader.TradeOption
			{
				TakingItemType = takingItemType,
				TakingItemStack = takingItemStack,
				GivingItemType = givingItemType,
				GivingItemStack = givingItemStack
			});
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x0053BC08 File Offset: 0x00539E08
		public bool TryGetTradeOption(Item item, out ItemTrader.TradeOption option)
		{
			option = null;
			int type = item.type;
			int stack = item.stack;
			for (int i = 0; i < this._options.Count; i++)
			{
				ItemTrader.TradeOption tradeOption = this._options[i];
				if (tradeOption.WillTradeFor(type, stack))
				{
					option = tradeOption;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x0053BC5C File Offset: 0x00539E5C
		public static ItemTrader CreateChlorophyteExtractinator()
		{
			ItemTrader itemTrader = new ItemTrader();
			itemTrader.AddOption_Interchangable(12, 699);
			itemTrader.AddOption_Interchangable(11, 700);
			itemTrader.AddOption_Interchangable(14, 701);
			itemTrader.AddOption_Interchangable(13, 702);
			itemTrader.AddOption_Interchangable(56, 880);
			itemTrader.AddOption_Interchangable(364, 1104);
			itemTrader.AddOption_Interchangable(365, 1105);
			itemTrader.AddOption_Interchangable(366, 1106);
			itemTrader.AddOption_CyclicLoop(new int[] { 134, 137, 139 });
			itemTrader.AddOption_Interchangable(20, 703);
			itemTrader.AddOption_Interchangable(22, 704);
			itemTrader.AddOption_Interchangable(21, 705);
			itemTrader.AddOption_Interchangable(19, 706);
			itemTrader.AddOption_Interchangable(57, 1257);
			itemTrader.AddOption_Interchangable(381, 1184);
			itemTrader.AddOption_Interchangable(382, 1191);
			itemTrader.AddOption_Interchangable(391, 1198);
			itemTrader.AddOption_Interchangable(86, 1329);
			itemTrader.AddOption_FromAny(3, new int[] { 61, 836, 409 });
			itemTrader.AddOption_FromAny(169, new int[] { 370, 1246, 408 });
			itemTrader.AddOption_FromAny(664, new int[] { 833, 835, 834 });
			itemTrader.AddOption_FromAny(3271, new int[] { 3276, 3277, 3339 });
			itemTrader.AddOption_FromAny(3272, new int[] { 3274, 3275, 3338 });
			return itemTrader;
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x0053BDFE File Offset: 0x00539FFE
		public ItemTrader()
		{
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x0053BE11 File Offset: 0x0053A011
		// Note: this type is marked as 'beforefieldinit'.
		static ItemTrader()
		{
		}

		// Token: 0x04004D3E RID: 19774
		public static ItemTrader ChlorophyteExtractinator = ItemTrader.CreateChlorophyteExtractinator();

		// Token: 0x04004D3F RID: 19775
		private List<ItemTrader.TradeOption> _options = new List<ItemTrader.TradeOption>();

		// Token: 0x020007D1 RID: 2001
		public class TradeOption
		{
			// Token: 0x06004236 RID: 16950 RVA: 0x006BE139 File Offset: 0x006BC339
			public bool WillTradeFor(int offeredItemType, int offeredItemStack)
			{
				return offeredItemType == this.TakingItemType && offeredItemStack >= this.TakingItemStack;
			}

			// Token: 0x06004237 RID: 16951 RVA: 0x0000357B File Offset: 0x0000177B
			public TradeOption()
			{
			}

			// Token: 0x04007109 RID: 28937
			public int TakingItemType;

			// Token: 0x0400710A RID: 28938
			public int TakingItemStack;

			// Token: 0x0400710B RID: 28939
			public int GivingItemType;

			// Token: 0x0400710C RID: 28940
			public int GivingItemStack;
		}
	}
}
