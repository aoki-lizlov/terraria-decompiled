using System;

namespace System
{
	// Token: 0x02000245 RID: 581
	internal struct Coord
	{
		// Token: 0x06001C06 RID: 7174 RVA: 0x0006A014 File Offset: 0x00068214
		public Coord(int x, int y)
		{
			this.X = (short)x;
			this.Y = (short)y;
		}

		// Token: 0x040018B2 RID: 6322
		public short X;

		// Token: 0x040018B3 RID: 6323
		public short Y;
	}
}
