using System;

namespace System.Globalization
{
	// Token: 0x020009BC RID: 2492
	internal readonly struct DaylightTimeStruct
	{
		// Token: 0x06005B55 RID: 23381 RVA: 0x00136EB1 File Offset: 0x001350B1
		public DaylightTimeStruct(DateTime start, DateTime end, TimeSpan delta)
		{
			this.Start = start;
			this.End = end;
			this.Delta = delta;
		}

		// Token: 0x040036B5 RID: 14005
		public readonly DateTime Start;

		// Token: 0x040036B6 RID: 14006
		public readonly DateTime End;

		// Token: 0x040036B7 RID: 14007
		public readonly TimeSpan Delta;
	}
}
