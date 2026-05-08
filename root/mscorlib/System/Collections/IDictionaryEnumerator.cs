using System;

namespace System.Collections
{
	// Token: 0x02000A6D RID: 2669
	public interface IDictionaryEnumerator : IEnumerator
	{
		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06006197 RID: 24983
		object Key { get; }

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x06006198 RID: 24984
		object Value { get; }

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06006199 RID: 24985
		DictionaryEntry Entry { get; }
	}
}
