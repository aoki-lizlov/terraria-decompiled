using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020009F3 RID: 2547
	[ComVisible(true)]
	[Serializable]
	public class ThaiBuddhistCalendar : Calendar
	{
		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06005E6C RID: 24172 RVA: 0x0013B77A File Offset: 0x0013997A
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06005E6D RID: 24173 RVA: 0x0013B781 File Offset: 0x00139981
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06005E6E RID: 24174 RVA: 0x00003FB7 File Offset: 0x000021B7
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x06005E6F RID: 24175 RVA: 0x0014313A File Offset: 0x0014133A
		public ThaiBuddhistCalendar()
		{
			this.helper = new GregorianCalendarHelper(this, ThaiBuddhistCalendar.thaiBuddhistEraInfo);
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06005E70 RID: 24176 RVA: 0x00029C12 File Offset: 0x00027E12
		internal override int ID
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x00143153 File Offset: 0x00141353
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this.helper.AddMonths(time, months);
		}

		// Token: 0x06005E72 RID: 24178 RVA: 0x00143162 File Offset: 0x00141362
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.helper.AddYears(time, years);
		}

		// Token: 0x06005E73 RID: 24179 RVA: 0x00143171 File Offset: 0x00141371
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this.helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x06005E74 RID: 24180 RVA: 0x00143181 File Offset: 0x00141381
		public override int GetDaysInYear(int year, int era)
		{
			return this.helper.GetDaysInYear(year, era);
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x00143190 File Offset: 0x00141390
		public override int GetDayOfMonth(DateTime time)
		{
			return this.helper.GetDayOfMonth(time);
		}

		// Token: 0x06005E76 RID: 24182 RVA: 0x0014319E File Offset: 0x0014139E
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this.helper.GetDayOfWeek(time);
		}

		// Token: 0x06005E77 RID: 24183 RVA: 0x001431AC File Offset: 0x001413AC
		public override int GetDayOfYear(DateTime time)
		{
			return this.helper.GetDayOfYear(time);
		}

		// Token: 0x06005E78 RID: 24184 RVA: 0x001431BA File Offset: 0x001413BA
		public override int GetMonthsInYear(int year, int era)
		{
			return this.helper.GetMonthsInYear(year, era);
		}

		// Token: 0x06005E79 RID: 24185 RVA: 0x001431C9 File Offset: 0x001413C9
		[ComVisible(false)]
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this.helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06005E7A RID: 24186 RVA: 0x001431D9 File Offset: 0x001413D9
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x06005E7B RID: 24187 RVA: 0x001431E7 File Offset: 0x001413E7
		public override int GetMonth(DateTime time)
		{
			return this.helper.GetMonth(time);
		}

		// Token: 0x06005E7C RID: 24188 RVA: 0x001431F5 File Offset: 0x001413F5
		public override int GetYear(DateTime time)
		{
			return this.helper.GetYear(time);
		}

		// Token: 0x06005E7D RID: 24189 RVA: 0x00143203 File Offset: 0x00141403
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this.helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x06005E7E RID: 24190 RVA: 0x00143215 File Offset: 0x00141415
		public override bool IsLeapYear(int year, int era)
		{
			return this.helper.IsLeapYear(year, era);
		}

		// Token: 0x06005E7F RID: 24191 RVA: 0x00143224 File Offset: 0x00141424
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			return this.helper.GetLeapMonth(year, era);
		}

		// Token: 0x06005E80 RID: 24192 RVA: 0x00143233 File Offset: 0x00141433
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this.helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x06005E81 RID: 24193 RVA: 0x00143244 File Offset: 0x00141444
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this.helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06005E82 RID: 24194 RVA: 0x00143269 File Offset: 0x00141469
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06005E83 RID: 24195 RVA: 0x00143276 File Offset: 0x00141476
		// (set) Token: 0x06005E84 RID: 24196 RVA: 0x001432A0 File Offset: 0x001414A0
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 2572);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.helper.MaxYear)
				{
					throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 99, this.helper.MaxYear));
				}
				this.twoDigitYearMax = value;
			}
		}

		// Token: 0x06005E85 RID: 24197 RVA: 0x00143303 File Offset: 0x00141503
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("Non-negative number required."));
			}
			return this.helper.ToFourDigitYear(year, this.TwoDigitYearMax);
		}

		// Token: 0x06005E86 RID: 24198 RVA: 0x00143330 File Offset: 0x00141530
		// Note: this type is marked as 'beforefieldinit'.
		static ThaiBuddhistCalendar()
		{
		}

		// Token: 0x040038E3 RID: 14563
		internal static EraInfo[] thaiBuddhistEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1, 1, 1, -543, 544, 10542)
		};

		// Token: 0x040038E4 RID: 14564
		public const int ThaiBuddhistEra = 1;

		// Token: 0x040038E5 RID: 14565
		internal GregorianCalendarHelper helper;

		// Token: 0x040038E6 RID: 14566
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 2572;
	}
}
