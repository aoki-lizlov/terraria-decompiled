using System;
using System.Diagnostics;

namespace ReLogic.Peripherals.RGB.Corsair
{
	// Token: 0x02000052 RID: 82
	[DebuggerDisplay("LedID = {LedId}")]
	internal struct CorsairLedPosition
	{
		// Token: 0x040001F3 RID: 499
		public CorsairLedId LedId;

		// Token: 0x040001F4 RID: 500
		public double Top;

		// Token: 0x040001F5 RID: 501
		public double Left;

		// Token: 0x040001F6 RID: 502
		public double Height;

		// Token: 0x040001F7 RID: 503
		public double Width;
	}
}
