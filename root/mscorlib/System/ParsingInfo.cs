using System;
using System.Globalization;

namespace System
{
	// Token: 0x020000F4 RID: 244
	internal struct ParsingInfo
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x00027003 File Offset: 0x00025203
		internal void Init()
		{
			this.dayOfWeek = -1;
			this.timeMark = DateTimeParse.TM.NotSet;
		}

		// Token: 0x0400100C RID: 4108
		internal Calendar calendar;

		// Token: 0x0400100D RID: 4109
		internal int dayOfWeek;

		// Token: 0x0400100E RID: 4110
		internal DateTimeParse.TM timeMark;

		// Token: 0x0400100F RID: 4111
		internal bool fUseHour12;

		// Token: 0x04001010 RID: 4112
		internal bool fUseTwoDigitYear;

		// Token: 0x04001011 RID: 4113
		internal bool fAllowInnerWhite;

		// Token: 0x04001012 RID: 4114
		internal bool fAllowTrailingWhite;

		// Token: 0x04001013 RID: 4115
		internal bool fCustomNumberParser;

		// Token: 0x04001014 RID: 4116
		internal DateTimeParse.MatchNumberDelegate parseNumberDelegate;
	}
}
