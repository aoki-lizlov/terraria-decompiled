using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x02000542 RID: 1346
	public interface IEntrySortStep<T> : IComparer<T>
	{
		// Token: 0x0600376D RID: 14189
		string GetDisplayNameKey();
	}
}
