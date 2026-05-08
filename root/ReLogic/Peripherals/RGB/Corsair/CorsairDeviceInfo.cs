using System;

namespace ReLogic.Peripherals.RGB.Corsair
{
	// Token: 0x02000051 RID: 81
	internal struct CorsairDeviceInfo
	{
		// Token: 0x040001ED RID: 493
		public CorsairDeviceType Type;

		// Token: 0x040001EE RID: 494
		public string Model;

		// Token: 0x040001EF RID: 495
		public CorsairPhysicalLayout PhysicalLayout;

		// Token: 0x040001F0 RID: 496
		public CorsairLogicalLayout LogicalLayout;

		// Token: 0x040001F1 RID: 497
		public CorsairDeviceCaps CapsMask;

		// Token: 0x040001F2 RID: 498
		public int LedsCount;
	}
}
