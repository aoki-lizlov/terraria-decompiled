using System;
using System.Collections;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000061 RID: 97
	internal interface IWrappedCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600049D RID: 1181
		object UnderlyingCollection { get; }
	}
}
