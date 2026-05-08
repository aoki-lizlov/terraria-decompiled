using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x020000D0 RID: 208
	[Obsolete("System.CurrentSystemTimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo.Local instead.")]
	[Serializable]
	internal class CurrentSystemTimeZone : TimeZone
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x0001CEC0 File Offset: 0x0001B0C0
		internal CurrentSystemTimeZone()
		{
			TimeZoneInfo local = TimeZoneInfo.Local;
			this.m_ticksOffset = local.BaseUtcOffset.Ticks;
			this.m_standardName = local.StandardName;
			this.m_daylightName = local.DaylightName;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0001CF10 File Offset: 0x0001B110
		public override string StandardName
		{
			get
			{
				return this.m_standardName;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001CF18 File Offset: 0x0001B118
		public override string DaylightName
		{
			get
			{
				return this.m_daylightName;
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001CF20 File Offset: 0x0001B120
		internal long GetUtcOffsetFromUniversalTime(DateTime time, ref bool isAmbiguousLocalDst)
		{
			TimeSpan timeSpan = new TimeSpan(this.m_ticksOffset);
			DaylightTime daylightChanges = this.GetDaylightChanges(time.Year);
			isAmbiguousLocalDst = false;
			if (daylightChanges == null || daylightChanges.Delta.Ticks == 0L)
			{
				return timeSpan.Ticks;
			}
			DateTime dateTime = daylightChanges.Start - timeSpan;
			DateTime dateTime2 = daylightChanges.End - timeSpan - daylightChanges.Delta;
			DateTime dateTime3;
			DateTime dateTime4;
			if (daylightChanges.Delta.Ticks > 0L)
			{
				dateTime3 = dateTime2 - daylightChanges.Delta;
				dateTime4 = dateTime2;
			}
			else
			{
				dateTime3 = dateTime;
				dateTime4 = dateTime - daylightChanges.Delta;
			}
			bool flag;
			if (dateTime > dateTime2)
			{
				flag = time < dateTime2 || time >= dateTime;
			}
			else
			{
				flag = time >= dateTime && time < dateTime2;
			}
			if (flag)
			{
				timeSpan += daylightChanges.Delta;
				if (time >= dateTime3 && time < dateTime4)
				{
					isAmbiguousLocalDst = true;
				}
			}
			return timeSpan.Ticks;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001D02C File Offset: 0x0001B22C
		public override DateTime ToLocalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Local)
			{
				return time;
			}
			bool flag = false;
			long utcOffsetFromUniversalTime = this.GetUtcOffsetFromUniversalTime(time, ref flag);
			long num = time.Ticks + utcOffsetFromUniversalTime;
			if (num > 3155378975999999999L)
			{
				return new DateTime(3155378975999999999L, DateTimeKind.Local);
			}
			if (num < 0L)
			{
				return new DateTime(0L, DateTimeKind.Local);
			}
			return new DateTime(num, DateTimeKind.Local, flag);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001D08D File Offset: 0x0001B28D
		public override DaylightTime GetDaylightChanges(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", SR.Format("Valid values are between {0} and {1}, inclusive.", 1, 9999));
			}
			return this.GetCachedDaylightChanges(year);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001D0C8 File Offset: 0x0001B2C8
		private static DaylightTime CreateDaylightChanges(int year)
		{
			DaylightTime daylightTime = null;
			if (TimeZoneInfo.Local.SupportsDaylightSavingTime)
			{
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in TimeZoneInfo.Local.GetAdjustmentRules())
				{
					if (adjustmentRule.DateStart.Year <= year && adjustmentRule.DateEnd.Year >= year && adjustmentRule.DaylightDelta != TimeSpan.Zero)
					{
						DateTime dateTime = TimeZoneInfo.TransitionTimeToDateTime(year, adjustmentRule.DaylightTransitionStart);
						DateTime dateTime2 = TimeZoneInfo.TransitionTimeToDateTime(year, adjustmentRule.DaylightTransitionEnd);
						TimeSpan daylightDelta = adjustmentRule.DaylightDelta;
						daylightTime = new DaylightTime(dateTime, dateTime2, daylightDelta);
						break;
					}
				}
			}
			if (daylightTime == null)
			{
				daylightTime = new DaylightTime(DateTime.MinValue, DateTime.MinValue, TimeSpan.Zero);
			}
			return daylightTime;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001D188 File Offset: 0x0001B388
		public override TimeSpan GetUtcOffset(DateTime time)
		{
			if (time.Kind == DateTimeKind.Utc)
			{
				return TimeSpan.Zero;
			}
			return new TimeSpan(TimeZone.CalculateUtcOffset(time, this.GetDaylightChanges(time.Year)).Ticks + this.m_ticksOffset);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001D1CC File Offset: 0x0001B3CC
		private DaylightTime GetCachedDaylightChanges(int year)
		{
			object obj = year;
			if (!this.m_CachedDaylightChanges.Contains(obj))
			{
				DaylightTime daylightTime = CurrentSystemTimeZone.CreateDaylightChanges(year);
				Hashtable cachedDaylightChanges = this.m_CachedDaylightChanges;
				lock (cachedDaylightChanges)
				{
					if (!this.m_CachedDaylightChanges.Contains(obj))
					{
						this.m_CachedDaylightChanges.Add(obj, daylightTime);
					}
				}
			}
			return (DaylightTime)this.m_CachedDaylightChanges[obj];
		}

		// Token: 0x04000F11 RID: 3857
		private long m_ticksOffset;

		// Token: 0x04000F12 RID: 3858
		private string m_standardName;

		// Token: 0x04000F13 RID: 3859
		private string m_daylightName;

		// Token: 0x04000F14 RID: 3860
		private readonly Hashtable m_CachedDaylightChanges = new Hashtable();
	}
}
