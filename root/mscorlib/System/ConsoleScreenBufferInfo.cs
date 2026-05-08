using System;

namespace System
{
	// Token: 0x02000247 RID: 583
	internal struct ConsoleScreenBufferInfo
	{
		// Token: 0x040018B8 RID: 6328
		public Coord Size;

		// Token: 0x040018B9 RID: 6329
		public Coord CursorPosition;

		// Token: 0x040018BA RID: 6330
		public short Attribute;

		// Token: 0x040018BB RID: 6331
		public SmallRect Window;

		// Token: 0x040018BC RID: 6332
		public Coord MaxWindowSize;
	}
}
