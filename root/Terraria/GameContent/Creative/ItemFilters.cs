using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
	// Token: 0x02000321 RID: 801
	public static class ItemFilters
	{
		// Token: 0x04005101 RID: 20737
		private const int framesPerRow = 11;

		// Token: 0x04005102 RID: 20738
		private const int framesPerColumn = 1;

		// Token: 0x04005103 RID: 20739
		private const int frameSizeOffsetX = -2;

		// Token: 0x04005104 RID: 20740
		private const int frameSizeOffsetY = 0;

		// Token: 0x02000880 RID: 2176
		public class BySearch : IItemEntryFilter, IEntryFilter<Item>, ISearchFilter<Item>
		{
			// Token: 0x0600447C RID: 17532 RVA: 0x006C2A76 File Offset: 0x006C0C76
			public BySearch()
			{
			}

			// Token: 0x0600447D RID: 17533 RVA: 0x006C2A98 File Offset: 0x006C0C98
			public bool FitsFilter(Item entry)
			{
				if (this._search == null)
				{
					return true;
				}
				int num = 1;
				float knockBack = entry.knockBack;
				int stack = entry.stack;
				entry.stack = 1;
				Main.MouseText_DrawItemTooltip_GetLinesInfo(entry, ref this._unusedYoyoLogo, ref this._unusedResearchLine, knockBack, ref num, this._toolTipLines, this._unusedColor);
				entry.stack = stack;
				for (int i = 0; i < num; i++)
				{
					if (this._toolTipLines[i].IndexOf(this._search, StringComparison.OrdinalIgnoreCase) != -1)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600447E RID: 17534 RVA: 0x006C2B14 File Offset: 0x006C0D14
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabSearch";
			}

			// Token: 0x0600447F RID: 17535 RVA: 0x006C2B1B File Offset: 0x006C0D1B
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Rank_Light", 1);
				return new UIImageFramed(asset, asset.Frame(1, 1, 0, 0, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x06004480 RID: 17536 RVA: 0x006C2B54 File Offset: 0x006C0D54
			public void SetSearch(string searchText)
			{
				this._search = searchText;
			}

			// Token: 0x040072AE RID: 29358
			private const int _tooltipMaxLines = 30;

			// Token: 0x040072AF RID: 29359
			private string[] _toolTipLines = new string[30];

			// Token: 0x040072B0 RID: 29360
			private Color[] _unusedColor = new Color[30];

			// Token: 0x040072B1 RID: 29361
			private int _unusedYoyoLogo;

			// Token: 0x040072B2 RID: 29362
			private int _unusedResearchLine;

			// Token: 0x040072B3 RID: 29363
			private string _search;
		}

		// Token: 0x02000881 RID: 2177
		public class BuildingBlock : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06004481 RID: 17537 RVA: 0x006C2B5D File Offset: 0x006C0D5D
			public bool FitsFilter(Item entry)
			{
				return entry.createWall != -1 || entry.tileWand != -1 || (entry.createTile != -1 && !Main.tileFrameImportant[entry.createTile]);
			}

			// Token: 0x06004482 RID: 17538 RVA: 0x006C2B8F File Offset: 0x006C0D8F
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabBlocks";
			}

			// Token: 0x06004483 RID: 17539 RVA: 0x006C2B98 File Offset: 0x006C0D98
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 4, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x06004484 RID: 17540 RVA: 0x0000357B File Offset: 0x0000177B
			public BuildingBlock()
			{
			}
		}

		// Token: 0x02000882 RID: 2178
		public class Furniture : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06004485 RID: 17541 RVA: 0x006C2BE8 File Offset: 0x006C0DE8
			public bool FitsFilter(Item entry)
			{
				int createTile = entry.createTile;
				return createTile != -1 && Main.tileFrameImportant[createTile];
			}

			// Token: 0x06004486 RID: 17542 RVA: 0x006C2C0F File Offset: 0x006C0E0F
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabFurniture";
			}

			// Token: 0x06004487 RID: 17543 RVA: 0x006C2C18 File Offset: 0x006C0E18
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 7, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x06004488 RID: 17544 RVA: 0x0000357B File Offset: 0x0000177B
			public Furniture()
			{
			}
		}

		// Token: 0x02000883 RID: 2179
		public class Tools : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06004489 RID: 17545 RVA: 0x006C2C68 File Offset: 0x006C0E68
			public bool FitsFilter(Item entry)
			{
				return entry.pick > 0 || entry.axe > 0 || entry.hammer > 0 || entry.fishingPole > 0 || entry.tileWand != -1 || this._itemIdsThatAreAccepted.Contains(entry.type);
			}

			// Token: 0x0600448A RID: 17546 RVA: 0x006C2CC2 File Offset: 0x006C0EC2
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabTools";
			}

			// Token: 0x0600448B RID: 17547 RVA: 0x006C2CCC File Offset: 0x006C0ECC
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 6, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x0600448C RID: 17548 RVA: 0x006C2D1C File Offset: 0x006C0F1C
			public Tools()
			{
			}

			// Token: 0x040072B4 RID: 29364
			private HashSet<int> _itemIdsThatAreAccepted = new HashSet<int>
			{
				213, 5295, 509, 850, 851, 3612, 3625, 3611, 510, 849,
				3620, 1071, 1543, 1072, 1544, 1100, 1545, 50, 3199, 3124,
				5358, 5359, 5360, 5361, 5437, 1326, 5335, 3384, 4263, 4819,
				4262, 946, 4707, 205, 206, 207, 1128, 3031, 4820, 5302,
				5364, 4460, 4608, 4872, 3032, 5303, 5304, 1991, 4821, 3183,
				779, 5134, 1299, 4711, 4049, 114, 5667
			};
		}

		// Token: 0x02000884 RID: 2180
		public class Weapon : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x0600448D RID: 17549 RVA: 0x006C2FE0 File Offset: 0x006C11E0
			public bool FitsFilter(Item entry)
			{
				return entry.damage > 0;
			}

			// Token: 0x0600448E RID: 17550 RVA: 0x006C2FEB File Offset: 0x006C11EB
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabWeapons";
			}

			// Token: 0x0600448F RID: 17551 RVA: 0x006C2FF4 File Offset: 0x006C11F4
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 0, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x06004490 RID: 17552 RVA: 0x0000357B File Offset: 0x0000177B
			public Weapon()
			{
			}
		}

		// Token: 0x02000885 RID: 2181
		public abstract class AArmor
		{
			// Token: 0x06004491 RID: 17553 RVA: 0x006C3041 File Offset: 0x006C1241
			public bool IsAnArmorThatMatchesSocialState(Item entry, bool shouldBeSocial)
			{
				return (entry.bodySlot != -1 || entry.headSlot != -1 || entry.legSlot != -1) && entry.vanity == shouldBeSocial;
			}

			// Token: 0x06004492 RID: 17554 RVA: 0x0000357B File Offset: 0x0000177B
			protected AArmor()
			{
			}
		}

		// Token: 0x02000886 RID: 2182
		public class Armor : ItemFilters.AArmor, IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06004493 RID: 17555 RVA: 0x006C3071 File Offset: 0x006C1271
			public bool FitsFilter(Item entry)
			{
				return base.IsAnArmorThatMatchesSocialState(entry, false);
			}

			// Token: 0x06004494 RID: 17556 RVA: 0x006C307B File Offset: 0x006C127B
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabArmor";
			}

			// Token: 0x06004495 RID: 17557 RVA: 0x006C3084 File Offset: 0x006C1284
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 2, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x06004496 RID: 17558 RVA: 0x006C30D1 File Offset: 0x006C12D1
			public Armor()
			{
			}
		}

		// Token: 0x02000887 RID: 2183
		public class Vanity : ItemFilters.AArmor, IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x06004497 RID: 17559 RVA: 0x006C30D9 File Offset: 0x006C12D9
			public bool FitsFilter(Item entry)
			{
				return base.IsAnArmorThatMatchesSocialState(entry, true);
			}

			// Token: 0x06004498 RID: 17560 RVA: 0x006C30E3 File Offset: 0x006C12E3
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabVanity";
			}

			// Token: 0x06004499 RID: 17561 RVA: 0x006C30EC File Offset: 0x006C12EC
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 8, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x0600449A RID: 17562 RVA: 0x006C30D1 File Offset: 0x006C12D1
			public Vanity()
			{
			}
		}

		// Token: 0x02000888 RID: 2184
		public abstract class AAccessories
		{
			// Token: 0x0600449B RID: 17563 RVA: 0x006C313C File Offset: 0x006C133C
			public bool IsAnAccessoryOfType(Item entry, ItemFilters.AAccessories.AccessoriesCategory categoryType)
			{
				bool flag = ItemFilters.AAccessories.IsMiscEquipment(entry);
				return (flag && categoryType == ItemFilters.AAccessories.AccessoriesCategory.Misc) || (!flag && categoryType == ItemFilters.AAccessories.AccessoriesCategory.NonMisc && entry.accessory);
			}

			// Token: 0x0600449C RID: 17564 RVA: 0x006C316C File Offset: 0x006C136C
			public static bool IsMiscEquipment(Item item)
			{
				return item.mountType != -1 || (item.buffType > 0 && Main.lightPet[item.buffType]) || (item.buffType > 0 && Main.vanityPet[item.buffType]) || Main.projHook[item.shoot];
			}

			// Token: 0x0600449D RID: 17565 RVA: 0x0000357B File Offset: 0x0000177B
			protected AAccessories()
			{
			}

			// Token: 0x02000ADA RID: 2778
			public enum AccessoriesCategory
			{
				// Token: 0x0400788A RID: 30858
				Misc,
				// Token: 0x0400788B RID: 30859
				NonMisc
			}
		}

		// Token: 0x02000889 RID: 2185
		public class Accessories : ItemFilters.AAccessories, IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x0600449E RID: 17566 RVA: 0x006C31BE File Offset: 0x006C13BE
			public bool FitsFilter(Item entry)
			{
				return base.IsAnAccessoryOfType(entry, ItemFilters.AAccessories.AccessoriesCategory.NonMisc);
			}

			// Token: 0x0600449F RID: 17567 RVA: 0x006C31C8 File Offset: 0x006C13C8
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabAccessories";
			}

			// Token: 0x060044A0 RID: 17568 RVA: 0x006C31D0 File Offset: 0x006C13D0
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 1, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x060044A1 RID: 17569 RVA: 0x006C321D File Offset: 0x006C141D
			public Accessories()
			{
			}
		}

		// Token: 0x0200088A RID: 2186
		public class MiscAccessories : ItemFilters.AAccessories, IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060044A2 RID: 17570 RVA: 0x006C3225 File Offset: 0x006C1425
			public bool FitsFilter(Item entry)
			{
				return base.IsAnAccessoryOfType(entry, ItemFilters.AAccessories.AccessoriesCategory.Misc);
			}

			// Token: 0x060044A3 RID: 17571 RVA: 0x006C322F File Offset: 0x006C142F
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabAccessoriesMisc";
			}

			// Token: 0x060044A4 RID: 17572 RVA: 0x006C3238 File Offset: 0x006C1438
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 9, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x060044A5 RID: 17573 RVA: 0x006C321D File Offset: 0x006C141D
			public MiscAccessories()
			{
			}
		}

		// Token: 0x0200088B RID: 2187
		public class Consumables : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060044A6 RID: 17574 RVA: 0x006C3288 File Offset: 0x006C1488
			public bool FitsFilter(Item entry)
			{
				int type = entry.type;
				if (type == 267 || type == 1307)
				{
					return true;
				}
				bool flag = entry.createTile != -1 || entry.createWall != -1 || entry.tileWand != -1;
				return entry.consumable && !flag;
			}

			// Token: 0x060044A7 RID: 17575 RVA: 0x006C32DE File Offset: 0x006C14DE
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabConsumables";
			}

			// Token: 0x060044A8 RID: 17576 RVA: 0x006C32E8 File Offset: 0x006C14E8
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 3, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x060044A9 RID: 17577 RVA: 0x0000357B File Offset: 0x0000177B
			public Consumables()
			{
			}
		}

		// Token: 0x0200088C RID: 2188
		public class GameplayItems : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060044AA RID: 17578 RVA: 0x006C3335 File Offset: 0x006C1535
			public bool FitsFilter(Item entry)
			{
				return ItemID.Sets.SortingPriorityMiscImportants[entry.type] != -1;
			}

			// Token: 0x060044AB RID: 17579 RVA: 0x006C3349 File Offset: 0x006C1549
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabMisc";
			}

			// Token: 0x060044AC RID: 17580 RVA: 0x006C3350 File Offset: 0x006C1550
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 5, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x060044AD RID: 17581 RVA: 0x0000357B File Offset: 0x0000177B
			public GameplayItems()
			{
			}
		}

		// Token: 0x0200088D RID: 2189
		public class MiscFallback : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060044AE RID: 17582 RVA: 0x006C339D File Offset: 0x006C159D
			public MiscFallback(List<IItemEntryFilter> otherFiltersToCheckAgainst)
			{
				this.otherFiltersToCheckAgainst = otherFiltersToCheckAgainst;
			}

			// Token: 0x060044AF RID: 17583 RVA: 0x006C33BC File Offset: 0x006C15BC
			public bool FitsFilter(Item entry)
			{
				bool? flag = this._fitsFilterByItemType[entry.type];
				if (flag == null)
				{
					bool?[] fitsFilterByItemType = this._fitsFilterByItemType;
					int type = entry.type;
					flag = new bool?(!this.otherFiltersToCheckAgainst.Any((IItemEntryFilter f) => f.FitsFilter(entry)));
					fitsFilterByItemType[type] = flag;
				}
				return flag.Value;
			}

			// Token: 0x060044B0 RID: 17584 RVA: 0x006C3349 File Offset: 0x006C1549
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabMisc";
			}

			// Token: 0x060044B1 RID: 17585 RVA: 0x006C3438 File Offset: 0x006C1638
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 5, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x040072B5 RID: 29365
			private readonly List<IItemEntryFilter> otherFiltersToCheckAgainst;

			// Token: 0x040072B6 RID: 29366
			private bool?[] _fitsFilterByItemType = new bool?[(int)ItemID.Count];

			// Token: 0x02000ADB RID: 2779
			[CompilerGenerated]
			private sealed class <>c__DisplayClass3_0
			{
				// Token: 0x06004CDD RID: 19677 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass3_0()
				{
				}

				// Token: 0x06004CDE RID: 19678 RVA: 0x006DB26B File Offset: 0x006D946B
				internal bool <FitsFilter>b__0(IItemEntryFilter f)
				{
					return f.FitsFilter(this.entry);
				}

				// Token: 0x0400788C RID: 30860
				public Item entry;
			}
		}

		// Token: 0x0200088E RID: 2190
		public class Materials : IItemEntryFilter, IEntryFilter<Item>
		{
			// Token: 0x060044B2 RID: 17586 RVA: 0x006C3485 File Offset: 0x006C1685
			public bool FitsFilter(Item entry)
			{
				return entry.material;
			}

			// Token: 0x060044B3 RID: 17587 RVA: 0x006C348D File Offset: 0x006C168D
			public string GetDisplayNameKey()
			{
				return "CreativePowers.TabMaterials";
			}

			// Token: 0x060044B4 RID: 17588 RVA: 0x006C3494 File Offset: 0x006C1694
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Icons", 1);
				return new UIImageFramed(asset, asset.Frame(11, 1, 10, 0, 0, 0).OffsetSize(-2, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x060044B5 RID: 17589 RVA: 0x0000357B File Offset: 0x0000177B
			public Materials()
			{
			}
		}
	}
}
