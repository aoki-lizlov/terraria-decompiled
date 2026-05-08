using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020009FC RID: 2556
	internal struct InternalEncodingDataItem
	{
		// Token: 0x0400393F RID: 14655
		[SecurityCritical]
		internal string webName;

		// Token: 0x04003940 RID: 14656
		internal ushort codePage;
	}
}
