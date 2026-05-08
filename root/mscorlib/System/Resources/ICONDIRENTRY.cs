using System;

namespace System.Resources
{
	// Token: 0x0200084A RID: 2122
	internal class ICONDIRENTRY
	{
		// Token: 0x060047D9 RID: 18393 RVA: 0x000ED7C4 File Offset: 0x000EB9C4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"ICONDIRENTRY (",
				this.bWidth.ToString(),
				"x",
				this.bHeight.ToString(),
				" ",
				this.wBitCount.ToString(),
				" bpp)"
			});
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x000025BE File Offset: 0x000007BE
		public ICONDIRENTRY()
		{
		}

		// Token: 0x04002DBF RID: 11711
		public byte bWidth;

		// Token: 0x04002DC0 RID: 11712
		public byte bHeight;

		// Token: 0x04002DC1 RID: 11713
		public byte bColorCount;

		// Token: 0x04002DC2 RID: 11714
		public byte bReserved;

		// Token: 0x04002DC3 RID: 11715
		public short wPlanes;

		// Token: 0x04002DC4 RID: 11716
		public short wBitCount;

		// Token: 0x04002DC5 RID: 11717
		public int dwBytesInRes;

		// Token: 0x04002DC6 RID: 11718
		public int dwImageOffset;

		// Token: 0x04002DC7 RID: 11719
		public byte[] image;
	}
}
