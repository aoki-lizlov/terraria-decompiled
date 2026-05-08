using System;

namespace Terraria
{
	// Token: 0x02000045 RID: 69
	public struct GetItemSettings
	{
		// Token: 0x0600077E RID: 1918 RVA: 0x002D6667 File Offset: 0x002D4867
		public GetItemSettings(bool LongText = false, bool NoText = false, bool CanGoIntoVoidVault = false, bool NoSound = false, Action<Item> StepAfterHandlingSlotNormally = null, bool NoCoinMerge = false)
		{
			this.LongText = LongText;
			this.NoText = NoText;
			this.CanGoIntoVoidVault = CanGoIntoVoidVault;
			this.NoSound = NoSound;
			this.StepAfterHandlingSlotNormally = StepAfterHandlingSlotNormally;
			this.NoCoinMerge = NoCoinMerge;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x002D6696 File Offset: 0x002D4896
		public void HandlePostAction(Item item)
		{
			if (this.StepAfterHandlingSlotNormally != null)
			{
				this.StepAfterHandlingSlotNormally(item);
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x002D66AC File Offset: 0x002D48AC
		private static void MakeNewAndShiny(Item item)
		{
			item.newAndShiny = true;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x002D66B8 File Offset: 0x002D48B8
		// Note: this type is marked as 'beforefieldinit'.
		static GetItemSettings()
		{
		}

		// Token: 0x040004ED RID: 1261
		public static GetItemSettings GiftRecieved = new GetItemSettings(true, false, false, false, null, false);

		// Token: 0x040004EE RID: 1262
		public static GetItemSettings LootAllFromBank = default(GetItemSettings);

		// Token: 0x040004EF RID: 1263
		public static GetItemSettings LootAllFromChest = new GetItemSettings(false, false, true, false, null, false);

		// Token: 0x040004F0 RID: 1264
		public static GetItemSettings PickupItemFromWorld = new GetItemSettings(false, false, true, false, null, false);

		// Token: 0x040004F1 RID: 1265
		public static GetItemSettings QuickTransferFromSlot = new GetItemSettings(false, true, false, false, null, false);

		// Token: 0x040004F2 RID: 1266
		public static GetItemSettings ReturnItemFromSlot = new GetItemSettings(false, true, false, false, null, false);

		// Token: 0x040004F3 RID: 1267
		public static GetItemSettings ReturnItemShowAsNew = new GetItemSettings(false, true, false, false, new Action<Item>(GetItemSettings.MakeNewAndShiny), false);

		// Token: 0x040004F4 RID: 1268
		public static GetItemSettings ItemCreatedFromItemUsage = default(GetItemSettings);

		// Token: 0x040004F5 RID: 1269
		public static GetItemSettings RefundConsumedItem = new GetItemSettings(false, true, true, true, null, false);

		// Token: 0x040004F6 RID: 1270
		public static GetItemSettings ReturnItemShowAsNewNoCoinMerge = new GetItemSettings(false, true, false, false, new Action<Item>(GetItemSettings.MakeNewAndShiny), true);

		// Token: 0x040004F7 RID: 1271
		public readonly bool LongText;

		// Token: 0x040004F8 RID: 1272
		public readonly bool NoText;

		// Token: 0x040004F9 RID: 1273
		public readonly bool CanGoIntoVoidVault;

		// Token: 0x040004FA RID: 1274
		public readonly bool NoSound;

		// Token: 0x040004FB RID: 1275
		public readonly bool NoCoinMerge;

		// Token: 0x040004FC RID: 1276
		public readonly Action<Item> StepAfterHandlingSlotNormally;
	}
}
