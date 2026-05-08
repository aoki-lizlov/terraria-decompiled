using System;

namespace System.Collections
{
	// Token: 0x02000A6F RID: 2671
	public interface IEnumerator
	{
		// Token: 0x0600619B RID: 24987
		bool MoveNext();

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x0600619C RID: 24988
		object Current { get; }

		// Token: 0x0600619D RID: 24989
		void Reset();
	}
}
