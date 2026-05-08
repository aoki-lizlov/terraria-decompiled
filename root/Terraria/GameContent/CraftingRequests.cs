using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.DataStructures;
using Terraria.Net;

namespace Terraria.GameContent
{
	// Token: 0x02000230 RID: 560
	public static class CraftingRequests
	{
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x00533A98 File Offset: 0x00531C98
		public static bool HasPendingRequests
		{
			get
			{
				return CraftingRequests._pendingCrafts.Count > 0;
			}
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00533AA7 File Offset: 0x00531CA7
		public static void Clear()
		{
			CraftingRequests._pendingCrafts.Clear();
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x00533AB4 File Offset: 0x00531CB4
		public static void CraftItem(Recipe recipe, int qty = 1, bool quickCraft = false)
		{
			Player localPlayer = Main.LocalPlayer;
			List<Chest> chests = Recipe._recipeChests;
			List<Recipe.RequiredItemEntry> list = new List<Recipe.RequiredItemEntry>();
			int num = 0;
			Func<Recipe.RequiredItemEntry, bool> <>9__0;
			while (num < qty && (num <= 0 || (Recipe.CollectedEnoughItemsToCraft(recipe) && Main.CursorHasSpaceToCraftRecipe(recipe))))
			{
				list.Clear();
				recipe.GetIngredientsForOneCraft(localPlayer, list);
				if (Main.netMode == 0)
				{
					goto IL_007C;
				}
				IEnumerable<Recipe.RequiredItemEntry> enumerable = list;
				Func<Recipe.RequiredItemEntry, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (Recipe.RequiredItemEntry req) => CraftingRequests.CanCraftLocally(req, chests));
				}
				if (enumerable.All(func))
				{
					goto IL_007C;
				}
				CraftingRequests.CraftViaRequest(recipe, quickCraft, chests, list);
				IL_009A:
				foreach (Recipe.RequiredItemEntry requiredItemEntry in list)
				{
					Recipe.SubtractOwnedItem(requiredItemEntry);
				}
				num++;
				continue;
				IL_007C:
				CraftingRequests.CraftLocally(recipe, quickCraft, chests, list);
				goto IL_009A;
			}
			CraftingEffects.OnCraft(recipe, quickCraft);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x00533BAC File Offset: 0x00531DAC
		private static Item CreateResult(Recipe recipe)
		{
			Item item = recipe.createItem.Clone();
			item.OnCreated(new RecipeItemCreationContext(recipe));
			if (item.stack <= 1)
			{
				item.Prefix(-1);
			}
			return item;
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x00533BE4 File Offset: 0x00531DE4
		private static void CraftLocally(Recipe recipe, bool quickCraft, List<Chest> chests, List<Recipe.RequiredItemEntry> ingredients)
		{
			foreach (Recipe.RequiredItemEntry requiredItemEntry in ingredients)
			{
				CraftingRequests.Consume(requiredItemEntry, chests, null, true);
			}
			Main.CraftItem_GrantItem(recipe, CraftingRequests.CreateResult(recipe), quickCraft);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x00533C40 File Offset: 0x00531E40
		private static void CraftViaRequest(Recipe recipe, bool quickCraft, List<Chest> chests, List<Recipe.RequiredItemEntry> ingredients)
		{
			List<Item> list = new List<Item>();
			List<Recipe.RequiredItemEntry> list2 = new List<Recipe.RequiredItemEntry>();
			foreach (Recipe.RequiredItemEntry requiredItemEntry in ingredients)
			{
				int num = CraftingRequests.Consume(requiredItemEntry, chests, list, false);
				if (num > 0)
				{
					list2.Add(new Recipe.RequiredItemEntry
					{
						itemIdOrRecipeGroup = requiredItemEntry.itemIdOrRecipeGroup,
						stack = num
					});
				}
			}
			Item item = CraftingRequests.CreateResult(recipe);
			if (!quickCraft)
			{
				FakeCursorItem.Add(item);
			}
			CraftingRequests._pendingCrafts.Enqueue(new CraftingRequests.RemoteCraftRequest
			{
				recipe = recipe,
				result = item,
				consumed = list,
				requested = list2,
				quickCraft = quickCraft
			});
			NetManager.Instance.SendToServer(CraftingRequests.NetCraftingRequestsModule.WriteRequest(list2, chests));
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x00533D2C File Offset: 0x00531F2C
		private static bool IsLocallyAccessible(Chest chest)
		{
			return chest.bankChest || chest.index == Main.LocalPlayer.chest;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x00533D4C File Offset: 0x00531F4C
		private static bool CanCraftLocally(Recipe.RequiredItemEntry req, List<Chest> chests)
		{
			int num = 0;
			num += CraftingRequests.CountMatches(req, Main.LocalPlayer.inventory, 58);
			foreach (Chest chest in chests)
			{
				if (CraftingRequests.IsLocallyAccessible(chest))
				{
					num += CraftingRequests.CountMatches(req, chest.item, chest.maxItems);
				}
			}
			return num >= req.stack;
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00533DD4 File Offset: 0x00531FD4
		private static int CountMatches(Recipe.RequiredItemEntry req, List<Chest> chests)
		{
			int num = 0;
			foreach (Chest chest in chests)
			{
				num += CraftingRequests.CountMatches(req, chest.item, chest.maxItems);
			}
			return num;
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x00533E34 File Offset: 0x00532034
		private static int CountMatches(Recipe.RequiredItemEntry req, Item[] inv, int maxItems)
		{
			int num = 0;
			for (int i = 0; i < maxItems; i++)
			{
				Item item = inv[i];
				if (req.Matches(item.type))
				{
					num += item.stack;
				}
			}
			return num;
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x00533E6C File Offset: 0x0053206C
		private static int Consume(Recipe.RequiredItemEntry req, List<Chest> chests, List<Item> consumedItems, bool fromChests)
		{
			int stack = req.stack;
			if (Main.netMode != 2)
			{
				CraftingRequests.ConsumeItemsFrom(Main.LocalPlayer.inventory, 58, req, ref stack, consumedItems, -1);
			}
			foreach (Chest chest in chests)
			{
				if (chest.bankChest || fromChests)
				{
					CraftingRequests.ConsumeItemsFrom(chest, req, ref stack, consumedItems);
				}
			}
			return stack;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x00533EF0 File Offset: 0x005320F0
		private static void ConsumeItemsFrom(Chest chest, Recipe.RequiredItemEntry req, ref int toConsume, List<Item> consumedItems = null)
		{
			CraftingRequests.ConsumeItemsFrom(chest.item, chest.maxItems, req, ref toConsume, consumedItems, chest.bankChest ? (-1) : chest.index);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00533F18 File Offset: 0x00532118
		private static void ConsumeItemsFrom(Item[] inventory, int maxItems, Recipe.RequiredItemEntry req, ref int toConsume, List<Item> consumedItems = null, int chestIndex = -1)
		{
			if (toConsume <= 0)
			{
				return;
			}
			int netMode = Main.netMode;
			int netMode2 = Main.netMode;
			for (int i = 0; i < maxItems; i++)
			{
				Item item = inventory[i];
				if (req.Matches(item.type))
				{
					if (item.stack > toConsume)
					{
						if (consumedItems != null)
						{
							Item item2 = item.Clone();
							item2.stack = toConsume;
							consumedItems.Add(item2);
						}
						item.stack -= toConsume;
						toConsume = 0;
					}
					else
					{
						toConsume -= item.stack;
						if (consumedItems != null)
						{
							consumedItems.Add(item);
						}
						inventory[i] = new Item();
					}
					if (chestIndex >= 0)
					{
						NetMessage.SendData(32, -1, -1, null, chestIndex, (float)i, 0f, 0f, 0, 0, 0);
					}
					if (toConsume <= 0)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00533FE0 File Offset: 0x005321E0
		public static bool CanCraftFromChest(Chest chest, int whoAmI)
		{
			if (Chest.IsLocked(chest.x, chest.y))
			{
				return false;
			}
			int num = Chest.UsingChest(chest.index);
			return num < 0 || num == whoAmI;
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0053401C File Offset: 0x0053221C
		private static void HandleRequest(int whoAmI, List<Recipe.RequiredItemEntry> items, List<Chest> chests)
		{
			chests.RemoveAll((Chest chest) => chest == null || !CraftingRequests.CanCraftFromChest(chest, whoAmI));
			if (!items.All((Recipe.RequiredItemEntry req) => CraftingRequests.CountMatches(req, chests) >= req.stack))
			{
				NetManager.Instance.SendToClient(CraftingRequests.NetCraftingRequestsModule.WriteResponse(false), whoAmI);
				return;
			}
			foreach (Recipe.RequiredItemEntry requiredItemEntry in items)
			{
				CraftingRequests.Consume(requiredItemEntry, chests, null, true);
			}
			NetManager.Instance.SendToClient(CraftingRequests.NetCraftingRequestsModule.WriteResponse(true), whoAmI);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x005340E0 File Offset: 0x005322E0
		private static void HandleResponse(bool approved)
		{
			CraftingRequests.RemoteCraftRequest remoteCraftRequest = CraftingRequests._pendingCrafts.Dequeue();
			FakeCursorItem.Remove(remoteCraftRequest.result.type, remoteCraftRequest.result.stack);
			if (approved)
			{
				Main.CraftItem_GrantItem(remoteCraftRequest.recipe, remoteCraftRequest.result, remoteCraftRequest.quickCraft);
				return;
			}
			foreach (Item item in remoteCraftRequest.consumed)
			{
				CraftingRequests.Refund(item);
			}
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00534174 File Offset: 0x00532374
		public static void Refund(Item item)
		{
			Main.LocalPlayer.GetOrDropItem(item, GetItemSettings.RefundConsumedItem);
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x00534188 File Offset: 0x00532388
		public static void SubtractPendingRequests()
		{
			foreach (CraftingRequests.RemoteCraftRequest remoteCraftRequest in CraftingRequests._pendingCrafts)
			{
				foreach (Recipe.RequiredItemEntry requiredItemEntry in remoteCraftRequest.requested)
				{
					Recipe.SubtractOwnedItem(requiredItemEntry);
				}
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x00534210 File Offset: 0x00532410
		public static void SavePossibleRefunds(BinaryWriter writer)
		{
			int num = CraftingRequests._pendingCrafts.Sum((CraftingRequests.RemoteCraftRequest c) => c.consumed.Count);
			writer.Write(num);
			foreach (CraftingRequests.RemoteCraftRequest remoteCraftRequest in CraftingRequests._pendingCrafts)
			{
				foreach (Item item in remoteCraftRequest.consumed)
				{
					item.Serialize(writer, ItemSerializationContext.SavingAndLoading);
				}
			}
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x005342CC File Offset: 0x005324CC
		// Note: this type is marked as 'beforefieldinit'.
		static CraftingRequests()
		{
		}

		// Token: 0x04004CC4 RID: 19652
		private static Queue<CraftingRequests.RemoteCraftRequest> _pendingCrafts = new Queue<CraftingRequests.RemoteCraftRequest>();

		// Token: 0x020007B2 RID: 1970
		public struct RemoteCraftRequest
		{
			// Token: 0x040070B4 RID: 28852
			public Recipe recipe;

			// Token: 0x040070B5 RID: 28853
			public Item result;

			// Token: 0x040070B6 RID: 28854
			public List<Item> consumed;

			// Token: 0x040070B7 RID: 28855
			public List<Recipe.RequiredItemEntry> requested;

			// Token: 0x040070B8 RID: 28856
			public bool quickCraft;
		}

		// Token: 0x020007B3 RID: 1971
		public class NetCraftingRequestsModule : NetModule
		{
			// Token: 0x060041CA RID: 16842 RVA: 0x006BCAB4 File Offset: 0x006BACB4
			public static NetPacket WriteRequest(List<Recipe.RequiredItemEntry> items, List<Chest> chests)
			{
				NetPacket netPacket = NetModule.CreatePacket<CraftingRequests.NetCraftingRequestsModule>(65530);
				netPacket.Writer.Write7BitEncodedInt(items.Count);
				foreach (Recipe.RequiredItemEntry requiredItemEntry in items)
				{
					netPacket.Writer.Write(requiredItemEntry.itemIdOrRecipeGroup);
					netPacket.Writer.Write7BitEncodedInt(requiredItemEntry.stack);
				}
				netPacket.Writer.Write7BitEncodedInt(chests.Count);
				foreach (Chest chest in chests)
				{
					netPacket.Writer.Write7BitEncodedInt(chest.index);
				}
				return netPacket;
			}

			// Token: 0x060041CB RID: 16843 RVA: 0x006BCB9C File Offset: 0x006BAD9C
			public static NetPacket WriteResponse(bool approved)
			{
				NetPacket netPacket = NetModule.CreatePacket<CraftingRequests.NetCraftingRequestsModule>(65530);
				netPacket.Writer.Write(approved);
				return netPacket;
			}

			// Token: 0x060041CC RID: 16844 RVA: 0x006BCBC4 File Offset: 0x006BADC4
			public void DeserializeRequest(BinaryReader reader, int userId)
			{
				int num = reader.Read7BitEncodedInt();
				List<Recipe.RequiredItemEntry> list = new List<Recipe.RequiredItemEntry>(num);
				for (int i = 0; i < num; i++)
				{
					list.Add(new Recipe.RequiredItemEntry(reader.ReadInt32(), reader.Read7BitEncodedInt()));
				}
				int num2 = reader.Read7BitEncodedInt();
				List<Chest> list2 = new List<Chest>(num2);
				for (int j = 0; j < num2; j++)
				{
					int num3 = reader.Read7BitEncodedInt();
					list2.Add((num3 < 0) ? null : Main.chest[num3]);
				}
				CraftingRequests.HandleRequest(userId, list, list2);
			}

			// Token: 0x060041CD RID: 16845 RVA: 0x006BCC4A File Offset: 0x006BAE4A
			public void DeserializeResponse(BinaryReader reader)
			{
				CraftingRequests.HandleResponse(reader.ReadBoolean());
			}

			// Token: 0x060041CE RID: 16846 RVA: 0x006BCC57 File Offset: 0x006BAE57
			public override bool Deserialize(BinaryReader reader, int userId)
			{
				if (Main.netMode == 2)
				{
					this.DeserializeRequest(reader, userId);
				}
				else
				{
					this.DeserializeResponse(reader);
				}
				return true;
			}

			// Token: 0x060041CF RID: 16847 RVA: 0x0055DC93 File Offset: 0x0055BE93
			public NetCraftingRequestsModule()
			{
			}
		}

		// Token: 0x020007B4 RID: 1972
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x060041D0 RID: 16848 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x060041D1 RID: 16849 RVA: 0x006BCC73 File Offset: 0x006BAE73
			internal bool <CraftItem>b__0(Recipe.RequiredItemEntry req)
			{
				return CraftingRequests.CanCraftLocally(req, this.chests);
			}

			// Token: 0x040070B9 RID: 28857
			public List<Chest> chests;

			// Token: 0x040070BA RID: 28858
			public Func<Recipe.RequiredItemEntry, bool> <>9__0;
		}

		// Token: 0x020007B5 RID: 1973
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0
		{
			// Token: 0x060041D2 RID: 16850 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x060041D3 RID: 16851 RVA: 0x006BCC81 File Offset: 0x006BAE81
			internal bool <HandleRequest>b__0(Chest chest)
			{
				return chest == null || !CraftingRequests.CanCraftFromChest(chest, this.whoAmI);
			}

			// Token: 0x060041D4 RID: 16852 RVA: 0x006BCC97 File Offset: 0x006BAE97
			internal bool <HandleRequest>b__1(Recipe.RequiredItemEntry req)
			{
				return CraftingRequests.CountMatches(req, this.chests) >= req.stack;
			}

			// Token: 0x040070BB RID: 28859
			public int whoAmI;

			// Token: 0x040070BC RID: 28860
			public List<Chest> chests;
		}

		// Token: 0x020007B6 RID: 1974
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060041D5 RID: 16853 RVA: 0x006BCCB0 File Offset: 0x006BAEB0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060041D6 RID: 16854 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060041D7 RID: 16855 RVA: 0x006BCCBC File Offset: 0x006BAEBC
			internal int <SavePossibleRefunds>b__21_0(CraftingRequests.RemoteCraftRequest c)
			{
				return c.consumed.Count;
			}

			// Token: 0x040070BD RID: 28861
			public static readonly CraftingRequests.<>c <>9 = new CraftingRequests.<>c();

			// Token: 0x040070BE RID: 28862
			public static Func<CraftingRequests.RemoteCraftRequest, int> <>9__21_0;
		}
	}
}
