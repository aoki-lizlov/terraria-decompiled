using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent
{
	// Token: 0x02000242 RID: 578
	public class QuickStacking
	{
		// Token: 0x060022AE RID: 8878 RVA: 0x00539A68 File Offset: 0x00537C68
		private static void AddToListyArray<T>(ref T[] arr, ref int count, T elem)
		{
			if (count == arr.Length)
			{
				Array.Resize<T>(ref arr, arr.Length * 2);
			}
			T[] array = arr;
			int num = count;
			count = num + 1;
			array[num] = elem;
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x00539A9A File Offset: 0x00537C9A
		private static int GetCategory(int type)
		{
			return ItemSorting.GetSortingLayerIndex(type);
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x00539AA2 File Offset: 0x00537CA2
		public static void QuickStackToNearbyInventories(Player player, bool smartStack = false)
		{
			QuickStacking.QuickStackToNearbyBanks(player);
			QuickStacking.QuickStackToNearbyChests(player, smartStack);
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x00539AB4 File Offset: 0x00537CB4
		private static void QuickStackToNearbyBanks(Player player)
		{
			List<PositionedChest> banksInRangeOf = NearbyChests.GetBanksInRangeOf(player, 0f);
			foreach (PositionedChest positionedChest in banksInRangeOf)
			{
				long num = ChestUI.MoveCoins(player.inventory, positionedChest.chest);
				Chest.VisualizeChestTransfer_CoinsBatch(player.Center, positionedChest.position, num, Chest.ItemTransferVisualizationSettings.PlayerToChest);
			}
			QuickStacking.SourceInventory sourceInventory = QuickStacking.PackQuickStackableItems(player, false);
			List<int> list;
			QuickStacking.Transfer(sourceInventory, banksInRangeOf, out list, false);
			QuickStacking.RestoreToPlayer(player, sourceInventory);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x00539B50 File Offset: 0x00537D50
		public static void QuickStackToNearbyChests(Player player, bool smartStack = false)
		{
			QuickStacking.QuickStackToNearbyChests(player, QuickStacking.PackQuickStackableItems(player, true), smartStack);
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x00539B60 File Offset: 0x00537D60
		internal static void QuickStackToNearbyChests(Player player, QuickStacking.SourceInventory inventory, bool smartStack)
		{
			if (Main.netMode == 1)
			{
				QuickStacking.SendQuickStackToNearbyChests(player, inventory, smartStack);
				return;
			}
			List<PositionedChest> chestsInRangeOf = NearbyChests.GetChestsInRangeOf(player.position, 0f);
			List<int> list;
			QuickStacking.Transfer(inventory, chestsInRangeOf, out list, smartStack);
			QuickStacking.IndicateBlockedChests(player, list);
			QuickStacking.RestoreToPlayer(player, inventory);
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x00539BA8 File Offset: 0x00537DA8
		internal static void IndicateBlockedChests(Player player, List<int> chests)
		{
			if (!chests.Any<int>())
			{
				return;
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(85, player.whoAmI, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			foreach (int num in chests)
			{
				Chest.IndicateBlockedChest(num);
			}
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x00539C2C File Offset: 0x00537E2C
		private static void SendQuickStackToNearbyChests(Player player, QuickStacking.SourceInventory inventory, bool smartStack)
		{
			QuickStacking.netInv = inventory;
			for (int i = 0; i < inventory.numItems; i++)
			{
				int slotId = inventory.slots[i].SlotId;
				player.LockNetSlot(slotId);
				NetMessage.SendData(5, -1, -1, null, player.whoAmI, (float)slotId, 0f, 0f, 0, 0, 0);
			}
			NetMessage.SendData(85, -1, -1, null, smartStack ? 1 : 0, 0f, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x00539CAC File Offset: 0x00537EAC
		internal static void WriteNetInventorySlots(BinaryWriter writer)
		{
			writer.Write(QuickStacking.netInv.numItems);
			for (int i = 0; i < QuickStacking.netInv.numItems; i++)
			{
				writer.Write((short)QuickStacking.netInv.slots[i].SlotId);
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x00539CFC File Offset: 0x00537EFC
		internal static QuickStacking.SourceInventory ReadNetInventory(Player player, BinaryReader reader)
		{
			QuickStacking.SourceInventory scratchInventory = QuickStacking.GetScratchInventory(player);
			Array.Clear(scratchInventory.transferBlocked, 0, scratchInventory.transferBlocked.Length);
			scratchInventory.numItems = reader.ReadInt32();
			for (int i = 0; i < scratchInventory.numItems; i++)
			{
				PlayerItemSlotID.SlotReference slotReference = new PlayerItemSlotID.SlotReference(player, (int)reader.ReadInt16());
				scratchInventory.slots[i] = slotReference;
				Item item = slotReference.Item;
				scratchInventory.items[i] = item;
			}
			return scratchInventory;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x00539D70 File Offset: 0x00537F70
		internal static void WriteBlockedChestList(BinaryWriter writer)
		{
			writer.Write(QuickStacking._blockedChests.Count);
			for (int i = 0; i < QuickStacking._blockedChests.Count; i++)
			{
				writer.Write((ushort)QuickStacking._blockedChests[i]);
			}
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x00539DB4 File Offset: 0x00537FB4
		internal static List<int> ReadBlockedChestList(BinaryReader reader)
		{
			QuickStacking._blockedChests.Clear();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				QuickStacking._blockedChests.Add((int)reader.ReadUInt16());
			}
			return QuickStacking._blockedChests;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x00539DF4 File Offset: 0x00537FF4
		private static void RestoreToPlayer(Player player, QuickStacking.SourceInventory inventory)
		{
			for (int i = 0; i < inventory.numItems; i++)
			{
				Item item = inventory.items[i];
				PlayerItemSlotID.SlotReference slotReference = inventory.slots[i];
				bool flag = inventory.transferBlocked[i];
				if (!false)
				{
					slotReference.Item = item;
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(5, -1, -1, null, player.whoAmI, (float)slotReference.SlotId, (float)(flag ? 1 : 0), 0f, 0, 0, 0);
				}
				else if (flag)
				{
					ItemSlot.IndicateBlockedSlot(slotReference);
				}
			}
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00539E78 File Offset: 0x00538078
		private static QuickStacking.SourceInventory GetScratchInventory(Player player)
		{
			return new QuickStacking.SourceInventory
			{
				items = QuickStacking.inventoryItemsScratch,
				numItems = 0,
				slots = QuickStacking.slotsScratch,
				transferBlocked = QuickStacking.blockedSlotsScratch,
				position = player.Center
			};
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x00539EC8 File Offset: 0x005380C8
		private static QuickStacking.SourceInventory PackQuickStackableItems(Player player, bool includeVoidBag)
		{
			QuickStacking.SourceInventory scratchInventory = QuickStacking.GetScratchInventory(player);
			Array.Clear(scratchInventory.transferBlocked, 0, scratchInventory.transferBlocked.Length);
			QuickStacking.AddQuickStackableItems(player, ref scratchInventory, PlayerItemSlotID.Inventory0 + 10, 40);
			if (player.useVoidBag() && includeVoidBag)
			{
				QuickStacking.AddQuickStackableItems(player, ref scratchInventory, PlayerItemSlotID.Bank4_0, player.bank4.maxItems);
			}
			return scratchInventory;
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x00539F28 File Offset: 0x00538128
		private static void AddQuickStackableItems(Player player, ref QuickStacking.SourceInventory inventory, int startSlot, int count)
		{
			for (int i = 0; i < count; i++)
			{
				PlayerItemSlotID.SlotReference slotReference = new PlayerItemSlotID.SlotReference(player, startSlot + i);
				Item item = slotReference.Item;
				if (!item.IsAir && !item.favorited && !item.IsACoin)
				{
					int numItems = inventory.numItems;
					inventory.numItems = numItems + 1;
					int num = numItems;
					inventory.slots[num] = slotReference;
					inventory.items[num] = item;
				}
			}
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x00539F94 File Offset: 0x00538194
		private static void Transfer(QuickStacking.SourceInventory source, List<PositionedChest> destinations, out List<int> blockedChests, bool smartStack = false)
		{
			QuickStacking.nextDestHelper = 0;
			List<QuickStacking.DestinationHelper> list = QuickStacking.destHelperListScratch;
			list.Clear();
			QuickStacking.MatchingItemTypeDestinationList matchingItemTypeDestinationList = QuickStacking.matchingItemTypeScratch;
			matchingItemTypeDestinationList.Reset();
			foreach (PositionedChest positionedChest in destinations)
			{
				if (!positionedChest.chest.IsEmpty())
				{
					QuickStacking.DestinationHelper destHelperFromPool = QuickStacking.GetDestHelperFromPool();
					destHelperFromPool.Reset(positionedChest);
					list.Add(destHelperFromPool);
					QuickStacking.BuildDestinationMetricsAndStackItems(source, destHelperFromPool, matchingItemTypeDestinationList);
				}
			}
			for (int i = 0; i < source.numItems; i++)
			{
				Item item = source.items[i];
				QuickStacking.DestinationHelper destinationHelper;
				if (!item.IsAir && matchingItemTypeDestinationList.Lookup(item.type, out destinationHelper))
				{
					QuickStacking.Consolidate(source, i);
					QuickStacking.InsertIntoFreeSlot(ref source.items[i], destinationHelper, source.position);
				}
			}
			if (smartStack)
			{
				for (int j = 0; j < source.numItems; j++)
				{
					Item item2 = source.items[j];
					QuickStacking.DestinationHelper destinationHelper2;
					if (!item2.IsAir && !source.transferBlocked[j] && QuickStacking.TryGetBestDestinationForCategory(QuickStacking.GetCategory(item2.type), list, out destinationHelper2))
					{
						if (destinationHelper2.locked)
						{
							source.transferBlocked[j] = true;
							destinationHelper2.transferBlocked = true;
						}
						else
						{
							QuickStacking.Consolidate(source, j);
							QuickStacking.InsertIntoFreeSlot(ref source.items[j], destinationHelper2, source.position);
						}
					}
				}
			}
			blockedChests = QuickStacking._blockedChests;
			blockedChests.Clear();
			foreach (QuickStacking.DestinationHelper destinationHelper3 in list)
			{
				if (destinationHelper3.transferBlocked)
				{
					blockedChests.Add(destinationHelper3.ChestIndex);
				}
			}
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0053A174 File Offset: 0x00538374
		private static QuickStacking.DestinationHelper GetDestHelperFromPool()
		{
			if (QuickStacking.nextDestHelper == QuickStacking.destHelperPool.Length)
			{
				Array.Resize<QuickStacking.DestinationHelper>(ref QuickStacking.destHelperPool, QuickStacking.destHelperPool.Length * 2);
			}
			if (QuickStacking.destHelperPool[QuickStacking.nextDestHelper] == null)
			{
				QuickStacking.destHelperPool[QuickStacking.nextDestHelper] = new QuickStacking.DestinationHelper();
			}
			return QuickStacking.destHelperPool[QuickStacking.nextDestHelper++];
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0053A1D4 File Offset: 0x005383D4
		private static void BuildDestinationMetricsAndStackItems(QuickStacking.SourceInventory source, QuickStacking.DestinationHelper dest, QuickStacking.MatchingItemTypeDestinationList destinationsForItemTypes)
		{
			for (int i = 0; i < dest.itemCount; i++)
			{
				Item item = dest.items[i];
				if (item.IsAir)
				{
					dest.AddFreeSlot(i);
				}
				else
				{
					dest.AddCategoryScore(QuickStacking.GetCategory(item.type));
					for (int j = 0; j < source.numItems; j++)
					{
						Item item2 = source.items[j];
						if (item2.type == item.type)
						{
							if (dest.locked)
							{
								source.transferBlocked[j] = true;
								dest.transferBlocked = true;
							}
							else
							{
								if (Item.CanStack(item2, item) && item.stack < item.maxStack)
								{
									QuickStacking.FillStack(item2, dest, i, source.position);
									if (item2.IsAir)
									{
										goto IL_00A4;
									}
								}
								destinationsForItemTypes.Add(item.type, dest);
							}
						}
						IL_00A4:;
					}
				}
			}
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0053A2A4 File Offset: 0x005384A4
		private static bool TryGetBestDestinationForCategory(int category, List<QuickStacking.DestinationHelper> destinations, out QuickStacking.DestinationHelper dest)
		{
			dest = null;
			int num = int.MinValue;
			foreach (QuickStacking.DestinationHelper destinationHelper in destinations)
			{
				int num2;
				if (destinationHelper.HasFreeSlots && destinationHelper.TryGetCategoryScore(category, out num2) && num2 > num)
				{
					dest = destinationHelper;
					num = num2;
				}
			}
			return dest != null;
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0053A318 File Offset: 0x00538518
		private static void FillStack(Item item, QuickStacking.DestinationHelper dest, int slotIndex, Vector2 srcPosition)
		{
			Item item2 = dest.items[slotIndex];
			int num = Math.Min(item2.maxStack - item2.stack, item.stack);
			Chest.VisualizeChestTransfer(srcPosition, dest.position, item.type, Chest.ItemTransferVisualizationSettings.PlayerToChest);
			item2.stack += num;
			item.stack -= num;
			if (item.stack == 0)
			{
				item.TurnToAir(false);
			}
			dest.SyncSlot(slotIndex);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0053A390 File Offset: 0x00538590
		private static void Consolidate(QuickStacking.SourceInventory source, int i)
		{
			Item item = source.items[i++];
			while (i < source.numItems)
			{
				Item item2 = source.items[i++];
				if (Item.CanStack(item, item2))
				{
					int num = Math.Min(item.maxStack - item.stack, item2.stack);
					item.stack += num;
					item2.stack -= num;
					if (item2.stack == 0)
					{
						item2.TurnToAir(false);
					}
				}
			}
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0053A414 File Offset: 0x00538614
		private static void InsertIntoFreeSlot(ref Item item, QuickStacking.DestinationHelper dest, Vector2 srcPosition)
		{
			Chest.VisualizeChestTransfer(srcPosition, dest.position, item.type, Chest.ItemTransferVisualizationSettings.PlayerToChest);
			int num = dest.ConsumeFreeSlot();
			Utils.Swap<Item>(ref item, ref dest.items[num]);
			dest.SyncSlot(num);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0000357B File Offset: 0x0000177B
		public QuickStacking()
		{
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0053A45C File Offset: 0x0053865C
		// Note: this type is marked as 'beforefieldinit'.
		static QuickStacking()
		{
		}

		// Token: 0x04004D10 RID: 19728
		private static QuickStacking.SourceInventory netInv;

		// Token: 0x04004D11 RID: 19729
		private static Item[] inventoryItemsScratch = new Item[400];

		// Token: 0x04004D12 RID: 19730
		private static PlayerItemSlotID.SlotReference[] slotsScratch = new PlayerItemSlotID.SlotReference[400];

		// Token: 0x04004D13 RID: 19731
		private static bool[] blockedSlotsScratch = new bool[400];

		// Token: 0x04004D14 RID: 19732
		private static List<QuickStacking.DestinationHelper> destHelperListScratch = new List<QuickStacking.DestinationHelper>();

		// Token: 0x04004D15 RID: 19733
		private static QuickStacking.MatchingItemTypeDestinationList matchingItemTypeScratch = new QuickStacking.MatchingItemTypeDestinationList();

		// Token: 0x04004D16 RID: 19734
		private static List<int> _blockedChests = new List<int>();

		// Token: 0x04004D17 RID: 19735
		private static QuickStacking.DestinationHelper[] destHelperPool = new QuickStacking.DestinationHelper[100];

		// Token: 0x04004D18 RID: 19736
		private static int nextDestHelper = 0;

		// Token: 0x020007CB RID: 1995
		private class DestinationHelper
		{
			// Token: 0x17000532 RID: 1330
			// (get) Token: 0x0600421D RID: 16925 RVA: 0x006BDD4A File Offset: 0x006BBF4A
			public Vector2 position
			{
				get
				{
					return this._chest.position;
				}
			}

			// Token: 0x0600421E RID: 16926 RVA: 0x006BDD58 File Offset: 0x006BBF58
			public void Reset(PositionedChest inventory)
			{
				this._chest = inventory;
				this.items = inventory.chest.item;
				this.itemCount = inventory.chest.maxItems;
				this.locked = inventory.chest.IsLockedOrInUse();
				this.transferBlocked = false;
				if (this.freeSlots.Length < this.itemCount)
				{
					Array.Resize<int>(ref this.freeSlots, this.itemCount);
				}
				Array.Clear(this.freeSlots, 0, this.freeSlots.Length);
				this.freeSlotStart = 0;
				this.freeSlotCount = 0;
				Array.Clear(this.categoryScores, 0, this.categoryScores.Length);
			}

			// Token: 0x17000533 RID: 1331
			// (get) Token: 0x0600421F RID: 16927 RVA: 0x006BDDFD File Offset: 0x006BBFFD
			public int ChestIndex
			{
				get
				{
					return this._chest.chest.index;
				}
			}

			// Token: 0x17000534 RID: 1332
			// (get) Token: 0x06004220 RID: 16928 RVA: 0x006BDE0F File Offset: 0x006BC00F
			public bool IsEmpty
			{
				get
				{
					return this.freeSlotCount == this.itemCount;
				}
			}

			// Token: 0x17000535 RID: 1333
			// (get) Token: 0x06004221 RID: 16929 RVA: 0x006BDE1F File Offset: 0x006BC01F
			public bool HasFreeSlots
			{
				get
				{
					return this.freeSlotStart < this.freeSlotCount;
				}
			}

			// Token: 0x06004222 RID: 16930 RVA: 0x006BDE2F File Offset: 0x006BC02F
			public void AddCategoryScore(int category)
			{
				this.categoryScores[category]++;
			}

			// Token: 0x06004223 RID: 16931 RVA: 0x006BDE42 File Offset: 0x006BC042
			public void AddFreeSlot(int i)
			{
				QuickStacking.AddToListyArray<int>(ref this.freeSlots, ref this.freeSlotCount, i);
			}

			// Token: 0x06004224 RID: 16932 RVA: 0x006BDE56 File Offset: 0x006BC056
			public bool TryGetCategoryScore(int category, out int score)
			{
				score = this.categoryScores[category];
				return score != 0;
			}

			// Token: 0x06004225 RID: 16933 RVA: 0x006BDE6C File Offset: 0x006BC06C
			public int ConsumeFreeSlot()
			{
				int[] array = this.freeSlots;
				int num = this.freeSlotStart;
				this.freeSlotStart = num + 1;
				return array[num];
			}

			// Token: 0x06004226 RID: 16934 RVA: 0x006BDE94 File Offset: 0x006BC094
			public void SyncSlot(int slot)
			{
				if (this._chest.chest.index >= 0)
				{
					NetMessage.SendData(32, -1, -1, null, this._chest.chest.index, (float)slot, 0f, 0f, 0, 0, 0);
				}
			}

			// Token: 0x06004227 RID: 16935 RVA: 0x006BDEDD File Offset: 0x006BC0DD
			public DestinationHelper()
			{
			}

			// Token: 0x040070F1 RID: 28913
			private PositionedChest _chest;

			// Token: 0x040070F2 RID: 28914
			public Item[] items;

			// Token: 0x040070F3 RID: 28915
			public int itemCount;

			// Token: 0x040070F4 RID: 28916
			public bool locked;

			// Token: 0x040070F5 RID: 28917
			public bool transferBlocked;

			// Token: 0x040070F6 RID: 28918
			private int[] freeSlots = new int[200];

			// Token: 0x040070F7 RID: 28919
			private int freeSlotStart;

			// Token: 0x040070F8 RID: 28920
			private int freeSlotCount;

			// Token: 0x040070F9 RID: 28921
			private int[] categoryScores = new int[ItemSorting.LayerCount];
		}

		// Token: 0x020007CC RID: 1996
		private class MatchingItemTypeDestinationList
		{
			// Token: 0x06004228 RID: 16936 RVA: 0x006BDF05 File Offset: 0x006BC105
			public MatchingItemTypeDestinationList()
			{
				this.Reset();
			}

			// Token: 0x06004229 RID: 16937 RVA: 0x006BDF33 File Offset: 0x006BC133
			public void Reset()
			{
				Array.Clear(this.firstEntryForType, 0, this.firstEntryForType.Length);
				Array.Clear(this.entries, 0, this.entries.Length);
				this.numEntries = 1;
			}

			// Token: 0x0600422A RID: 16938 RVA: 0x006BDF64 File Offset: 0x006BC164
			private int Tail(int type)
			{
				int num = this.firstEntryForType[type];
				if (num == 0)
				{
					return 0;
				}
				while (this.entries[num].next != 0)
				{
					num = this.entries[num].next;
				}
				return num;
			}

			// Token: 0x0600422B RID: 16939 RVA: 0x006BDFA8 File Offset: 0x006BC1A8
			internal void Add(int type, QuickStacking.DestinationHelper value)
			{
				int num = this.Tail(type);
				if (num == 0)
				{
					this.firstEntryForType[type] = this.AddEntry(value);
					return;
				}
				if (this.entries[num].value == value)
				{
					return;
				}
				this.entries[num].next = this.AddEntry(value);
			}

			// Token: 0x0600422C RID: 16940 RVA: 0x006BE000 File Offset: 0x006BC200
			private int AddEntry(QuickStacking.DestinationHelper value)
			{
				if (this.numEntries == this.entries.Length)
				{
					Array.Resize<QuickStacking.MatchingItemTypeDestinationList.LinkedEntry>(ref this.entries, this.entries.Length * 2);
				}
				int num = this.numEntries;
				QuickStacking.AddToListyArray<QuickStacking.MatchingItemTypeDestinationList.LinkedEntry>(ref this.entries, ref this.numEntries, new QuickStacking.MatchingItemTypeDestinationList.LinkedEntry
				{
					value = value
				});
				return num;
			}

			// Token: 0x0600422D RID: 16941 RVA: 0x006BE05C File Offset: 0x006BC25C
			public bool Lookup(int type, out QuickStacking.DestinationHelper value)
			{
				value = null;
				int i = this.firstEntryForType[type];
				while (i > 0)
				{
					QuickStacking.MatchingItemTypeDestinationList.LinkedEntry linkedEntry = this.entries[i];
					value = linkedEntry.value;
					if (value.HasFreeSlots)
					{
						return true;
					}
					i = linkedEntry.next;
					this.firstEntryForType[type] = i;
				}
				return false;
			}

			// Token: 0x040070FA RID: 28922
			private QuickStacking.MatchingItemTypeDestinationList.LinkedEntry[] entries = new QuickStacking.MatchingItemTypeDestinationList.LinkedEntry[1000];

			// Token: 0x040070FB RID: 28923
			private int numEntries;

			// Token: 0x040070FC RID: 28924
			private int[] firstEntryForType = new int[(int)ItemID.Count];

			// Token: 0x02000ACC RID: 2764
			private struct LinkedEntry
			{
				// Token: 0x0400787C RID: 30844
				public QuickStacking.DestinationHelper value;

				// Token: 0x0400787D RID: 30845
				public int next;
			}
		}

		// Token: 0x020007CD RID: 1997
		internal struct SourceInventory
		{
			// Token: 0x040070FD RID: 28925
			public Item[] items;

			// Token: 0x040070FE RID: 28926
			public int numItems;

			// Token: 0x040070FF RID: 28927
			public PlayerItemSlotID.SlotReference[] slots;

			// Token: 0x04007100 RID: 28928
			public bool[] transferBlocked;

			// Token: 0x04007101 RID: 28929
			public Vector2 position;
		}
	}
}
