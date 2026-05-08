using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x02000231 RID: 561
	public static class EmergencyStacking
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x005342D8 File Offset: 0x005324D8
		private static EmergencyStacking.Group[] GroupLookup
		{
			get
			{
				if (EmergencyStacking._groupLookup != null)
				{
					return EmergencyStacking._groupLookup;
				}
				int count = EmergencyStacking.PreservationOrder.Count;
				foreach (EmergencyStacking.Group group in EmergencyStacking.PreservationOrder)
				{
					group.StackingPriority = count--;
				}
				EmergencyStacking._groupLookup = (from t in Enumerable.Range(0, (int)ItemID.Count)
					select EmergencyStacking.PreservationOrder.First((EmergencyStacking.Group g) => g.Contains(ContentSamples.ItemsByType[t]))).ToArray<EmergencyStacking.Group>();
				return EmergencyStacking._groupLookup;
			}
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00534384 File Offset: 0x00532584
		public static bool HasPendingTransferInvolving(WorldItem item)
		{
			return EmergencyStacking.HasPendingTransfer[item.whoAmI];
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x00534394 File Offset: 0x00532594
		public static void ClearPendingTransfersInvolving(WorldItem item)
		{
			if (!EmergencyStacking.HasPendingTransferInvolving(item))
			{
				return;
			}
			EmergencyStacking.HasPendingTransfer[item.whoAmI] = false;
			EmergencyStacking.PendingTransfers.RemoveAll((EmergencyStacking.Transfer t) => t.src == item || t.dst == item);
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x005343E8 File Offset: 0x005325E8
		public static bool EmergencyStackItemsToMakeSpace(out int freeSlot)
		{
			int num = Math.Max(EmergencyStacking.ItemsToStackEachTime, EmergencyStacking.PendingTransfers.Count + 1);
			EmergencyStacking.PendingTransfers.Clear();
			EmergencyStacking.FindBestTransfers(EmergencyStacking.MemoStackableItems(), EmergencyStacking.PendingTransfers, num);
			EmergencyStacking.ProcessPendingTransfers(out freeSlot);
			EmergencyStacking.RequestOwnershipReleaseForPendingTransfers();
			return freeSlot < 400;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0053443C File Offset: 0x0053263C
		public static void ProcessPendingTransfers()
		{
			if (EmergencyStacking.PendingTransfers.Count == 0)
			{
				return;
			}
			int num;
			EmergencyStacking.ProcessPendingTransfers(out num);
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x00534460 File Offset: 0x00532660
		private static void ProcessPendingTransfers(out int freeSlot)
		{
			freeSlot = 400;
			for (int i = 0; i < EmergencyStacking.PendingTransfers.Count; i++)
			{
				EmergencyStacking.UpdateDestinationFromPreviousTransfers(EmergencyStacking.PendingTransfers, i);
				EmergencyStacking.Transfer transfer = EmergencyStacking.PendingTransfers[i];
				EmergencyStacking.DoTransfer(transfer);
				if (transfer.src.IsAir)
				{
					freeSlot = Math.Min(freeSlot, transfer.src.whoAmI);
				}
			}
			if (Main.netMode != 2)
			{
				EmergencyStacking.PendingTransfers.Clear();
				return;
			}
			Array.Clear(EmergencyStacking.HasPendingTransfer, 0, EmergencyStacking.HasPendingTransfer.Length);
			EmergencyStacking.PendingTransfers.RemoveAll((EmergencyStacking.Transfer t) => t.NumToTransfer == 0);
			foreach (EmergencyStacking.Transfer transfer2 in EmergencyStacking.PendingTransfers)
			{
				EmergencyStacking.HasPendingTransfer[transfer2.src.whoAmI] = true;
				EmergencyStacking.HasPendingTransfer[transfer2.dst.whoAmI] = true;
			}
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x00534578 File Offset: 0x00532778
		private static void UpdateDestinationFromPreviousTransfers(List<EmergencyStacking.Transfer> transfers, int i)
		{
			EmergencyStacking.Transfer transfer = transfers[i];
			WorldItem worldItem = transfer.dst;
			int num = i - 1;
			while (worldItem.IsAir && num >= 0)
			{
				if (transfers[num].src == worldItem)
				{
					worldItem = transfers[num].dst;
				}
				num--;
			}
			if (worldItem != transfer.dst)
			{
				transfer.dst = worldItem;
				transfers[i] = transfer;
			}
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x005345E0 File Offset: 0x005327E0
		private static EmergencyStacking.StackableItem[] MemoStackableItems()
		{
			List<Rectangle> playerViewRects = EmergencyStacking.GetPlayerViewRects();
			EmergencyStacking.StackableItem[] array = EmergencyStacking.stackableItemsScratch;
			Array.Clear(array, 0, array.Length);
			for (int i = 0; i < 400; i++)
			{
				WorldItem worldItem = Main.item[i];
				if (!worldItem.IsAir && worldItem.stack < worldItem.maxStack && !worldItem.instanced && worldItem.shimmerTime == 0f && Main.timeItemSlotCannotBeReusedFor[i] == 0)
				{
					array[i] = new EmergencyStacking.StackableItem
					{
						type = worldItem.type,
						age = worldItem.timeSinceItemSpawned,
						isOnScreen = EmergencyStacking.AnyContains(playerViewRects, worldItem.Center.ToPoint()),
						item = worldItem
					};
				}
			}
			return array;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x005346A8 File Offset: 0x005328A8
		private static List<Rectangle> GetPlayerViewRects()
		{
			List<Rectangle> list = EmergencyStacking.playerViewRectsScratch;
			list.Clear();
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active)
				{
					list.Add(Utils.CenteredRectangle(player.Center.ToPoint(), EmergencyStacking.PlayerViewRectSize));
				}
			}
			return list;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00534700 File Offset: 0x00532900
		private static void FindBestTransfers(EmergencyStacking.StackableItem[] stackableItems, List<EmergencyStacking.Transfer> transfers, int limit)
		{
			foreach (EmergencyStacking.StackableItem stackableItem in stackableItems)
			{
				if (stackableItem.type != 0)
				{
					EmergencyStacking.Transfer transfer = new EmergencyStacking.Transfer
					{
						distanceOrder = int.MaxValue
					};
					foreach (EmergencyStacking.StackableItem stackableItem2 in stackableItems)
					{
						if (stackableItem.type == stackableItem2.type && stackableItem.item != stackableItem2.item && !stackableItem.IsPreferredDestination(stackableItem2) && Item.CanStack(stackableItem.item.inner, stackableItem2.item.inner))
						{
							int num = EmergencyStacking.DistanceBetween(stackableItem.item, stackableItem2.item);
							if (num <= EmergencyStacking.MaxTransferDistance)
							{
								EmergencyStacking.Group group = EmergencyStacking.GroupLookup[stackableItem.type];
								EmergencyStacking.Transfer transfer2 = new EmergencyStacking.Transfer
								{
									src = stackableItem.item,
									dst = stackableItem2.item,
									distanceOrder = num / group.DistanceStepSize,
									preservationOrder = group.StackingPriority,
									distance = num
								};
								if (stackableItem.isOnScreen)
								{
									transfer2.distanceOrder += EmergencyStacking.OnScreenDistancePriorityPenalty;
								}
								if (transfer2.CompareTo(transfer) < 0)
								{
									transfer = transfer2;
								}
							}
						}
					}
					if (transfer.src != null)
					{
						EmergencyStacking.AddToOrderedList(transfers, limit, transfer);
					}
				}
			}
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0053487C File Offset: 0x00532A7C
		private static void DoTransfer(EmergencyStacking.Transfer t)
		{
			WorldItem src = t.src;
			WorldItem dst = t.dst;
			if (!t.HasOwnership)
			{
				return;
			}
			int numToTransfer = t.NumToTransfer;
			if (numToTransfer == 0)
			{
				return;
			}
			src.stack -= numToTransfer;
			dst.stack += numToTransfer;
			if (src.stack <= 0)
			{
				src.TurnToAir(false);
			}
			if (dst.stack == dst.maxStack)
			{
				EmergencyStacking.OnReachingMaxStack(dst);
			}
			if (Main.netMode != 0)
			{
				NetMessage.SendData(21, -1, -1, null, dst.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				NetMessage.SendData(21, -1, -1, null, src.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00534938 File Offset: 0x00532B38
		private static void RequestOwnershipReleaseForPendingTransfers()
		{
			if (EmergencyStacking.PendingTransfers.Count == 0)
			{
				return;
			}
			for (int i = 0; i < 400; i++)
			{
				if (EmergencyStacking.HasPendingTransfer[i] && Main.item[i].playerIndexTheItemIsReservedFor != Main.myPlayer)
				{
					Main.item[i].FindOwner();
				}
			}
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0053498C File Offset: 0x00532B8C
		private static void AddToOrderedList(List<EmergencyStacking.Transfer> list, int limit, EmergencyStacking.Transfer item)
		{
			int num = 0;
			while (num < list.Count && item.CompareTo(list[num]) >= 0)
			{
				num++;
			}
			if (num == limit)
			{
				return;
			}
			if (list.Count == limit)
			{
				list.RemoveAt(list.Count - 1);
			}
			list.Insert(num, item);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x005349E0 File Offset: 0x00532BE0
		private static bool AnyContains(List<Rectangle> rects, Point point)
		{
			foreach (Rectangle rectangle in rects)
			{
				if (rectangle.Contains(point))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00534A38 File Offset: 0x00532C38
		private static int DistanceBetween(WorldItem a, WorldItem b)
		{
			Vector2 vector = a.position - b.position;
			return Math.Abs((int)vector.X) + Math.Abs((int)vector.Y);
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00534A70 File Offset: 0x00532C70
		private static void OnReachingMaxStack(WorldItem item)
		{
			switch (item.type)
			{
			case 71:
				item.SetDefaults(72);
				return;
			case 72:
				item.SetDefaults(73);
				return;
			case 73:
				item.SetDefaults(74);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00534AB4 File Offset: 0x00532CB4
		// Note: this type is marked as 'beforefieldinit'.
		static EmergencyStacking()
		{
		}

		// Token: 0x04004CC5 RID: 19653
		public static readonly List<EmergencyStacking.Group> PreservationOrder = new List<EmergencyStacking.Group>
		{
			EmergencyStacking.Group.RareCurrency,
			EmergencyStacking.Group.Equipment,
			EmergencyStacking.Group.SilverCoins,
			EmergencyStacking.Group.CopperCoins,
			EmergencyStacking.Group.FallenStars,
			EmergencyStacking.Group.Default
		};

		// Token: 0x04004CC6 RID: 19654
		private static readonly Point PlayerViewRectSize = new Point(2320, 1600);

		// Token: 0x04004CC7 RID: 19655
		private static readonly int ItemsToStackEachTime = 20;

		// Token: 0x04004CC8 RID: 19656
		private static readonly int MaxTransferDistance = 2400;

		// Token: 0x04004CC9 RID: 19657
		private static readonly int OnScreenDistancePriorityPenalty = 3;

		// Token: 0x04004CCA RID: 19658
		private static EmergencyStacking.Group[] _groupLookup;

		// Token: 0x04004CCB RID: 19659
		private static readonly List<EmergencyStacking.Transfer> PendingTransfers = new List<EmergencyStacking.Transfer>(EmergencyStacking.ItemsToStackEachTime);

		// Token: 0x04004CCC RID: 19660
		private static readonly bool[] HasPendingTransfer = new bool[401];

		// Token: 0x04004CCD RID: 19661
		private static readonly EmergencyStacking.StackableItem[] stackableItemsScratch = new EmergencyStacking.StackableItem[400];

		// Token: 0x04004CCE RID: 19662
		private static readonly List<Rectangle> playerViewRectsScratch = new List<Rectangle>(255);

		// Token: 0x020007B7 RID: 1975
		public class Group
		{
			// Token: 0x060041D8 RID: 16856 RVA: 0x006BCCC9 File Offset: 0x006BAEC9
			public Group()
			{
			}

			// Token: 0x060041D9 RID: 16857 RVA: 0x006BCCE7 File Offset: 0x006BAEE7
			public Group(int type)
			{
				this.Add(type);
			}

			// Token: 0x060041DA RID: 16858 RVA: 0x006BCD0D File Offset: 0x006BAF0D
			public Group(Predicate<Item> condition)
			{
				this.Add(condition);
			}

			// Token: 0x060041DB RID: 16859 RVA: 0x006BCD33 File Offset: 0x006BAF33
			public EmergencyStacking.Group Add(Predicate<Item> condition)
			{
				this.Conditions.Add(condition);
				return this;
			}

			// Token: 0x060041DC RID: 16860 RVA: 0x006BCD44 File Offset: 0x006BAF44
			public EmergencyStacking.Group Add(int type)
			{
				return this.Add((Item item) => item.type == type);
			}

			// Token: 0x060041DD RID: 16861 RVA: 0x006BCD70 File Offset: 0x006BAF70
			public bool Contains(Item item)
			{
				return this.Conditions.Any((Predicate<Item> p) => p(item));
			}

			// Token: 0x060041DE RID: 16862 RVA: 0x006BCDA4 File Offset: 0x006BAFA4
			// Note: this type is marked as 'beforefieldinit'.
			static Group()
			{
			}

			// Token: 0x040070BF RID: 28863
			public static readonly int DefaultStackDistanceStepSize = 160;

			// Token: 0x040070C0 RID: 28864
			public int DistanceStepSize = EmergencyStacking.Group.DefaultStackDistanceStepSize;

			// Token: 0x040070C1 RID: 28865
			private List<Predicate<Item>> Conditions = new List<Predicate<Item>>();

			// Token: 0x040070C2 RID: 28866
			internal int StackingPriority;

			// Token: 0x040070C3 RID: 28867
			public static EmergencyStacking.Group FallenStars = new EmergencyStacking.Group(75)
			{
				DistanceStepSize = EmergencyStacking.Group.DefaultStackDistanceStepSize * 4
			};

			// Token: 0x040070C4 RID: 28868
			public static EmergencyStacking.Group CopperCoins = new EmergencyStacking.Group(71);

			// Token: 0x040070C5 RID: 28869
			public static EmergencyStacking.Group SilverCoins = new EmergencyStacking.Group(72);

			// Token: 0x040070C6 RID: 28870
			public static EmergencyStacking.Group Equipment = new EmergencyStacking.Group((Item item) => item.OnlyNeedOneInInventory());

			// Token: 0x040070C7 RID: 28871
			public static EmergencyStacking.Group RareCurrency = new EmergencyStacking.Group
			{
				DistanceStepSize = EmergencyStacking.Group.DefaultStackDistanceStepSize / 4
			}.Add(73).Add(74).Add(3822);

			// Token: 0x040070C8 RID: 28872
			public static EmergencyStacking.Group Default = new EmergencyStacking.Group((Item item) => true);

			// Token: 0x02000AB2 RID: 2738
			[CompilerGenerated]
			private sealed class <>c__DisplayClass8_0
			{
				// Token: 0x06004C1A RID: 19482 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass8_0()
				{
				}

				// Token: 0x06004C1B RID: 19483 RVA: 0x006DA7DC File Offset: 0x006D89DC
				internal bool <Add>b__0(Item item)
				{
					return item.type == this.type;
				}

				// Token: 0x04007869 RID: 30825
				public int type;
			}

			// Token: 0x02000AB3 RID: 2739
			[CompilerGenerated]
			private sealed class <>c__DisplayClass9_0
			{
				// Token: 0x06004C1C RID: 19484 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass9_0()
				{
				}

				// Token: 0x06004C1D RID: 19485 RVA: 0x006DA7EC File Offset: 0x006D89EC
				internal bool <Contains>b__0(Predicate<Item> p)
				{
					return p(this.item);
				}

				// Token: 0x0400786A RID: 30826
				public Item item;
			}

			// Token: 0x02000AB4 RID: 2740
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004C1E RID: 19486 RVA: 0x006DA7FA File Offset: 0x006D89FA
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004C1F RID: 19487 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004C20 RID: 19488 RVA: 0x006DA806 File Offset: 0x006D8A06
				internal bool <.cctor>b__16_0(Item item)
				{
					return item.OnlyNeedOneInInventory();
				}

				// Token: 0x06004C21 RID: 19489 RVA: 0x000379E9 File Offset: 0x00035BE9
				internal bool <.cctor>b__16_1(Item item)
				{
					return true;
				}

				// Token: 0x0400786B RID: 30827
				public static readonly EmergencyStacking.Group.<>c <>9 = new EmergencyStacking.Group.<>c();
			}
		}

		// Token: 0x020007B8 RID: 1976
		private struct StackableItem
		{
			// Token: 0x060041DF RID: 16863 RVA: 0x006BCE50 File Offset: 0x006BB050
			public bool IsPreferredDestination(EmergencyStacking.StackableItem other)
			{
				if (this.isOnScreen != other.isOnScreen)
				{
					return this.isOnScreen;
				}
				if (this.age != other.age)
				{
					return this.age < other.age;
				}
				return this.item.whoAmI < other.item.whoAmI;
			}

			// Token: 0x040070C9 RID: 28873
			public int type;

			// Token: 0x040070CA RID: 28874
			public int age;

			// Token: 0x040070CB RID: 28875
			public bool isOnScreen;

			// Token: 0x040070CC RID: 28876
			public WorldItem item;
		}

		// Token: 0x020007B9 RID: 1977
		private struct Transfer : IComparable<EmergencyStacking.Transfer>
		{
			// Token: 0x060041E0 RID: 16864 RVA: 0x006BCEA8 File Offset: 0x006BB0A8
			public int CompareTo(EmergencyStacking.Transfer other)
			{
				int num = 0;
				if (num == 0)
				{
					num = this.distanceOrder.CompareTo(other.distanceOrder);
				}
				if (num == 0)
				{
					num = this.preservationOrder.CompareTo(other.preservationOrder);
				}
				if (num == 0)
				{
					num = this.distance.CompareTo(other.distance);
				}
				return num;
			}

			// Token: 0x060041E1 RID: 16865 RVA: 0x006BCEF8 File Offset: 0x006BB0F8
			public override string ToString()
			{
				return string.Format("({0},{1},{2}) {3} -> {4}", new object[] { this.distanceOrder, this.preservationOrder, this.distance, this.src, this.dst });
			}

			// Token: 0x1700052F RID: 1327
			// (get) Token: 0x060041E2 RID: 16866 RVA: 0x006BCF51 File Offset: 0x006BB151
			public bool HasOwnership
			{
				get
				{
					return this.src.playerIndexTheItemIsReservedFor == Main.myPlayer && this.dst.playerIndexTheItemIsReservedFor == Main.myPlayer;
				}
			}

			// Token: 0x17000530 RID: 1328
			// (get) Token: 0x060041E3 RID: 16867 RVA: 0x006BCF7C File Offset: 0x006BB17C
			public int NumToTransfer
			{
				get
				{
					if (!Item.CanStack(this.src.inner, this.dst.inner))
					{
						return 0;
					}
					return Math.Min(this.src.stack, this.dst.maxStack - this.dst.stack);
				}
			}

			// Token: 0x040070CD RID: 28877
			public WorldItem src;

			// Token: 0x040070CE RID: 28878
			public WorldItem dst;

			// Token: 0x040070CF RID: 28879
			public int distanceOrder;

			// Token: 0x040070D0 RID: 28880
			public int preservationOrder;

			// Token: 0x040070D1 RID: 28881
			public int distance;
		}

		// Token: 0x020007BA RID: 1978
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x060041E4 RID: 16868 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x060041E5 RID: 16869 RVA: 0x006BCFCF File Offset: 0x006BB1CF
			internal bool <get_GroupLookup>b__1(EmergencyStacking.Group g)
			{
				return g.Contains(ContentSamples.ItemsByType[this.t]);
			}

			// Token: 0x040070D2 RID: 28882
			public int t;
		}

		// Token: 0x020007BB RID: 1979
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060041E6 RID: 16870 RVA: 0x006BCFE7 File Offset: 0x006BB1E7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060041E7 RID: 16871 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060041E8 RID: 16872 RVA: 0x006BCFF4 File Offset: 0x006BB1F4
			internal EmergencyStacking.Group <get_GroupLookup>b__10_0(int t)
			{
				EmergencyStacking.<>c__DisplayClass10_0 CS$<>8__locals1 = new EmergencyStacking.<>c__DisplayClass10_0();
				CS$<>8__locals1.t = t;
				return EmergencyStacking.PreservationOrder.First((EmergencyStacking.Group g) => g.Contains(ContentSamples.ItemsByType[CS$<>8__locals1.t]));
			}

			// Token: 0x060041E9 RID: 16873 RVA: 0x006BD024 File Offset: 0x006BB224
			internal bool <ProcessPendingTransfers>b__17_0(EmergencyStacking.Transfer t)
			{
				return t.NumToTransfer == 0;
			}

			// Token: 0x040070D3 RID: 28883
			public static readonly EmergencyStacking.<>c <>9 = new EmergencyStacking.<>c();

			// Token: 0x040070D4 RID: 28884
			public static Func<int, EmergencyStacking.Group> <>9__10_0;

			// Token: 0x040070D5 RID: 28885
			public static Predicate<EmergencyStacking.Transfer> <>9__17_0;
		}

		// Token: 0x020007BC RID: 1980
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x060041EA RID: 16874 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x060041EB RID: 16875 RVA: 0x006BD030 File Offset: 0x006BB230
			internal bool <ClearPendingTransfersInvolving>b__0(EmergencyStacking.Transfer t)
			{
				return t.src == this.item || t.dst == this.item;
			}

			// Token: 0x040070D6 RID: 28886
			public WorldItem item;
		}
	}
}
