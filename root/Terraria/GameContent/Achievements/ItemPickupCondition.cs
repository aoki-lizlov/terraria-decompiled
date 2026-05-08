using System;
using System.Collections.Generic;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x02000288 RID: 648
	public class ItemPickupCondition : AchievementCondition
	{
		// Token: 0x060024FE RID: 9470 RVA: 0x00553214 File Offset: 0x00551414
		private ItemPickupCondition(short itemId)
			: base("ITEM_PICKUP_" + itemId)
		{
			this._itemIds = new short[] { itemId };
			ItemPickupCondition.ListenForPickup(this);
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x00553242 File Offset: 0x00551442
		private ItemPickupCondition(short[] itemIds)
			: base("ITEM_PICKUP_" + itemIds[0])
		{
			this._itemIds = itemIds;
			ItemPickupCondition.ListenForPickup(this);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x0055326C File Offset: 0x0055146C
		private static void ListenForPickup(ItemPickupCondition condition)
		{
			if (!ItemPickupCondition._isListenerHooked)
			{
				AchievementsHelper.OnItemPickup += ItemPickupCondition.ItemPickupListener;
				ItemPickupCondition._isListenerHooked = true;
			}
			for (int i = 0; i < condition._itemIds.Length; i++)
			{
				if (!ItemPickupCondition._listeners.ContainsKey(condition._itemIds[i]))
				{
					ItemPickupCondition._listeners[condition._itemIds[i]] = new List<ItemPickupCondition>();
				}
				ItemPickupCondition._listeners[condition._itemIds[i]].Add(condition);
			}
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x005532F0 File Offset: 0x005514F0
		private static void ItemPickupListener(Player player, short itemId, int count)
		{
			if (player.whoAmI != Main.myPlayer)
			{
				return;
			}
			if (ItemPickupCondition._listeners.ContainsKey(itemId))
			{
				foreach (ItemPickupCondition itemPickupCondition in ItemPickupCondition._listeners[itemId])
				{
					itemPickupCondition.Complete();
				}
			}
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x00553360 File Offset: 0x00551560
		public static AchievementCondition Create(params short[] items)
		{
			return new ItemPickupCondition(items);
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x00553368 File Offset: 0x00551568
		public static AchievementCondition Create(short item)
		{
			return new ItemPickupCondition(item);
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x00553370 File Offset: 0x00551570
		public static AchievementCondition[] CreateMany(params short[] items)
		{
			AchievementCondition[] array = new AchievementCondition[items.Length];
			for (int i = 0; i < items.Length; i++)
			{
				array[i] = new ItemPickupCondition(items[i]);
			}
			return array;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x005533A0 File Offset: 0x005515A0
		// Note: this type is marked as 'beforefieldinit'.
		static ItemPickupCondition()
		{
		}

		// Token: 0x04004F77 RID: 20343
		private const string Identifier = "ITEM_PICKUP";

		// Token: 0x04004F78 RID: 20344
		private static Dictionary<short, List<ItemPickupCondition>> _listeners = new Dictionary<short, List<ItemPickupCondition>>();

		// Token: 0x04004F79 RID: 20345
		private static bool _isListenerHooked;

		// Token: 0x04004F7A RID: 20346
		private short[] _itemIds;
	}
}
