using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007C8 RID: 1992
	public interface ITuple
	{
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x0600459E RID: 17822
		int Length { get; }

		// Token: 0x17000ABC RID: 2748
		object this[int index] { get; }
	}
}
