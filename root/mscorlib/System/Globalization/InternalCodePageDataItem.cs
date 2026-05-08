using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020009FD RID: 2557
	internal struct InternalCodePageDataItem
	{
		// Token: 0x04003941 RID: 14657
		internal ushort codePage;

		// Token: 0x04003942 RID: 14658
		internal ushort uiFamilyCodePage;

		// Token: 0x04003943 RID: 14659
		internal uint flags;

		// Token: 0x04003944 RID: 14660
		[SecurityCritical]
		internal string Names;
	}
}
