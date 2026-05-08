using System;
using System.Collections;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000064 RID: 100
	internal interface IWrappedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004D9 RID: 1241
		object UnderlyingDictionary { get; }
	}
}
