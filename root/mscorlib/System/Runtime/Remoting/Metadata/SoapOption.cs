using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020005AC RID: 1452
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum SoapOption
	{
		// Token: 0x04002590 RID: 9616
		None = 0,
		// Token: 0x04002591 RID: 9617
		AlwaysIncludeTypes = 1,
		// Token: 0x04002592 RID: 9618
		XsdString = 2,
		// Token: 0x04002593 RID: 9619
		EmbedAll = 4,
		// Token: 0x04002594 RID: 9620
		Option1 = 8,
		// Token: 0x04002595 RID: 9621
		Option2 = 16
	}
}
