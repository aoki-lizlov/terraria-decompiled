using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.ID;

namespace Terraria.UI
{
	// Token: 0x020000EB RID: 235
	public class ItemSorting
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x004E637C File Offset: 0x004E457C
		public static int LayerCount
		{
			get
			{
				return ItemSorting._layerCount;
			}
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x004E6384 File Offset: 0x004E4584
		public static void SetupWhiteLists()
		{
			ItemSorting._layerWhiteLists.Clear();
			List<ItemSorting.ItemSortingLayer> list = new List<ItemSorting.ItemSortingLayer>();
			List<Item> list2 = new List<Item>();
			List<int> list3 = new List<int>();
			list.Add(ItemSorting.ItemSortingLayers.WeaponsMelee);
			list.Add(ItemSorting.ItemSortingLayers.WeaponsRanged);
			list.Add(ItemSorting.ItemSortingLayers.WeaponsMagic);
			list.Add(ItemSorting.ItemSortingLayers.WeaponsMinions);
			list.Add(ItemSorting.ItemSortingLayers.WeaponsAssorted);
			list.Add(ItemSorting.ItemSortingLayers.WeaponsAmmo);
			list.Add(ItemSorting.ItemSortingLayers.ToolsPicksaws);
			list.Add(ItemSorting.ItemSortingLayers.ToolsHamaxes);
			list.Add(ItemSorting.ItemSortingLayers.ToolsPickaxes);
			list.Add(ItemSorting.ItemSortingLayers.ToolsAxes);
			list.Add(ItemSorting.ItemSortingLayers.ToolsHammers);
			list.Add(ItemSorting.ItemSortingLayers.ToolsTerraforming);
			list.Add(ItemSorting.ItemSortingLayers.ToolsFishing);
			list.Add(ItemSorting.ItemSortingLayers.ToolsGolf);
			list.Add(ItemSorting.ItemSortingLayers.ToolsInstruments);
			list.Add(ItemSorting.ItemSortingLayers.ToolsKeys);
			list.Add(ItemSorting.ItemSortingLayers.ToolsKites);
			list.Add(ItemSorting.ItemSortingLayers.ToolsAmmoLeftovers);
			list.Add(ItemSorting.ItemSortingLayers.ToolsMisc);
			list.Add(ItemSorting.ItemSortingLayers.ArmorCombat);
			list.Add(ItemSorting.ItemSortingLayers.ArmorVanity);
			list.Add(ItemSorting.ItemSortingLayers.ArmorAccessories);
			list.Add(ItemSorting.ItemSortingLayers.EquipGrapple);
			list.Add(ItemSorting.ItemSortingLayers.EquipMount);
			list.Add(ItemSorting.ItemSortingLayers.EquipCart);
			list.Add(ItemSorting.ItemSortingLayers.EquipLightPet);
			list.Add(ItemSorting.ItemSortingLayers.EquipVanityPet);
			list.Add(ItemSorting.ItemSortingLayers.PotionsDyes);
			list.Add(ItemSorting.ItemSortingLayers.PotionsHairDyes);
			list.Add(ItemSorting.ItemSortingLayers.PotionsLife);
			list.Add(ItemSorting.ItemSortingLayers.PotionsJustTheMushroom);
			list.Add(ItemSorting.ItemSortingLayers.PotionsMana);
			list.Add(ItemSorting.ItemSortingLayers.PotionsElixirs);
			list.Add(ItemSorting.ItemSortingLayers.PotionsBuffs);
			list.Add(ItemSorting.ItemSortingLayers.PotionsFood);
			list.Add(ItemSorting.ItemSortingLayers.MiscValuables);
			list.Add(ItemSorting.ItemSortingLayers.MiscPainting);
			list.Add(ItemSorting.ItemSortingLayers.MiscWiring);
			list.Add(ItemSorting.ItemSortingLayers.MiscMaterials);
			list.Add(ItemSorting.ItemSortingLayers.MiscJustTheGlowingMushroom);
			list.Add(ItemSorting.ItemSortingLayers.MiscRopes);
			list.Add(ItemSorting.ItemSortingLayers.MiscHerbsAndSeeds);
			list.Add(ItemSorting.ItemSortingLayers.MiscAcorns);
			list.Add(ItemSorting.ItemSortingLayers.MiscGems);
			list.Add(ItemSorting.ItemSortingLayers.MiscBossBags);
			list.Add(ItemSorting.ItemSortingLayers.MiscCritters);
			list.Add(ItemSorting.ItemSortingLayers.MiscExtractinator);
			list.Add(ItemSorting.ItemSortingLayers.LastMaterials);
			list.Add(ItemSorting.ItemSortingLayers.LastTilesImportant);
			list.Add(ItemSorting.ItemSortingLayers.LastTilesCommon);
			list.Add(ItemSorting.ItemSortingLayers.LastNotTrash);
			list.Add(ItemSorting.ItemSortingLayers.LastTrash);
			for (int i = -48; i < (int)ItemID.Count; i++)
			{
				Item item = new Item();
				item.netDefaults(i);
				list2.Add(item);
				list3.Add(i + 48);
			}
			Item[] array = list2.ToArray();
			ItemSorting._layerCount = list.Count;
			ItemSorting._layerIndexForItemType = new int[(int)ItemID.Count];
			for (int j = 0; j < list.Count; j++)
			{
				ItemSorting.ItemSortingLayer itemSortingLayer = list[j];
				List<int> list4 = itemSortingLayer.SortingMethod(itemSortingLayer, array, list3);
				List<int> list5 = new List<int>();
				for (int k = 0; k < list4.Count; k++)
				{
					Item item2 = array[list4[k]];
					list5.Add(item2.type);
					ItemSorting._layerIndexForItemType[item2.type] = j;
				}
				ItemSorting._layerWhiteLists.Add(itemSortingLayer.Name, list5);
			}
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x004E66D0 File Offset: 0x004E48D0
		private static void AddSortingPrioritiesBasedOnPlayerDamage(List<ItemSorting.ItemSortingLayer> list)
		{
			Player player = Main.player[Main.myPlayer];
			ItemSorting._damageRankings.Clear();
			ItemSorting._damageRankings.Add(new ItemSorting.DamageTypeSortingLayerEntry(player.meleeDamage, ItemSorting.ItemSortingLayers.WeaponsMelee, 0));
			ItemSorting._damageRankings.Add(new ItemSorting.DamageTypeSortingLayerEntry(player.rangedDamage, ItemSorting.ItemSortingLayers.WeaponsRanged, 1));
			ItemSorting._damageRankings.Add(new ItemSorting.DamageTypeSortingLayerEntry(player.magicDamage, ItemSorting.ItemSortingLayers.WeaponsMagic, 2));
			ItemSorting._damageRankings.Add(new ItemSorting.DamageTypeSortingLayerEntry(player.minionDamage, ItemSorting.ItemSortingLayers.WeaponsMinions, 3));
			ItemSorting._damageRankings.Sort(new Comparison<ItemSorting.DamageTypeSortingLayerEntry>(ItemSorting.Descending));
			foreach (ItemSorting.DamageTypeSortingLayerEntry damageTypeSortingLayerEntry in ItemSorting._damageRankings)
			{
				list.Add(damageTypeSortingLayerEntry.Layer);
			}
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x004E67C0 File Offset: 0x004E49C0
		private static int Descending(ItemSorting.DamageTypeSortingLayerEntry x, ItemSorting.DamageTypeSortingLayerEntry y)
		{
			int num = y.Multiplier.CompareTo(x.Multiplier);
			if (num == 0)
			{
				num = x.Index.CompareTo(y.Index);
			}
			return num;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x004E67F8 File Offset: 0x004E49F8
		private static void SetupSortingPriorities()
		{
			Player player = Main.player[Main.myPlayer];
			ItemSorting._layerList.Clear();
			ItemSorting.AddSortingPrioritiesBasedOnPlayerDamage(ItemSorting._layerList);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.WeaponsAssorted);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.WeaponsAmmo);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsPicksaws);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsHamaxes);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsPickaxes);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsAxes);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsHammers);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsTerraforming);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsFishing);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsGolf);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsInstruments);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsKeys);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsKites);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsAmmoLeftovers);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsMisc);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ArmorCombat);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ArmorVanity);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ArmorAccessories);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.EquipGrapple);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.EquipMount);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.EquipCart);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.EquipLightPet);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.EquipVanityPet);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsDyes);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsHairDyes);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsLife);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsJustTheMushroom);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsMana);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsElixirs);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsBuffs);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsFood);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscValuables);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscPainting);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscWiring);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscMaterials);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscJustTheGlowingMushroom);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscRopes);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscHerbsAndSeeds);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscAcorns);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscGems);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscBossBags);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscCritters);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscExtractinator);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.LastMaterials);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.LastTilesImportant);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.LastTilesCommon);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.LastNotTrash);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.LastTrash);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x004E6AF8 File Offset: 0x004E4CF8
		private static void Sort(bool withFeedback, Item[] inv, params int[] ignoreSlots)
		{
			ItemSorting.SetupSortingPriorities();
			ItemSorting._sort_itemsToSort.Clear();
			ItemSorting._sort_sortedItemIndexes.Clear();
			ItemSorting._sort_counts.Clear();
			ItemSorting._sort_itemsCache.Clear();
			ItemSorting._sort_availableSortingSlots.Clear();
			for (int i = 0; i < inv.Length; i++)
			{
				if (!ignoreSlots.Contains(i))
				{
					Item item = inv[i];
					if (item != null && item.stack != 0 && item.type != 0 && !item.favorited)
					{
						ItemSorting._sort_itemsToSort.Add(i);
					}
				}
			}
			for (int j = 0; j < ItemSorting._sort_itemsToSort.Count; j++)
			{
				Item item2 = inv[ItemSorting._sort_itemsToSort[j]];
				if (item2.stack < item2.maxStack)
				{
					int num = item2.maxStack - item2.stack;
					for (int k = j; k < ItemSorting._sort_itemsToSort.Count; k++)
					{
						if (j != k)
						{
							Item item3 = inv[ItemSorting._sort_itemsToSort[k]];
							if (Item.CanStack(item2, item3) && item3.stack != item3.maxStack)
							{
								int num2 = item3.stack;
								if (num < num2)
								{
									num2 = num;
								}
								item2.stack += num2;
								item3.stack -= num2;
								num -= num2;
								if (item3.stack == 0)
								{
									inv[ItemSorting._sort_itemsToSort[k]] = new Item();
									ItemSorting._sort_itemsToSort.Remove(ItemSorting._sort_itemsToSort[k]);
									j--;
									k--;
									break;
								}
								if (num == 0)
								{
									break;
								}
							}
						}
					}
				}
			}
			ItemSorting._sort_availableSortingSlots.AddRange(ItemSorting._sort_itemsToSort);
			for (int l = 0; l < inv.Length; l++)
			{
				if (!ignoreSlots.Contains(l) && !ItemSorting._sort_availableSortingSlots.Contains(l))
				{
					Item item4 = inv[l];
					if (item4 == null || item4.stack == 0 || item4.type == 0)
					{
						ItemSorting._sort_availableSortingSlots.Add(l);
					}
				}
			}
			ItemSorting._sort_availableSortingSlots.Sort();
			foreach (ItemSorting.ItemSortingLayer itemSortingLayer in ItemSorting._layerList)
			{
				List<int> list = itemSortingLayer.SortingMethod(itemSortingLayer, inv, ItemSorting._sort_itemsToSort);
				if (list.Count > 0)
				{
					ItemSorting._sort_counts.Add(list.Count);
				}
				ItemSorting._sort_sortedItemIndexes.AddRange(list);
			}
			ItemSorting._sort_sortedItemIndexes.AddRange(ItemSorting._sort_itemsToSort);
			foreach (int num3 in ItemSorting._sort_sortedItemIndexes)
			{
				ItemSorting._sort_itemsCache.Add(inv[num3]);
				inv[num3] = new Item();
			}
			float num4 = 1f / (float)ItemSorting._sort_counts.Count;
			float num5 = num4 / 2f;
			for (int m = 0; m < ItemSorting._sort_itemsCache.Count; m++)
			{
				int num6 = ItemSorting._sort_availableSortingSlots[0];
				if (withFeedback)
				{
					ItemSlot.SetGlow(num6, num5, Main.player[Main.myPlayer].chest != -1);
				}
				List<int> sort_counts = ItemSorting._sort_counts;
				int num7 = sort_counts[0];
				sort_counts[0] = num7 - 1;
				if (ItemSorting._sort_counts[0] == 0)
				{
					ItemSorting._sort_counts.RemoveAt(0);
					num5 += num4;
				}
				inv[num6] = ItemSorting._sort_itemsCache[m];
				ItemSorting._sort_availableSortingSlots.Remove(num6);
			}
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x004E6EA8 File Offset: 0x004E50A8
		public static string GetSortingLayer(int itemType)
		{
			foreach (KeyValuePair<string, List<int>> keyValuePair in ItemSorting._layerWhiteLists)
			{
				if (keyValuePair.Value.Contains(itemType))
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x004E6F10 File Offset: 0x004E5110
		public static int GetSortingLayerIndex(int itemType)
		{
			return ItemSorting._layerIndexForItemType[itemType];
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x004E6F1C File Offset: 0x004E511C
		public static void SortInventory()
		{
			if (Main.LocalPlayer.HasLockedInventory())
			{
				return;
			}
			if (!Main.LocalPlayer.HasItem(905))
			{
				ItemSorting.SortCoins();
			}
			ItemSorting.SortAmmo();
			ItemSorting.Sort(true, Main.player[Main.myPlayer].inventory, new int[]
			{
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
				50, 51, 52, 53, 54, 55, 56, 57, 58
			});
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x004E6F7C File Offset: 0x004E517C
		public static void SortChest()
		{
			int chest = Main.player[Main.myPlayer].chest;
			if (chest == -1)
			{
				return;
			}
			Item[] item = Main.player[Main.myPlayer].bank.item;
			if (chest == -3)
			{
				Item[] item2 = Main.player[Main.myPlayer].bank2.item;
			}
			if (chest == -4)
			{
				Item[] item3 = Main.player[Main.myPlayer].bank3.item;
			}
			if (chest == -5)
			{
				Item[] item4 = Main.player[Main.myPlayer].bank4.item;
			}
			if (chest > -1)
			{
				Item[] item5 = Main.chest[chest].item;
			}
			ItemSorting.SortInventory(Main.LocalPlayer.GetCurrentContainer(), true, true);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x004E7028 File Offset: 0x004E5228
		public static void SortInventory(Chest chest, bool withSync, bool withFeedback)
		{
			Item[] item = chest.item;
			Array.Resize<ItemSorting.MemoryStamp>(ref ItemSorting._sortInventory_preStamps, chest.maxItems);
			Array.Resize<ItemSorting.MemoryStamp>(ref ItemSorting._sortInventory_postStamps, chest.maxItems);
			for (int i = 0; i < chest.maxItems; i++)
			{
				ItemSorting._sortInventory_preStamps[i] = new ItemSorting.MemoryStamp(item[i]);
			}
			ItemSorting.Sort(withFeedback, item, new int[0]);
			for (int j = 0; j < chest.maxItems; j++)
			{
				ItemSorting._sortInventory_postStamps[j] = new ItemSorting.MemoryStamp(item[j]);
			}
			if (withSync && Main.netMode == 1 && Main.player[Main.myPlayer].chest > -1)
			{
				for (int k = 0; k < chest.maxItems; k++)
				{
					if (ItemSorting._sortInventory_postStamps[k] != ItemSorting._sortInventory_preStamps[k])
					{
						NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)k, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x004E7126 File Offset: 0x004E5326
		public static void SortAmmo()
		{
			ItemSorting.ClearAmmoSlotSpaces();
			ItemSorting.FillAmmoFromInventory();
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x004E7134 File Offset: 0x004E5334
		public static void FillAmmoFromInventory()
		{
			ItemSorting._fillAmmoFromInventory_acceptedAmmoTypes.Clear();
			ItemSorting._fillAmmoFromInventory_emptyAmmoSlots.Clear();
			Item[] inventory = Main.player[Main.myPlayer].inventory;
			for (int i = 54; i < 58; i++)
			{
				ItemSlot.SetGlow(i, 0.31f, false);
				Item item = inventory[i];
				if (item.IsAir)
				{
					ItemSorting._fillAmmoFromInventory_emptyAmmoSlots.Add(i);
				}
				else if (item.ammo != AmmoID.None)
				{
					if (!ItemSorting._fillAmmoFromInventory_acceptedAmmoTypes.Contains(item.type))
					{
						ItemSorting._fillAmmoFromInventory_acceptedAmmoTypes.Add(item.type);
					}
					ItemSorting.RefillItemStack(inventory, inventory[i], 0, 50);
				}
			}
			if (ItemSorting._fillAmmoFromInventory_emptyAmmoSlots.Count < 1)
			{
				return;
			}
			for (int j = 0; j < 50; j++)
			{
				Item item2 = inventory[j];
				if (item2.stack >= 1 && item2.CanFillEmptyAmmoSlot() && ItemSorting._fillAmmoFromInventory_acceptedAmmoTypes.Contains(item2.type) && !item2.favorited)
				{
					int num = ItemSorting._fillAmmoFromInventory_emptyAmmoSlots[0];
					ItemSorting._fillAmmoFromInventory_emptyAmmoSlots.Remove(num);
					Utils.Swap<Item>(ref inventory[j], ref inventory[num]);
					ItemSorting.RefillItemStack(inventory, inventory[num], 0, 50);
					if (ItemSorting._fillAmmoFromInventory_emptyAmmoSlots.Count == 0)
					{
						break;
					}
				}
			}
			if (ItemSorting._fillAmmoFromInventory_emptyAmmoSlots.Count < 1)
			{
				return;
			}
			for (int k = 0; k < 50; k++)
			{
				Item item3 = inventory[k];
				if (item3.stack >= 1 && item3.CanFillEmptyAmmoSlot() && item3.FitsAmmoSlot() && !item3.favorited)
				{
					int num2 = ItemSorting._fillAmmoFromInventory_emptyAmmoSlots[0];
					ItemSorting._fillAmmoFromInventory_emptyAmmoSlots.Remove(num2);
					Utils.Swap<Item>(ref inventory[k], ref inventory[num2]);
					ItemSorting.RefillItemStack(inventory, inventory[num2], 0, 50);
					if (ItemSorting._fillAmmoFromInventory_emptyAmmoSlots.Count == 0)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x004E7300 File Offset: 0x004E5500
		public static void ClearAmmoSlotSpaces()
		{
			Item[] inventory = Main.player[Main.myPlayer].inventory;
			for (int i = 54; i < 58; i++)
			{
				Item item = inventory[i];
				if (!item.IsAir && item.ammo != AmmoID.None && item.stack < item.maxStack)
				{
					int num = (item.favorited ? 54 : (i + 1));
					ItemSorting.RefillItemStack(inventory, item, num, 58);
				}
			}
			for (int j = 54; j < 58; j++)
			{
				if (inventory[j].type > 0 && !inventory[j].favorited)
				{
					ItemSorting.TrySlidingUp(inventory, j, 54);
				}
			}
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x004E73A0 File Offset: 0x004E55A0
		private static void SortCoins()
		{
			Item[] inventory = Main.LocalPlayer.inventory;
			bool flag;
			long num = Utils.CoinsCount(out flag, inventory, new int[] { 58 });
			int commonMaxStack = Item.CommonMaxStack;
			if (flag)
			{
				return;
			}
			int[] array = Utils.CoinsSplit(num);
			int num2 = 0;
			for (int i = 0; i < 3; i++)
			{
				int j = array[i];
				while (j > 0)
				{
					j -= 99;
					num2++;
				}
			}
			int k = array[3];
			while (k > commonMaxStack)
			{
				k -= commonMaxStack;
				num2++;
			}
			int num3 = 0;
			for (int l = 0; l < 58; l++)
			{
				if (inventory[l].type >= 71 && inventory[l].type <= 74 && inventory[l].stack > 0)
				{
					num3++;
				}
			}
			if (num3 < num2)
			{
				return;
			}
			for (int m = 0; m < 58; m++)
			{
				if (inventory[m].type >= 71 && inventory[m].type <= 74 && inventory[m].stack > 0)
				{
					inventory[m].TurnToAir(false);
				}
			}
			int num4 = 100;
			do
			{
				int num5 = -1;
				for (int n = 3; n >= 0; n--)
				{
					if (array[n] > 0)
					{
						num5 = n;
						break;
					}
				}
				if (num5 == -1)
				{
					return;
				}
				int num6 = array[num5];
				if (num5 == 3 && num6 > commonMaxStack)
				{
					num6 = commonMaxStack;
				}
				bool flag2 = false;
				if (!flag2)
				{
					for (int num7 = 50; num7 < 54; num7++)
					{
						if (inventory[num7].IsAir)
						{
							inventory[num7].SetDefaults(71 + num5, null);
							inventory[num7].stack = num6;
							array[num5] -= num6;
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					for (int num8 = 0; num8 < 50; num8++)
					{
						if (inventory[num8].IsAir)
						{
							inventory[num8].SetDefaults(71 + num5, null);
							inventory[num8].stack = num6;
							array[num5] -= num6;
							break;
						}
					}
				}
				num4--;
			}
			while (num4 > 0);
			for (int num9 = 3; num9 >= 0; num9--)
			{
				if (array[num9] > 0)
				{
					Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetItemSource_InventoryOverflow(), 71 + num9, array[num9]);
				}
			}
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x004E75D4 File Offset: 0x004E57D4
		private static void RefillItemStack(Item[] inv, Item itemToRefill, int loopStartIndex, int loopEndIndex)
		{
			int num = itemToRefill.maxStack - itemToRefill.stack;
			if (num <= 0)
			{
				return;
			}
			for (int i = loopStartIndex; i < loopEndIndex; i++)
			{
				Item item = inv[i];
				if (item.stack >= 1 && item.type == itemToRefill.type && !item.favorited)
				{
					int num2 = item.stack;
					if (num2 > num)
					{
						num2 = num;
					}
					num -= num2;
					itemToRefill.stack += num2;
					item.stack -= num2;
					if (item.stack <= 0)
					{
						item.TurnToAir(false);
					}
					if (num <= 0)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x004E7664 File Offset: 0x004E5864
		private static void TrySlidingUp(Item[] inv, int slot, int minimumIndex)
		{
			for (int i = minimumIndex; i < slot; i++)
			{
				if (inv[i].IsAir)
				{
					Utils.Swap<Item>(ref inv[i], ref inv[slot]);
					return;
				}
			}
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0000357B File Offset: 0x0000177B
		public ItemSorting()
		{
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x004E769C File Offset: 0x004E589C
		// Note: this type is marked as 'beforefieldinit'.
		static ItemSorting()
		{
		}

		// Token: 0x0400131D RID: 4893
		private static List<ItemSorting.ItemSortingLayer> _layerList = new List<ItemSorting.ItemSortingLayer>();

		// Token: 0x0400131E RID: 4894
		private static Dictionary<string, List<int>> _layerWhiteLists = new Dictionary<string, List<int>>();

		// Token: 0x0400131F RID: 4895
		private static int[] _layerIndexForItemType;

		// Token: 0x04001320 RID: 4896
		private static int _layerCount;

		// Token: 0x04001321 RID: 4897
		private static List<ItemSorting.DamageTypeSortingLayerEntry> _damageRankings = new List<ItemSorting.DamageTypeSortingLayerEntry>();

		// Token: 0x04001322 RID: 4898
		private static readonly List<int> _sort_itemsToSort = new List<int>();

		// Token: 0x04001323 RID: 4899
		private static readonly List<int> _sort_sortedItemIndexes = new List<int>();

		// Token: 0x04001324 RID: 4900
		private static readonly List<int> _sort_counts = new List<int>();

		// Token: 0x04001325 RID: 4901
		private static readonly List<Item> _sort_itemsCache = new List<Item>();

		// Token: 0x04001326 RID: 4902
		private static readonly List<int> _sort_availableSortingSlots = new List<int>();

		// Token: 0x04001327 RID: 4903
		private static ItemSorting.MemoryStamp[] _sortInventory_preStamps = new ItemSorting.MemoryStamp[0];

		// Token: 0x04001328 RID: 4904
		private static ItemSorting.MemoryStamp[] _sortInventory_postStamps = new ItemSorting.MemoryStamp[0];

		// Token: 0x04001329 RID: 4905
		private static readonly List<int> _fillAmmoFromInventory_acceptedAmmoTypes = new List<int>();

		// Token: 0x0400132A RID: 4906
		private static readonly List<int> _fillAmmoFromInventory_emptyAmmoSlots = new List<int>();

		// Token: 0x02000703 RID: 1795
		private class ItemSortingLayer
		{
			// Token: 0x06003FE4 RID: 16356 RVA: 0x0069C189 File Offset: 0x0069A389
			public ItemSortingLayer(string name, Func<ItemSorting.ItemSortingLayer, Item[], List<int>, List<int>> method)
			{
				this.Name = name;
				this.SortingMethod = method;
			}

			// Token: 0x06003FE5 RID: 16357 RVA: 0x0069C1A0 File Offset: 0x0069A3A0
			public void Validate(ref List<int> indexesSortable, Item[] inv)
			{
				List<int> list;
				if (ItemSorting._layerWhiteLists.TryGetValue(this.Name, out list))
				{
					indexesSortable = indexesSortable.Where((int i) => list.Contains(inv[i].type)).ToList<int>();
				}
			}

			// Token: 0x06003FE6 RID: 16358 RVA: 0x0069C1EC File Offset: 0x0069A3EC
			public override string ToString()
			{
				return this.Name;
			}

			// Token: 0x04006862 RID: 26722
			public readonly string Name;

			// Token: 0x04006863 RID: 26723
			public readonly Func<ItemSorting.ItemSortingLayer, Item[], List<int>, List<int>> SortingMethod;

			// Token: 0x02000A51 RID: 2641
			[CompilerGenerated]
			private sealed class <>c__DisplayClass3_0
			{
				// Token: 0x06004AD2 RID: 19154 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass3_0()
				{
				}

				// Token: 0x06004AD3 RID: 19155 RVA: 0x006D4D90 File Offset: 0x006D2F90
				internal bool <Validate>b__0(int i)
				{
					return this.list.Contains(this.inv[i].type);
				}

				// Token: 0x04007730 RID: 30512
				public List<int> list;

				// Token: 0x04007731 RID: 30513
				public Item[] inv;
			}
		}

		// Token: 0x02000704 RID: 1796
		private class ItemSortingLayers
		{
			// Token: 0x06003FE7 RID: 16359 RVA: 0x0069C1F4 File Offset: 0x0069A3F4
			private static void SortIndicesStable(List<int> list, Comparison<int> comparison)
			{
				list.Sort(delegate(int x, int y)
				{
					int num = comparison(x, y);
					if (num == 0)
					{
						num = x.CompareTo(y);
					}
					return num;
				});
			}

			// Token: 0x06003FE8 RID: 16360 RVA: 0x0069C220 File Offset: 0x0069A420
			public static int CompareWithPrioritySet(int[] prioritySet, int typeOne, int typeTwo)
			{
				if (typeOne < 0 || typeTwo < 0)
				{
					return 0;
				}
				if (prioritySet[typeOne] >= 0 && prioritySet[typeTwo] < 0)
				{
					return -1;
				}
				if (prioritySet[typeOne] < 0 && prioritySet[typeTwo] >= 0)
				{
					return 1;
				}
				if (prioritySet[typeOne] < 0 && prioritySet[typeTwo] < 0)
				{
					return 0;
				}
				return prioritySet[typeOne].CompareTo(prioritySet[typeTwo]);
			}

			// Token: 0x06003FE9 RID: 16361 RVA: 0x0000357B File Offset: 0x0000177B
			public ItemSortingLayers()
			{
			}

			// Token: 0x06003FEA RID: 16362 RVA: 0x0069C270 File Offset: 0x0069A470
			// Note: this type is marked as 'beforefieldinit'.
			static ItemSortingLayers()
			{
			}

			// Token: 0x04006864 RID: 26724
			public static ItemSorting.ItemSortingLayer WeaponsMelee = new ItemSorting.ItemSortingLayer("Weapons - Melee", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list = itemsToSort.Where((int i) => inv[i].damage > 0 && !inv[i].consumable && inv[i].ammo == 0 && inv[i].melee && inv[i].pick < 1 && inv[i].hammer < 1 && inv[i].axe < 1).ToList<int>();
				layer.Validate(ref list, inv);
				foreach (int num in list)
				{
					itemsToSort.Remove(num);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
				{
					int num2 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num2 == 0)
					{
						num2 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					return num2;
				});
				return list;
			});

			// Token: 0x04006865 RID: 26725
			public static ItemSorting.ItemSortingLayer WeaponsRanged = new ItemSorting.ItemSortingLayer("Weapons - Ranged", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list2 = itemsToSort.Where((int i) => (inv[i].damage > 0 && !inv[i].consumable && inv[i].ammo == 0 && inv[i].ranged) || (inv[i].type >= 0 && ItemID.Sets.SortingPriorityWeaponsRanged[inv[i].type] > -1)).ToList<int>();
				layer.Validate(ref list2, inv);
				foreach (int num3 in list2)
				{
					itemsToSort.Remove(num3);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list2, delegate(int x, int y)
				{
					int num4 = ItemSorting.ItemSortingLayers.CompareWithPrioritySet(ItemID.Sets.SortingPriorityWeaponsRanged, inv[x].type, inv[y].type);
					if (num4 == 0)
					{
						num4 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num4 == 0)
					{
						num4 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					return num4;
				});
				return list2;
			});

			// Token: 0x04006866 RID: 26726
			public static ItemSorting.ItemSortingLayer WeaponsMagic = new ItemSorting.ItemSortingLayer("Weapons - Magic", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list3 = itemsToSort.Where((int i) => inv[i].damage > 0 && !inv[i].consumable && inv[i].ammo == 0 && inv[i].magic).ToList<int>();
				layer.Validate(ref list3, inv);
				foreach (int num5 in list3)
				{
					itemsToSort.Remove(num5);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list3, delegate(int x, int y)
				{
					int num6 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num6 == 0)
					{
						num6 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					return num6;
				});
				return list3;
			});

			// Token: 0x04006867 RID: 26727
			public static ItemSorting.ItemSortingLayer WeaponsMinions = new ItemSorting.ItemSortingLayer("Weapons - Minions", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list4 = itemsToSort.Where((int i) => inv[i].damage > 0 && !inv[i].consumable && inv[i].summon).ToList<int>();
				layer.Validate(ref list4, inv);
				foreach (int num7 in list4)
				{
					itemsToSort.Remove(num7);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list4, delegate(int x, int y)
				{
					int num8 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num8 == 0)
					{
						num8 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					return num8;
				});
				return list4;
			});

			// Token: 0x04006868 RID: 26728
			public static ItemSorting.ItemSortingLayer WeaponsAssorted = new ItemSorting.ItemSortingLayer("Weapons - Assorted", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list5 = itemsToSort.Where((int i) => inv[i].damage > 0 && inv[i].ammo == 0 && inv[i].pick == 0 && inv[i].axe == 0 && inv[i].hammer == 0).ToList<int>();
				layer.Validate(ref list5, inv);
				foreach (int num9 in list5)
				{
					itemsToSort.Remove(num9);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list5, delegate(int x, int y)
				{
					int num10 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num10 == 0)
					{
						num10 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					return num10;
				});
				return list5;
			});

			// Token: 0x04006869 RID: 26729
			public static ItemSorting.ItemSortingLayer WeaponsAmmo = new ItemSorting.ItemSortingLayer("Weapons - Ammo", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list6 = itemsToSort.Where((int i) => inv[i].ammo > 0 && inv[i].damage > 0).ToList<int>();
				layer.Validate(ref list6, inv);
				foreach (int num11 in list6)
				{
					itemsToSort.Remove(num11);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list6, delegate(int x, int y)
				{
					int num12 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num12 == 0)
					{
						num12 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					return num12;
				});
				return list6;
			});

			// Token: 0x0400686A RID: 26730
			public static ItemSorting.ItemSortingLayer ToolsPicksaws = new ItemSorting.ItemSortingLayer("Tools - Picksaws", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list7 = itemsToSort.Where((int i) => inv[i].pick > 0 && inv[i].axe > 0).ToList<int>();
				layer.Validate(ref list7, inv);
				foreach (int num13 in list7)
				{
					itemsToSort.Remove(num13);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list7, (int x, int y) => inv[x].pick.CompareTo(inv[y].pick));
				return list7;
			});

			// Token: 0x0400686B RID: 26731
			public static ItemSorting.ItemSortingLayer ToolsHamaxes = new ItemSorting.ItemSortingLayer("Tools - Hamaxes", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list8 = itemsToSort.Where((int i) => inv[i].hammer > 0 && inv[i].axe > 0).ToList<int>();
				layer.Validate(ref list8, inv);
				foreach (int num14 in list8)
				{
					itemsToSort.Remove(num14);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list8, (int x, int y) => inv[x].axe.CompareTo(inv[y].axe));
				return list8;
			});

			// Token: 0x0400686C RID: 26732
			public static ItemSorting.ItemSortingLayer ToolsPickaxes = new ItemSorting.ItemSortingLayer("Tools - Pickaxes", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list9 = itemsToSort.Where((int i) => inv[i].pick > 0).ToList<int>();
				layer.Validate(ref list9, inv);
				foreach (int num15 in list9)
				{
					itemsToSort.Remove(num15);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list9, (int x, int y) => inv[x].pick.CompareTo(inv[y].pick));
				return list9;
			});

			// Token: 0x0400686D RID: 26733
			public static ItemSorting.ItemSortingLayer ToolsAxes = new ItemSorting.ItemSortingLayer("Tools - Axes", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list10 = itemsToSort.Where((int i) => inv[i].axe > 0).ToList<int>();
				layer.Validate(ref list10, inv);
				foreach (int num16 in list10)
				{
					itemsToSort.Remove(num16);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list10, (int x, int y) => inv[x].axe.CompareTo(inv[y].axe));
				return list10;
			});

			// Token: 0x0400686E RID: 26734
			public static ItemSorting.ItemSortingLayer ToolsHammers = new ItemSorting.ItemSortingLayer("Tools - Hammers", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list11 = itemsToSort.Where((int i) => inv[i].hammer > 0).ToList<int>();
				layer.Validate(ref list11, inv);
				foreach (int num17 in list11)
				{
					itemsToSort.Remove(num17);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list11, (int x, int y) => inv[x].hammer.CompareTo(inv[y].hammer));
				return list11;
			});

			// Token: 0x0400686F RID: 26735
			public static ItemSorting.ItemSortingLayer ToolsTerraforming = new ItemSorting.ItemSortingLayer("Tools - Terraforming", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list12 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityTerraforming[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list12, inv);
				foreach (int num18 in list12)
				{
					itemsToSort.Remove(num18);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list12, delegate(int x, int y)
				{
					int num19 = ItemID.Sets.SortingPriorityTerraforming[inv[x].type].CompareTo(ItemID.Sets.SortingPriorityTerraforming[inv[y].type]);
					if (num19 == 0)
					{
						num19 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num19;
				});
				return list12;
			});

			// Token: 0x04006870 RID: 26736
			public static ItemSorting.ItemSortingLayer ToolsFishing = new ItemSorting.ItemSortingLayer("Tools - Fishing", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list13 = itemsToSort.Where((int i) => inv[i].fishingPole > 0 || inv[i].bait > 0 || inv[i].questItem || (inv[i].type > 0 && (ItemID.Sets.IsFishingCrate[inv[i].type] || ItemID.Sets.IsBasicFish[inv[i].type] || ItemID.Sets.SortingPriorityToolsFishing[inv[i].type] > -1))).ToList<int>();
				layer.Validate(ref list13, inv);
				foreach (int num20 in list13)
				{
					itemsToSort.Remove(num20);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list13, delegate(int x, int y)
				{
					int num21 = ItemSorting.ItemSortingLayers.CompareWithPrioritySet(ItemID.Sets.SortingPriorityToolsFishing, inv[x].type, inv[y].type);
					if (num21 == 0)
					{
						num21 = inv[y].fishingPole.CompareTo(inv[x].fishingPole);
					}
					if (num21 == 0)
					{
						num21 = inv[y].bait.CompareTo(inv[x].bait);
					}
					if (num21 == 0)
					{
						num21 = inv[y].questItem.CompareTo(inv[x].questItem);
					}
					if (num21 == 0 && inv[y].type >= 0 && inv[x].type >= 0)
					{
						if (num21 == 0)
						{
							num21 = ItemID.Sets.IsFishingCrate[inv[y].type].CompareTo(ItemID.Sets.IsFishingCrate[inv[x].type]);
						}
						if (num21 == 0)
						{
							num21 = ItemID.Sets.IsBasicFish[inv[y].type].CompareTo(ItemID.Sets.IsBasicFish[inv[x].type]);
						}
					}
					if (num21 == 0)
					{
						num21 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num21 == 0)
					{
						num21 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num21;
				});
				return list13;
			});

			// Token: 0x04006871 RID: 26737
			public static ItemSorting.ItemSortingLayer ToolsGolf = new ItemSorting.ItemSortingLayer("Tools - Golf", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list14 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsGolf[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list14, inv);
				foreach (int num22 in list14)
				{
					itemsToSort.Remove(num22);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list14, delegate(int x, int y)
				{
					int num23 = ItemID.Sets.SortingPriorityToolsGolf[inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsGolf[inv[y].type]);
					if (num23 == 0)
					{
						num23 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num23 == 0)
					{
						num23 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num23;
				});
				return list14;
			});

			// Token: 0x04006872 RID: 26738
			public static ItemSorting.ItemSortingLayer ToolsInstruments = new ItemSorting.ItemSortingLayer("Tools - Instruments", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list15 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsInstruments[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list15, inv);
				foreach (int num24 in list15)
				{
					itemsToSort.Remove(num24);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list15, delegate(int x, int y)
				{
					int num25 = ItemID.Sets.SortingPriorityToolsInstruments[inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsInstruments[inv[y].type]);
					if (num25 == 0)
					{
						num25 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num25 == 0)
					{
						num25 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num25;
				});
				return list15;
			});

			// Token: 0x04006873 RID: 26739
			public static ItemSorting.ItemSortingLayer ToolsKeys = new ItemSorting.ItemSortingLayer("Tools - Keys", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list16 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsKeys[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list16, inv);
				foreach (int num26 in list16)
				{
					itemsToSort.Remove(num26);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list16, delegate(int x, int y)
				{
					int num27 = ItemID.Sets.SortingPriorityToolsKeys[inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsKeys[inv[y].type]);
					if (num27 == 0)
					{
						num27 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num27 == 0)
					{
						num27 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num27;
				});
				return list16;
			});

			// Token: 0x04006874 RID: 26740
			public static ItemSorting.ItemSortingLayer ToolsKites = new ItemSorting.ItemSortingLayer("Tools - Kites", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list17 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsKites[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list17, inv);
				foreach (int num28 in list17)
				{
					itemsToSort.Remove(num28);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list17, delegate(int x, int y)
				{
					int num29 = ItemID.Sets.SortingPriorityToolsKites[inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsKites[inv[y].type]);
					if (num29 == 0)
					{
						num29 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num29 == 0)
					{
						num29 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num29;
				});
				return list17;
			});

			// Token: 0x04006875 RID: 26741
			public static ItemSorting.ItemSortingLayer ToolsAmmoLeftovers = new ItemSorting.ItemSortingLayer("Weapons - Ammo Leftovers", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list18 = itemsToSort.Where((int i) => inv[i].ammo > 0 && inv[i].type >= 0 && inv[i].type < (int)ItemID.Count && !ItemID.Sets.IsFood[inv[i].type] && ItemID.Sets.SortingPriorityMiscAcorns[inv[i].type] == -1).ToList<int>();
				layer.Validate(ref list18, inv);
				foreach (int num30 in list18)
				{
					itemsToSort.Remove(num30);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list18, delegate(int x, int y)
				{
					int num31 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num31 == 0)
					{
						num31 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					return num31;
				});
				return list18;
			});

			// Token: 0x04006876 RID: 26742
			public static ItemSorting.ItemSortingLayer ToolsMisc = new ItemSorting.ItemSortingLayer("Tools - Misc", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list19 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsMisc[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list19, inv);
				foreach (int num32 in list19)
				{
					itemsToSort.Remove(num32);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list19, delegate(int x, int y)
				{
					int num33 = ItemID.Sets.SortingPriorityToolsMisc[inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsMisc[inv[y].type]);
					if (num33 == 0)
					{
						num33 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num33 == 0)
					{
						num33 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num33;
				});
				return list19;
			});

			// Token: 0x04006877 RID: 26743
			public static ItemSorting.ItemSortingLayer ArmorCombat = new ItemSorting.ItemSortingLayer("Armor - Combat", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list20 = itemsToSort.Where((int i) => (inv[i].bodySlot >= 0 || inv[i].headSlot >= 0 || inv[i].legSlot >= 0) && !inv[i].vanity).ToList<int>();
				layer.Validate(ref list20, inv);
				foreach (int num34 in list20)
				{
					itemsToSort.Remove(num34);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list20, delegate(int x, int y)
				{
					int num35 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num35 == 0)
					{
						num35 = inv[y].OriginalDefense.CompareTo(inv[x].OriginalDefense);
					}
					if (num35 == 0)
					{
						num35 = inv[x].type.CompareTo(inv[y].type);
					}
					return num35;
				});
				return list20;
			});

			// Token: 0x04006878 RID: 26744
			public static ItemSorting.ItemSortingLayer ArmorVanity = new ItemSorting.ItemSortingLayer("Armor - Vanity", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list21 = itemsToSort.Where((int i) => (inv[i].bodySlot >= 0 || inv[i].headSlot >= 0 || inv[i].legSlot >= 0) && inv[i].vanity).ToList<int>();
				layer.Validate(ref list21, inv);
				foreach (int num36 in list21)
				{
					itemsToSort.Remove(num36);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list21, delegate(int x, int y)
				{
					int num37 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num37 == 0)
					{
						num37 = inv[x].type.CompareTo(inv[y].type);
					}
					return num37;
				});
				return list21;
			});

			// Token: 0x04006879 RID: 26745
			public static ItemSorting.ItemSortingLayer ArmorAccessories = new ItemSorting.ItemSortingLayer("Armor - Accessories", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list22 = itemsToSort.Where((int i) => inv[i].accessory).ToList<int>();
				layer.Validate(ref list22, inv);
				foreach (int num38 in list22)
				{
					itemsToSort.Remove(num38);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list22, delegate(int x, int y)
				{
					int num39 = inv[x].vanity.CompareTo(inv[y].vanity);
					if (num39 == 0)
					{
						num39 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num39 == 0)
					{
						num39 = inv[y].OriginalDefense.CompareTo(inv[x].OriginalDefense);
					}
					if (num39 == 0)
					{
						num39 = inv[x].type.CompareTo(inv[y].type);
					}
					return num39;
				});
				return list22;
			});

			// Token: 0x0400687A RID: 26746
			public static ItemSorting.ItemSortingLayer EquipGrapple = new ItemSorting.ItemSortingLayer("Equip - Grapple", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list23 = itemsToSort.Where((int i) => Main.projHook[inv[i].shoot]).ToList<int>();
				layer.Validate(ref list23, inv);
				foreach (int num40 in list23)
				{
					itemsToSort.Remove(num40);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list23, delegate(int x, int y)
				{
					int num41 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num41 == 0)
					{
						num41 = inv[x].type.CompareTo(inv[y].type);
					}
					return num41;
				});
				return list23;
			});

			// Token: 0x0400687B RID: 26747
			public static ItemSorting.ItemSortingLayer EquipMount = new ItemSorting.ItemSortingLayer("Equip - Mount", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list24 = itemsToSort.Where((int i) => inv[i].mountType != -1 && !MountID.Sets.Cart[inv[i].mountType]).ToList<int>();
				layer.Validate(ref list24, inv);
				foreach (int num42 in list24)
				{
					itemsToSort.Remove(num42);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list24, delegate(int x, int y)
				{
					int num43 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num43 == 0)
					{
						num43 = inv[x].type.CompareTo(inv[y].type);
					}
					return num43;
				});
				return list24;
			});

			// Token: 0x0400687C RID: 26748
			public static ItemSorting.ItemSortingLayer EquipCart = new ItemSorting.ItemSortingLayer("Equip - Cart", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list25 = itemsToSort.Where((int i) => inv[i].mountType != -1 && MountID.Sets.Cart[inv[i].mountType]).ToList<int>();
				layer.Validate(ref list25, inv);
				foreach (int num44 in list25)
				{
					itemsToSort.Remove(num44);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list25, delegate(int x, int y)
				{
					int num45 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num45 == 0)
					{
						num45 = inv[x].type.CompareTo(inv[y].type);
					}
					return num45;
				});
				return list25;
			});

			// Token: 0x0400687D RID: 26749
			public static ItemSorting.ItemSortingLayer EquipLightPet = new ItemSorting.ItemSortingLayer("Equip - Light Pet", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list26 = itemsToSort.Where((int i) => inv[i].buffType > 0 && Main.lightPet[inv[i].buffType]).ToList<int>();
				layer.Validate(ref list26, inv);
				foreach (int num46 in list26)
				{
					itemsToSort.Remove(num46);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list26, delegate(int x, int y)
				{
					int num47 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num47 == 0)
					{
						num47 = inv[x].type.CompareTo(inv[y].type);
					}
					return num47;
				});
				return list26;
			});

			// Token: 0x0400687E RID: 26750
			public static ItemSorting.ItemSortingLayer EquipVanityPet = new ItemSorting.ItemSortingLayer("Equip - Vanity Pet", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list27 = itemsToSort.Where((int i) => inv[i].buffType > 0 && Main.vanityPet[inv[i].buffType]).ToList<int>();
				layer.Validate(ref list27, inv);
				foreach (int num48 in list27)
				{
					itemsToSort.Remove(num48);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list27, delegate(int x, int y)
				{
					int num49 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num49 == 0)
					{
						num49 = inv[x].type.CompareTo(inv[y].type);
					}
					return num49;
				});
				return list27;
			});

			// Token: 0x0400687F RID: 26751
			public static ItemSorting.ItemSortingLayer PotionsLife = new ItemSorting.ItemSortingLayer("Potions - Life", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list28 = itemsToSort.Where((int i) => inv[i].consumable && inv[i].healLife > 0 && inv[i].healMana < 1 && inv[i].type != 5).ToList<int>();
				layer.Validate(ref list28, inv);
				foreach (int num50 in list28)
				{
					itemsToSort.Remove(num50);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list28, delegate(int x, int y)
				{
					int num51 = inv[y].healLife.CompareTo(inv[x].healLife);
					if (num51 == 0)
					{
						num51 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num51;
				});
				return list28;
			});

			// Token: 0x04006880 RID: 26752
			public static ItemSorting.ItemSortingLayer PotionsJustTheMushroom = new ItemSorting.ItemSortingLayer("Potions - Just The Mushroom", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list29 = itemsToSort.Where((int i) => inv[i].type == 5).ToList<int>();
				layer.Validate(ref list29, inv);
				foreach (int num52 in list29)
				{
					itemsToSort.Remove(num52);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list29, (int x, int y) => inv[y].stack.CompareTo(inv[x].stack));
				return list29;
			});

			// Token: 0x04006881 RID: 26753
			public static ItemSorting.ItemSortingLayer PotionsMana = new ItemSorting.ItemSortingLayer("Potions - Mana", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list30 = itemsToSort.Where((int i) => inv[i].consumable && inv[i].healLife < 1 && inv[i].healMana > 0).ToList<int>();
				layer.Validate(ref list30, inv);
				foreach (int num53 in list30)
				{
					itemsToSort.Remove(num53);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list30, delegate(int x, int y)
				{
					int num54 = inv[y].healMana.CompareTo(inv[x].healMana);
					if (num54 == 0)
					{
						num54 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num54;
				});
				return list30;
			});

			// Token: 0x04006882 RID: 26754
			public static ItemSorting.ItemSortingLayer PotionsElixirs = new ItemSorting.ItemSortingLayer("Potions - Elixirs", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list31 = itemsToSort.Where((int i) => inv[i].consumable && inv[i].healLife > 0 && inv[i].healMana > 0).ToList<int>();
				layer.Validate(ref list31, inv);
				foreach (int num55 in list31)
				{
					itemsToSort.Remove(num55);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list31, delegate(int x, int y)
				{
					int num56 = inv[y].healLife.CompareTo(inv[x].healLife);
					if (num56 == 0)
					{
						num56 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num56;
				});
				return list31;
			});

			// Token: 0x04006883 RID: 26755
			public static ItemSorting.ItemSortingLayer PotionsBuffs = new ItemSorting.ItemSortingLayer("Potions - Buffs", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list32 = itemsToSort.Where((int i) => (inv[i].consumable && inv[i].buffType > 0 && inv[i].type >= 0 && inv[i].type < (int)ItemID.Count && !ItemID.Sets.IsFood[inv[i].type]) || (inv[i].type >= 0 && ItemID.Sets.SortingPriorityPotionsBuffs[inv[i].type] > -1)).ToList<int>();
				layer.Validate(ref list32, inv);
				foreach (int num57 in list32)
				{
					itemsToSort.Remove(num57);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list32, delegate(int x, int y)
				{
					int num58 = ItemSorting.ItemSortingLayers.CompareWithPrioritySet(ItemID.Sets.SortingPriorityPotionsBuffs, inv[x].type, inv[y].type);
					if (num58 == 0)
					{
						num58 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num58 == 0)
					{
						num58 = inv[x].type.CompareTo(inv[y].type);
					}
					if (num58 == 0)
					{
						num58 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num58;
				});
				return list32;
			});

			// Token: 0x04006884 RID: 26756
			public static ItemSorting.ItemSortingLayer PotionsFood = new ItemSorting.ItemSortingLayer("Potions - Food", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list33 = itemsToSort.Where((int i) => inv[i].consumable && inv[i].buffType > 0 && inv[i].type >= 0 && inv[i].type < (int)ItemID.Count && ItemID.Sets.IsFood[inv[i].type]).ToList<int>();
				layer.Validate(ref list33, inv);
				foreach (int num59 in list33)
				{
					itemsToSort.Remove(num59);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list33, delegate(int x, int y)
				{
					int num60 = ((inv[y].buffType < 0 || inv[y].buffType >= BuffID.Count) ? 0 : BuffID.Sets.SortingPriorityFoodBuffs[inv[y].buffType].CompareTo(BuffID.Sets.SortingPriorityFoodBuffs[inv[x].buffType]));
					if (num60 == 0)
					{
						num60 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num60 == 0)
					{
						num60 = inv[x].type.CompareTo(inv[y].type);
					}
					if (num60 == 0)
					{
						num60 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num60;
				});
				return list33;
			});

			// Token: 0x04006885 RID: 26757
			public static ItemSorting.ItemSortingLayer PotionsDyes = new ItemSorting.ItemSortingLayer("Potions - Dyes", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list34 = itemsToSort.Where((int i) => inv[i].dye > 0 || (inv[i].type >= 0 && ItemID.Sets.SortingPriorityPotionsDyeMaterial[inv[i].type] > -1)).ToList<int>();
				layer.Validate(ref list34, inv);
				foreach (int num61 in list34)
				{
					itemsToSort.Remove(num61);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list34, delegate(int x, int y)
				{
					int num62 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num62 == 0)
					{
						num62 = inv[y].dye.CompareTo(inv[x].dye);
					}
					if (num62 == 0)
					{
						num62 = ItemSorting.ItemSortingLayers.CompareWithPrioritySet(ItemID.Sets.SortingPriorityPotionsDyeMaterial, inv[x].type, inv[y].type);
					}
					if (num62 == 0)
					{
						num62 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num62;
				});
				return list34;
			});

			// Token: 0x04006886 RID: 26758
			public static ItemSorting.ItemSortingLayer PotionsHairDyes = new ItemSorting.ItemSortingLayer("Potions - Hair Dyes", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list35 = itemsToSort.Where((int i) => inv[i].hairDye >= 0).ToList<int>();
				layer.Validate(ref list35, inv);
				foreach (int num63 in list35)
				{
					itemsToSort.Remove(num63);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list35, delegate(int x, int y)
				{
					int num64 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num64 == 0)
					{
						num64 = inv[y].hairDye.CompareTo(inv[x].hairDye);
					}
					if (num64 == 0)
					{
						num64 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num64;
				});
				return list35;
			});

			// Token: 0x04006887 RID: 26759
			public static ItemSorting.ItemSortingLayer MiscValuables = new ItemSorting.ItemSortingLayer("Misc - Importants", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list36 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscImportants[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list36, inv);
				foreach (int num65 in list36)
				{
					itemsToSort.Remove(num65);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list36, delegate(int x, int y)
				{
					int num66 = ItemID.Sets.SortingPriorityMiscImportants[inv[x].type].CompareTo(ItemID.Sets.SortingPriorityMiscImportants[inv[y].type]);
					if (num66 == 0)
					{
						num66 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num66;
				});
				return list36;
			});

			// Token: 0x04006888 RID: 26760
			public static ItemSorting.ItemSortingLayer MiscWiring = new ItemSorting.ItemSortingLayer("Misc - Wiring", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list37 = itemsToSort.Where((int i) => (inv[i].type > 0 && ItemID.Sets.SortingPriorityWiring[inv[i].type] > -1) || inv[i].mech).ToList<int>();
				layer.Validate(ref list37, inv);
				foreach (int num67 in list37)
				{
					itemsToSort.Remove(num67);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list37, delegate(int x, int y)
				{
					int num68 = ItemID.Sets.SortingPriorityWiring[inv[y].type].CompareTo(ItemID.Sets.SortingPriorityWiring[inv[x].type]);
					if (num68 == 0)
					{
						num68 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num68 == 0)
					{
						num68 = inv[y].type.CompareTo(inv[x].type);
					}
					if (num68 == 0)
					{
						num68 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num68;
				});
				return list37;
			});

			// Token: 0x04006889 RID: 26761
			public static ItemSorting.ItemSortingLayer MiscMaterials = new ItemSorting.ItemSortingLayer("Misc - Materials", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list38 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityMaterials[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list38, inv);
				foreach (int num69 in list38)
				{
					itemsToSort.Remove(num69);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list38, delegate(int x, int y)
				{
					int num70 = ItemID.Sets.SortingPriorityMaterials[inv[y].type].CompareTo(ItemID.Sets.SortingPriorityMaterials[inv[x].type]);
					if (num70 == 0)
					{
						num70 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num70;
				});
				return list38;
			});

			// Token: 0x0400688A RID: 26762
			public static ItemSorting.ItemSortingLayer MiscJustTheGlowingMushroom = new ItemSorting.ItemSortingLayer("Misc - Just The Glowing Mushroom", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list39 = itemsToSort.Where((int i) => inv[i].type == 183).ToList<int>();
				layer.Validate(ref list39, inv);
				foreach (int num71 in list39)
				{
					itemsToSort.Remove(num71);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list39, (int x, int y) => inv[y].stack.CompareTo(inv[x].stack));
				return list39;
			});

			// Token: 0x0400688B RID: 26763
			public static ItemSorting.ItemSortingLayer MiscExtractinator = new ItemSorting.ItemSortingLayer("Misc - Extractinator", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list40 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityExtractibles[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list40, inv);
				foreach (int num72 in list40)
				{
					itemsToSort.Remove(num72);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list40, delegate(int x, int y)
				{
					int num73 = ItemID.Sets.SortingPriorityExtractibles[inv[y].type].CompareTo(ItemID.Sets.SortingPriorityExtractibles[inv[x].type]);
					if (num73 == 0)
					{
						num73 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num73;
				});
				return list40;
			});

			// Token: 0x0400688C RID: 26764
			public static ItemSorting.ItemSortingLayer MiscPainting = new ItemSorting.ItemSortingLayer("Misc - Painting", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list41 = itemsToSort.Where((int i) => (inv[i].type > 0 && ItemID.Sets.SortingPriorityPainting[inv[i].type] > -1) || inv[i].paint > 0).ToList<int>();
				layer.Validate(ref list41, inv);
				foreach (int num74 in list41)
				{
					itemsToSort.Remove(num74);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list41, delegate(int x, int y)
				{
					int num75 = ItemID.Sets.SortingPriorityPainting[inv[y].type].CompareTo(ItemID.Sets.SortingPriorityPainting[inv[x].type]);
					if (num75 == 0)
					{
						num75 = inv[x].paint.CompareTo(inv[y].paint);
					}
					if (num75 == 0)
					{
						num75 = inv[x].paintCoating.CompareTo(inv[y].paintCoating);
					}
					if (num75 == 0)
					{
						num75 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num75;
				});
				return list41;
			});

			// Token: 0x0400688D RID: 26765
			public static ItemSorting.ItemSortingLayer MiscRopes = new ItemSorting.ItemSortingLayer("Misc - Ropes", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list42 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityRopes[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list42, inv);
				foreach (int num76 in list42)
				{
					itemsToSort.Remove(num76);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list42, delegate(int x, int y)
				{
					int num77 = ItemID.Sets.SortingPriorityRopes[inv[y].type].CompareTo(ItemID.Sets.SortingPriorityRopes[inv[x].type]);
					if (num77 == 0)
					{
						num77 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num77;
				});
				return list42;
			});

			// Token: 0x0400688E RID: 26766
			public static ItemSorting.ItemSortingLayer MiscHerbsAndSeeds = new ItemSorting.ItemSortingLayer("Misc - Herbs And Seeds", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list43 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscHerbsAndSeeds[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list43, inv);
				foreach (int num78 in list43)
				{
					itemsToSort.Remove(num78);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list43, delegate(int x, int y)
				{
					int num79 = ItemID.Sets.SortingPriorityMiscHerbsAndSeeds[inv[y].type].CompareTo(ItemID.Sets.SortingPriorityMiscHerbsAndSeeds[inv[x].type]);
					if (num79 == 0)
					{
						num79 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num79;
				});
				return list43;
			});

			// Token: 0x0400688F RID: 26767
			public static ItemSorting.ItemSortingLayer MiscGems = new ItemSorting.ItemSortingLayer("Misc - Gems", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list44 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscGems[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list44, inv);
				foreach (int num80 in list44)
				{
					itemsToSort.Remove(num80);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list44, delegate(int x, int y)
				{
					int num81 = ItemID.Sets.SortingPriorityMiscGems[inv[y].type].CompareTo(ItemID.Sets.SortingPriorityMiscGems[inv[x].type]);
					if (num81 == 0)
					{
						num81 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num81;
				});
				return list44;
			});

			// Token: 0x04006890 RID: 26768
			public static ItemSorting.ItemSortingLayer MiscAcorns = new ItemSorting.ItemSortingLayer("Misc - Acorns", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list45 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscAcorns[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list45, inv);
				foreach (int num82 in list45)
				{
					itemsToSort.Remove(num82);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list45, delegate(int x, int y)
				{
					int num83 = ItemID.Sets.SortingPriorityMiscAcorns[inv[y].type].CompareTo(ItemID.Sets.SortingPriorityMiscAcorns[inv[x].type]);
					if (num83 == 0)
					{
						num83 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num83;
				});
				return list45;
			});

			// Token: 0x04006891 RID: 26769
			public static ItemSorting.ItemSortingLayer MiscBossBags = new ItemSorting.ItemSortingLayer("Misc - Boss Bags", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list46 = itemsToSort.Where((int i) => inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscBossBags[inv[i].type] > -1).ToList<int>();
				layer.Validate(ref list46, inv);
				foreach (int num84 in list46)
				{
					itemsToSort.Remove(num84);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list46, delegate(int x, int y)
				{
					int num85 = ItemID.Sets.SortingPriorityMiscBossBags[inv[x].type].CompareTo(ItemID.Sets.SortingPriorityMiscBossBags[inv[y].type]);
					if (num85 == 0)
					{
						num85 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num85 == 0)
					{
						num85 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num85;
				});
				return list46;
			});

			// Token: 0x04006892 RID: 26770
			public static ItemSorting.ItemSortingLayer MiscCritters = new ItemSorting.ItemSortingLayer("Misc - Critters", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list47 = itemsToSort.Where((int i) => inv[i].makeNPC > 0).ToList<int>();
				layer.Validate(ref list47, inv);
				foreach (int num86 in list47)
				{
					itemsToSort.Remove(num86);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list47, delegate(int x, int y)
				{
					int num87 = inv[x].makeNPC.CompareTo(inv[y].makeNPC);
					if (num87 == 0)
					{
						num87 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num87;
				});
				return list47;
			});

			// Token: 0x04006893 RID: 26771
			public static ItemSorting.ItemSortingLayer LastMaterials = new ItemSorting.ItemSortingLayer("Last - Materials", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list48 = itemsToSort.Where((int i) => inv[i].createTile < 0 && inv[i].createWall < 1 && inv[i].rare != -1).ToList<int>();
				layer.Validate(ref list48, inv);
				foreach (int num88 in list48)
				{
					itemsToSort.Remove(num88);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list48, delegate(int x, int y)
				{
					int num89 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num89 == 0)
					{
						num89 = inv[y].value.CompareTo(inv[x].value);
					}
					if (num89 == 0)
					{
						num89 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num89;
				});
				return list48;
			});

			// Token: 0x04006894 RID: 26772
			public static ItemSorting.ItemSortingLayer LastTilesImportant = new ItemSorting.ItemSortingLayer("Last - Tiles (Frame Important)", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list49 = itemsToSort.Where((int i) => inv[i].createTile >= 0 && Main.tileFrameImportant[inv[i].createTile]).ToList<int>();
				layer.Validate(ref list49, inv);
				foreach (int num90 in list49)
				{
					itemsToSort.Remove(num90);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list49, delegate(int x, int y)
				{
					int num91 = string.Compare(inv[x].Name, inv[y].Name, StringComparison.OrdinalIgnoreCase);
					if (num91 == 0)
					{
						num91 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num91;
				});
				return list49;
			});

			// Token: 0x04006895 RID: 26773
			public static ItemSorting.ItemSortingLayer LastTilesCommon = new ItemSorting.ItemSortingLayer("Last - Tiles (Common), Walls", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list50 = itemsToSort.Where((int i) => inv[i].createWall > 0 || inv[i].createTile >= 0).ToList<int>();
				layer.Validate(ref list50, inv);
				foreach (int num92 in list50)
				{
					itemsToSort.Remove(num92);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list50, delegate(int x, int y)
				{
					int num93 = string.Compare(inv[x].Name, inv[y].Name, StringComparison.OrdinalIgnoreCase);
					if (num93 == 0)
					{
						num93 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num93;
				});
				return list50;
			});

			// Token: 0x04006896 RID: 26774
			public static ItemSorting.ItemSortingLayer LastNotTrash = new ItemSorting.ItemSortingLayer("Last - Not Trash", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list51 = itemsToSort.Where((int i) => inv[i].OriginalRarity >= 0).ToList<int>();
				layer.Validate(ref list51, inv);
				foreach (int num94 in list51)
				{
					itemsToSort.Remove(num94);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list51, delegate(int x, int y)
				{
					int num95 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num95 == 0)
					{
						num95 = string.Compare(inv[x].Name, inv[y].Name, StringComparison.OrdinalIgnoreCase);
					}
					if (num95 == 0)
					{
						num95 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num95;
				});
				return list51;
			});

			// Token: 0x04006897 RID: 26775
			public static ItemSorting.ItemSortingLayer LastTrash = new ItemSorting.ItemSortingLayer("Last - Trash", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> list52 = new List<int>(itemsToSort);
				layer.Validate(ref list52, inv);
				foreach (int num96 in list52)
				{
					itemsToSort.Remove(num96);
				}
				ItemSorting.ItemSortingLayers.SortIndicesStable(list52, delegate(int x, int y)
				{
					int num97 = inv[y].value.CompareTo(inv[x].value);
					if (num97 == 0)
					{
						num97 = inv[y].stack.CompareTo(inv[x].stack);
					}
					return num97;
				});
				return list52;
			});

			// Token: 0x02000A52 RID: 2642
			[CompilerGenerated]
			private sealed class <>c__DisplayClass0_0
			{
				// Token: 0x06004AD4 RID: 19156 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass0_0()
				{
				}

				// Token: 0x06004AD5 RID: 19157 RVA: 0x006D4DAC File Offset: 0x006D2FAC
				internal int <SortIndicesStable>b__0(int x, int y)
				{
					int num = this.comparison(x, y);
					if (num == 0)
					{
						num = x.CompareTo(y);
					}
					return num;
				}

				// Token: 0x04007732 RID: 30514
				public Comparison<int> comparison;
			}

			// Token: 0x02000A53 RID: 2643
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_0
			{
				// Token: 0x06004AD6 RID: 19158 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_0()
				{
				}

				// Token: 0x06004AD7 RID: 19159 RVA: 0x006D4DD4 File Offset: 0x006D2FD4
				internal bool <.cctor>b__52(int i)
				{
					return this.inv[i].damage > 0 && !this.inv[i].consumable && this.inv[i].ammo == 0 && this.inv[i].melee && this.inv[i].pick < 1 && this.inv[i].hammer < 1 && this.inv[i].axe < 1;
				}

				// Token: 0x06004AD8 RID: 19160 RVA: 0x006D4E50 File Offset: 0x006D3050
				internal int <.cctor>b__53(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[y].OriginalDamage.CompareTo(this.inv[x].OriginalDamage);
					}
					return num;
				}

				// Token: 0x04007733 RID: 30515
				public Item[] inv;
			}

			// Token: 0x02000A54 RID: 2644
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_1
			{
				// Token: 0x06004AD9 RID: 19161 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_1()
				{
				}

				// Token: 0x06004ADA RID: 19162 RVA: 0x006D4EA8 File Offset: 0x006D30A8
				internal bool <.cctor>b__54(int i)
				{
					return (this.inv[i].damage > 0 && !this.inv[i].consumable && this.inv[i].ammo == 0 && this.inv[i].ranged) || (this.inv[i].type >= 0 && ItemID.Sets.SortingPriorityWeaponsRanged[this.inv[i].type] > -1);
				}

				// Token: 0x06004ADB RID: 19163 RVA: 0x006D4F1C File Offset: 0x006D311C
				internal int <.cctor>b__55(int x, int y)
				{
					int num = ItemSorting.ItemSortingLayers.CompareWithPrioritySet(ItemID.Sets.SortingPriorityWeaponsRanged, this.inv[x].type, this.inv[y].type);
					if (num == 0)
					{
						num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					}
					if (num == 0)
					{
						num = this.inv[y].OriginalDamage.CompareTo(this.inv[x].OriginalDamage);
					}
					return num;
				}

				// Token: 0x04007734 RID: 30516
				public Item[] inv;
			}

			// Token: 0x02000A55 RID: 2645
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_2
			{
				// Token: 0x06004ADC RID: 19164 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_2()
				{
				}

				// Token: 0x06004ADD RID: 19165 RVA: 0x006D4F9B File Offset: 0x006D319B
				internal bool <.cctor>b__56(int i)
				{
					return this.inv[i].damage > 0 && !this.inv[i].consumable && this.inv[i].ammo == 0 && this.inv[i].magic;
				}

				// Token: 0x06004ADE RID: 19166 RVA: 0x006D4FDC File Offset: 0x006D31DC
				internal int <.cctor>b__57(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[y].OriginalDamage.CompareTo(this.inv[x].OriginalDamage);
					}
					return num;
				}

				// Token: 0x04007735 RID: 30517
				public Item[] inv;
			}

			// Token: 0x02000A56 RID: 2646
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_3
			{
				// Token: 0x06004ADF RID: 19167 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_3()
				{
				}

				// Token: 0x06004AE0 RID: 19168 RVA: 0x006D5033 File Offset: 0x006D3233
				internal bool <.cctor>b__58(int i)
				{
					return this.inv[i].damage > 0 && !this.inv[i].consumable && this.inv[i].summon;
				}

				// Token: 0x06004AE1 RID: 19169 RVA: 0x006D5064 File Offset: 0x006D3264
				internal int <.cctor>b__59(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[y].OriginalDamage.CompareTo(this.inv[x].OriginalDamage);
					}
					return num;
				}

				// Token: 0x04007736 RID: 30518
				public Item[] inv;
			}

			// Token: 0x02000A57 RID: 2647
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_4
			{
				// Token: 0x06004AE2 RID: 19170 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_4()
				{
				}

				// Token: 0x06004AE3 RID: 19171 RVA: 0x006D50BC File Offset: 0x006D32BC
				internal bool <.cctor>b__60(int i)
				{
					return this.inv[i].damage > 0 && this.inv[i].ammo == 0 && this.inv[i].pick == 0 && this.inv[i].axe == 0 && this.inv[i].hammer == 0;
				}

				// Token: 0x06004AE4 RID: 19172 RVA: 0x006D5118 File Offset: 0x006D3318
				internal int <.cctor>b__61(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[y].OriginalDamage.CompareTo(this.inv[x].OriginalDamage);
					}
					return num;
				}

				// Token: 0x04007737 RID: 30519
				public Item[] inv;
			}

			// Token: 0x02000A58 RID: 2648
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_5
			{
				// Token: 0x06004AE5 RID: 19173 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_5()
				{
				}

				// Token: 0x06004AE6 RID: 19174 RVA: 0x006D516F File Offset: 0x006D336F
				internal bool <.cctor>b__62(int i)
				{
					return this.inv[i].ammo > 0 && this.inv[i].damage > 0;
				}

				// Token: 0x06004AE7 RID: 19175 RVA: 0x006D5194 File Offset: 0x006D3394
				internal int <.cctor>b__63(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[y].OriginalDamage.CompareTo(this.inv[x].OriginalDamage);
					}
					return num;
				}

				// Token: 0x04007738 RID: 30520
				public Item[] inv;
			}

			// Token: 0x02000A59 RID: 2649
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_6
			{
				// Token: 0x06004AE8 RID: 19176 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_6()
				{
				}

				// Token: 0x06004AE9 RID: 19177 RVA: 0x006D51EB File Offset: 0x006D33EB
				internal bool <.cctor>b__64(int i)
				{
					return this.inv[i].pick > 0 && this.inv[i].axe > 0;
				}

				// Token: 0x06004AEA RID: 19178 RVA: 0x006D520F File Offset: 0x006D340F
				internal int <.cctor>b__65(int x, int y)
				{
					return this.inv[x].pick.CompareTo(this.inv[y].pick);
				}

				// Token: 0x04007739 RID: 30521
				public Item[] inv;
			}

			// Token: 0x02000A5A RID: 2650
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_7
			{
				// Token: 0x06004AEB RID: 19179 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_7()
				{
				}

				// Token: 0x06004AEC RID: 19180 RVA: 0x006D5230 File Offset: 0x006D3430
				internal bool <.cctor>b__66(int i)
				{
					return this.inv[i].hammer > 0 && this.inv[i].axe > 0;
				}

				// Token: 0x06004AED RID: 19181 RVA: 0x006D5254 File Offset: 0x006D3454
				internal int <.cctor>b__67(int x, int y)
				{
					return this.inv[x].axe.CompareTo(this.inv[y].axe);
				}

				// Token: 0x0400773A RID: 30522
				public Item[] inv;
			}

			// Token: 0x02000A5B RID: 2651
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_8
			{
				// Token: 0x06004AEE RID: 19182 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_8()
				{
				}

				// Token: 0x06004AEF RID: 19183 RVA: 0x006D5275 File Offset: 0x006D3475
				internal bool <.cctor>b__68(int i)
				{
					return this.inv[i].pick > 0;
				}

				// Token: 0x06004AF0 RID: 19184 RVA: 0x006D5287 File Offset: 0x006D3487
				internal int <.cctor>b__69(int x, int y)
				{
					return this.inv[x].pick.CompareTo(this.inv[y].pick);
				}

				// Token: 0x0400773B RID: 30523
				public Item[] inv;
			}

			// Token: 0x02000A5C RID: 2652
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_9
			{
				// Token: 0x06004AF1 RID: 19185 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_9()
				{
				}

				// Token: 0x06004AF2 RID: 19186 RVA: 0x006D52A8 File Offset: 0x006D34A8
				internal bool <.cctor>b__70(int i)
				{
					return this.inv[i].axe > 0;
				}

				// Token: 0x06004AF3 RID: 19187 RVA: 0x006D52BA File Offset: 0x006D34BA
				internal int <.cctor>b__71(int x, int y)
				{
					return this.inv[x].axe.CompareTo(this.inv[y].axe);
				}

				// Token: 0x0400773C RID: 30524
				public Item[] inv;
			}

			// Token: 0x02000A5D RID: 2653
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_10
			{
				// Token: 0x06004AF4 RID: 19188 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_10()
				{
				}

				// Token: 0x06004AF5 RID: 19189 RVA: 0x006D52DB File Offset: 0x006D34DB
				internal bool <.cctor>b__72(int i)
				{
					return this.inv[i].hammer > 0;
				}

				// Token: 0x06004AF6 RID: 19190 RVA: 0x006D52ED File Offset: 0x006D34ED
				internal int <.cctor>b__73(int x, int y)
				{
					return this.inv[x].hammer.CompareTo(this.inv[y].hammer);
				}

				// Token: 0x0400773D RID: 30525
				public Item[] inv;
			}

			// Token: 0x02000A5E RID: 2654
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_11
			{
				// Token: 0x06004AF7 RID: 19191 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_11()
				{
				}

				// Token: 0x06004AF8 RID: 19192 RVA: 0x006D530E File Offset: 0x006D350E
				internal bool <.cctor>b__74(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityTerraforming[this.inv[i].type] > -1;
				}

				// Token: 0x06004AF9 RID: 19193 RVA: 0x006D5338 File Offset: 0x006D3538
				internal int <.cctor>b__75(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityTerraforming[this.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityTerraforming[this.inv[y].type]);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x0400773E RID: 30526
				public Item[] inv;
			}

			// Token: 0x02000A5F RID: 2655
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_12
			{
				// Token: 0x06004AFA RID: 19194 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_12()
				{
				}

				// Token: 0x06004AFB RID: 19195 RVA: 0x006D539C File Offset: 0x006D359C
				internal bool <.cctor>b__76(int i)
				{
					return this.inv[i].fishingPole > 0 || this.inv[i].bait > 0 || this.inv[i].questItem || (this.inv[i].type > 0 && (ItemID.Sets.IsFishingCrate[this.inv[i].type] || ItemID.Sets.IsBasicFish[this.inv[i].type] || ItemID.Sets.SortingPriorityToolsFishing[this.inv[i].type] > -1));
				}

				// Token: 0x06004AFC RID: 19196 RVA: 0x006D5430 File Offset: 0x006D3630
				internal int <.cctor>b__77(int x, int y)
				{
					int num = ItemSorting.ItemSortingLayers.CompareWithPrioritySet(ItemID.Sets.SortingPriorityToolsFishing, this.inv[x].type, this.inv[y].type);
					if (num == 0)
					{
						num = this.inv[y].fishingPole.CompareTo(this.inv[x].fishingPole);
					}
					if (num == 0)
					{
						num = this.inv[y].bait.CompareTo(this.inv[x].bait);
					}
					if (num == 0)
					{
						num = this.inv[y].questItem.CompareTo(this.inv[x].questItem);
					}
					if (num == 0 && this.inv[y].type >= 0 && this.inv[x].type >= 0)
					{
						if (num == 0)
						{
							num = ItemID.Sets.IsFishingCrate[this.inv[y].type].CompareTo(ItemID.Sets.IsFishingCrate[this.inv[x].type]);
						}
						if (num == 0)
						{
							num = ItemID.Sets.IsBasicFish[this.inv[y].type].CompareTo(ItemID.Sets.IsBasicFish[this.inv[x].type]);
						}
					}
					if (num == 0)
					{
						num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x0400773F RID: 30527
				public Item[] inv;
			}

			// Token: 0x02000A60 RID: 2656
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_13
			{
				// Token: 0x06004AFD RID: 19197 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_13()
				{
				}

				// Token: 0x06004AFE RID: 19198 RVA: 0x006D55A1 File Offset: 0x006D37A1
				internal bool <.cctor>b__78(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsGolf[this.inv[i].type] > -1;
				}

				// Token: 0x06004AFF RID: 19199 RVA: 0x006D55CC File Offset: 0x006D37CC
				internal int <.cctor>b__79(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityToolsGolf[this.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsGolf[this.inv[y].type]);
					if (num == 0)
					{
						num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007740 RID: 30528
				public Item[] inv;
			}

			// Token: 0x02000A61 RID: 2657
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_14
			{
				// Token: 0x06004B00 RID: 19200 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_14()
				{
				}

				// Token: 0x06004B01 RID: 19201 RVA: 0x006D5653 File Offset: 0x006D3853
				internal bool <.cctor>b__80(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsInstruments[this.inv[i].type] > -1;
				}

				// Token: 0x06004B02 RID: 19202 RVA: 0x006D5680 File Offset: 0x006D3880
				internal int <.cctor>b__81(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityToolsInstruments[this.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsInstruments[this.inv[y].type]);
					if (num == 0)
					{
						num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007741 RID: 30529
				public Item[] inv;
			}

			// Token: 0x02000A62 RID: 2658
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_15
			{
				// Token: 0x06004B03 RID: 19203 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_15()
				{
				}

				// Token: 0x06004B04 RID: 19204 RVA: 0x006D5707 File Offset: 0x006D3907
				internal bool <.cctor>b__82(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsKeys[this.inv[i].type] > -1;
				}

				// Token: 0x06004B05 RID: 19205 RVA: 0x006D5734 File Offset: 0x006D3934
				internal int <.cctor>b__83(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityToolsKeys[this.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsKeys[this.inv[y].type]);
					if (num == 0)
					{
						num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007742 RID: 30530
				public Item[] inv;
			}

			// Token: 0x02000A63 RID: 2659
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_16
			{
				// Token: 0x06004B06 RID: 19206 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_16()
				{
				}

				// Token: 0x06004B07 RID: 19207 RVA: 0x006D57BB File Offset: 0x006D39BB
				internal bool <.cctor>b__84(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsKites[this.inv[i].type] > -1;
				}

				// Token: 0x06004B08 RID: 19208 RVA: 0x006D57E8 File Offset: 0x006D39E8
				internal int <.cctor>b__85(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityToolsKites[this.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsKites[this.inv[y].type]);
					if (num == 0)
					{
						num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007743 RID: 30531
				public Item[] inv;
			}

			// Token: 0x02000A64 RID: 2660
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_17
			{
				// Token: 0x06004B09 RID: 19209 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_17()
				{
				}

				// Token: 0x06004B0A RID: 19210 RVA: 0x006D5870 File Offset: 0x006D3A70
				internal bool <.cctor>b__86(int i)
				{
					return this.inv[i].ammo > 0 && this.inv[i].type >= 0 && this.inv[i].type < (int)ItemID.Count && !ItemID.Sets.IsFood[this.inv[i].type] && ItemID.Sets.SortingPriorityMiscAcorns[this.inv[i].type] == -1;
				}

				// Token: 0x06004B0B RID: 19211 RVA: 0x006D58E0 File Offset: 0x006D3AE0
				internal int <.cctor>b__87(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[y].OriginalDamage.CompareTo(this.inv[x].OriginalDamage);
					}
					return num;
				}

				// Token: 0x04007744 RID: 30532
				public Item[] inv;
			}

			// Token: 0x02000A65 RID: 2661
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_18
			{
				// Token: 0x06004B0C RID: 19212 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_18()
				{
				}

				// Token: 0x06004B0D RID: 19213 RVA: 0x006D5937 File Offset: 0x006D3B37
				internal bool <.cctor>b__88(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsMisc[this.inv[i].type] > -1;
				}

				// Token: 0x06004B0E RID: 19214 RVA: 0x006D5964 File Offset: 0x006D3B64
				internal int <.cctor>b__89(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityToolsMisc[this.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsMisc[this.inv[y].type]);
					if (num == 0)
					{
						num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007745 RID: 30533
				public Item[] inv;
			}

			// Token: 0x02000A66 RID: 2662
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_19
			{
				// Token: 0x06004B0F RID: 19215 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_19()
				{
				}

				// Token: 0x06004B10 RID: 19216 RVA: 0x006D59EC File Offset: 0x006D3BEC
				internal bool <.cctor>b__90(int i)
				{
					return (this.inv[i].bodySlot >= 0 || this.inv[i].headSlot >= 0 || this.inv[i].legSlot >= 0) && !this.inv[i].vanity;
				}

				// Token: 0x06004B11 RID: 19217 RVA: 0x006D5A3C File Offset: 0x006D3C3C
				internal int <.cctor>b__91(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[y].OriginalDefense.CompareTo(this.inv[x].OriginalDefense);
					}
					if (num == 0)
					{
						num = this.inv[x].type.CompareTo(this.inv[y].type);
					}
					return num;
				}

				// Token: 0x04007746 RID: 30534
				public Item[] inv;
			}

			// Token: 0x02000A67 RID: 2663
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_20
			{
				// Token: 0x06004B12 RID: 19218 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_20()
				{
				}

				// Token: 0x06004B13 RID: 19219 RVA: 0x006D5AB8 File Offset: 0x006D3CB8
				internal bool <.cctor>b__92(int i)
				{
					return (this.inv[i].bodySlot >= 0 || this.inv[i].headSlot >= 0 || this.inv[i].legSlot >= 0) && this.inv[i].vanity;
				}

				// Token: 0x06004B14 RID: 19220 RVA: 0x006D5B04 File Offset: 0x006D3D04
				internal int <.cctor>b__93(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[x].type.CompareTo(this.inv[y].type);
					}
					return num;
				}

				// Token: 0x04007747 RID: 30535
				public Item[] inv;
			}

			// Token: 0x02000A68 RID: 2664
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_21
			{
				// Token: 0x06004B15 RID: 19221 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_21()
				{
				}

				// Token: 0x06004B16 RID: 19222 RVA: 0x006D5B58 File Offset: 0x006D3D58
				internal bool <.cctor>b__94(int i)
				{
					return this.inv[i].accessory;
				}

				// Token: 0x06004B17 RID: 19223 RVA: 0x006D5B68 File Offset: 0x006D3D68
				internal int <.cctor>b__95(int x, int y)
				{
					int num = this.inv[x].vanity.CompareTo(this.inv[y].vanity);
					if (num == 0)
					{
						num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					}
					if (num == 0)
					{
						num = this.inv[y].OriginalDefense.CompareTo(this.inv[x].OriginalDefense);
					}
					if (num == 0)
					{
						num = this.inv[x].type.CompareTo(this.inv[y].type);
					}
					return num;
				}

				// Token: 0x04007748 RID: 30536
				public Item[] inv;
			}

			// Token: 0x02000A69 RID: 2665
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_22
			{
				// Token: 0x06004B18 RID: 19224 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_22()
				{
				}

				// Token: 0x06004B19 RID: 19225 RVA: 0x006D5C05 File Offset: 0x006D3E05
				internal bool <.cctor>b__96(int i)
				{
					return Main.projHook[this.inv[i].shoot];
				}

				// Token: 0x06004B1A RID: 19226 RVA: 0x006D5C1C File Offset: 0x006D3E1C
				internal int <.cctor>b__97(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[x].type.CompareTo(this.inv[y].type);
					}
					return num;
				}

				// Token: 0x04007749 RID: 30537
				public Item[] inv;
			}

			// Token: 0x02000A6A RID: 2666
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_23
			{
				// Token: 0x06004B1B RID: 19227 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_23()
				{
				}

				// Token: 0x06004B1C RID: 19228 RVA: 0x006D5C70 File Offset: 0x006D3E70
				internal bool <.cctor>b__98(int i)
				{
					return this.inv[i].mountType != -1 && !MountID.Sets.Cart[this.inv[i].mountType];
				}

				// Token: 0x06004B1D RID: 19229 RVA: 0x006D5C9C File Offset: 0x006D3E9C
				internal int <.cctor>b__99(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[x].type.CompareTo(this.inv[y].type);
					}
					return num;
				}

				// Token: 0x0400774A RID: 30538
				public Item[] inv;
			}

			// Token: 0x02000A6B RID: 2667
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_24
			{
				// Token: 0x06004B1E RID: 19230 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_24()
				{
				}

				// Token: 0x06004B1F RID: 19231 RVA: 0x006D5CF0 File Offset: 0x006D3EF0
				internal bool <.cctor>b__100(int i)
				{
					return this.inv[i].mountType != -1 && MountID.Sets.Cart[this.inv[i].mountType];
				}

				// Token: 0x06004B20 RID: 19232 RVA: 0x006D5D18 File Offset: 0x006D3F18
				internal int <.cctor>b__101(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[x].type.CompareTo(this.inv[y].type);
					}
					return num;
				}

				// Token: 0x0400774B RID: 30539
				public Item[] inv;
			}

			// Token: 0x02000A6C RID: 2668
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_25
			{
				// Token: 0x06004B21 RID: 19233 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_25()
				{
				}

				// Token: 0x06004B22 RID: 19234 RVA: 0x006D5D6C File Offset: 0x006D3F6C
				internal bool <.cctor>b__102(int i)
				{
					return this.inv[i].buffType > 0 && Main.lightPet[this.inv[i].buffType];
				}

				// Token: 0x06004B23 RID: 19235 RVA: 0x006D5D94 File Offset: 0x006D3F94
				internal int <.cctor>b__103(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[x].type.CompareTo(this.inv[y].type);
					}
					return num;
				}

				// Token: 0x0400774C RID: 30540
				public Item[] inv;
			}

			// Token: 0x02000A6D RID: 2669
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_26
			{
				// Token: 0x06004B24 RID: 19236 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_26()
				{
				}

				// Token: 0x06004B25 RID: 19237 RVA: 0x006D5DE8 File Offset: 0x006D3FE8
				internal bool <.cctor>b__104(int i)
				{
					return this.inv[i].buffType > 0 && Main.vanityPet[this.inv[i].buffType];
				}

				// Token: 0x06004B26 RID: 19238 RVA: 0x006D5E10 File Offset: 0x006D4010
				internal int <.cctor>b__105(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[x].type.CompareTo(this.inv[y].type);
					}
					return num;
				}

				// Token: 0x0400774D RID: 30541
				public Item[] inv;
			}

			// Token: 0x02000A6E RID: 2670
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_27
			{
				// Token: 0x06004B27 RID: 19239 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_27()
				{
				}

				// Token: 0x06004B28 RID: 19240 RVA: 0x006D5E64 File Offset: 0x006D4064
				internal bool <.cctor>b__106(int i)
				{
					return this.inv[i].consumable && this.inv[i].healLife > 0 && this.inv[i].healMana < 1 && this.inv[i].type != 5;
				}

				// Token: 0x06004B29 RID: 19241 RVA: 0x006D5EB8 File Offset: 0x006D40B8
				internal int <.cctor>b__107(int x, int y)
				{
					int num = this.inv[y].healLife.CompareTo(this.inv[x].healLife);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x0400774E RID: 30542
				public Item[] inv;
			}

			// Token: 0x02000A6F RID: 2671
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_28
			{
				// Token: 0x06004B2A RID: 19242 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_28()
				{
				}

				// Token: 0x06004B2B RID: 19243 RVA: 0x006D5F09 File Offset: 0x006D4109
				internal bool <.cctor>b__108(int i)
				{
					return this.inv[i].type == 5;
				}

				// Token: 0x06004B2C RID: 19244 RVA: 0x006D5F1B File Offset: 0x006D411B
				internal int <.cctor>b__109(int x, int y)
				{
					return this.inv[y].stack.CompareTo(this.inv[x].stack);
				}

				// Token: 0x0400774F RID: 30543
				public Item[] inv;
			}

			// Token: 0x02000A70 RID: 2672
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_29
			{
				// Token: 0x06004B2D RID: 19245 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_29()
				{
				}

				// Token: 0x06004B2E RID: 19246 RVA: 0x006D5F3C File Offset: 0x006D413C
				internal bool <.cctor>b__110(int i)
				{
					return this.inv[i].consumable && this.inv[i].healLife < 1 && this.inv[i].healMana > 0;
				}

				// Token: 0x06004B2F RID: 19247 RVA: 0x006D5F70 File Offset: 0x006D4170
				internal int <.cctor>b__111(int x, int y)
				{
					int num = this.inv[y].healMana.CompareTo(this.inv[x].healMana);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007750 RID: 30544
				public Item[] inv;
			}

			// Token: 0x02000A71 RID: 2673
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_30
			{
				// Token: 0x06004B30 RID: 19248 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_30()
				{
				}

				// Token: 0x06004B31 RID: 19249 RVA: 0x006D5FC1 File Offset: 0x006D41C1
				internal bool <.cctor>b__112(int i)
				{
					return this.inv[i].consumable && this.inv[i].healLife > 0 && this.inv[i].healMana > 0;
				}

				// Token: 0x06004B32 RID: 19250 RVA: 0x006D5FF4 File Offset: 0x006D41F4
				internal int <.cctor>b__113(int x, int y)
				{
					int num = this.inv[y].healLife.CompareTo(this.inv[x].healLife);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007751 RID: 30545
				public Item[] inv;
			}

			// Token: 0x02000A72 RID: 2674
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_31
			{
				// Token: 0x06004B33 RID: 19251 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_31()
				{
				}

				// Token: 0x06004B34 RID: 19252 RVA: 0x006D6048 File Offset: 0x006D4248
				internal bool <.cctor>b__114(int i)
				{
					return (this.inv[i].consumable && this.inv[i].buffType > 0 && this.inv[i].type >= 0 && this.inv[i].type < (int)ItemID.Count && !ItemID.Sets.IsFood[this.inv[i].type]) || (this.inv[i].type >= 0 && ItemID.Sets.SortingPriorityPotionsBuffs[this.inv[i].type] > -1);
				}

				// Token: 0x06004B35 RID: 19253 RVA: 0x006D60D8 File Offset: 0x006D42D8
				internal int <.cctor>b__115(int x, int y)
				{
					int num = ItemSorting.ItemSortingLayers.CompareWithPrioritySet(ItemID.Sets.SortingPriorityPotionsBuffs, this.inv[x].type, this.inv[y].type);
					if (num == 0)
					{
						num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					}
					if (num == 0)
					{
						num = this.inv[x].type.CompareTo(this.inv[y].type);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007752 RID: 30546
				public Item[] inv;
			}

			// Token: 0x02000A73 RID: 2675
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_32
			{
				// Token: 0x06004B36 RID: 19254 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_32()
				{
				}

				// Token: 0x06004B37 RID: 19255 RVA: 0x006D6178 File Offset: 0x006D4378
				internal bool <.cctor>b__116(int i)
				{
					return this.inv[i].consumable && this.inv[i].buffType > 0 && this.inv[i].type >= 0 && this.inv[i].type < (int)ItemID.Count && ItemID.Sets.IsFood[this.inv[i].type];
				}

				// Token: 0x06004B38 RID: 19256 RVA: 0x006D61E0 File Offset: 0x006D43E0
				internal int <.cctor>b__117(int x, int y)
				{
					int num = ((this.inv[y].buffType < 0 || this.inv[y].buffType >= BuffID.Count) ? 0 : BuffID.Sets.SortingPriorityFoodBuffs[this.inv[y].buffType].CompareTo(BuffID.Sets.SortingPriorityFoodBuffs[this.inv[x].buffType]));
					if (num == 0)
					{
						num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					}
					if (num == 0)
					{
						num = this.inv[x].type.CompareTo(this.inv[y].type);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007753 RID: 30547
				public Item[] inv;
			}

			// Token: 0x02000A74 RID: 2676
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_33
			{
				// Token: 0x06004B39 RID: 19257 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_33()
				{
				}

				// Token: 0x06004B3A RID: 19258 RVA: 0x006D62B1 File Offset: 0x006D44B1
				internal bool <.cctor>b__118(int i)
				{
					return this.inv[i].dye > 0 || (this.inv[i].type >= 0 && ItemID.Sets.SortingPriorityPotionsDyeMaterial[this.inv[i].type] > -1);
				}

				// Token: 0x06004B3B RID: 19259 RVA: 0x006D62F0 File Offset: 0x006D44F0
				internal int <.cctor>b__119(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[y].dye.CompareTo(this.inv[x].dye);
					}
					if (num == 0)
					{
						num = ItemSorting.ItemSortingLayers.CompareWithPrioritySet(ItemID.Sets.SortingPriorityPotionsDyeMaterial, this.inv[x].type, this.inv[y].type);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007754 RID: 30548
				public Item[] inv;
			}

			// Token: 0x02000A75 RID: 2677
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_34
			{
				// Token: 0x06004B3C RID: 19260 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_34()
				{
				}

				// Token: 0x06004B3D RID: 19261 RVA: 0x006D638F File Offset: 0x006D458F
				internal bool <.cctor>b__120(int i)
				{
					return this.inv[i].hairDye >= 0;
				}

				// Token: 0x06004B3E RID: 19262 RVA: 0x006D63A4 File Offset: 0x006D45A4
				internal int <.cctor>b__121(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[y].hairDye.CompareTo(this.inv[x].hairDye);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007755 RID: 30549
				public Item[] inv;
			}

			// Token: 0x02000A76 RID: 2678
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_35
			{
				// Token: 0x06004B3F RID: 19263 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_35()
				{
				}

				// Token: 0x06004B40 RID: 19264 RVA: 0x006D641B File Offset: 0x006D461B
				internal bool <.cctor>b__122(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscImportants[this.inv[i].type] > -1;
				}

				// Token: 0x06004B41 RID: 19265 RVA: 0x006D6448 File Offset: 0x006D4648
				internal int <.cctor>b__123(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityMiscImportants[this.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityMiscImportants[this.inv[y].type]);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007756 RID: 30550
				public Item[] inv;
			}

			// Token: 0x02000A77 RID: 2679
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_36
			{
				// Token: 0x06004B42 RID: 19266 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_36()
				{
				}

				// Token: 0x06004B43 RID: 19267 RVA: 0x006D64A9 File Offset: 0x006D46A9
				internal bool <.cctor>b__124(int i)
				{
					return (this.inv[i].type > 0 && ItemID.Sets.SortingPriorityWiring[this.inv[i].type] > -1) || this.inv[i].mech;
				}

				// Token: 0x06004B44 RID: 19268 RVA: 0x006D64E0 File Offset: 0x006D46E0
				internal int <.cctor>b__125(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityWiring[this.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityWiring[this.inv[x].type]);
					if (num == 0)
					{
						num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					}
					if (num == 0)
					{
						num = this.inv[y].type.CompareTo(this.inv[x].type);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007757 RID: 30551
				public Item[] inv;
			}

			// Token: 0x02000A78 RID: 2680
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_37
			{
				// Token: 0x06004B45 RID: 19269 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_37()
				{
				}

				// Token: 0x06004B46 RID: 19270 RVA: 0x006D658A File Offset: 0x006D478A
				internal bool <.cctor>b__126(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityMaterials[this.inv[i].type] > -1;
				}

				// Token: 0x06004B47 RID: 19271 RVA: 0x006D65B4 File Offset: 0x006D47B4
				internal int <.cctor>b__127(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityMaterials[this.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityMaterials[this.inv[x].type]);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007758 RID: 30552
				public Item[] inv;
			}

			// Token: 0x02000A79 RID: 2681
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_38
			{
				// Token: 0x06004B48 RID: 19272 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_38()
				{
				}

				// Token: 0x06004B49 RID: 19273 RVA: 0x006D6615 File Offset: 0x006D4815
				internal bool <.cctor>b__128(int i)
				{
					return this.inv[i].type == 183;
				}

				// Token: 0x06004B4A RID: 19274 RVA: 0x006D662B File Offset: 0x006D482B
				internal int <.cctor>b__129(int x, int y)
				{
					return this.inv[y].stack.CompareTo(this.inv[x].stack);
				}

				// Token: 0x04007759 RID: 30553
				public Item[] inv;
			}

			// Token: 0x02000A7A RID: 2682
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_39
			{
				// Token: 0x06004B4B RID: 19275 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_39()
				{
				}

				// Token: 0x06004B4C RID: 19276 RVA: 0x006D664C File Offset: 0x006D484C
				internal bool <.cctor>b__130(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityExtractibles[this.inv[i].type] > -1;
				}

				// Token: 0x06004B4D RID: 19277 RVA: 0x006D6678 File Offset: 0x006D4878
				internal int <.cctor>b__131(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityExtractibles[this.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityExtractibles[this.inv[x].type]);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x0400775A RID: 30554
				public Item[] inv;
			}

			// Token: 0x02000A7B RID: 2683
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_40
			{
				// Token: 0x06004B4E RID: 19278 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_40()
				{
				}

				// Token: 0x06004B4F RID: 19279 RVA: 0x006D66D9 File Offset: 0x006D48D9
				internal bool <.cctor>b__132(int i)
				{
					return (this.inv[i].type > 0 && ItemID.Sets.SortingPriorityPainting[this.inv[i].type] > -1) || this.inv[i].paint > 0;
				}

				// Token: 0x06004B50 RID: 19280 RVA: 0x006D6714 File Offset: 0x006D4914
				internal int <.cctor>b__133(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityPainting[this.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityPainting[this.inv[x].type]);
					if (num == 0)
					{
						num = this.inv[x].paint.CompareTo(this.inv[y].paint);
					}
					if (num == 0)
					{
						num = this.inv[x].paintCoating.CompareTo(this.inv[y].paintCoating);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x0400775B RID: 30555
				public Item[] inv;
			}

			// Token: 0x02000A7C RID: 2684
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_41
			{
				// Token: 0x06004B51 RID: 19281 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_41()
				{
				}

				// Token: 0x06004B52 RID: 19282 RVA: 0x006D67BB File Offset: 0x006D49BB
				internal bool <.cctor>b__134(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityRopes[this.inv[i].type] > -1;
				}

				// Token: 0x06004B53 RID: 19283 RVA: 0x006D67E8 File Offset: 0x006D49E8
				internal int <.cctor>b__135(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityRopes[this.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityRopes[this.inv[x].type]);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x0400775C RID: 30556
				public Item[] inv;
			}

			// Token: 0x02000A7D RID: 2685
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_42
			{
				// Token: 0x06004B54 RID: 19284 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_42()
				{
				}

				// Token: 0x06004B55 RID: 19285 RVA: 0x006D6849 File Offset: 0x006D4A49
				internal bool <.cctor>b__136(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscHerbsAndSeeds[this.inv[i].type] > -1;
				}

				// Token: 0x06004B56 RID: 19286 RVA: 0x006D6874 File Offset: 0x006D4A74
				internal int <.cctor>b__137(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityMiscHerbsAndSeeds[this.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityMiscHerbsAndSeeds[this.inv[x].type]);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x0400775D RID: 30557
				public Item[] inv;
			}

			// Token: 0x02000A7E RID: 2686
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_43
			{
				// Token: 0x06004B57 RID: 19287 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_43()
				{
				}

				// Token: 0x06004B58 RID: 19288 RVA: 0x006D68D5 File Offset: 0x006D4AD5
				internal bool <.cctor>b__138(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscGems[this.inv[i].type] > -1;
				}

				// Token: 0x06004B59 RID: 19289 RVA: 0x006D6900 File Offset: 0x006D4B00
				internal int <.cctor>b__139(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityMiscGems[this.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityMiscGems[this.inv[x].type]);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x0400775E RID: 30558
				public Item[] inv;
			}

			// Token: 0x02000A7F RID: 2687
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_44
			{
				// Token: 0x06004B5A RID: 19290 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_44()
				{
				}

				// Token: 0x06004B5B RID: 19291 RVA: 0x006D6961 File Offset: 0x006D4B61
				internal bool <.cctor>b__140(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscAcorns[this.inv[i].type] > -1;
				}

				// Token: 0x06004B5C RID: 19292 RVA: 0x006D698C File Offset: 0x006D4B8C
				internal int <.cctor>b__141(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityMiscAcorns[this.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityMiscAcorns[this.inv[x].type]);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x0400775F RID: 30559
				public Item[] inv;
			}

			// Token: 0x02000A80 RID: 2688
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_45
			{
				// Token: 0x06004B5D RID: 19293 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_45()
				{
				}

				// Token: 0x06004B5E RID: 19294 RVA: 0x006D69ED File Offset: 0x006D4BED
				internal bool <.cctor>b__142(int i)
				{
					return this.inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscBossBags[this.inv[i].type] > -1;
				}

				// Token: 0x06004B5F RID: 19295 RVA: 0x006D6A18 File Offset: 0x006D4C18
				internal int <.cctor>b__143(int x, int y)
				{
					int num = ItemID.Sets.SortingPriorityMiscBossBags[this.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityMiscBossBags[this.inv[y].type]);
					if (num == 0)
					{
						num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007760 RID: 30560
				public Item[] inv;
			}

			// Token: 0x02000A81 RID: 2689
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_46
			{
				// Token: 0x06004B60 RID: 19296 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_46()
				{
				}

				// Token: 0x06004B61 RID: 19297 RVA: 0x006D6A9F File Offset: 0x006D4C9F
				internal bool <.cctor>b__144(int i)
				{
					return this.inv[i].makeNPC > 0;
				}

				// Token: 0x06004B62 RID: 19298 RVA: 0x006D6AB4 File Offset: 0x006D4CB4
				internal int <.cctor>b__145(int x, int y)
				{
					int num = this.inv[x].makeNPC.CompareTo(this.inv[y].makeNPC);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007761 RID: 30561
				public Item[] inv;
			}

			// Token: 0x02000A82 RID: 2690
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_47
			{
				// Token: 0x06004B63 RID: 19299 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_47()
				{
				}

				// Token: 0x06004B64 RID: 19300 RVA: 0x006D6B05 File Offset: 0x006D4D05
				internal bool <.cctor>b__146(int i)
				{
					return this.inv[i].createTile < 0 && this.inv[i].createWall < 1 && this.inv[i].rare != -1;
				}

				// Token: 0x06004B65 RID: 19301 RVA: 0x006D6B3C File Offset: 0x006D4D3C
				internal int <.cctor>b__147(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = this.inv[y].value.CompareTo(this.inv[x].value);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007762 RID: 30562
				public Item[] inv;
			}

			// Token: 0x02000A83 RID: 2691
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_48
			{
				// Token: 0x06004B66 RID: 19302 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_48()
				{
				}

				// Token: 0x06004B67 RID: 19303 RVA: 0x006D6BB3 File Offset: 0x006D4DB3
				internal bool <.cctor>b__148(int i)
				{
					return this.inv[i].createTile >= 0 && Main.tileFrameImportant[this.inv[i].createTile];
				}

				// Token: 0x06004B68 RID: 19304 RVA: 0x006D6BDC File Offset: 0x006D4DDC
				internal int <.cctor>b__149(int x, int y)
				{
					int num = string.Compare(this.inv[x].Name, this.inv[y].Name, StringComparison.OrdinalIgnoreCase);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007763 RID: 30563
				public Item[] inv;
			}

			// Token: 0x02000A84 RID: 2692
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_49
			{
				// Token: 0x06004B69 RID: 19305 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_49()
				{
				}

				// Token: 0x06004B6A RID: 19306 RVA: 0x006D6C2E File Offset: 0x006D4E2E
				internal bool <.cctor>b__150(int i)
				{
					return this.inv[i].createWall > 0 || this.inv[i].createTile >= 0;
				}

				// Token: 0x06004B6B RID: 19307 RVA: 0x006D6C58 File Offset: 0x006D4E58
				internal int <.cctor>b__151(int x, int y)
				{
					int num = string.Compare(this.inv[x].Name, this.inv[y].Name, StringComparison.OrdinalIgnoreCase);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007764 RID: 30564
				public Item[] inv;
			}

			// Token: 0x02000A85 RID: 2693
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_50
			{
				// Token: 0x06004B6C RID: 19308 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_50()
				{
				}

				// Token: 0x06004B6D RID: 19309 RVA: 0x006D6CAA File Offset: 0x006D4EAA
				internal bool <.cctor>b__152(int i)
				{
					return this.inv[i].OriginalRarity >= 0;
				}

				// Token: 0x06004B6E RID: 19310 RVA: 0x006D6CC0 File Offset: 0x006D4EC0
				internal int <.cctor>b__153(int x, int y)
				{
					int num = this.inv[y].OriginalRarity.CompareTo(this.inv[x].OriginalRarity);
					if (num == 0)
					{
						num = string.Compare(this.inv[x].Name, this.inv[y].Name, StringComparison.OrdinalIgnoreCase);
					}
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007765 RID: 30565
				public Item[] inv;
			}

			// Token: 0x02000A86 RID: 2694
			[CompilerGenerated]
			private sealed class <>c__DisplayClass55_51
			{
				// Token: 0x06004B6F RID: 19311 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass55_51()
				{
				}

				// Token: 0x06004B70 RID: 19312 RVA: 0x006D6D38 File Offset: 0x006D4F38
				internal int <.cctor>b__154(int x, int y)
				{
					int num = this.inv[y].value.CompareTo(this.inv[x].value);
					if (num == 0)
					{
						num = this.inv[y].stack.CompareTo(this.inv[x].stack);
					}
					return num;
				}

				// Token: 0x04007766 RID: 30566
				public Item[] inv;
			}

			// Token: 0x02000A87 RID: 2695
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004B71 RID: 19313 RVA: 0x006D6D89 File Offset: 0x006D4F89
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004B72 RID: 19314 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004B73 RID: 19315 RVA: 0x006D6D98 File Offset: 0x006D4F98
				internal List<int> <.cctor>b__55_0(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_0 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_0();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].damage > 0 && !CS$<>8__locals1.inv[i].consumable && CS$<>8__locals1.inv[i].ammo == 0 && CS$<>8__locals1.inv[i].melee && CS$<>8__locals1.inv[i].pick < 1 && CS$<>8__locals1.inv[i].hammer < 1 && CS$<>8__locals1.inv[i].axe < 1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalDamage.CompareTo(CS$<>8__locals1.inv[x].OriginalDamage);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B74 RID: 19316 RVA: 0x006D6E30 File Offset: 0x006D5030
				internal List<int> <.cctor>b__55_1(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_1 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_1();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => (CS$<>8__locals1.inv[i].damage > 0 && !CS$<>8__locals1.inv[i].consumable && CS$<>8__locals1.inv[i].ammo == 0 && CS$<>8__locals1.inv[i].ranged) || (CS$<>8__locals1.inv[i].type >= 0 && ItemID.Sets.SortingPriorityWeaponsRanged[CS$<>8__locals1.inv[i].type] > -1)).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemSorting.ItemSortingLayers.CompareWithPrioritySet(ItemID.Sets.SortingPriorityWeaponsRanged, CS$<>8__locals1.inv[x].type, CS$<>8__locals1.inv[y].type);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalDamage.CompareTo(CS$<>8__locals1.inv[x].OriginalDamage);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B75 RID: 19317 RVA: 0x006D6EC8 File Offset: 0x006D50C8
				internal List<int> <.cctor>b__55_2(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_2 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_2();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].damage > 0 && !CS$<>8__locals1.inv[i].consumable && CS$<>8__locals1.inv[i].ammo == 0 && CS$<>8__locals1.inv[i].magic).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalDamage.CompareTo(CS$<>8__locals1.inv[x].OriginalDamage);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B76 RID: 19318 RVA: 0x006D6F60 File Offset: 0x006D5160
				internal List<int> <.cctor>b__55_3(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_3 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_3();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].damage > 0 && !CS$<>8__locals1.inv[i].consumable && CS$<>8__locals1.inv[i].summon).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalDamage.CompareTo(CS$<>8__locals1.inv[x].OriginalDamage);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B77 RID: 19319 RVA: 0x006D6FF8 File Offset: 0x006D51F8
				internal List<int> <.cctor>b__55_4(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_4 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_4();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].damage > 0 && CS$<>8__locals1.inv[i].ammo == 0 && CS$<>8__locals1.inv[i].pick == 0 && CS$<>8__locals1.inv[i].axe == 0 && CS$<>8__locals1.inv[i].hammer == 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalDamage.CompareTo(CS$<>8__locals1.inv[x].OriginalDamage);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B78 RID: 19320 RVA: 0x006D7090 File Offset: 0x006D5290
				internal List<int> <.cctor>b__55_5(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_5 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_5();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].ammo > 0 && CS$<>8__locals1.inv[i].damage > 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalDamage.CompareTo(CS$<>8__locals1.inv[x].OriginalDamage);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B79 RID: 19321 RVA: 0x006D7128 File Offset: 0x006D5328
				internal List<int> <.cctor>b__55_6(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_6 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_6();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].pick > 0 && CS$<>8__locals1.inv[i].axe > 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, (int x, int y) => CS$<>8__locals1.inv[x].pick.CompareTo(CS$<>8__locals1.inv[y].pick));
					return list;
				}

				// Token: 0x06004B7A RID: 19322 RVA: 0x006D71C0 File Offset: 0x006D53C0
				internal List<int> <.cctor>b__55_7(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_7 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_7();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].hammer > 0 && CS$<>8__locals1.inv[i].axe > 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, (int x, int y) => CS$<>8__locals1.inv[x].axe.CompareTo(CS$<>8__locals1.inv[y].axe));
					return list;
				}

				// Token: 0x06004B7B RID: 19323 RVA: 0x006D7258 File Offset: 0x006D5458
				internal List<int> <.cctor>b__55_8(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_8 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_8();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].pick > 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, (int x, int y) => CS$<>8__locals1.inv[x].pick.CompareTo(CS$<>8__locals1.inv[y].pick));
					return list;
				}

				// Token: 0x06004B7C RID: 19324 RVA: 0x006D72F0 File Offset: 0x006D54F0
				internal List<int> <.cctor>b__55_9(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_9 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_9();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].axe > 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, (int x, int y) => CS$<>8__locals1.inv[x].axe.CompareTo(CS$<>8__locals1.inv[y].axe));
					return list;
				}

				// Token: 0x06004B7D RID: 19325 RVA: 0x006D7388 File Offset: 0x006D5588
				internal List<int> <.cctor>b__55_10(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_10 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_10();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].hammer > 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, (int x, int y) => CS$<>8__locals1.inv[x].hammer.CompareTo(CS$<>8__locals1.inv[y].hammer));
					return list;
				}

				// Token: 0x06004B7E RID: 19326 RVA: 0x006D7420 File Offset: 0x006D5620
				internal List<int> <.cctor>b__55_11(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_11 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_11();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityTerraforming[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityTerraforming[CS$<>8__locals1.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityTerraforming[CS$<>8__locals1.inv[y].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B7F RID: 19327 RVA: 0x006D74B8 File Offset: 0x006D56B8
				internal List<int> <.cctor>b__55_12(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_12 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_12();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].fishingPole > 0 || CS$<>8__locals1.inv[i].bait > 0 || CS$<>8__locals1.inv[i].questItem || (CS$<>8__locals1.inv[i].type > 0 && (ItemID.Sets.IsFishingCrate[CS$<>8__locals1.inv[i].type] || ItemID.Sets.IsBasicFish[CS$<>8__locals1.inv[i].type] || ItemID.Sets.SortingPriorityToolsFishing[CS$<>8__locals1.inv[i].type] > -1))).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemSorting.ItemSortingLayers.CompareWithPrioritySet(ItemID.Sets.SortingPriorityToolsFishing, CS$<>8__locals1.inv[x].type, CS$<>8__locals1.inv[y].type);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].fishingPole.CompareTo(CS$<>8__locals1.inv[x].fishingPole);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].bait.CompareTo(CS$<>8__locals1.inv[x].bait);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].questItem.CompareTo(CS$<>8__locals1.inv[x].questItem);
						}
						if (num2 == 0 && CS$<>8__locals1.inv[y].type >= 0 && CS$<>8__locals1.inv[x].type >= 0)
						{
							if (num2 == 0)
							{
								num2 = ItemID.Sets.IsFishingCrate[CS$<>8__locals1.inv[y].type].CompareTo(ItemID.Sets.IsFishingCrate[CS$<>8__locals1.inv[x].type]);
							}
							if (num2 == 0)
							{
								num2 = ItemID.Sets.IsBasicFish[CS$<>8__locals1.inv[y].type].CompareTo(ItemID.Sets.IsBasicFish[CS$<>8__locals1.inv[x].type]);
							}
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B80 RID: 19328 RVA: 0x006D7550 File Offset: 0x006D5750
				internal List<int> <.cctor>b__55_13(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_13 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_13();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsGolf[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityToolsGolf[CS$<>8__locals1.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsGolf[CS$<>8__locals1.inv[y].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B81 RID: 19329 RVA: 0x006D75E8 File Offset: 0x006D57E8
				internal List<int> <.cctor>b__55_14(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_14 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_14();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsInstruments[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityToolsInstruments[CS$<>8__locals1.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsInstruments[CS$<>8__locals1.inv[y].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B82 RID: 19330 RVA: 0x006D7680 File Offset: 0x006D5880
				internal List<int> <.cctor>b__55_15(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_15 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_15();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsKeys[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityToolsKeys[CS$<>8__locals1.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsKeys[CS$<>8__locals1.inv[y].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B83 RID: 19331 RVA: 0x006D7718 File Offset: 0x006D5918
				internal List<int> <.cctor>b__55_16(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_16 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_16();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsKites[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityToolsKites[CS$<>8__locals1.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsKites[CS$<>8__locals1.inv[y].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B84 RID: 19332 RVA: 0x006D77B0 File Offset: 0x006D59B0
				internal List<int> <.cctor>b__55_17(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_17 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_17();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].ammo > 0 && CS$<>8__locals1.inv[i].type >= 0 && CS$<>8__locals1.inv[i].type < (int)ItemID.Count && !ItemID.Sets.IsFood[CS$<>8__locals1.inv[i].type] && ItemID.Sets.SortingPriorityMiscAcorns[CS$<>8__locals1.inv[i].type] == -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalDamage.CompareTo(CS$<>8__locals1.inv[x].OriginalDamage);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B85 RID: 19333 RVA: 0x006D7848 File Offset: 0x006D5A48
				internal List<int> <.cctor>b__55_18(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_18 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_18();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityToolsMisc[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityToolsMisc[CS$<>8__locals1.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityToolsMisc[CS$<>8__locals1.inv[y].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B86 RID: 19334 RVA: 0x006D78E0 File Offset: 0x006D5AE0
				internal List<int> <.cctor>b__55_19(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_19 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_19();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => (CS$<>8__locals1.inv[i].bodySlot >= 0 || CS$<>8__locals1.inv[i].headSlot >= 0 || CS$<>8__locals1.inv[i].legSlot >= 0) && !CS$<>8__locals1.inv[i].vanity).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalDefense.CompareTo(CS$<>8__locals1.inv[x].OriginalDefense);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[x].type.CompareTo(CS$<>8__locals1.inv[y].type);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B87 RID: 19335 RVA: 0x006D7978 File Offset: 0x006D5B78
				internal List<int> <.cctor>b__55_20(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_20 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_20();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => (CS$<>8__locals1.inv[i].bodySlot >= 0 || CS$<>8__locals1.inv[i].headSlot >= 0 || CS$<>8__locals1.inv[i].legSlot >= 0) && CS$<>8__locals1.inv[i].vanity).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[x].type.CompareTo(CS$<>8__locals1.inv[y].type);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B88 RID: 19336 RVA: 0x006D7A10 File Offset: 0x006D5C10
				internal List<int> <.cctor>b__55_21(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_21 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_21();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].accessory).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[x].vanity.CompareTo(CS$<>8__locals1.inv[y].vanity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalDefense.CompareTo(CS$<>8__locals1.inv[x].OriginalDefense);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[x].type.CompareTo(CS$<>8__locals1.inv[y].type);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B89 RID: 19337 RVA: 0x006D7AA8 File Offset: 0x006D5CA8
				internal List<int> <.cctor>b__55_22(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_22 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_22();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => Main.projHook[CS$<>8__locals1.inv[i].shoot]).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[x].type.CompareTo(CS$<>8__locals1.inv[y].type);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B8A RID: 19338 RVA: 0x006D7B40 File Offset: 0x006D5D40
				internal List<int> <.cctor>b__55_23(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_23 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_23();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].mountType != -1 && !MountID.Sets.Cart[CS$<>8__locals1.inv[i].mountType]).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[x].type.CompareTo(CS$<>8__locals1.inv[y].type);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B8B RID: 19339 RVA: 0x006D7BD8 File Offset: 0x006D5DD8
				internal List<int> <.cctor>b__55_24(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_24 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_24();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].mountType != -1 && MountID.Sets.Cart[CS$<>8__locals1.inv[i].mountType]).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[x].type.CompareTo(CS$<>8__locals1.inv[y].type);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B8C RID: 19340 RVA: 0x006D7C70 File Offset: 0x006D5E70
				internal List<int> <.cctor>b__55_25(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_25 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_25();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].buffType > 0 && Main.lightPet[CS$<>8__locals1.inv[i].buffType]).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[x].type.CompareTo(CS$<>8__locals1.inv[y].type);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B8D RID: 19341 RVA: 0x006D7D08 File Offset: 0x006D5F08
				internal List<int> <.cctor>b__55_26(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_26 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_26();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].buffType > 0 && Main.vanityPet[CS$<>8__locals1.inv[i].buffType]).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[x].type.CompareTo(CS$<>8__locals1.inv[y].type);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B8E RID: 19342 RVA: 0x006D7DA0 File Offset: 0x006D5FA0
				internal List<int> <.cctor>b__55_27(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_27 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_27();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].consumable && CS$<>8__locals1.inv[i].healLife > 0 && CS$<>8__locals1.inv[i].healMana < 1 && CS$<>8__locals1.inv[i].type != 5).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].healLife.CompareTo(CS$<>8__locals1.inv[x].healLife);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B8F RID: 19343 RVA: 0x006D7E38 File Offset: 0x006D6038
				internal List<int> <.cctor>b__55_28(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_28 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_28();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type == 5).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, (int x, int y) => CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack));
					return list;
				}

				// Token: 0x06004B90 RID: 19344 RVA: 0x006D7ED0 File Offset: 0x006D60D0
				internal List<int> <.cctor>b__55_29(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_29 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_29();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].consumable && CS$<>8__locals1.inv[i].healLife < 1 && CS$<>8__locals1.inv[i].healMana > 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].healMana.CompareTo(CS$<>8__locals1.inv[x].healMana);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B91 RID: 19345 RVA: 0x006D7F68 File Offset: 0x006D6168
				internal List<int> <.cctor>b__55_30(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_30 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_30();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].consumable && CS$<>8__locals1.inv[i].healLife > 0 && CS$<>8__locals1.inv[i].healMana > 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].healLife.CompareTo(CS$<>8__locals1.inv[x].healLife);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B92 RID: 19346 RVA: 0x006D8000 File Offset: 0x006D6200
				internal List<int> <.cctor>b__55_31(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_31 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_31();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => (CS$<>8__locals1.inv[i].consumable && CS$<>8__locals1.inv[i].buffType > 0 && CS$<>8__locals1.inv[i].type >= 0 && CS$<>8__locals1.inv[i].type < (int)ItemID.Count && !ItemID.Sets.IsFood[CS$<>8__locals1.inv[i].type]) || (CS$<>8__locals1.inv[i].type >= 0 && ItemID.Sets.SortingPriorityPotionsBuffs[CS$<>8__locals1.inv[i].type] > -1)).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemSorting.ItemSortingLayers.CompareWithPrioritySet(ItemID.Sets.SortingPriorityPotionsBuffs, CS$<>8__locals1.inv[x].type, CS$<>8__locals1.inv[y].type);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[x].type.CompareTo(CS$<>8__locals1.inv[y].type);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B93 RID: 19347 RVA: 0x006D8098 File Offset: 0x006D6298
				internal List<int> <.cctor>b__55_32(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_32 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_32();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].consumable && CS$<>8__locals1.inv[i].buffType > 0 && CS$<>8__locals1.inv[i].type >= 0 && CS$<>8__locals1.inv[i].type < (int)ItemID.Count && ItemID.Sets.IsFood[CS$<>8__locals1.inv[i].type]).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ((CS$<>8__locals1.inv[y].buffType < 0 || CS$<>8__locals1.inv[y].buffType >= BuffID.Count) ? 0 : BuffID.Sets.SortingPriorityFoodBuffs[CS$<>8__locals1.inv[y].buffType].CompareTo(BuffID.Sets.SortingPriorityFoodBuffs[CS$<>8__locals1.inv[x].buffType]));
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[x].type.CompareTo(CS$<>8__locals1.inv[y].type);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B94 RID: 19348 RVA: 0x006D8130 File Offset: 0x006D6330
				internal List<int> <.cctor>b__55_33(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_33 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_33();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].dye > 0 || (CS$<>8__locals1.inv[i].type >= 0 && ItemID.Sets.SortingPriorityPotionsDyeMaterial[CS$<>8__locals1.inv[i].type] > -1)).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].dye.CompareTo(CS$<>8__locals1.inv[x].dye);
						}
						if (num2 == 0)
						{
							num2 = ItemSorting.ItemSortingLayers.CompareWithPrioritySet(ItemID.Sets.SortingPriorityPotionsDyeMaterial, CS$<>8__locals1.inv[x].type, CS$<>8__locals1.inv[y].type);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B95 RID: 19349 RVA: 0x006D81C8 File Offset: 0x006D63C8
				internal List<int> <.cctor>b__55_34(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_34 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_34();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].hairDye >= 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].hairDye.CompareTo(CS$<>8__locals1.inv[x].hairDye);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B96 RID: 19350 RVA: 0x006D8260 File Offset: 0x006D6460
				internal List<int> <.cctor>b__55_35(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_35 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_35();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscImportants[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityMiscImportants[CS$<>8__locals1.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityMiscImportants[CS$<>8__locals1.inv[y].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B97 RID: 19351 RVA: 0x006D82F8 File Offset: 0x006D64F8
				internal List<int> <.cctor>b__55_36(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_36 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_36();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => (CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityWiring[CS$<>8__locals1.inv[i].type] > -1) || CS$<>8__locals1.inv[i].mech).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityWiring[CS$<>8__locals1.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityWiring[CS$<>8__locals1.inv[x].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].type.CompareTo(CS$<>8__locals1.inv[x].type);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B98 RID: 19352 RVA: 0x006D8390 File Offset: 0x006D6590
				internal List<int> <.cctor>b__55_37(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_37 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_37();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityMaterials[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityMaterials[CS$<>8__locals1.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityMaterials[CS$<>8__locals1.inv[x].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B99 RID: 19353 RVA: 0x006D8428 File Offset: 0x006D6628
				internal List<int> <.cctor>b__55_38(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_38 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_38();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type == 183).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, (int x, int y) => CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack));
					return list;
				}

				// Token: 0x06004B9A RID: 19354 RVA: 0x006D84C0 File Offset: 0x006D66C0
				internal List<int> <.cctor>b__55_39(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_39 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_39();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityExtractibles[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityExtractibles[CS$<>8__locals1.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityExtractibles[CS$<>8__locals1.inv[x].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B9B RID: 19355 RVA: 0x006D8558 File Offset: 0x006D6758
				internal List<int> <.cctor>b__55_40(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_40 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_40();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => (CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityPainting[CS$<>8__locals1.inv[i].type] > -1) || CS$<>8__locals1.inv[i].paint > 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityPainting[CS$<>8__locals1.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityPainting[CS$<>8__locals1.inv[x].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[x].paint.CompareTo(CS$<>8__locals1.inv[y].paint);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[x].paintCoating.CompareTo(CS$<>8__locals1.inv[y].paintCoating);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B9C RID: 19356 RVA: 0x006D85F0 File Offset: 0x006D67F0
				internal List<int> <.cctor>b__55_41(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_41 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_41();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityRopes[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityRopes[CS$<>8__locals1.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityRopes[CS$<>8__locals1.inv[x].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B9D RID: 19357 RVA: 0x006D8688 File Offset: 0x006D6888
				internal List<int> <.cctor>b__55_42(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_42 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_42();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscHerbsAndSeeds[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityMiscHerbsAndSeeds[CS$<>8__locals1.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityMiscHerbsAndSeeds[CS$<>8__locals1.inv[x].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B9E RID: 19358 RVA: 0x006D8720 File Offset: 0x006D6920
				internal List<int> <.cctor>b__55_43(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_43 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_43();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscGems[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityMiscGems[CS$<>8__locals1.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityMiscGems[CS$<>8__locals1.inv[x].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004B9F RID: 19359 RVA: 0x006D87B8 File Offset: 0x006D69B8
				internal List<int> <.cctor>b__55_44(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_44 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_44();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscAcorns[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityMiscAcorns[CS$<>8__locals1.inv[y].type].CompareTo(ItemID.Sets.SortingPriorityMiscAcorns[CS$<>8__locals1.inv[x].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004BA0 RID: 19360 RVA: 0x006D8850 File Offset: 0x006D6A50
				internal List<int> <.cctor>b__55_45(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_45 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_45();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].type > 0 && ItemID.Sets.SortingPriorityMiscBossBags[CS$<>8__locals1.inv[i].type] > -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = ItemID.Sets.SortingPriorityMiscBossBags[CS$<>8__locals1.inv[x].type].CompareTo(ItemID.Sets.SortingPriorityMiscBossBags[CS$<>8__locals1.inv[y].type]);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004BA1 RID: 19361 RVA: 0x006D88E8 File Offset: 0x006D6AE8
				internal List<int> <.cctor>b__55_46(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_46 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_46();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].makeNPC > 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[x].makeNPC.CompareTo(CS$<>8__locals1.inv[y].makeNPC);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004BA2 RID: 19362 RVA: 0x006D8980 File Offset: 0x006D6B80
				internal List<int> <.cctor>b__55_47(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_47 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_47();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].createTile < 0 && CS$<>8__locals1.inv[i].createWall < 1 && CS$<>8__locals1.inv[i].rare != -1).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].value.CompareTo(CS$<>8__locals1.inv[x].value);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004BA3 RID: 19363 RVA: 0x006D8A18 File Offset: 0x006D6C18
				internal List<int> <.cctor>b__55_48(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_48 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_48();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].createTile >= 0 && Main.tileFrameImportant[CS$<>8__locals1.inv[i].createTile]).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = string.Compare(CS$<>8__locals1.inv[x].Name, CS$<>8__locals1.inv[y].Name, StringComparison.OrdinalIgnoreCase);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004BA4 RID: 19364 RVA: 0x006D8AB0 File Offset: 0x006D6CB0
				internal List<int> <.cctor>b__55_49(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_49 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_49();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].createWall > 0 || CS$<>8__locals1.inv[i].createTile >= 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = string.Compare(CS$<>8__locals1.inv[x].Name, CS$<>8__locals1.inv[y].Name, StringComparison.OrdinalIgnoreCase);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004BA5 RID: 19365 RVA: 0x006D8B48 File Offset: 0x006D6D48
				internal List<int> <.cctor>b__55_50(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_50 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_50();
					CS$<>8__locals1.inv = inv;
					List<int> list = itemsToSort.Where((int i) => CS$<>8__locals1.inv[i].OriginalRarity >= 0).ToList<int>();
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].OriginalRarity.CompareTo(CS$<>8__locals1.inv[x].OriginalRarity);
						if (num2 == 0)
						{
							num2 = string.Compare(CS$<>8__locals1.inv[x].Name, CS$<>8__locals1.inv[y].Name, StringComparison.OrdinalIgnoreCase);
						}
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x06004BA6 RID: 19366 RVA: 0x006D8BE0 File Offset: 0x006D6DE0
				internal List<int> <.cctor>b__55_51(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
				{
					ItemSorting.ItemSortingLayers.<>c__DisplayClass55_51 CS$<>8__locals1 = new ItemSorting.ItemSortingLayers.<>c__DisplayClass55_51();
					CS$<>8__locals1.inv = inv;
					List<int> list = new List<int>(itemsToSort);
					layer.Validate(ref list, CS$<>8__locals1.inv);
					foreach (int num in list)
					{
						itemsToSort.Remove(num);
					}
					ItemSorting.ItemSortingLayers.SortIndicesStable(list, delegate(int x, int y)
					{
						int num2 = CS$<>8__locals1.inv[y].value.CompareTo(CS$<>8__locals1.inv[x].value);
						if (num2 == 0)
						{
							num2 = CS$<>8__locals1.inv[y].stack.CompareTo(CS$<>8__locals1.inv[x].stack);
						}
						return num2;
					});
					return list;
				}

				// Token: 0x04007767 RID: 30567
				public static readonly ItemSorting.ItemSortingLayers.<>c <>9 = new ItemSorting.ItemSortingLayers.<>c();
			}
		}

		// Token: 0x02000705 RID: 1797
		private struct DamageTypeSortingLayerEntry
		{
			// Token: 0x06003FEB RID: 16363 RVA: 0x0069C8C9 File Offset: 0x0069AAC9
			public DamageTypeSortingLayerEntry(float multiplier, ItemSorting.ItemSortingLayer layer, int index)
			{
				this.Multiplier = multiplier;
				this.Layer = layer;
				this.Index = index;
			}

			// Token: 0x04006898 RID: 26776
			public float Multiplier;

			// Token: 0x04006899 RID: 26777
			public ItemSorting.ItemSortingLayer Layer;

			// Token: 0x0400689A RID: 26778
			public int Index;
		}

		// Token: 0x02000706 RID: 1798
		private struct MemoryStamp
		{
			// Token: 0x06003FEC RID: 16364 RVA: 0x0069C8E0 File Offset: 0x0069AAE0
			public MemoryStamp(int itemType, int stack, int prefix)
			{
				this.ItemType = itemType;
				this.Stack = stack;
				this.Prefix = prefix;
			}

			// Token: 0x06003FED RID: 16365 RVA: 0x0069C8F7 File Offset: 0x0069AAF7
			public MemoryStamp(Item item)
			{
				this.ItemType = item.type;
				this.Stack = item.stack;
				this.Prefix = (int)item.prefix;
			}

			// Token: 0x06003FEE RID: 16366 RVA: 0x0069C91D File Offset: 0x0069AB1D
			public override bool Equals(object obj)
			{
				return obj != null && obj is ItemSorting.MemoryStamp && this.Equals((ItemSorting.MemoryStamp)obj);
			}

			// Token: 0x06003FEF RID: 16367 RVA: 0x0069C938 File Offset: 0x0069AB38
			public bool Equals(ItemSorting.MemoryStamp other)
			{
				return this.ItemType == other.ItemType && this.Stack == other.Stack && this.Prefix == other.Prefix;
			}

			// Token: 0x06003FF0 RID: 16368 RVA: 0x0069C966 File Offset: 0x0069AB66
			public override int GetHashCode()
			{
				return (((this.ItemType * 397) ^ this.Stack) * 397) ^ this.Prefix;
			}

			// Token: 0x06003FF1 RID: 16369 RVA: 0x0069C988 File Offset: 0x0069AB88
			public static bool operator ==(ItemSorting.MemoryStamp left, ItemSorting.MemoryStamp right)
			{
				return left.Equals(right);
			}

			// Token: 0x06003FF2 RID: 16370 RVA: 0x0069C992 File Offset: 0x0069AB92
			public static bool operator !=(ItemSorting.MemoryStamp left, ItemSorting.MemoryStamp right)
			{
				return !left.Equals(right);
			}

			// Token: 0x0400689B RID: 26779
			public int ItemType;

			// Token: 0x0400689C RID: 26780
			public int Stack;

			// Token: 0x0400689D RID: 26781
			public int Prefix;
		}
	}
}
