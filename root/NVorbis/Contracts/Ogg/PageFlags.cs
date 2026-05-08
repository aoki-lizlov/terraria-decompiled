using System;

namespace NVorbis.Contracts.Ogg
{
	// Token: 0x0200003D RID: 61
	[Flags]
	internal enum PageFlags
	{
		// Token: 0x040000DE RID: 222
		None = 0,
		// Token: 0x040000DF RID: 223
		ContinuesPacket = 1,
		// Token: 0x040000E0 RID: 224
		BeginningOfStream = 2,
		// Token: 0x040000E1 RID: 225
		EndOfStream = 4
	}
}
