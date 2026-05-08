using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000613 RID: 1555
	[Flags]
	internal enum SxSRequirements
	{
		// Token: 0x04002686 RID: 9862
		None = 0,
		// Token: 0x04002687 RID: 9863
		AppDomainID = 1,
		// Token: 0x04002688 RID: 9864
		ProcessID = 2,
		// Token: 0x04002689 RID: 9865
		CLRInstanceID = 4,
		// Token: 0x0400268A RID: 9866
		AssemblyName = 8,
		// Token: 0x0400268B RID: 9867
		TypeName = 16
	}
}
