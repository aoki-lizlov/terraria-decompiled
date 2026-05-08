using System;

namespace Terraria.ID
{
	// Token: 0x02000192 RID: 402
	public static class ImmunityCooldownID
	{
		// Token: 0x06001EFB RID: 7931 RVA: 0x005121B5 File Offset: 0x005103B5
		// Note: this type is marked as 'beforefieldinit'.
		static ImmunityCooldownID()
		{
		}

		// Token: 0x04001808 RID: 6152
		public static readonly int General = -1;

		// Token: 0x04001809 RID: 6153
		public static readonly int TileContactDamage = 0;

		// Token: 0x0400180A RID: 6154
		public static readonly int BossNoCheese = 1;

		// Token: 0x0400180B RID: 6155
		public static readonly int LegacyUnused2 = 2;

		// Token: 0x0400180C RID: 6156
		public static readonly int WrongBugNet = 3;

		// Token: 0x0400180D RID: 6157
		public static readonly int Lava = 4;

		// Token: 0x0400180E RID: 6158
		public static readonly int PaladinsShield = 5;

		// Token: 0x0400180F RID: 6159
		public static readonly int Count = 6;

		// Token: 0x0200075C RID: 1884
		public static class Sets
		{
			// Token: 0x06004101 RID: 16641 RVA: 0x0069F0B8 File Offset: 0x0069D2B8
			public static ImmunityCooldownID.Sets.BoolSet CreateBoolSet(params int[] types)
			{
				ImmunityCooldownID.Sets.BoolSet boolSet = new ImmunityCooldownID.Sets.BoolSet(ImmunityCooldownID.Count);
				foreach (int num in types)
				{
					boolSet[num] = true;
				}
				return boolSet;
			}

			// Token: 0x06004102 RID: 16642 RVA: 0x0069F0F0 File Offset: 0x0069D2F0
			// Note: this type is marked as 'beforefieldinit'.
			static Sets()
			{
			}

			// Token: 0x04006A01 RID: 27137
			public static ImmunityCooldownID.Sets.BoolSet Retaliate = ImmunityCooldownID.Sets.CreateBoolSet(new int[]
			{
				ImmunityCooldownID.General,
				ImmunityCooldownID.BossNoCheese,
				ImmunityCooldownID.PaladinsShield
			});

			// Token: 0x04006A02 RID: 27138
			public static ImmunityCooldownID.Sets.BoolSet Counter = ImmunityCooldownID.Sets.CreateBoolSet(new int[]
			{
				ImmunityCooldownID.General,
				ImmunityCooldownID.BossNoCheese
			});

			// Token: 0x04006A03 RID: 27139
			public static ImmunityCooldownID.Sets.BoolSet TeamDamageShare = ImmunityCooldownID.Sets.CreateBoolSet(new int[]
			{
				ImmunityCooldownID.General,
				ImmunityCooldownID.BossNoCheese
			});

			// Token: 0x04006A04 RID: 27140
			public static ImmunityCooldownID.Sets.BoolSet ImmuneTimerOnlyLimitsEffects = ImmunityCooldownID.Sets.CreateBoolSet(new int[] { ImmunityCooldownID.PaladinsShield });

			// Token: 0x02000A90 RID: 2704
			public struct BoolSet
			{
				// Token: 0x170005BE RID: 1470
				public bool this[int idx]
				{
					get
					{
						return this._arr[idx + 1];
					}
					set
					{
						this._arr[idx + 1] = value;
					}
				}

				// Token: 0x06004BD1 RID: 19409 RVA: 0x006D95A6 File Offset: 0x006D77A6
				public BoolSet(int count)
				{
					this._arr = new bool[count + 1];
				}

				// Token: 0x04007780 RID: 30592
				private readonly bool[] _arr;
			}
		}
	}
}
