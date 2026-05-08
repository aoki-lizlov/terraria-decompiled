using System;
using System.Collections;

namespace System.Resources
{
	// Token: 0x02000829 RID: 2089
	public interface IResourceReader : IEnumerable, IDisposable
	{
		// Token: 0x060046C7 RID: 18119
		void Close();

		// Token: 0x060046C8 RID: 18120
		IDictionaryEnumerator GetEnumerator();
	}
}
