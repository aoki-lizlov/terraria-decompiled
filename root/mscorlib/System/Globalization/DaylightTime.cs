using System;

namespace System.Globalization
{
	// Token: 0x020009BB RID: 2491
	[Serializable]
	public class DaylightTime
	{
		// Token: 0x06005B51 RID: 23377 RVA: 0x00136E7C File Offset: 0x0013507C
		public DaylightTime(DateTime start, DateTime end, TimeSpan delta)
		{
			this._start = start;
			this._end = end;
			this._delta = delta;
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06005B52 RID: 23378 RVA: 0x00136E99 File Offset: 0x00135099
		public DateTime Start
		{
			get
			{
				return this._start;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06005B53 RID: 23379 RVA: 0x00136EA1 File Offset: 0x001350A1
		public DateTime End
		{
			get
			{
				return this._end;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06005B54 RID: 23380 RVA: 0x00136EA9 File Offset: 0x001350A9
		public TimeSpan Delta
		{
			get
			{
				return this._delta;
			}
		}

		// Token: 0x040036B2 RID: 14002
		private readonly DateTime _start;

		// Token: 0x040036B3 RID: 14003
		private readonly DateTime _end;

		// Token: 0x040036B4 RID: 14004
		private readonly TimeSpan _delta;
	}
}
