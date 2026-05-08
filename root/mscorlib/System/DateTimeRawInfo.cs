using System;

namespace System
{
	// Token: 0x020000F0 RID: 240
	internal struct DateTimeRawInfo
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x00026EB2 File Offset: 0x000250B2
		internal unsafe void Init(int* numberBuffer)
		{
			this.month = -1;
			this.year = -1;
			this.dayOfWeek = -1;
			this.era = -1;
			this.timeMark = DateTimeParse.TM.NotSet;
			this.fraction = -1.0;
			this.num = numberBuffer;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00026EF0 File Offset: 0x000250F0
		internal unsafe void AddNumber(int value)
		{
			ref int ptr = ref *this.num;
			int num = this.numCount;
			this.numCount = num + 1;
			*((ref ptr) + (IntPtr)num * 4) = value;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00026F1A File Offset: 0x0002511A
		internal unsafe int GetNumber(int index)
		{
			return this.num[index];
		}

		// Token: 0x04000FD8 RID: 4056
		private unsafe int* num;

		// Token: 0x04000FD9 RID: 4057
		internal int numCount;

		// Token: 0x04000FDA RID: 4058
		internal int month;

		// Token: 0x04000FDB RID: 4059
		internal int year;

		// Token: 0x04000FDC RID: 4060
		internal int dayOfWeek;

		// Token: 0x04000FDD RID: 4061
		internal int era;

		// Token: 0x04000FDE RID: 4062
		internal DateTimeParse.TM timeMark;

		// Token: 0x04000FDF RID: 4063
		internal double fraction;

		// Token: 0x04000FE0 RID: 4064
		internal bool hasSameDateAndTimeSeparators;
	}
}
