using System;
using System.Globalization;
using System.Threading;

namespace System
{
	// Token: 0x0200015D RID: 349
	[Obsolete("System.TimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo instead.")]
	[Serializable]
	public abstract class TimeZone
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0003DEB8 File Offset: 0x0003C0B8
		private static object InternalSyncObject
		{
			get
			{
				if (TimeZone.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref TimeZone.s_InternalSyncObject, obj, null);
				}
				return TimeZone.s_InternalSyncObject;
			}
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x000025BE File Offset: 0x000007BE
		protected TimeZone()
		{
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0003DEE4 File Offset: 0x0003C0E4
		public static TimeZone CurrentTimeZone
		{
			get
			{
				TimeZone timeZone = TimeZone.currentTimeZone;
				if (timeZone == null)
				{
					object internalSyncObject = TimeZone.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (TimeZone.currentTimeZone == null)
						{
							TimeZone.currentTimeZone = new CurrentSystemTimeZone();
						}
						timeZone = TimeZone.currentTimeZone;
					}
				}
				return timeZone;
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0003DF48 File Offset: 0x0003C148
		internal static void ResetTimeZone()
		{
			if (TimeZone.currentTimeZone != null)
			{
				object internalSyncObject = TimeZone.InternalSyncObject;
				lock (internalSyncObject)
				{
					TimeZone.currentTimeZone = null;
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000F50 RID: 3920
		public abstract string StandardName { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000F51 RID: 3921
		public abstract string DaylightName { get; }

		// Token: 0x06000F52 RID: 3922
		public abstract TimeSpan GetUtcOffset(DateTime time);

		// Token: 0x06000F53 RID: 3923 RVA: 0x0003DF94 File Offset: 0x0003C194
		public virtual DateTime ToUniversalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Utc)
			{
				return time;
			}
			long num = time.Ticks - this.GetUtcOffset(time).Ticks;
			if (num > 3155378975999999999L)
			{
				return new DateTime(3155378975999999999L, DateTimeKind.Utc);
			}
			if (num < 0L)
			{
				return new DateTime(0L, DateTimeKind.Utc);
			}
			return new DateTime(num, DateTimeKind.Utc);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0003DFF8 File Offset: 0x0003C1F8
		public virtual DateTime ToLocalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Local)
			{
				return time;
			}
			bool flag = false;
			long utcOffsetFromUniversalTime = ((CurrentSystemTimeZone)TimeZone.CurrentTimeZone).GetUtcOffsetFromUniversalTime(time, ref flag);
			return new DateTime(time.Ticks + utcOffsetFromUniversalTime, DateTimeKind.Local, flag);
		}

		// Token: 0x06000F55 RID: 3925
		public abstract DaylightTime GetDaylightChanges(int year);

		// Token: 0x06000F56 RID: 3926 RVA: 0x0003E036 File Offset: 0x0003C236
		public virtual bool IsDaylightSavingTime(DateTime time)
		{
			return TimeZone.IsDaylightSavingTime(time, this.GetDaylightChanges(time.Year));
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0003E04B File Offset: 0x0003C24B
		public static bool IsDaylightSavingTime(DateTime time, DaylightTime daylightTimes)
		{
			return TimeZone.CalculateUtcOffset(time, daylightTimes) != TimeSpan.Zero;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0003E060 File Offset: 0x0003C260
		internal static TimeSpan CalculateUtcOffset(DateTime time, DaylightTime daylightTimes)
		{
			if (daylightTimes == null)
			{
				return TimeSpan.Zero;
			}
			if (time.Kind == DateTimeKind.Utc)
			{
				return TimeSpan.Zero;
			}
			DateTime dateTime = daylightTimes.Start + daylightTimes.Delta;
			DateTime end = daylightTimes.End;
			DateTime dateTime2;
			DateTime dateTime3;
			if (daylightTimes.Delta.Ticks > 0L)
			{
				dateTime2 = end - daylightTimes.Delta;
				dateTime3 = end;
			}
			else
			{
				dateTime2 = dateTime;
				dateTime3 = dateTime - daylightTimes.Delta;
			}
			bool flag = false;
			if (dateTime > end)
			{
				if (time >= dateTime || time < end)
				{
					flag = true;
				}
			}
			else if (time >= dateTime && time < end)
			{
				flag = true;
			}
			if (flag && time >= dateTime2 && time < dateTime3)
			{
				flag = time.IsAmbiguousDaylightSavingTime();
			}
			if (flag)
			{
				return daylightTimes.Delta;
			}
			return TimeSpan.Zero;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0003E139 File Offset: 0x0003C339
		internal static void ClearCachedData()
		{
			TimeZone.currentTimeZone = null;
		}

		// Token: 0x040011A6 RID: 4518
		private static volatile TimeZone currentTimeZone;

		// Token: 0x040011A7 RID: 4519
		private static object s_InternalSyncObject;
	}
}
