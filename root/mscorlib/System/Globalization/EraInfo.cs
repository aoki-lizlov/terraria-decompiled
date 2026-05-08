using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020009E2 RID: 2530
	[Serializable]
	internal class EraInfo
	{
		// Token: 0x06005CB7 RID: 23735 RVA: 0x0013DF3C File Offset: 0x0013C13C
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x0013DF88 File Offset: 0x0013C188
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear, string eraName, string abbrevEraName, string englishEraName)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
			this.eraName = eraName;
			this.abbrevEraName = abbrevEraName;
			this.englishEraName = englishEraName;
		}

		// Token: 0x0400381A RID: 14362
		internal int era;

		// Token: 0x0400381B RID: 14363
		internal long ticks;

		// Token: 0x0400381C RID: 14364
		internal int yearOffset;

		// Token: 0x0400381D RID: 14365
		internal int minEraYear;

		// Token: 0x0400381E RID: 14366
		internal int maxEraYear;

		// Token: 0x0400381F RID: 14367
		[OptionalField(VersionAdded = 4)]
		internal string eraName;

		// Token: 0x04003820 RID: 14368
		[OptionalField(VersionAdded = 4)]
		internal string abbrevEraName;

		// Token: 0x04003821 RID: 14369
		[OptionalField(VersionAdded = 4)]
		internal string englishEraName;
	}
}
