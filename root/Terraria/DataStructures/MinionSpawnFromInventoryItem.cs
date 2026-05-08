using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000554 RID: 1364
	public class MinionSpawnFromInventoryItem : MinionSpawnInfo
	{
		// Token: 0x0600378F RID: 14223 RVA: 0x0062F6E9 File Offset: 0x0062D8E9
		public MinionSpawnFromInventoryItem(Item item)
		{
			this.ItemType = item.type;
			this.ItemPrefix = (int)item.prefix;
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x0062F709 File Offset: 0x0062D909
		protected virtual bool ItemMatches(Item item)
		{
			return item.type == this.ItemType && (int)item.prefix == this.ItemPrefix;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x0062F72C File Offset: 0x0062D92C
		public override void TryRespawn(Player player)
		{
			Item item = this.FindMatchingItem(player);
			if (item != null)
			{
				if (item.buffType > 0)
				{
					int num = item.buffTime;
					if (num == 0)
					{
						num = 3600;
					}
					player.AddBuff(item.buffType, num, false);
				}
				player.SilentlyShootItem(item);
			}
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x0062F774 File Offset: 0x0062D974
		protected Item FindMatchingItem(Player player)
		{
			Item[] inventory = player.inventory;
			for (int i = 0; i < 50; i++)
			{
				Item item = inventory[i];
				if (this.ItemMatches(item))
				{
					return item;
				}
			}
			return null;
		}

		// Token: 0x04005BC3 RID: 23491
		public int ItemType;

		// Token: 0x04005BC4 RID: 23492
		public int ItemPrefix;
	}
}
