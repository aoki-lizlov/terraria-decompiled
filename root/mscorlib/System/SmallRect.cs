using System;

namespace System
{
	// Token: 0x02000246 RID: 582
	internal struct SmallRect
	{
		// Token: 0x06001C07 RID: 7175 RVA: 0x0006A026 File Offset: 0x00068226
		public SmallRect(int left, int top, int right, int bottom)
		{
			this.Left = (short)left;
			this.Top = (short)top;
			this.Right = (short)right;
			this.Bottom = (short)bottom;
		}

		// Token: 0x040018B4 RID: 6324
		public short Left;

		// Token: 0x040018B5 RID: 6325
		public short Top;

		// Token: 0x040018B6 RID: 6326
		public short Right;

		// Token: 0x040018B7 RID: 6327
		public short Bottom;
	}
}
