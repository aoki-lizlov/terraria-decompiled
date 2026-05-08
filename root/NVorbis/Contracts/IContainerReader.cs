using System;
using System.Collections.Generic;

namespace NVorbis.Contracts
{
	// Token: 0x02000026 RID: 38
	public interface IContainerReader : IDisposable
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001AE RID: 430
		// (set) Token: 0x060001AF RID: 431
		NewStreamHandler NewStreamCallback { get; set; }

		// Token: 0x060001B0 RID: 432
		IList<IPacketProvider> GetStreams();

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001B1 RID: 433
		bool CanSeek { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001B2 RID: 434
		long ContainerBits { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001B3 RID: 435
		long WasteBits { get; }

		// Token: 0x060001B4 RID: 436
		bool TryInit();

		// Token: 0x060001B5 RID: 437
		bool FindNextStream();
	}
}
