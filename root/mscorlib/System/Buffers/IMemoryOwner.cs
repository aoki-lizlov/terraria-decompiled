using System;

namespace System.Buffers
{
	// Token: 0x02000B2E RID: 2862
	public interface IMemoryOwner<T> : IDisposable
	{
		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x060068EB RID: 26859
		Memory<T> Memory { get; }
	}
}
