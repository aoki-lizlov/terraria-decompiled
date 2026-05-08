using System;

namespace NVorbis.Contracts.Ogg
{
	// Token: 0x0200003B RID: 59
	internal interface IPageReader : IDisposable
	{
		// Token: 0x06000248 RID: 584
		void Lock();

		// Token: 0x06000249 RID: 585
		bool Release();

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600024A RID: 586
		long ContainerBits { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600024B RID: 587
		long WasteBits { get; }

		// Token: 0x0600024C RID: 588
		bool ReadNextPage();

		// Token: 0x0600024D RID: 589
		bool ReadPageAt(long offset);
	}
}
