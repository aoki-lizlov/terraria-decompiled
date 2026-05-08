using System;

namespace Terraria.ID
{
	// Token: 0x0200019F RID: 415
	public static class PlayerItemSlotID
	{
		// Token: 0x06001F17 RID: 7959 RVA: 0x00513A2C File Offset: 0x00511C2C
		static PlayerItemSlotID()
		{
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x00513B34 File Offset: 0x00511D34
		private static int AllocateSlots(int amount, bool canNetRelay)
		{
			int nextSlotId = PlayerItemSlotID._nextSlotId;
			PlayerItemSlotID._nextSlotId += amount;
			int num = PlayerItemSlotID.CanRelay.Length;
			Array.Resize<bool>(ref PlayerItemSlotID.CanRelay, num + amount);
			for (int i = num; i < PlayerItemSlotID._nextSlotId; i++)
			{
				PlayerItemSlotID.CanRelay[i] = canNetRelay;
			}
			return nextSlotId;
		}

		// Token: 0x04001921 RID: 6433
		public static readonly int Inventory0 = PlayerItemSlotID.AllocateSlots(58, true);

		// Token: 0x04001922 RID: 6434
		public static readonly int InventoryMouseItem = PlayerItemSlotID.AllocateSlots(1, true);

		// Token: 0x04001923 RID: 6435
		public static readonly int Armor0 = PlayerItemSlotID.AllocateSlots(20, true);

		// Token: 0x04001924 RID: 6436
		public static readonly int Dye0 = PlayerItemSlotID.AllocateSlots(10, true);

		// Token: 0x04001925 RID: 6437
		public static readonly int Misc0 = PlayerItemSlotID.AllocateSlots(5, true);

		// Token: 0x04001926 RID: 6438
		public static readonly int MiscDye0 = PlayerItemSlotID.AllocateSlots(5, true);

		// Token: 0x04001927 RID: 6439
		public static readonly int Bank1_0 = PlayerItemSlotID.AllocateSlots(200, false);

		// Token: 0x04001928 RID: 6440
		public static readonly int Bank2_0 = PlayerItemSlotID.AllocateSlots(200, false);

		// Token: 0x04001929 RID: 6441
		public static readonly int TrashItem = PlayerItemSlotID.AllocateSlots(1, false);

		// Token: 0x0400192A RID: 6442
		public static readonly int Bank3_0 = PlayerItemSlotID.AllocateSlots(200, false);

		// Token: 0x0400192B RID: 6443
		public static readonly int Bank4_0 = PlayerItemSlotID.AllocateSlots(200, true);

		// Token: 0x0400192C RID: 6444
		public static readonly int Loadout1_Armor_0 = PlayerItemSlotID.AllocateSlots(20, true);

		// Token: 0x0400192D RID: 6445
		public static readonly int Loadout1_Dye_0 = PlayerItemSlotID.AllocateSlots(10, true);

		// Token: 0x0400192E RID: 6446
		public static readonly int Loadout2_Armor_0 = PlayerItemSlotID.AllocateSlots(20, true);

		// Token: 0x0400192F RID: 6447
		public static readonly int Loadout2_Dye_0 = PlayerItemSlotID.AllocateSlots(10, true);

		// Token: 0x04001930 RID: 6448
		public static readonly int Loadout3_Armor_0 = PlayerItemSlotID.AllocateSlots(20, true);

		// Token: 0x04001931 RID: 6449
		public static readonly int Loadout3_Dye_0 = PlayerItemSlotID.AllocateSlots(10, true);

		// Token: 0x04001932 RID: 6450
		public static readonly int Count = PlayerItemSlotID._nextSlotId;

		// Token: 0x04001933 RID: 6451
		public static bool[] CanRelay = new bool[0];

		// Token: 0x04001934 RID: 6452
		private static int _nextSlotId;

		// Token: 0x02000762 RID: 1890
		public struct SlotReference
		{
			// Token: 0x06004113 RID: 16659 RVA: 0x006A09FE File Offset: 0x0069EBFE
			public SlotReference(Player player, int slot)
			{
				this.Player = player;
				this.SlotId = slot;
			}

			// Token: 0x17000527 RID: 1319
			// (get) Token: 0x06004114 RID: 16660 RVA: 0x006A0A10 File Offset: 0x0069EC10
			// (set) Token: 0x06004115 RID: 16661 RVA: 0x006A0A60 File Offset: 0x0069EC60
			public Item Item
			{
				get
				{
					if (this.SlotId == PlayerItemSlotID.TrashItem)
					{
						return this.Player.trashItem;
					}
					Item[] array;
					int num;
					if (!this.TryGetArraySlot(out array, out num))
					{
						throw new IndexOutOfRangeException("SlotId: " + this.SlotId);
					}
					return array[num];
				}
				set
				{
					if (this.SlotId == PlayerItemSlotID.TrashItem)
					{
						this.Player.trashItem = value;
						return;
					}
					Item[] array;
					int num;
					if (!this.TryGetArraySlot(out array, out num))
					{
						throw new IndexOutOfRangeException("SlotId: " + this.SlotId);
					}
					array[num] = value;
				}
			}

			// Token: 0x06004116 RID: 16662 RVA: 0x006A0AB4 File Offset: 0x0069ECB4
			private bool TryGetArraySlot(out Item[] arr, out int slot)
			{
				if (this.SlotId >= PlayerItemSlotID.Loadout3_Dye_0)
				{
					slot = this.SlotId - PlayerItemSlotID.Loadout3_Dye_0;
					arr = this.Player.Loadouts[2].Dye;
				}
				else if (this.SlotId >= PlayerItemSlotID.Loadout3_Armor_0)
				{
					slot = this.SlotId - PlayerItemSlotID.Loadout3_Armor_0;
					arr = this.Player.Loadouts[2].Armor;
				}
				else if (this.SlotId >= PlayerItemSlotID.Loadout2_Dye_0)
				{
					slot = this.SlotId - PlayerItemSlotID.Loadout2_Dye_0;
					arr = this.Player.Loadouts[1].Dye;
				}
				else if (this.SlotId >= PlayerItemSlotID.Loadout2_Armor_0)
				{
					slot = this.SlotId - PlayerItemSlotID.Loadout2_Armor_0;
					arr = this.Player.Loadouts[1].Armor;
				}
				else if (this.SlotId >= PlayerItemSlotID.Loadout1_Dye_0)
				{
					slot = this.SlotId - PlayerItemSlotID.Loadout1_Dye_0;
					arr = this.Player.Loadouts[0].Dye;
				}
				else if (this.SlotId >= PlayerItemSlotID.Loadout1_Armor_0)
				{
					slot = this.SlotId - PlayerItemSlotID.Loadout1_Armor_0;
					arr = this.Player.Loadouts[0].Armor;
				}
				else if (this.SlotId >= PlayerItemSlotID.Bank4_0)
				{
					slot = this.SlotId - PlayerItemSlotID.Bank4_0;
					arr = this.Player.bank4.item;
				}
				else if (this.SlotId >= PlayerItemSlotID.Bank3_0)
				{
					slot = this.SlotId - PlayerItemSlotID.Bank3_0;
					arr = this.Player.bank3.item;
				}
				else
				{
					if (this.SlotId >= PlayerItemSlotID.TrashItem)
					{
						slot = 0;
						arr = null;
						return false;
					}
					if (this.SlotId >= PlayerItemSlotID.Bank2_0)
					{
						slot = this.SlotId - PlayerItemSlotID.Bank2_0;
						arr = this.Player.bank2.item;
					}
					else if (this.SlotId >= PlayerItemSlotID.Bank1_0)
					{
						slot = this.SlotId - PlayerItemSlotID.Bank1_0;
						arr = this.Player.bank.item;
					}
					else if (this.SlotId >= PlayerItemSlotID.MiscDye0)
					{
						slot = this.SlotId - PlayerItemSlotID.MiscDye0;
						arr = this.Player.miscDyes;
					}
					else if (this.SlotId >= PlayerItemSlotID.Misc0)
					{
						slot = this.SlotId - PlayerItemSlotID.Misc0;
						arr = this.Player.miscEquips;
					}
					else if (this.SlotId >= PlayerItemSlotID.Dye0)
					{
						slot = this.SlotId - PlayerItemSlotID.Dye0;
						arr = this.Player.dye;
					}
					else if (this.SlotId >= PlayerItemSlotID.Armor0)
					{
						slot = this.SlotId - PlayerItemSlotID.Armor0;
						arr = this.Player.armor;
					}
					else
					{
						slot = this.SlotId - PlayerItemSlotID.Inventory0;
						arr = this.Player.inventory;
					}
				}
				return true;
			}

			// Token: 0x04006A10 RID: 27152
			public readonly Player Player;

			// Token: 0x04006A11 RID: 27153
			public readonly int SlotId;
		}
	}
}
