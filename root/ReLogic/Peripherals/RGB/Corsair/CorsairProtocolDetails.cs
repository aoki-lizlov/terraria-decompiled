using System;

namespace ReLogic.Peripherals.RGB.Corsair
{
	// Token: 0x02000055 RID: 85
	internal struct CorsairProtocolDetails
	{
		// Token: 0x040001FE RID: 510
		public IntPtr SdkVersion;

		// Token: 0x040001FF RID: 511
		public IntPtr ServerVersion;

		// Token: 0x04000200 RID: 512
		public int SdkProtocolVersion;

		// Token: 0x04000201 RID: 513
		public int ServerProtocolVersion;

		// Token: 0x04000202 RID: 514
		public byte BreakingChanges;
	}
}
