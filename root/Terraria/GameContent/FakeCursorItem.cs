using System;

namespace Terraria.GameContent
{
	// Token: 0x02000233 RID: 563
	public static class FakeCursorItem
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x00535724 File Offset: 0x00533924
		public static Item Item
		{
			get
			{
				int num = (Main.mouseItem.IsAir ? 0 : Main.mouseItem.stack);
				if (FakeCursorItem._type != FakeCursorItem._item.type)
				{
					FakeCursorItem._item.SetDefaults(FakeCursorItem._type, null);
				}
				else
				{
					FakeCursorItem._item.Refresh(true);
				}
				if (FakeCursorItem._prefix != (int)FakeCursorItem._item.prefix)
				{
					FakeCursorItem._item.Prefix(FakeCursorItem._prefix);
				}
				FakeCursorItem._item.stack = FakeCursorItem._stack + num;
				return FakeCursorItem._item;
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x005357B0 File Offset: 0x005339B0
		public static void Reset()
		{
			FakeCursorItem._type = 0;
			FakeCursorItem._stack = 0;
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x005357BE File Offset: 0x005339BE
		public static void Add(int itemType, int itemStack, int itemPrefix = 0)
		{
			if (itemStack == 0)
			{
				return;
			}
			if (FakeCursorItem._type == itemType)
			{
				FakeCursorItem._stack += itemStack;
			}
			else
			{
				FakeCursorItem._stack = itemStack;
			}
			FakeCursorItem._type = itemType;
			FakeCursorItem._prefix = itemPrefix;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x005357EC File Offset: 0x005339EC
		public static void Add(Item item)
		{
			FakeCursorItem.Add(item.type, item.stack, (int)item.prefix);
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x00535805 File Offset: 0x00533A05
		public static void Remove(int itemType, int itemStack)
		{
			if (itemStack == 0)
			{
				return;
			}
			if (FakeCursorItem._type != itemType)
			{
				return;
			}
			FakeCursorItem._stack -= itemStack;
			if (FakeCursorItem._stack <= 0)
			{
				FakeCursorItem._type = 0;
			}
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0053582E File Offset: 0x00533A2E
		// Note: this type is marked as 'beforefieldinit'.
		static FakeCursorItem()
		{
		}

		// Token: 0x04004CCF RID: 19663
		private static int _type;

		// Token: 0x04004CD0 RID: 19664
		private static int _stack;

		// Token: 0x04004CD1 RID: 19665
		private static int _prefix;

		// Token: 0x04004CD2 RID: 19666
		private static Item _item = new Item();
	}
}
