using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Terraria.GameInput
{
	// Token: 0x02000091 RID: 145
	public class TriggersSet
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x004D5A98 File Offset: 0x004D3C98
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x004D5AAA File Offset: 0x004D3CAA
		public bool MouseLeft
		{
			get
			{
				return this.KeyStatus["MouseLeft"];
			}
			set
			{
				this.KeyStatus["MouseLeft"] = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x004D5ABD File Offset: 0x004D3CBD
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x004D5ACF File Offset: 0x004D3CCF
		public bool MouseRight
		{
			get
			{
				return this.KeyStatus["MouseRight"];
			}
			set
			{
				this.KeyStatus["MouseRight"] = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x004D5AE2 File Offset: 0x004D3CE2
		// (set) Token: 0x0600160D RID: 5645 RVA: 0x004D5AF4 File Offset: 0x004D3CF4
		public bool Up
		{
			get
			{
				return this.KeyStatus["Up"];
			}
			set
			{
				this.KeyStatus["Up"] = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x004D5B07 File Offset: 0x004D3D07
		// (set) Token: 0x0600160F RID: 5647 RVA: 0x004D5B19 File Offset: 0x004D3D19
		public bool Down
		{
			get
			{
				return this.KeyStatus["Down"];
			}
			set
			{
				this.KeyStatus["Down"] = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x004D5B2C File Offset: 0x004D3D2C
		// (set) Token: 0x06001611 RID: 5649 RVA: 0x004D5B3E File Offset: 0x004D3D3E
		public bool Left
		{
			get
			{
				return this.KeyStatus["Left"];
			}
			set
			{
				this.KeyStatus["Left"] = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x004D5B51 File Offset: 0x004D3D51
		// (set) Token: 0x06001613 RID: 5651 RVA: 0x004D5B63 File Offset: 0x004D3D63
		public bool Right
		{
			get
			{
				return this.KeyStatus["Right"];
			}
			set
			{
				this.KeyStatus["Right"] = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x004D5B76 File Offset: 0x004D3D76
		// (set) Token: 0x06001615 RID: 5653 RVA: 0x004D5B88 File Offset: 0x004D3D88
		public bool Jump
		{
			get
			{
				return this.KeyStatus["Jump"];
			}
			set
			{
				this.KeyStatus["Jump"] = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x004D5B9B File Offset: 0x004D3D9B
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x004D5BAD File Offset: 0x004D3DAD
		public bool Throw
		{
			get
			{
				return this.KeyStatus["Throw"];
			}
			set
			{
				this.KeyStatus["Throw"] = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x004D5BC0 File Offset: 0x004D3DC0
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x004D5BD2 File Offset: 0x004D3DD2
		public bool Inventory
		{
			get
			{
				return this.KeyStatus["Inventory"];
			}
			set
			{
				this.KeyStatus["Inventory"] = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x004D5BE5 File Offset: 0x004D3DE5
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x004D5BF7 File Offset: 0x004D3DF7
		public bool Grapple
		{
			get
			{
				return this.KeyStatus["Grapple"];
			}
			set
			{
				this.KeyStatus["Grapple"] = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x004D5C0A File Offset: 0x004D3E0A
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x004D5C1C File Offset: 0x004D3E1C
		public bool SmartSelect
		{
			get
			{
				return this.KeyStatus["SmartSelect"];
			}
			set
			{
				this.KeyStatus["SmartSelect"] = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x004D5C2F File Offset: 0x004D3E2F
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x004D5C41 File Offset: 0x004D3E41
		public bool SmartCursor
		{
			get
			{
				return this.KeyStatus["SmartCursor"];
			}
			set
			{
				this.KeyStatus["SmartCursor"] = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x004D5C54 File Offset: 0x004D3E54
		// (set) Token: 0x06001621 RID: 5665 RVA: 0x004D5C66 File Offset: 0x004D3E66
		public bool QuickMount
		{
			get
			{
				return this.KeyStatus["QuickMount"];
			}
			set
			{
				this.KeyStatus["QuickMount"] = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x004D5C79 File Offset: 0x004D3E79
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x004D5C8B File Offset: 0x004D3E8B
		public bool QuickHeal
		{
			get
			{
				return this.KeyStatus["QuickHeal"];
			}
			set
			{
				this.KeyStatus["QuickHeal"] = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x004D5C9E File Offset: 0x004D3E9E
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x004D5CB0 File Offset: 0x004D3EB0
		public bool QuickMana
		{
			get
			{
				return this.KeyStatus["QuickMana"];
			}
			set
			{
				this.KeyStatus["QuickMana"] = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x004D5CC3 File Offset: 0x004D3EC3
		// (set) Token: 0x06001627 RID: 5671 RVA: 0x004D5CD5 File Offset: 0x004D3ED5
		public bool QuickBuff
		{
			get
			{
				return this.KeyStatus["QuickBuff"];
			}
			set
			{
				this.KeyStatus["QuickBuff"] = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x004D5CE8 File Offset: 0x004D3EE8
		// (set) Token: 0x06001629 RID: 5673 RVA: 0x004D5CFA File Offset: 0x004D3EFA
		public bool Loadout1
		{
			get
			{
				return this.KeyStatus["Loadout1"];
			}
			set
			{
				this.KeyStatus["Loadout1"] = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x004D5D0D File Offset: 0x004D3F0D
		// (set) Token: 0x0600162B RID: 5675 RVA: 0x004D5D1F File Offset: 0x004D3F1F
		public bool Loadout2
		{
			get
			{
				return this.KeyStatus["Loadout2"];
			}
			set
			{
				this.KeyStatus["Loadout2"] = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x004D5D32 File Offset: 0x004D3F32
		// (set) Token: 0x0600162D RID: 5677 RVA: 0x004D5D44 File Offset: 0x004D3F44
		public bool Loadout3
		{
			get
			{
				return this.KeyStatus["Loadout3"];
			}
			set
			{
				this.KeyStatus["Loadout3"] = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x004D5D57 File Offset: 0x004D3F57
		// (set) Token: 0x0600162F RID: 5679 RVA: 0x004D5D69 File Offset: 0x004D3F69
		public bool Dash
		{
			get
			{
				return this.KeyStatus["Dash"];
			}
			set
			{
				this.KeyStatus["Dash"] = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x004D5D7C File Offset: 0x004D3F7C
		// (set) Token: 0x06001631 RID: 5681 RVA: 0x004D5D8E File Offset: 0x004D3F8E
		public bool ArmorSetAbility
		{
			get
			{
				return this.KeyStatus["ArmorSetAbility"];
			}
			set
			{
				this.KeyStatus["ArmorSetAbility"] = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x004D5DA1 File Offset: 0x004D3FA1
		// (set) Token: 0x06001633 RID: 5683 RVA: 0x004D5DB3 File Offset: 0x004D3FB3
		public bool NextLoadout
		{
			get
			{
				return this.KeyStatus["NextLoadout"];
			}
			set
			{
				this.KeyStatus["NextLoadout"] = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x004D5DC6 File Offset: 0x004D3FC6
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x004D5DD8 File Offset: 0x004D3FD8
		public bool PreviousLoadout
		{
			get
			{
				return this.KeyStatus["PreviousLoadout"];
			}
			set
			{
				this.KeyStatus["PreviousLoadout"] = value;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x004D5DEB File Offset: 0x004D3FEB
		// (set) Token: 0x06001637 RID: 5687 RVA: 0x004D5DFD File Offset: 0x004D3FFD
		public bool MapZoomIn
		{
			get
			{
				return this.KeyStatus["MapZoomIn"];
			}
			set
			{
				this.KeyStatus["MapZoomIn"] = value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x004D5E10 File Offset: 0x004D4010
		// (set) Token: 0x06001639 RID: 5689 RVA: 0x004D5E22 File Offset: 0x004D4022
		public bool MapZoomOut
		{
			get
			{
				return this.KeyStatus["MapZoomOut"];
			}
			set
			{
				this.KeyStatus["MapZoomOut"] = value;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x004D5E35 File Offset: 0x004D4035
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x004D5E47 File Offset: 0x004D4047
		public bool MapAlphaUp
		{
			get
			{
				return this.KeyStatus["MapAlphaUp"];
			}
			set
			{
				this.KeyStatus["MapAlphaUp"] = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x004D5E5A File Offset: 0x004D405A
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x004D5E6C File Offset: 0x004D406C
		public bool MapAlphaDown
		{
			get
			{
				return this.KeyStatus["MapAlphaDown"];
			}
			set
			{
				this.KeyStatus["MapAlphaDown"] = value;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x004D5E7F File Offset: 0x004D407F
		// (set) Token: 0x0600163F RID: 5695 RVA: 0x004D5E91 File Offset: 0x004D4091
		public bool MapFull
		{
			get
			{
				return this.KeyStatus["MapFull"];
			}
			set
			{
				this.KeyStatus["MapFull"] = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x004D5EA4 File Offset: 0x004D40A4
		// (set) Token: 0x06001641 RID: 5697 RVA: 0x004D5EB6 File Offset: 0x004D40B6
		public bool MapStyle
		{
			get
			{
				return this.KeyStatus["MapStyle"];
			}
			set
			{
				this.KeyStatus["MapStyle"] = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x004D5EC9 File Offset: 0x004D40C9
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x004D5EDB File Offset: 0x004D40DB
		public bool Hotbar1
		{
			get
			{
				return this.KeyStatus["Hotbar1"];
			}
			set
			{
				this.KeyStatus["Hotbar1"] = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x004D5EEE File Offset: 0x004D40EE
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x004D5F00 File Offset: 0x004D4100
		public bool Hotbar2
		{
			get
			{
				return this.KeyStatus["Hotbar2"];
			}
			set
			{
				this.KeyStatus["Hotbar2"] = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x004D5F13 File Offset: 0x004D4113
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x004D5F25 File Offset: 0x004D4125
		public bool Hotbar3
		{
			get
			{
				return this.KeyStatus["Hotbar3"];
			}
			set
			{
				this.KeyStatus["Hotbar3"] = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x004D5F38 File Offset: 0x004D4138
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x004D5F4A File Offset: 0x004D414A
		public bool Hotbar4
		{
			get
			{
				return this.KeyStatus["Hotbar4"];
			}
			set
			{
				this.KeyStatus["Hotbar4"] = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x004D5F5D File Offset: 0x004D415D
		// (set) Token: 0x0600164B RID: 5707 RVA: 0x004D5F6F File Offset: 0x004D416F
		public bool Hotbar5
		{
			get
			{
				return this.KeyStatus["Hotbar5"];
			}
			set
			{
				this.KeyStatus["Hotbar5"] = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x004D5F82 File Offset: 0x004D4182
		// (set) Token: 0x0600164D RID: 5709 RVA: 0x004D5F94 File Offset: 0x004D4194
		public bool Hotbar6
		{
			get
			{
				return this.KeyStatus["Hotbar6"];
			}
			set
			{
				this.KeyStatus["Hotbar6"] = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x004D5FA7 File Offset: 0x004D41A7
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x004D5FB9 File Offset: 0x004D41B9
		public bool Hotbar7
		{
			get
			{
				return this.KeyStatus["Hotbar7"];
			}
			set
			{
				this.KeyStatus["Hotbar7"] = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x004D5FCC File Offset: 0x004D41CC
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x004D5FDE File Offset: 0x004D41DE
		public bool Hotbar8
		{
			get
			{
				return this.KeyStatus["Hotbar8"];
			}
			set
			{
				this.KeyStatus["Hotbar8"] = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x004D5FF1 File Offset: 0x004D41F1
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x004D6003 File Offset: 0x004D4203
		public bool Hotbar9
		{
			get
			{
				return this.KeyStatus["Hotbar9"];
			}
			set
			{
				this.KeyStatus["Hotbar9"] = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x004D6016 File Offset: 0x004D4216
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x004D6028 File Offset: 0x004D4228
		public bool Hotbar10
		{
			get
			{
				return this.KeyStatus["Hotbar10"];
			}
			set
			{
				this.KeyStatus["Hotbar10"] = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x004D603B File Offset: 0x004D423B
		// (set) Token: 0x06001657 RID: 5719 RVA: 0x004D604D File Offset: 0x004D424D
		public bool HotbarMinus
		{
			get
			{
				return this.KeyStatus["HotbarMinus"];
			}
			set
			{
				this.KeyStatus["HotbarMinus"] = value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x004D6060 File Offset: 0x004D4260
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x004D6072 File Offset: 0x004D4272
		public bool HotbarPlus
		{
			get
			{
				return this.KeyStatus["HotbarPlus"];
			}
			set
			{
				this.KeyStatus["HotbarPlus"] = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x004D6085 File Offset: 0x004D4285
		// (set) Token: 0x0600165B RID: 5723 RVA: 0x004D6097 File Offset: 0x004D4297
		public bool DpadRadial1
		{
			get
			{
				return this.KeyStatus["DpadRadial1"];
			}
			set
			{
				this.KeyStatus["DpadRadial1"] = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x004D60AA File Offset: 0x004D42AA
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x004D60BC File Offset: 0x004D42BC
		public bool DpadRadial2
		{
			get
			{
				return this.KeyStatus["DpadRadial2"];
			}
			set
			{
				this.KeyStatus["DpadRadial2"] = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x004D60CF File Offset: 0x004D42CF
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x004D60E1 File Offset: 0x004D42E1
		public bool DpadRadial3
		{
			get
			{
				return this.KeyStatus["DpadRadial3"];
			}
			set
			{
				this.KeyStatus["DpadRadial3"] = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x004D60F4 File Offset: 0x004D42F4
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x004D6106 File Offset: 0x004D4306
		public bool DpadRadial4
		{
			get
			{
				return this.KeyStatus["DpadRadial4"];
			}
			set
			{
				this.KeyStatus["DpadRadial4"] = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x004D6119 File Offset: 0x004D4319
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x004D612B File Offset: 0x004D432B
		public bool RadialHotbar
		{
			get
			{
				return this.KeyStatus["RadialHotbar"];
			}
			set
			{
				this.KeyStatus["RadialHotbar"] = value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x004D613E File Offset: 0x004D433E
		// (set) Token: 0x06001665 RID: 5733 RVA: 0x004D6150 File Offset: 0x004D4350
		public bool RadialQuickbar
		{
			get
			{
				return this.KeyStatus["RadialQuickbar"];
			}
			set
			{
				this.KeyStatus["RadialQuickbar"] = value;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x004D6163 File Offset: 0x004D4363
		// (set) Token: 0x06001667 RID: 5735 RVA: 0x004D6175 File Offset: 0x004D4375
		public bool DpadMouseSnap1
		{
			get
			{
				return this.KeyStatus["DpadSnap1"];
			}
			set
			{
				this.KeyStatus["DpadSnap1"] = value;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x004D6188 File Offset: 0x004D4388
		// (set) Token: 0x06001669 RID: 5737 RVA: 0x004D619A File Offset: 0x004D439A
		public bool DpadMouseSnap2
		{
			get
			{
				return this.KeyStatus["DpadSnap2"];
			}
			set
			{
				this.KeyStatus["DpadSnap2"] = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x004D61AD File Offset: 0x004D43AD
		// (set) Token: 0x0600166B RID: 5739 RVA: 0x004D61BF File Offset: 0x004D43BF
		public bool DpadMouseSnap3
		{
			get
			{
				return this.KeyStatus["DpadSnap3"];
			}
			set
			{
				this.KeyStatus["DpadSnap3"] = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x004D61D2 File Offset: 0x004D43D2
		// (set) Token: 0x0600166D RID: 5741 RVA: 0x004D61E4 File Offset: 0x004D43E4
		public bool DpadMouseSnap4
		{
			get
			{
				return this.KeyStatus["DpadSnap4"];
			}
			set
			{
				this.KeyStatus["DpadSnap4"] = value;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x004D61F7 File Offset: 0x004D43F7
		// (set) Token: 0x0600166F RID: 5743 RVA: 0x004D6209 File Offset: 0x004D4409
		public bool MenuUp
		{
			get
			{
				return this.KeyStatus["MenuUp"];
			}
			set
			{
				this.KeyStatus["MenuUp"] = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x004D621C File Offset: 0x004D441C
		// (set) Token: 0x06001671 RID: 5745 RVA: 0x004D622E File Offset: 0x004D442E
		public bool MenuDown
		{
			get
			{
				return this.KeyStatus["MenuDown"];
			}
			set
			{
				this.KeyStatus["MenuDown"] = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x004D6241 File Offset: 0x004D4441
		// (set) Token: 0x06001673 RID: 5747 RVA: 0x004D6253 File Offset: 0x004D4453
		public bool MenuLeft
		{
			get
			{
				return this.KeyStatus["MenuLeft"];
			}
			set
			{
				this.KeyStatus["MenuLeft"] = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x004D6266 File Offset: 0x004D4466
		// (set) Token: 0x06001675 RID: 5749 RVA: 0x004D6278 File Offset: 0x004D4478
		public bool MenuRight
		{
			get
			{
				return this.KeyStatus["MenuRight"];
			}
			set
			{
				this.KeyStatus["MenuRight"] = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x004D628B File Offset: 0x004D448B
		// (set) Token: 0x06001677 RID: 5751 RVA: 0x004D629D File Offset: 0x004D449D
		public bool LockOn
		{
			get
			{
				return this.KeyStatus["LockOn"];
			}
			set
			{
				this.KeyStatus["LockOn"] = value;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x004D62B0 File Offset: 0x004D44B0
		// (set) Token: 0x06001679 RID: 5753 RVA: 0x004D62C2 File Offset: 0x004D44C2
		public bool ViewZoomIn
		{
			get
			{
				return this.KeyStatus["ViewZoomIn"];
			}
			set
			{
				this.KeyStatus["ViewZoomIn"] = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x004D62D5 File Offset: 0x004D44D5
		// (set) Token: 0x0600167B RID: 5755 RVA: 0x004D62E7 File Offset: 0x004D44E7
		public bool ViewZoomOut
		{
			get
			{
				return this.KeyStatus["ViewZoomOut"];
			}
			set
			{
				this.KeyStatus["ViewZoomOut"] = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x004D62FA File Offset: 0x004D44FA
		// (set) Token: 0x0600167D RID: 5757 RVA: 0x004D630C File Offset: 0x004D450C
		public bool OpenCreativePowersMenu
		{
			get
			{
				return this.KeyStatus["ToggleCreativeMenu"];
			}
			set
			{
				this.KeyStatus["ToggleCreativeMenu"] = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x004D631F File Offset: 0x004D451F
		// (set) Token: 0x0600167F RID: 5759 RVA: 0x004D6331 File Offset: 0x004D4531
		public bool ToggleCameraMode
		{
			get
			{
				return this.KeyStatus["ToggleCameraMode"];
			}
			set
			{
				this.KeyStatus["ToggleCameraMode"] = value;
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x004D6344 File Offset: 0x004D4544
		public void Reset()
		{
			string[] array = this.KeyStatus.Keys.ToArray<string>();
			for (int i = 0; i < array.Length; i++)
			{
				this.KeyStatus[array[i]] = false;
			}
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x004D6380 File Offset: 0x004D4580
		public void CloneFrom(TriggersSet other)
		{
			this.KeyStatus.Clear();
			this.LatestInputMode.Clear();
			foreach (KeyValuePair<string, bool> keyValuePair in other.KeyStatus)
			{
				this.KeyStatus.Add(keyValuePair.Key, keyValuePair.Value);
			}
			this.UsedMovementKey = other.UsedMovementKey;
			this.HotbarScrollCD = other.HotbarScrollCD;
			this.HotbarHoldTime = other.HotbarHoldTime;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x004D6420 File Offset: 0x004D4620
		public void SetupKeys()
		{
			this.KeyStatus.Clear();
			foreach (string text in PlayerInput.KnownTriggers)
			{
				this.KeyStatus.Add(text, false);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x004D6484 File Offset: 0x004D4684
		public Vector2 DirectionsRaw
		{
			get
			{
				return new Vector2((float)(this.Right.ToInt() - this.Left.ToInt()), (float)(this.Down.ToInt() - this.Up.ToInt()));
			}
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x004D64BC File Offset: 0x004D46BC
		public Vector2 GetNavigatorDirections()
		{
			bool flag = Main.gameMenu || Main.ingameOptionsWindow || Main.editChest || Main.editSign || ((Main.playerInventory || Main.LocalPlayer.talkNPC != -1) && PlayerInput.CurrentProfile.UsingDpadMovekeys());
			bool flag2 = this.Up || (flag && this.MenuUp);
			bool flag3 = this.Right || (flag && this.MenuRight);
			bool flag4 = this.Down || (flag && this.MenuDown);
			bool flag5 = this.Left || (flag && this.MenuLeft);
			return new Vector2((float)(flag3.ToInt() - flag5.ToInt()), (float)(flag4.ToInt() - flag2.ToInt()));
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x004D658C File Offset: 0x004D478C
		public void CopyInto(Player p)
		{
			if (PlayerInput.CurrentInputMode != InputMode.XBoxGamepadUI && !PlayerInput.CursorIsBusy)
			{
				p.controlUp = this.Up;
				p.controlDown = this.Down;
				p.controlLeft = this.Left;
				p.controlRight = this.Right;
				p.controlJump = this.Jump;
				p.controlHook = this.Grapple;
				if (!p.mouseInterface)
				{
					p.controlTorch = this.SmartSelect;
				}
				p.controlSmart = this.SmartCursor;
				p.controlMount = this.QuickMount;
				p.controlQuickHeal = this.QuickHeal;
				p.controlQuickMana = this.QuickMana;
				p.controlCreativeMenu = this.OpenCreativePowersMenu;
				if (this.QuickBuff)
				{
					p.QuickBuff();
				}
				if (Utils.JustBecameTrue(this.Loadout1, ref p.releaseLoadout1))
				{
					p.TrySwitchingLoadout(0);
				}
				if (Utils.JustBecameTrue(this.Loadout2, ref p.releaseLoadout2))
				{
					p.TrySwitchingLoadout(1);
				}
				if (Utils.JustBecameTrue(this.Loadout3, ref p.releaseLoadout3))
				{
					p.TrySwitchingLoadout(2);
				}
				if (Utils.JustBecameTrue(this.NextLoadout, ref p.releaseNextLoadout))
				{
					p.TrySwitchingToNextLoadout();
				}
				if (Utils.JustBecameTrue(this.PreviousLoadout, ref p.releasePreviousLoadout))
				{
					p.TrySwitchingToPreviousLoadout();
				}
				p.controlDash = this.Dash;
				if (!p.controlDash)
				{
					p.releaseDash = true;
				}
				p.controlArmorSetAbility = this.ArmorSetAbility;
			}
			p.controlInv = this.Inventory;
			p.controlThrow = this.Throw;
			p.mapZoomIn = this.MapZoomIn;
			p.mapZoomOut = this.MapZoomOut;
			p.mapAlphaUp = this.MapAlphaUp;
			p.mapAlphaDown = this.MapAlphaDown;
			p.mapFullScreen = this.MapFull;
			p.mapStyle = this.MapStyle;
			if (this.MouseLeft)
			{
				if (!Main.blockMouse && !p.mouseInterface)
				{
					p.controlUseItem = true;
				}
			}
			else
			{
				Main.blockMouse = false;
			}
			if (!Main.playerInventory && Main.player[Main.myPlayer].stressBall && Main.player[Main.myPlayer].CanUseStressBall() && !this.MouseLeft && !Main.blockMouse && !p.mouseInterface)
			{
				p.controlUseItem = true;
			}
			if (!this.MouseRight && !Main.playerInventory)
			{
				PlayerInput.LockGamepadTileUseButton = false;
			}
			if (this.MouseRight && !p.mouseInterface && !Main.blockMouse && !this.ShouldLockTileUsage() && !PlayerInput.InBuildingMode)
			{
				p.controlUseTile = true;
			}
			if (PlayerInput.InBuildingMode && this.MouseRight)
			{
				p.controlInv = true;
			}
			InputMode inputMode;
			if (this.SmartSelect && this.LatestInputMode.TryGetValue("SmartSelect", out inputMode) && this.IsInputFromGamepad(inputMode))
			{
				PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Gamepad);
			}
			bool flag = PlayerInput.Triggers.Current.HotbarPlus || PlayerInput.Triggers.Current.HotbarMinus;
			if (flag)
			{
				this.HotbarHoldTime++;
			}
			else
			{
				this.HotbarHoldTime = 0;
			}
			if (this.HotbarScrollCD > 0 && (this.HotbarScrollCD != 1 || !flag || PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired <= 0))
			{
				this.HotbarScrollCD--;
			}
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x004D68BC File Offset: 0x004D4ABC
		public void CopyIntoDuringChat(Player p)
		{
			if (this.MouseLeft)
			{
				if (!Main.blockMouse && !p.mouseInterface)
				{
					p.controlUseItem = true;
				}
			}
			else
			{
				Main.blockMouse = false;
			}
			if (!this.MouseRight && !Main.playerInventory)
			{
				PlayerInput.LockGamepadTileUseButton = false;
			}
			if (this.MouseRight && !p.mouseInterface && !Main.blockMouse && !this.ShouldLockTileUsage() && !PlayerInput.InBuildingMode)
			{
				p.controlUseTile = true;
			}
			bool flag = PlayerInput.Triggers.Current.HotbarPlus || PlayerInput.Triggers.Current.HotbarMinus;
			if (flag)
			{
				this.HotbarHoldTime++;
			}
			else
			{
				this.HotbarHoldTime = 0;
			}
			if (this.HotbarScrollCD > 0 && (this.HotbarScrollCD != 1 || !flag || PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired <= 0))
			{
				this.HotbarScrollCD--;
			}
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x004D69A0 File Offset: 0x004D4BA0
		private bool ShouldLockTileUsage()
		{
			return PlayerInput.LockGamepadTileUseButton && PlayerInput.UsingGamepad;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x004D69B0 File Offset: 0x004D4BB0
		private bool IsInputFromGamepad(InputMode mode)
		{
			return mode > InputMode.Mouse;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x004D69B9 File Offset: 0x004D4BB9
		public TriggersSet()
		{
		}

		// Token: 0x04001176 RID: 4470
		public Dictionary<string, bool> KeyStatus = new Dictionary<string, bool>();

		// Token: 0x04001177 RID: 4471
		public Dictionary<string, InputMode> LatestInputMode = new Dictionary<string, InputMode>();

		// Token: 0x04001178 RID: 4472
		public bool UsedMovementKey = true;

		// Token: 0x04001179 RID: 4473
		public int HotbarScrollCD;

		// Token: 0x0400117A RID: 4474
		public int HotbarHoldTime;
	}
}
