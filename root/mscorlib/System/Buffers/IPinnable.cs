using System;

namespace System.Buffers
{
	// Token: 0x02000B2F RID: 2863
	public interface IPinnable
	{
		// Token: 0x060068EC RID: 26860
		MemoryHandle Pin(int elementIndex);

		// Token: 0x060068ED RID: 26861
		void Unpin();
	}
}
