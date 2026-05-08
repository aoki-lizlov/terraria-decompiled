using System;

namespace System
{
	// Token: 0x0200010A RID: 266
	public interface IProgress<in T>
	{
		// Token: 0x06000A37 RID: 2615
		void Report(T value);
	}
}
