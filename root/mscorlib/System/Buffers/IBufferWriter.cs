using System;

namespace System.Buffers
{
	// Token: 0x02000B3A RID: 2874
	public interface IBufferWriter<T>
	{
		// Token: 0x0600691D RID: 26909
		void Advance(int count);

		// Token: 0x0600691E RID: 26910
		Memory<T> GetMemory(int sizeHint = 0);

		// Token: 0x0600691F RID: 26911
		Span<T> GetSpan(int sizeHint = 0);
	}
}
