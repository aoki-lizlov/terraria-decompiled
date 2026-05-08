using System;

namespace System.Globalization
{
	// Token: 0x020009BF RID: 2495
	internal struct HebrewNumberParsingContext
	{
		// Token: 0x06005B57 RID: 23383 RVA: 0x00136EFB File Offset: 0x001350FB
		public HebrewNumberParsingContext(int result)
		{
			this.state = HebrewNumber.HS.Start;
			this.result = result;
		}

		// Token: 0x040036BC RID: 14012
		internal HebrewNumber.HS state;

		// Token: 0x040036BD RID: 14013
		internal int result;
	}
}
