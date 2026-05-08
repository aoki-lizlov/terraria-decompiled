using System;

namespace rail
{
	// Token: 0x020001A5 RID: 421
	public class RailVoiceCaptureSpecification
	{
		// Token: 0x060018E8 RID: 6376 RVA: 0x00002119 File Offset: 0x00000319
		public RailVoiceCaptureSpecification()
		{
		}

		// Token: 0x040005D2 RID: 1490
		public EnumRailVoiceCaptureChannel channels;

		// Token: 0x040005D3 RID: 1491
		public uint samples_per_second;

		// Token: 0x040005D4 RID: 1492
		public uint bits_per_sample;

		// Token: 0x040005D5 RID: 1493
		public EnumRailVoiceCaptureFormat capture_format;
	}
}
