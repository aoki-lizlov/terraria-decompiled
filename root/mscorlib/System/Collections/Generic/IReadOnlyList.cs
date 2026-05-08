using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AF7 RID: 2807
	public interface IReadOnlyList<out T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x17001215 RID: 4629
		T this[int index] { get; }
	}
}
