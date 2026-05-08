using System;
using System.Collections.Generic;

namespace Terraria.ID
{
	// Token: 0x020001A7 RID: 423
	public static class AmmoID
	{
		// Token: 0x06001F20 RID: 7968 RVA: 0x00513EAC File Offset: 0x005120AC
		// Note: this type is marked as 'beforefieldinit'.
		static AmmoID()
		{
		}

		// Token: 0x04001998 RID: 6552
		public static int None = 0;

		// Token: 0x04001999 RID: 6553
		public static int Gel = 23;

		// Token: 0x0400199A RID: 6554
		public static int Arrow = 40;

		// Token: 0x0400199B RID: 6555
		public static int Coin = 71;

		// Token: 0x0400199C RID: 6556
		public static int FallenStar = 75;

		// Token: 0x0400199D RID: 6557
		public static int Bullet = 97;

		// Token: 0x0400199E RID: 6558
		public static int Sand = 169;

		// Token: 0x0400199F RID: 6559
		public static int Dart = 283;

		// Token: 0x040019A0 RID: 6560
		public static int Rocket = 771;

		// Token: 0x040019A1 RID: 6561
		public static int Solution = 780;

		// Token: 0x040019A2 RID: 6562
		public static int Flare = 931;

		// Token: 0x040019A3 RID: 6563
		public static int Snowball = 949;

		// Token: 0x040019A4 RID: 6564
		public static int StyngerBolt = 1261;

		// Token: 0x040019A5 RID: 6565
		public static int CandyCorn = 1783;

		// Token: 0x040019A6 RID: 6566
		public static int JackOLantern = 1785;

		// Token: 0x040019A7 RID: 6567
		public static int Stake = 1836;

		// Token: 0x040019A8 RID: 6568
		public static int NailFriendly = 3108;

		// Token: 0x040019A9 RID: 6569
		public static int Acorn = 27;

		// Token: 0x0200076A RID: 1898
		public class Sets
		{
			// Token: 0x06004120 RID: 16672 RVA: 0x0000357B File Offset: 0x0000177B
			public Sets()
			{
			}

			// Token: 0x06004121 RID: 16673 RVA: 0x006A0F48 File Offset: 0x0069F148
			// Note: this type is marked as 'beforefieldinit'.
			static Sets()
			{
			}

			// Token: 0x04006A1A RID: 27162
			public static SetFactory Factory = new SetFactory((int)ItemID.Count);

			// Token: 0x04006A1B RID: 27163
			public static Dictionary<int, Dictionary<int, int>> SpecificLauncherAmmoProjectileMatches = new Dictionary<int, Dictionary<int, int>>
			{
				{
					759,
					new Dictionary<int, int>
					{
						{ 771, 134 },
						{ 772, 137 },
						{ 773, 140 },
						{ 774, 143 },
						{ 4445, 776 },
						{ 4446, 780 },
						{ 4457, 793 },
						{ 4458, 796 },
						{ 4459, 799 },
						{ 4447, 784 },
						{ 4448, 787 },
						{ 4449, 790 }
					}
				},
				{
					758,
					new Dictionary<int, int>
					{
						{ 771, 133 },
						{ 772, 136 },
						{ 773, 139 },
						{ 774, 142 },
						{ 4445, 777 },
						{ 4446, 781 },
						{ 4457, 794 },
						{ 4458, 797 },
						{ 4459, 800 },
						{ 4447, 785 },
						{ 4448, 788 },
						{ 4449, 791 }
					}
				},
				{
					760,
					new Dictionary<int, int>
					{
						{ 771, 135 },
						{ 772, 138 },
						{ 773, 141 },
						{ 774, 144 },
						{ 4445, 778 },
						{ 4446, 782 },
						{ 4457, 795 },
						{ 4458, 798 },
						{ 4459, 801 },
						{ 4447, 786 },
						{ 4448, 789 },
						{ 4449, 792 }
					}
				},
				{
					1946,
					new Dictionary<int, int>
					{
						{ 771, 338 },
						{ 772, 339 },
						{ 773, 340 },
						{ 774, 341 },
						{ 4445, 803 },
						{ 4446, 804 },
						{ 4457, 808 },
						{ 4458, 809 },
						{ 4459, 810 },
						{ 4447, 805 },
						{ 4448, 806 },
						{ 4449, 807 }
					}
				},
				{
					3930,
					new Dictionary<int, int>
					{
						{ 771, 715 },
						{ 772, 716 },
						{ 773, 717 },
						{ 774, 718 },
						{ 4445, 717 },
						{ 4446, 718 },
						{ 4457, 717 },
						{ 4458, 718 },
						{ 4459, 717 },
						{ 4447, 717 },
						{ 4448, 717 },
						{ 4449, 717 }
					}
				}
			};

			// Token: 0x04006A1C RID: 27164
			public static bool[] IsArrow = AmmoID.Sets.Factory.CreateBoolSet(new int[]
			{
				AmmoID.Arrow,
				AmmoID.Stake
			});

			// Token: 0x04006A1D RID: 27165
			public static bool[] IsBullet = AmmoID.Sets.Factory.CreateBoolSet(new int[]
			{
				AmmoID.Bullet,
				AmmoID.CandyCorn
			});

			// Token: 0x04006A1E RID: 27166
			public static bool[] IsSpecialist = AmmoID.Sets.Factory.CreateBoolSet(new int[]
			{
				AmmoID.Rocket,
				AmmoID.StyngerBolt,
				AmmoID.JackOLantern,
				AmmoID.NailFriendly,
				AmmoID.Coin,
				AmmoID.Flare,
				AmmoID.Dart,
				AmmoID.Snowball,
				AmmoID.FallenStar,
				AmmoID.Gel
			});
		}
	}
}
