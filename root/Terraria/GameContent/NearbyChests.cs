using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x0200023C RID: 572
	public static class NearbyChests
	{
		// Token: 0x06002287 RID: 8839 RVA: 0x0053915C File Offset: 0x0053735C
		public static List<PositionedChest> GetChestsInRangeOf(Vector2 position, float range = 0f)
		{
			if (range <= 0f)
			{
				range = 600f;
			}
			List<PositionedChest> scratch = NearbyChests._scratch;
			scratch.Clear();
			for (int i = 0; i < 8000; i++)
			{
				Chest chest = Main.chest[i];
				if (chest != null)
				{
					Vector2 vector = new Vector2((float)(chest.x * 16 + 16), (float)(chest.y * 16 + 16));
					if (Vector2.Distance(vector, position) <= range)
					{
						scratch.Add(new PositionedChest(chest, vector));
					}
				}
			}
			return scratch;
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x005391D8 File Offset: 0x005373D8
		public static List<PositionedChest> GetBanksInRangeOf(Player player, float range = 0f)
		{
			if (range <= 0f)
			{
				range = 600f;
			}
			List<PositionedChest> scratch = NearbyChests._scratch;
			scratch.Clear();
			int num = (int)(range / 16f + 2f);
			Point point = player.Center.ToTileCoordinates();
			Rectangle rectangle = new Rectangle(point.X - num, point.Y - num, num * 2 + 1, num * 2 + 1);
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile.active)
				{
					int num2 = -1;
					if (projectile.TryGetContainerIndex(out num2))
					{
						Vector2 vector = projectile.Hitbox.ClosestPointInRect(player.Center);
						Chest chest;
						if (rectangle.Contains(vector.ToTileCoordinates()) && NearbyChests.ContainerIndexToPlayerBank(player, num2, out chest) && !scratch.Contains(chest))
						{
							scratch.Add(new PositionedChest(chest, projectile.Center));
						}
					}
				}
			}
			for (int j = rectangle.Left; j < rectangle.Right; j++)
			{
				for (int k = rectangle.Top; k < rectangle.Bottom; k++)
				{
					if (WorldGen.InWorld(j, k, 0))
					{
						int num3 = 0;
						int type = (int)Main.tile[j, k].type;
						if (type == 29)
						{
							num3 = -2;
						}
						else if (type == 97)
						{
							num3 = -3;
						}
						else if (type == 463)
						{
							num3 = -4;
						}
						else if (type == 491)
						{
							num3 = -5;
						}
						Chest chest2;
						if (NearbyChests.ContainerIndexToPlayerBank(player, num3, out chest2) && !scratch.Contains(chest2))
						{
							scratch.Add(new PositionedChest(chest2, new Vector2((float)(j * 16 + 16), (float)(k * 16 + 16))));
						}
					}
				}
			}
			return scratch;
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0053939C File Offset: 0x0053759C
		private static bool Contains(this List<PositionedChest> list, Chest chest)
		{
			using (List<PositionedChest>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.chest == chest)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x005393F4 File Offset: 0x005375F4
		private static bool ContainerIndexToPlayerBank(Player player, int container, out Chest bank)
		{
			bank = null;
			if (container == -2)
			{
				bank = player.bank;
				return true;
			}
			if (container == -3)
			{
				bank = player.bank2;
				return true;
			}
			if (container == -4)
			{
				bank = player.bank3;
				return true;
			}
			if (container == -5)
			{
				bank = player.bank4;
				for (int i = 0; i < 58; i++)
				{
					if (player.inventory[i].stack > 0 && player.inventory[i].type == 5325)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x00539474 File Offset: 0x00537674
		// Note: this type is marked as 'beforefieldinit'.
		static NearbyChests()
		{
		}

		// Token: 0x04004D02 RID: 19714
		private static List<PositionedChest> _scratch = new List<PositionedChest>();
	}
}
