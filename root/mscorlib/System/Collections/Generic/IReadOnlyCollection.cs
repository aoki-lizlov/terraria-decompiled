using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AF5 RID: 2805
	public interface IReadOnlyCollection<out T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06006737 RID: 26423
		int Count { get; }
	}
}
