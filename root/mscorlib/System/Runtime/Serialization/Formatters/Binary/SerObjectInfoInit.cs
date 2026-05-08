using System;
using System.Collections;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000681 RID: 1665
	internal sealed class SerObjectInfoInit
	{
		// Token: 0x06003EB9 RID: 16057 RVA: 0x000DA276 File Offset: 0x000D8476
		public SerObjectInfoInit()
		{
		}

		// Token: 0x040028A5 RID: 10405
		internal Hashtable seenBeforeTable = new Hashtable();

		// Token: 0x040028A6 RID: 10406
		internal int objectInfoIdCount = 1;

		// Token: 0x040028A7 RID: 10407
		internal SerStack oiPool = new SerStack("SerObjectInfo Pool");
	}
}
