using System;

namespace System.Reflection.Emit
{
	// Token: 0x020008F9 RID: 2297
	internal struct ILExceptionBlock
	{
		// Token: 0x06004FF3 RID: 20467 RVA: 0x00004088 File Offset: 0x00002288
		internal void Debug()
		{
		}

		// Token: 0x040030FE RID: 12542
		public const int CATCH = 0;

		// Token: 0x040030FF RID: 12543
		public const int FILTER = 1;

		// Token: 0x04003100 RID: 12544
		public const int FINALLY = 2;

		// Token: 0x04003101 RID: 12545
		public const int FAULT = 4;

		// Token: 0x04003102 RID: 12546
		public const int FILTER_START = -1;

		// Token: 0x04003103 RID: 12547
		internal Type extype;

		// Token: 0x04003104 RID: 12548
		internal int type;

		// Token: 0x04003105 RID: 12549
		internal int start;

		// Token: 0x04003106 RID: 12550
		internal int len;

		// Token: 0x04003107 RID: 12551
		internal int filter_offset;
	}
}
