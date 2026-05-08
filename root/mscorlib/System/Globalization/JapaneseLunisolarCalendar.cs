using System;

namespace System.Globalization
{
	// Token: 0x020009E9 RID: 2537
	[Serializable]
	public class JapaneseLunisolarCalendar : EastAsianLunisolarCalendar
	{
		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06005D47 RID: 23879 RVA: 0x001400C7 File Offset: 0x0013E2C7
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JapaneseLunisolarCalendar.minDate;
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06005D48 RID: 23880 RVA: 0x001400CE File Offset: 0x0013E2CE
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return JapaneseLunisolarCalendar.maxDate;
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06005D49 RID: 23881 RVA: 0x0013F461 File Offset: 0x0013D661
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 354;
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06005D4A RID: 23882 RVA: 0x001400D5 File Offset: 0x0013E2D5
		internal override int MinCalendarYear
		{
			get
			{
				return 1960;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06005D4B RID: 23883 RVA: 0x001400DC File Offset: 0x0013E2DC
		internal override int MaxCalendarYear
		{
			get
			{
				return 2049;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06005D4C RID: 23884 RVA: 0x001400C7 File Offset: 0x0013E2C7
		internal override DateTime MinDate
		{
			get
			{
				return JapaneseLunisolarCalendar.minDate;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06005D4D RID: 23885 RVA: 0x001400CE File Offset: 0x0013E2CE
		internal override DateTime MaxDate
		{
			get
			{
				return JapaneseLunisolarCalendar.maxDate;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06005D4E RID: 23886 RVA: 0x001400E3 File Offset: 0x0013E2E3
		internal override EraInfo[] CalEraInfo
		{
			get
			{
				return JapaneseCalendar.GetEraInfo();
			}
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x001400EC File Offset: 0x0013E2EC
		internal override int GetYearInfo(int LunarYear, int Index)
		{
			if (LunarYear < 1960 || LunarYear > 2049)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 1960, 2049));
			}
			return JapaneseLunisolarCalendar.yinfo[LunarYear - 1960, Index];
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x0014014E File Offset: 0x0013E34E
		internal override int GetYear(int year, DateTime time)
		{
			return this.helper.GetYear(year, time);
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x0014015D File Offset: 0x0013E35D
		internal override int GetGregorianYear(int year, int era)
		{
			return this.helper.GetGregorianYear(year, era);
		}

		// Token: 0x06005D52 RID: 23890 RVA: 0x0014016C File Offset: 0x0013E36C
		private static EraInfo[] TrimEras(EraInfo[] baseEras)
		{
			EraInfo[] array = new EraInfo[baseEras.Length];
			int num = 0;
			for (int i = 0; i < baseEras.Length; i++)
			{
				if (baseEras[i].yearOffset + baseEras[i].minEraYear < 2049)
				{
					if (baseEras[i].yearOffset + baseEras[i].maxEraYear < 1960)
					{
						break;
					}
					array[num] = baseEras[i];
					num++;
				}
			}
			if (num == 0)
			{
				return baseEras;
			}
			Array.Resize<EraInfo>(ref array, num);
			return array;
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x001401DA File Offset: 0x0013E3DA
		public JapaneseLunisolarCalendar()
		{
			this.helper = new GregorianCalendarHelper(this, JapaneseLunisolarCalendar.TrimEras(JapaneseCalendar.GetEraInfo()));
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x001401F8 File Offset: 0x0013E3F8
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06005D55 RID: 23893 RVA: 0x00019B62 File Offset: 0x00017D62
		internal override int BaseCalendarID
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06005D56 RID: 23894 RVA: 0x00020036 File Offset: 0x0001E236
		internal override int ID
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06005D57 RID: 23895 RVA: 0x00140206 File Offset: 0x0013E406
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x00140214 File Offset: 0x0013E414
		// Note: this type is marked as 'beforefieldinit'.
		static JapaneseLunisolarCalendar()
		{
		}

		// Token: 0x0400386F RID: 14447
		public const int JapaneseEra = 1;

		// Token: 0x04003870 RID: 14448
		internal GregorianCalendarHelper helper;

		// Token: 0x04003871 RID: 14449
		internal const int MIN_LUNISOLAR_YEAR = 1960;

		// Token: 0x04003872 RID: 14450
		internal const int MAX_LUNISOLAR_YEAR = 2049;

		// Token: 0x04003873 RID: 14451
		internal const int MIN_GREGORIAN_YEAR = 1960;

		// Token: 0x04003874 RID: 14452
		internal const int MIN_GREGORIAN_MONTH = 1;

		// Token: 0x04003875 RID: 14453
		internal const int MIN_GREGORIAN_DAY = 28;

		// Token: 0x04003876 RID: 14454
		internal const int MAX_GREGORIAN_YEAR = 2050;

		// Token: 0x04003877 RID: 14455
		internal const int MAX_GREGORIAN_MONTH = 1;

		// Token: 0x04003878 RID: 14456
		internal const int MAX_GREGORIAN_DAY = 22;

		// Token: 0x04003879 RID: 14457
		internal static DateTime minDate = new DateTime(1960, 1, 28);

		// Token: 0x0400387A RID: 14458
		internal static DateTime maxDate = new DateTime(new DateTime(2050, 1, 22, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x0400387B RID: 14459
		private static readonly int[,] yinfo = new int[,]
		{
			{ 6, 1, 28, 44368 },
			{ 0, 2, 15, 43856 },
			{ 0, 2, 5, 19808 },
			{ 4, 1, 25, 42352 },
			{ 0, 2, 13, 42352 },
			{ 0, 2, 2, 21104 },
			{ 3, 1, 22, 26928 },
			{ 0, 2, 9, 55632 },
			{ 7, 1, 30, 27304 },
			{ 0, 2, 17, 22176 },
			{ 0, 2, 6, 39632 },
			{ 5, 1, 27, 19176 },
			{ 0, 2, 15, 19168 },
			{ 0, 2, 3, 42208 },
			{ 4, 1, 23, 53864 },
			{ 0, 2, 11, 53840 },
			{ 8, 1, 31, 54600 },
			{ 0, 2, 18, 46400 },
			{ 0, 2, 7, 54944 },
			{ 6, 1, 28, 38608 },
			{ 0, 2, 16, 38320 },
			{ 0, 2, 5, 18864 },
			{ 4, 1, 25, 42200 },
			{ 0, 2, 13, 42160 },
			{ 10, 2, 2, 45656 },
			{ 0, 2, 20, 27216 },
			{ 0, 2, 9, 27968 },
			{ 6, 1, 29, 46504 },
			{ 0, 2, 18, 11104 },
			{ 0, 2, 6, 38320 },
			{ 5, 1, 27, 18872 },
			{ 0, 2, 15, 18800 },
			{ 0, 2, 4, 25776 },
			{ 3, 1, 23, 27216 },
			{ 0, 2, 10, 59984 },
			{ 8, 1, 31, 27976 },
			{ 0, 2, 19, 23248 },
			{ 0, 2, 8, 11104 },
			{ 5, 1, 28, 37744 },
			{ 0, 2, 16, 37600 },
			{ 0, 2, 5, 51552 },
			{ 4, 1, 24, 58536 },
			{ 0, 2, 12, 54432 },
			{ 0, 2, 1, 55888 },
			{ 2, 1, 22, 23208 },
			{ 0, 2, 9, 22208 },
			{ 7, 1, 29, 43736 },
			{ 0, 2, 18, 9680 },
			{ 0, 2, 7, 37584 },
			{ 5, 1, 26, 51544 },
			{ 0, 2, 14, 43344 },
			{ 0, 2, 3, 46240 },
			{ 3, 1, 23, 47696 },
			{ 0, 2, 10, 46416 },
			{ 9, 1, 31, 21928 },
			{ 0, 2, 19, 19360 },
			{ 0, 2, 8, 42416 },
			{ 5, 1, 28, 21176 },
			{ 0, 2, 16, 21168 },
			{ 0, 2, 5, 43344 },
			{ 4, 1, 25, 46248 },
			{ 0, 2, 12, 27296 },
			{ 0, 2, 1, 44368 },
			{ 2, 1, 22, 21928 },
			{ 0, 2, 10, 19296 },
			{ 6, 1, 29, 42352 },
			{ 0, 2, 17, 42352 },
			{ 0, 2, 7, 21104 },
			{ 5, 1, 27, 26928 },
			{ 0, 2, 13, 55600 },
			{ 0, 2, 3, 23200 },
			{ 3, 1, 23, 43856 },
			{ 0, 2, 11, 38608 },
			{ 11, 1, 31, 19176 },
			{ 0, 2, 19, 19168 },
			{ 0, 2, 8, 42192 },
			{ 6, 1, 28, 53864 },
			{ 0, 2, 15, 53840 },
			{ 0, 2, 4, 54560 },
			{ 5, 1, 24, 55968 },
			{ 0, 2, 12, 46752 },
			{ 0, 2, 1, 38608 },
			{ 2, 1, 22, 19160 },
			{ 0, 2, 10, 18864 },
			{ 7, 1, 30, 42168 },
			{ 0, 2, 17, 42160 },
			{ 0, 2, 6, 45648 },
			{ 5, 1, 26, 46376 },
			{ 0, 2, 14, 27968 },
			{ 0, 2, 2, 44448 }
		};
	}
}
