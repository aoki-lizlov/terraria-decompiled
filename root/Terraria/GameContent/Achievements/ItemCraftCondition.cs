using System;
using System.Collections.Generic;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x02000287 RID: 647
	public class ItemCraftCondition : AchievementCondition
	{
		// Token: 0x060024F6 RID: 9462 RVA: 0x00553089 File Offset: 0x00551289
		private ItemCraftCondition(short itemId)
			: base("ITEM_PICKUP_" + itemId)
		{
			this._itemIds = new short[] { itemId };
			ItemCraftCondition.ListenForCraft(this);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x005530B7 File Offset: 0x005512B7
		private ItemCraftCondition(short[] itemIds)
			: base("ITEM_PICKUP_" + itemIds[0])
		{
			this._itemIds = itemIds;
			ItemCraftCondition.ListenForCraft(this);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x005530E0 File Offset: 0x005512E0
		private static void ListenForCraft(ItemCraftCondition condition)
		{
			if (!ItemCraftCondition._isListenerHooked)
			{
				AchievementsHelper.OnItemCraft += ItemCraftCondition.ItemCraftListener;
				ItemCraftCondition._isListenerHooked = true;
			}
			for (int i = 0; i < condition._itemIds.Length; i++)
			{
				if (!ItemCraftCondition._listeners.ContainsKey(condition._itemIds[i]))
				{
					ItemCraftCondition._listeners[condition._itemIds[i]] = new List<ItemCraftCondition>();
				}
				ItemCraftCondition._listeners[condition._itemIds[i]].Add(condition);
			}
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x00553164 File Offset: 0x00551364
		private static void ItemCraftListener(short itemId, int count)
		{
			if (ItemCraftCondition._listeners.ContainsKey(itemId))
			{
				foreach (ItemCraftCondition itemCraftCondition in ItemCraftCondition._listeners[itemId])
				{
					itemCraftCondition.Complete();
				}
			}
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x005531C8 File Offset: 0x005513C8
		public static AchievementCondition Create(params short[] items)
		{
			return new ItemCraftCondition(items);
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x005531D0 File Offset: 0x005513D0
		public static AchievementCondition Create(short item)
		{
			return new ItemCraftCondition(item);
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x005531D8 File Offset: 0x005513D8
		public static AchievementCondition[] CreateMany(params short[] items)
		{
			AchievementCondition[] array = new AchievementCondition[items.Length];
			for (int i = 0; i < items.Length; i++)
			{
				array[i] = new ItemCraftCondition(items[i]);
			}
			return array;
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x00553208 File Offset: 0x00551408
		// Note: this type is marked as 'beforefieldinit'.
		static ItemCraftCondition()
		{
		}

		// Token: 0x04004F73 RID: 20339
		private const string Identifier = "ITEM_PICKUP";

		// Token: 0x04004F74 RID: 20340
		private static Dictionary<short, List<ItemCraftCondition>> _listeners = new Dictionary<short, List<ItemCraftCondition>>();

		// Token: 0x04004F75 RID: 20341
		private static bool _isListenerHooked;

		// Token: 0x04004F76 RID: 20342
		private short[] _itemIds;
	}
}
